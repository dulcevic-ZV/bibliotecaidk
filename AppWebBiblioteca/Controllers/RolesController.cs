using AppWebBiblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public  async Task<IActionResult> Index()
    {
        var roles = _roleManager.Roles.ToList();
        var model = new List<UsuarioRol>();

        foreach (var role in roles)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
            model.Add(new UsuarioRol
            {
                Role = role,
                Users = usersInRole
            });
        }

        return View(model);
    }

    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rol = await _roleManager.FindByIdAsync(id);
        if (rol == null)
        {
            return NotFound();
        }

        var usersInRole = await _userManager.GetUsersInRoleAsync(rol.Name!);
        var allUsers = _userManager.Users.ToList();

        var usersNotInRole = allUsers
            .Where(u => !usersInRole.Any(r => r.Id == u.Id))
            .ToList();

        ViewBag.UsersNotInRole = usersNotInRole;

        var model = new UsuarioRol
        {
            Role = rol,
            Users = usersInRole
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(string roleId, string userId)
    {
        var rol = await _roleManager.FindByIdAsync(roleId);
        var user = await _userManager.FindByIdAsync(userId);

        if (rol == null || user == null)
        {
            return NotFound();
        }

        if (!await _userManager.IsInRoleAsync(user, rol.Name!))
        {
            await _userManager.AddToRoleAsync(user, rol.Name!);
        }

        return RedirectToAction(nameof(Details), new { id = roleId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string roleId, string userId)
    {
        var rol = await _roleManager.FindByIdAsync(roleId);
        var user = await _userManager.FindByIdAsync(userId);

        if (rol == null || user == null)
        {
            return NotFound();
        }

        if (await _userManager.IsInRoleAsync(user, rol.Name!))
        {
            await _userManager.RemoveFromRoleAsync(user, rol.Name!);
        }

        return RedirectToAction(nameof(Details), new { id = roleId });
    }
}
