using Microsoft.AspNetCore.Mvc;
using AppWebBiblioteca.Models;

namespace AppWebBiblioteca.Controllers
{
    public class LibrosController : Controller
    {
        public IActionResult Index()
        {
            List<Libro> libros = new List<Libro>()
            {
                new Libro
                {
                    Id = 1,
                    Title= "Drácula",
                    Autor= "Bram Stoker",
                    Category= "Novela",
                    Price = 15.90,
                    Available = false
                },
                new Libro
                {
                    Id = 2,
                    Title= "Witch Hat Atelier", 
                    Autor= "Kamome Shirahama", //THE GOAT
                    Category= "Manga",
                    Price = 18.00,
                    Available = true
                }
            };
            ViewBag.Nombre = "Olruggio";
            ViewBag.Libros = libros;
            return View(libros);
        }
    }
}
