using Microsoft.EntityFrameworkCore;
using ProjetoUniversidade.Models;

namespace ProjetoUniversidade.Data
{
    public class EscolaContexto : DbContext
    {
        public EscolaContexto(DbContextOptions<EscolaContexto> options) : base(options)
        {
            /*Certify the database has been created*/
            this.Database.EnsureCreated();
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Estudante> Estudantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().ToTable("Curso");
            modelBuilder.Entity<Matricula>().ToTable("Matricula");
            modelBuilder.Entity<Estudante>().ToTable("Estudante");
        }
    }
}
