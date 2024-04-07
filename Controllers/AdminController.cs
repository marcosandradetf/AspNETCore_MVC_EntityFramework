using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfnetMVC.Controllers
{
    [Authorize(Roles = "Admin")] // Apenas usuários com a função Admin terão acesso a este controlador
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Exibir lista de usuários e suas funções
        public IActionResult Index()
        {
            var usersWithRoles = _userManager.Users
            .Select(user => new
            {
                user.UserName,
                Roles = _userManager.GetRolesAsync(user).Result
            });

            return View(usersWithRoles);
        }
        
        public async Task<IActionResult> Edit(string email)
        {
            // Encontre o usuário pelo email
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(); // Trate o caso em que o usuário não é encontrado
            }

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            List<(string, string)> model = new List<(string, string)>();
            
            model.Add(("user", user.UserName));

            foreach (var role in userRoles)
            {
                model.Add(("userRole", role));
            }
            
            foreach (var role in roles)
            {
                model.Add(("systemRole", role.Name));
            }
    
            // Envie o modelo para a view
            return View(model);
            
        }
        
        public async Task<IActionResult> PostEdit(string email, List<String> roles)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            // Remove todas as roles do usuário para evitar duplicatas
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            
            foreach (var role in roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            
            return RedirectToAction("Index");
        }



    }

}
