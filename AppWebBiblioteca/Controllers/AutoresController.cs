using AppWebBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppWebBiblioteca.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IAutorService _service;
        public AutoresController(IAutorService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var autor = _service.ObtenerAutores(); 
            return View(autor);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var autorId = _service.ObtenerPorId(id);
            if (autorId == null)
            {
                return NotFound();
            }
            return View(autorId);
        }

        public IActionResult Create(Autor autor)
        {
            var autorCrear = _service.CreateAutor(autor);
            return View(autorCrear);
        }
    }
}
