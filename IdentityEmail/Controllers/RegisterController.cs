using IdentityEmail.Dtos;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityEmail.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRegisterDto createUserRegisterDto)
        {

            AppUser appUser = new AppUser()
            {
                Name = createUserRegisterDto.Name,
                Surname = createUserRegisterDto.Surname,
                Email = createUserRegisterDto.Email,
                UserName = createUserRegisterDto.UserName
            };

            var result = await _userManager.CreateAsync(appUser, createUserRegisterDto.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View();
        }
    }
}
