﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoUniversidade.Data;
using ProjetoUniversidade.Models;

namespace ProjetoUniversidade.Controllers
{
    public class EstudantesController : Controller
    {
        private readonly EscolaContexto _context;

        public EstudantesController(EscolaContexto context)
        {
            _context = context;
        }

        // GET: Estudantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estudantes.ToListAsync());
        }

        // GET: Estudantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudante = await _context.Estudantes
                .Include(s => s.Matriculas)
                    .ThenInclude(e => e.Curso)
                /*O método AsNoTracking melhora o desempenho em cenários onde as entidades retornadas não serão atualizadas no tempo de vida do contexto atual.*/
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.EstudanteID == id);
            if (estudante == null)
            {
                return NotFound();
            }

            return View(estudante);
        }

        // GET: Estudantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SobreNome, Nome, DataMatricula")] Estudante estudante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(estudante);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(("Index"));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "It was not possible to Add. " + "Try again, if the problem persists" + "please contact your system administrator");
            }

            return View(estudante);
        }

        // GET: Estudantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudante = await _context.Estudantes.FindAsync(id);

            if (estudante == null)
            {
                return NotFound();
            }
            return View(estudante);
        }

        // POST: Estudantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id != null)
            {
                return NotFound();
            }

            var atualizarEstudante = await _context.Estudantes.SingleOrDefaultAsync(s => s.EstudanteID == id);

            if (await TryUpdateModelAsync<Estudante>(atualizarEstudante, "", s => s.Nome, s => s.SobreNome, s => s.DataMatricula))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "It was not possible to save" + "Try again, if the problem persists" + "please contact your system administrator");
                }
            }

            
            return View(atualizarEstudante);
        }

        // GET: Estudantes/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudante = await _context.Estudantes
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.EstudanteID == id);

            if (estudante == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "The Deletion failed. Try again, if the problem persists" + "please contact your system administrator.";
            }

            return View(estudante);
        }

        // POST: Estudantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudante = await _context.Estudantes
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.EstudanteID == id);

            if (estudante == null)
            {
                return RedirectToAction("index");
            }

            try
            {
                _context.Estudantes.Remove(estudante);
                await _context.SaveChangesAsync();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private bool EstudanteExists(int id)
        {
            return _context.Estudantes.Any(e => e.EstudanteID == id);
        }
    }
}