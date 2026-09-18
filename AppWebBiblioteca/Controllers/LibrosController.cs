using Microsoft.AspNetCore.Mvc;
using AppWebBiblioteca.Models;
using AppWebBiblioteca.Repositories;
using Microsoft.EntityFrameworkCore;
using AppWebBiblioteca.Data;

namespace AppWebBiblioteca.Controllers
{
    public class LibrosController : Controller
    {
        private readonly BibliotecaContext _context;

        public LibrosController(BibliotecaContext context)
        {
            _context = context;
        }

        /*Retornar libros*/

        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros.ToListAsync();
            return View(libros);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
