using Microsoft.AspNetCore.Mvc;
using AppWebBiblioteca.Models;
using AppWebBiblioteca.Repositories;

namespace AppWebBiblioteca.Controllers
{
    public class LibrosController : Controller
    {
        private readonly IRepositorioLibro _repositorio;

        public LibrosController(IRepositorioLibro repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Index()
        {
            var libros = _repositorio.ObtenerTodos();
            return View(libros);
        }



    }
}
