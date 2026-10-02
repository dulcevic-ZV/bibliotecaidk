using System.ComponentModel.DataAnnotations;

namespace AppWebBiblioteca.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Es obligatorio el user o gmail")]
        [Display(Name = "User or gmail")]
        public string UserNameOrEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es obligatorio la contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; } 
        public string? ReturnUrl { get; set; }
    }
}
