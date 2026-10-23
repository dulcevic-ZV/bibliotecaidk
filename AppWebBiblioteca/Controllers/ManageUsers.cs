using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppWebBiblioteca.Controllers
{
    public class ManageUsers : Controller
    {
        private readonly  UserManager<IdentityUser> _userManager;

        public ManageUsers(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager.GetUsersInRoleAsync("Admin"))
        }
    }
}
