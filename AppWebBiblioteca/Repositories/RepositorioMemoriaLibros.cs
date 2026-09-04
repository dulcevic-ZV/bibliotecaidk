using AppWebBiblioteca.Models;

namespace AppWebBiblioteca.Repositories
{
    public class RepositorioMemoriaLibros : IRepositorioLibro
    {
        public IEnumerable<Libro> ObtenerTodos()
        {
            return new List<Libro>()
            {
                new Libro
                {
                    Id = 1,
                    Title= "Drácula",
                    Autor= "Bram Stoker",
                    Category= "Novela",
                    Price = 15.90

                },
                new Libro
                {
                    Id = 2,
                    Title= "Witch Hat Atelier",
                    Autor= "Kamome Shirahama",
                    Category= "Manga",
                    Price = 18.00

                }
            };
        }

    }
}
