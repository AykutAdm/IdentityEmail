using IdentityEmail.Dtos;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityEmail.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByNameAsync(loginUserDto.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(loginUserDto.UserName, loginUserDto.Password, false, false);

                if (result.Succeeded)
                {
                    // Kullanıcıyı çıkış yap
                    await _signInManager.SignOutAsync();

                    // UserId'yi TempData'ya at
                    TempData["UserId"] = user.Id;

                    return RedirectToAction("VerifyLoginCode", "Login");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult VerifyLoginCode()
        {
            if (TempData["UserId"] == null)
            {
                return RedirectToAction("UserLogin");
            }

            ViewBag.UserId = TempData["UserId"];
            TempData.Keep("UserId");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyLoginCode(string code, int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user.ConfirmCode == code)
            {
                // Kod doğru, giriş yap
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Dashboard");
            }

            // Kod yanlış
            TempData["Error"] = "Kod hatalı!";
            TempData["UserId"] = userId;
            return RedirectToAction("VerifyLoginCode");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Default");
        }

    }
}