using AppWebBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using AppWebBiblioteca.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;

namespace AppWebBiblioteca.Controllers
{

    [Authorize]
    public class AutoresController : Controller
    {
        private readonly BibliotecaContext _context;

        private readonly ILogger<AutoresController> _logger;
        public AutoresController(BibliotecaContext context, ILogger<AutoresController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Consultar listado de autores");

            try
            {
                var autores = await _context.Autores.ToListAsync();
                return View(autores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error al consultar el listado de autores");
                throw;
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("Consultando los detalles del autor con identificador {AutorId}", id);
            try
            {
                var autor = await _context.Autores.FindAsync(id);

                if (autor == null)
                {
                    _logger.LogWarning("No se encontró el autor con Id {AutorId}", id);
                    return NotFound();
                }

                return View(autor);
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error al consultar los detalles del autor con Id {AutorId}", id);
                throw;
            }
        }

        [Authorize(Roles = "Admin, Bibliotecario")]
        public IActionResult Create()
        {
            _logger.LogInformation("Iniciando la creación de un autor");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Create(Autor autor)
        {
            _logger.LogInformation("Iniciando la creación de un autor");
            if (!ModelState.IsValid)
            {
                _logger.LogError("El módelo no es válido");
                return View(autor);
            }

            try
            {
                _context.Autores.Add(autor);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError("Se produjo un error al crear el autor");
                throw;
            }
        }


        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Iniciando la edición del autor con identificador {AutorId}", id);
            try
            {
                var autor = await _context.Autores.FindAsync(id);
                if (autor == null)
                {
                    return NotFound();
                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error al intentar editar el autor {AutorId}", id);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<IActionResult> Edit(int id, Autor autor)
        {
            _logger.LogInformation("Editando el autor con identificador {AutorId}", id);

            if (id != autor.Id)
            {
                _logger.LogWarning("No se ha encontrado el autor solicitado con Id {AutorId}", id);
                return BadRequest("No existe el autor");
            }
            if (!ModelState.IsValid)
            {
                return View(autor);
            }

            try
            {
                var exist = await _context.Autores.AnyAsync(a => a.Id == id);
                if (!exist)
                {
                    return NotFound("No existe");
                }
                _context.Update(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo un error durante la edición del autor {AutorId}", id);
                throw;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Iniciando la eliminación del autor con id {AutorId}", id);
            try
            {
                var autor = await _context.Autores.FindAsync(id);
                if (autor == null)
                {
                    return NotFound();
                }

                _context.Autores.Remove(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante la eliminación del autor con id {AutorId}", id);
                throw;
            }
        }
    }
}

