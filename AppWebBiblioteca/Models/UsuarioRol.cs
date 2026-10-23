using Microsoft.AspNetCore.Identity;

namespace AppWebBiblioteca.Models
{
    public class UsuarioRol
    {
        public IdentityRole Role { get; set; } = null!;
        public IList<IdentityUser> Users { get; set; } = new List<IdentityUser>();
    }
}
