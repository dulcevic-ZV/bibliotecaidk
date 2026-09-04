
using AppWebBiblioteca.Controllers;
using AppWebBiblioteca.Models;

namespace AppWebBiblioteca.Repositories
{
    public class AutorService : IAutorService
    {
        private readonly List<Autor> autores = new List<Autor>()
        {
            new Autor
            {
                Id = 1,
                Nombre = "Shirahama",
                Apellido = "Kamome",
                FechaDeNacimiento = new DateTime(1990, 5, 7),
                Nacionalidad = "Japonesa",
                EsActivo = true
            },
            new Autor
            {
                Id = 2,
                Nombre = "Koyoharu",
                Apellido = "Gotouge",
                FechaDeNacimiento = new DateTime(1989, 5, 5),
                Nacionalidad = "Japonesa",
                EsActivo = true
            }
        };

        public Autor CreateAutor(Autor autor)
        {
            if (autor == null) return null;

            var nextId = autores.Any() ? autores.Max(a => a.Id) + 1 : 1;
            autor.Id = nextId;
            autores.Add(autor);
            return autor;
        }

        public IEnumerable<Autor> ObtenerAutores()
        {
            return autores;
        }

        public Autor ObtenerPorId(int id)
        {
            return autores.FirstOrDefault(a => a.Id == id);
        }
    }
}
