using AppWebBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using AppWebBiblioteca.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppWebBiblioteca.Controllers
{

    [Authorize]
    public class AutoresController : Controller
    {
        private readonly BibliotecaContext _context;
        public AutoresController(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var autores = await _context.Autores.ToListAsync();
            return View(autores);
        }

        public async Task<IActionResult> Details(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        [Authorize(Roles = "Admin, Bibliotecario")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Create(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return View(autor);
            }

            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Edit(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Edit(int id, Autor autor)
        {

            if (id != autor.Id)
            {
                return BadRequest("No existe el autor");
            }
            if (!ModelState.IsValid)
            {
                return View(autor);
            }

            var exist = await _context.Autores.AnyAsync(a => a.Id == id);
            if (!exist)
            {
                return NotFound("No existe");
            }
            _context.Update(autor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

