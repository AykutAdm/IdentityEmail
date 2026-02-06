using IdentityEmail.Context;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityEmail.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutTopbarMessageComponentPartial : ViewComponent
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public _UserLayoutTopbarMessageComponentPartial(EmailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _context.Messages.Where(x => x.ReceiverEmail == user.Email).OrderByDescending(y => y.SendDate).Take(4).ToList();
            return View(values);
        }
    }
}
