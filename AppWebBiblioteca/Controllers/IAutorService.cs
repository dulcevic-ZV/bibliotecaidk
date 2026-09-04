using AppWebBiblioteca.Models;

namespace AppWebBiblioteca.Controllers
{
    public interface IAutorService
    {
        IEnumerable<Autor> ObtenerAutores();

        Autor ObtenerPorId(int id);

        Autor CreateAutor(Autor autor);
    }
}

