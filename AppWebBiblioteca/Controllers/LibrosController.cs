using Microsoft.AspNetCore.Mvc;
using AppWebBiblioteca.Models;
using AppWebBiblioteca.Repositories;
using Microsoft.EntityFrameworkCore;
using AppWebBiblioteca.Data;
using Microsoft.AspNetCore.Authorization;

namespace AppWebBiblioteca.Controllers
{
    [Authorize]
    public class LibrosController : Controller
    {
        private readonly BibliotecaContext _context;
        private readonly ILogger<LibrosController> _logger;
        public LibrosController(BibliotecaContext context, ILogger<LibrosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /*Retornar libros*/

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Consultando listado de libros");
            var libros = await _context.Libros.ToListAsync();
            return View(libros);
        }

        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("Consultando detalles del libro con identificador {LibroId}", id);
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                _logger.LogWarning("El libro con identificador {LibroId} no se ha encontrado", id);
                return NotFound();
            }
            return View(libro);
        }

        [Authorize(Roles = "Admin, Bibliotecario")]
        public IActionResult Create()
        {
            _logger.LogInformation("Iniciando la creación de un nuevo libro");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Create(Libro libro)
        {
            _logger.LogInformation("Creando un nuevo libro");
            
            if (!ModelState.IsValid)
            {
                _logger.LogError("Error, modelo invalido");
                return View(libro);
            }

            try
            {
                _context.Libros.Add(libro);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido una excepción durante la creaión de un nuevo libro");
                throw;
            }
        }


        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Iniciando la edición de un libro con identificador {LibroId}", id);
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                _logger.LogWarning("El libro con identificador {LibroId} no se ha encontrado", id); ;
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Edit(int id, Libro libro)
        {
            _logger.LogInformation("Editando el libro con identificador {LibroId}", id);
            if (id != libro.Id)
            {
                _logger.LogWarning("El libro con identificador {LibroId} no coincide", id);
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Modelo inválido");
                return View(libro);
            }

            try
            {
                var exits = await _context.Libros.AnyAsync(l => l.Id == id);
                if (!exits)
                {
                    _logger.LogWarning("El libro con identificador {LibroId} no existe", id);
                    return NotFound();
                }
                _context.Update(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido una excepción durante la edición del libro");
                throw;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Eliminando libro con identificador {LibroId}", id);
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                _logger.LogWarning("El libro con identificador {LibroId} no se encontró", id);
                return NotFound();
            }

            try
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido una excepción");
                throw;
            }
        }

    }
}
