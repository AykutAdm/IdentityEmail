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
        private readonly SignInManager<AppUser> _signInManager;

        public RegisterController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRegisterDto createUserRegisterDto)
        {
            Random random = new Random();
            int confirmCode = random.Next(100000, 999999);


            AppUser appUser = new AppUser()
            {
                Name = createUserRegisterDto.Name,
                Surname = createUserRegisterDto.Surname,
                Email = createUserRegisterDto.Email,
                UserName = createUserRegisterDto.UserName,
                ConfirmCode = confirmCode.ToString()

            };

            var result = await _userManager.CreateAsync(appUser, createUserRegisterDto.Password);

            if (result.Succeeded)
            {
                TempData["ConfirmCode"] = confirmCode.ToString();
                TempData["UserId"] = appUser.Id;
                return RedirectToAction("VerifyCode", "Register");
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


        [HttpGet]
        public IActionResult VerifyCode()
        {
            ViewBag.ConfirmCode = TempData["ConfirmCode"];
            ViewBag.UserId = TempData["UserId"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCode(string code, int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            // Kod doğru mu kontrol et
            if (user.ConfirmCode == code)
            {
                // Two Factor'ı aktif et
                await _userManager.SetTwoFactorEnabledAsync(user, true);

                // Başarı mesajı
                TempData["Success"] = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";

                // Login sayfasına yönlendir (otomatik giriş yapma)
                return RedirectToAction("UserLogin", "Login");
            }

            // Kod yanlış
            TempData["Error"] = "Kod hatalı!";
            return View();
        }
    }
}
