using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoUniversidade.Models
{
    public class Curso
    {
        /*O atributo [DatabaseGenerated] é usado em campos computados e quando definido com a opção DatabaseGeneratedOption.None faz com que o banco de dados não gere um valor para a propriedade quando linhas forem inseridas ou atualizadas na respectiva tabela.*/
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CursoID { get; set; }
        public string Titulo { get; set; }
        public int Creditos { get; set; }

        /**/
        public ICollection<Matricula> Matriculas { get; set; }

    }
}
