using System.ComponentModel.DataAnnotations;

namespace AppWebBiblioteca.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Nombre de usuario obligatorio")]
        [Display(Name ="Usuario")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Correo obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        [Display(Name = "Correo")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Contraseña obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Reepetir contraseña obligatorio")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "No coinciden las contraseñas")]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
