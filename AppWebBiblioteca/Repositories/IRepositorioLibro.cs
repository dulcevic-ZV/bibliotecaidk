using AppWebBiblioteca.Models;

namespace AppWebBiblioteca.Repositories
{
    public interface IRepositorioLibro
    {
        IEnumerable<Libro> ObtenerTodos();


    }
}
