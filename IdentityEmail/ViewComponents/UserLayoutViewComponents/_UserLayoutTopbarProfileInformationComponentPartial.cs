using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityEmail.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutTopbarProfileInformationComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _UserLayoutTopbarProfileInformationComponentPartial(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }
    }
}
