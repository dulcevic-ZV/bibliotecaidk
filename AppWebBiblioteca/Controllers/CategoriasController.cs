using AppWebBiblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AppWebBiblioteca.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly string _connectionString;
        private readonly ILogger<CategoriasController> _logger;
        public CategoriasController(IConfiguration configuration, ILogger<CategoriasController> logger)
        {
            _connectionString = configuration.GetConnectionString("BibliotecaDB");
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Consultando listado de categorías");
            try
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
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido una excepción al traer el listado de categorías");
                throw;
            }
          
           
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Iniciando la creación de una nueva categoría");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Categoria categoria)
        {
            _logger.LogInformation("Creando una nueva categoría");
            if (categoria==null || string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                _logger.LogWarning("Existen datos nulos");
                ModelState.AddModelError("Nombre", "El nombre es obligatorio");
                return View(categoria);
            }
            try
            {

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
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido una excepción");
                throw;
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {

            _logger.LogInformation("Iniciando la edición de una categoría");
            try
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
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido una excepción durante la obtención de la edición de categorías");
                throw;
            }
        }

        [HttpPost]

        public IActionResult Update(Categoria categoria, int id)
        {
            _logger.LogInformation("Editando una categoría");
            if (categoria == null || string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                _logger.LogWarning("Existen datos nulos");
                ModelState.AddModelError("Nombre", "El nombre es obligatorio");
                return View(categoria);
            }

            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    var sql = "UPDATE Categorias SET Nombre = @Nombre, Descripcion = @Descripcion WHERE ID = @id";
                    using (SqlCommand comando = new SqlCommand(sql, conexion))
                    {
                        comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = categoria.ID;
                        comando.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                        comando.Parameters.AddWithValue("@Descripcion", (object)categoria.Descripcion ?? System.DBNull.Value);

                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }

                return RedirectToAction("Index");
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido una excepción durante la edición de categorías");
                throw;
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Iniciando la eliminación de una categoría");
            try
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
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un problema durante la eliminación de un libro");
                throw;
            }
        }
    }
}
