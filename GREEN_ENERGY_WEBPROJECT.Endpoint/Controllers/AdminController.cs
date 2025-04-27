using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GREEN_ENERGY_WEBPROJECT.Endpoint.Models; // <-- hozzá kell adnod!

namespace GREEN_ENERGY_WEBPROJECT.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<Users> _userManager; // <<< IdentityUser helyett Users!

        public AdminController(UserManager<Users> userManager) // <<< IdentityUser helyett Users!
        {
            _userManager = userManager;
        }

        public IActionResult UserManagement()
        {
            if (User.Identity.Name != "admin@green.com")
            {
                return Forbid();
            }

            var users = _userManager.Users;
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (User.Identity.Name != "admin@green.com")
            {
                return Forbid();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("UserManagement");
        }
    }
}
