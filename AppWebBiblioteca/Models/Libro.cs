using System.ComponentModel.DataAnnotations;

namespace AppWebBiblioteca.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required]

        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Autor { get; set; }
        [Required]
        [StringLength(50)]
        public string Category { get; set; }
        [Required]
        public double Price { get; set; }
        public string Imagen { get; set; } = string.Empty;
    }
}
