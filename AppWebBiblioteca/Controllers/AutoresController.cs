using AppWebBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppWebBiblioteca.Controllers
{
    public class AutoresController : Controller
    {
        private static List<Autor> _autores = new List<Autor>()
            {
                new Autor
                {
                    Id=1,
                    Nombre = "Shirahama",
                    Apellido = "Kamome",
                    FechaDeNacimiento = new DateTime(1990, 5, 7),
                    Nacionalidad = "Japonesa",
                    EsActivo = true
                },

                      new Autor
                {
                    Id=2,
                    Nombre = "Koyoharu",
                    Apellido = "Gotouge",
                    FechaDeNacimiento = new DateTime(1989, 5, 5),
                    Nacionalidad = "Japonesa",
                    EsActivo = true
                },

            };

        public IActionResult Index()
        {

            return View(_autores);
        }

        public IActionResult Details(int id)
        {
            var autor = _autores.FirstOrDefault(x => x.Id == id);
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
        public IActionResult Create(Autor autor)
        {
            if (!ModelState.IsValid) //valida el modelo
            {
                return View(autor);
            }

            if (_autores.Any())
            {
                autor.Id = _autores.Max(x => x.Id) + 1;
            }
            else
            {
                autor.Id = 1;
            }

            _autores.Add(autor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var autor = _autores.FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            _autores.Remove(autor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id, Autor autores)
        {
            if (!ModelState.IsValid)
            {
                return View(autores);
            }
            var autor= _autores.FirstOrDefault(autores => autores.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            autor.Nombre = autores.Nombre;
            autor.Apellido = autores.Apellido;
            autor.Nacionalidad = autores.Nacionalidad;
            autor.FechaDeNacimiento = autores.FechaDeNacimiento;

            return RedirectToAction(nameof(Index));
        }

    }
}
