using System;
using System.Collections.Generic;

namespace ProjetoUniversidade.Models
{
    public class Estudante
    {
        public int EstudanteID { get; set; }
        public string SobreNome { get; set; }
        public string Nome { get; set; }
        public DateTime DataMatricula { get; set; }

        // A propriedade de Matricula é uma propriedade de navegação. As propriedades de navegação tratam outras entidades que estão relacionadas com esta entidade permitindo que acessemos propriedades relacionadas.
        public ICollection <Matricula> Matriculas { get; set; }
    }
}
