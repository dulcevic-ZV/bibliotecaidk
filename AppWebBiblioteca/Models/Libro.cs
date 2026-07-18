namespace AppWebBiblioteca.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Autor { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }

    }
}
