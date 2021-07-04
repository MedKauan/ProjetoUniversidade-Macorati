namespace ProjetoUniversidade.Models
{
    public enum Nota
    {
        A, B, C, D, F
    }

    public class Matricula
    {
        public int MatriculaId { get; set; }
        public int CursoId { get; set; }
        public int EstudanteID { get; set; }
        //"?" = pode ser nulo
        public Nota? Nota { get; set; }

        public Curso Curso { get; set; }
        public Estudante Estudante { get; set; }
    }
}
