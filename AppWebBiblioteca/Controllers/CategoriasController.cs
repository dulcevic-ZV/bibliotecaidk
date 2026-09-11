using Microsoft.AspNetCore.Mvc;
using AppWebBiblioteca.Models;
using Microsoft.Data.SqlClient;

namespace AppWebBiblioteca.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly string _connectionString;
        public CategoriasController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BibliotecaDB");
        }
        public IActionResult Index()
        {
            var categorias = new List<Categoria>();
            using (var conexion = new SqlConnection(_connectionString))
            {
                var sql = "SELECT ID, Nombre, Descripcion FROM Categorias";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            categorias.Add(new Categoria
                            {
                                ID = lector.GetInt32(0),
                                Nombre = lector.GetString(1),
                                Descripcion = lector.IsDBNull(2) ? null : lector.GetString(2),
                            });
                        }
                    }
                }
            }
          
            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Categoria categoria)
        {
            if (categoria==null || string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                ModelState.AddModelError("Nombre", "El nombre es obligatorio");
                return View(categoria);
            }

            using (var conexion = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Categorias(Nombre, Descripcion) VALUES (@Nombre, @Descripcion)";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    comando.Parameters.AddWithValue("@Descripcion", (object)categoria.Descripcion ?? System.DBNull.Value);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
            TempData["SuccessMessage"] = "Categoría guardada correctamente";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {

            Categoria categoria = new Categoria();

            using (var conexion = new SqlConnection(_connectionString))
            {
                var sql = "SELECT ID, Nombre, Descripcion FROM Categorias WHERE ID = @id";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            categoria.ID = lector.GetInt32(lector.GetOrdinal("ID"));
                            categoria.Nombre = lector.GetString(lector.GetOrdinal("Nombre"));
                            categoria.Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? null : lector.GetString(lector.GetOrdinal("Descripcion"));
                        }
                    }
                }
            }

            return View(categoria);
        }

        [HttpPost]

        public IActionResult Update(Categoria categoria, int id)
        {
            if (categoria == null || string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                ModelState.AddModelError("Nombre", "El nombre es obligatorio");
                return View(categoria);
            }

            using (var conexion = new SqlConnection(_connectionString))
            {
                var sql = "UPDATE Categorias SET Nombre = @Nombre, Descripcion = @Descripcion WHERE ID = @id";
                using (SqlCommand comando = new SqlCommand(sql, conexion) )
                {
                    comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = categoria.ID;
                    comando.Parameters.AddWithValue("@Nombre", categoria.Nombre) ;
                    comando.Parameters.AddWithValue("@Descripcion", (object)categoria.Descripcion ?? System.DBNull.Value);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            using (var conexion = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM Categorias WHERE ID=@id";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
