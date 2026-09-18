using System.ComponentModel.DataAnnotations;

namespace AppWebBiblioteca.Models
{
    public class Autor
    {
        public int Id { get; set; }

        [Required]

        [StringLength(100)]
        public string Nombre { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaDeNacimiento { get; set; }

        [StringLength(50)]
        public string Nacionalidad { get; set; }

    }
}
