using Microsoft.AspNetCore.Mvc;
using AppWebBiblioteca.Models;

namespace AppWebBiblioteca.Controllers
{
    public class LibrosController : Controller
    {
        private static List<Libro> _libros = new List<Libro>()
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
                    Autor= "Kamome Shirahama", //THE GOAT
                    Category= "Manga",
                    Price = 18.00
                
                }
            };
        public IActionResult Index()
        {
            return View(_libros);
        }

        public IActionResult Details(int id)
        {
            var autor = _libros.FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Libro libro)
        {
            if (!ModelState.IsValid) //valida el modelo
            {
                return View(libro);
            }

            if (_libros.Any())
            {
                libro.Id = _libros.Max(x => x.Id) + 1;
            }
            else
            {
                libro.Id = 1;
            }

            _libros.Add(libro);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var autor = _libros.FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            _libros.Remove(autor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id, Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }
            var libros = _libros.FirstOrDefault(libro => libro.Id == id);
            if (libros == null)
            {
                return NotFound();
            }

            libros.Title = libro.Title;
            libros.Autor = libro.Autor;
            libros.Category = libro.Category;
            libros.Price = libro.Price;
       
       

            return RedirectToAction(nameof(Index));
        }
    }
}
