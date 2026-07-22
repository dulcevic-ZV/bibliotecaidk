namespace AppWebBiblioteca.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateOnly FechaDeNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public bool EsActivo { get; set; }

    }
}
