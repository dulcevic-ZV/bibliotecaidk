using AppWebBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppWebBiblioteca.Controllers
{
    public class AutoresController : Controller
    {
        public IActionResult Index()
        {
            List<Autor> autores = new List<Autor>()
            {
                new Autor
                {
                    Id=1,
                    Nombre = "Shirahama",
                    Apellido = "Kamome",
                    FechaDeNacimiento = new DateOnly(1990, 5, 7),
                    Nacionalidad = "Japonesa",
                    EsActivo = true
                },
                   new Autor
                {
                    Id=2,
                    Nombre = "Bram",
                    Apellido = "Stoker",
                    FechaDeNacimiento = new DateOnly(1847, 11, 8),
                    Nacionalidad = "Irlandés",
                    EsActivo = false
                },
                      new Autor
                {
                    Id=3,
                    Nombre = "Koyoharu",
                    Apellido = "Gotouge",
                    FechaDeNacimiento = new DateOnly(1989, 5, 5),
                    Nacionalidad = "Japonesa",
                    EsActivo = true
                },
                   new Autor
                {
                    Id=4,
                    Nombre = "Arthur",
                    Apellido = "Conan Doyle",
                    FechaDeNacimiento = new DateOnly(1859, 5, 22),
                    Nacionalidad = "Británico",
                    EsActivo = false
                },
                   new Autor
                {
                    Id=1,
                    Nombre = "Louisa",
                    Apellido = "May Alcott",
                    FechaDeNacimiento = new DateOnly(1832, 11, 29),
                    Nacionalidad = "Estadounidense",
                    EsActivo = false
                },
            };
            ViewBag.Autores = autores;
            return View();
        }
    }
}
