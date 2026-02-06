using IdentityEmail.Dtos;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityEmail.ViewComponents.UserDashboardViewComponents
{
    public class _UserDashboardProfileInformationComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _UserDashboardProfileInformationComponentPartial(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            UserEditDto userEditDto = new UserEditDto();
            userEditDto.Email = values.Email;
            userEditDto.Name = values.Name;
            userEditDto.Surname = values.Surname;
            userEditDto.ImageUrl = values.ImageUrl;
            userEditDto.About = values.About;
            return View(userEditDto);
        }
    }
}
