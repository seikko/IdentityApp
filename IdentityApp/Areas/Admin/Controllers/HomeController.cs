using IdentityApp.Areas.Admin.Models;
using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModelList = users.Select(x => new UserViewModel()
            {
                Email = x.Email,
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName
            }).ToList();
            return View(userViewModelList);
        }
    }
}
