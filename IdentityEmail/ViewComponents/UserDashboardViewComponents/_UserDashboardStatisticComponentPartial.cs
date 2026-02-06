using IdentityEmail.Context;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityEmail.ViewComponents.UserDashboardViewComponents
{
    public class _UserDashboardStatisticComponentPartial : ViewComponent
    {
        private readonly EmailContext _emailContext;
        private readonly UserManager<AppUser> _userManager;

        public _UserDashboardStatisticComponentPartial(EmailContext emailContext, UserManager<AppUser> userManager)
        {
            _emailContext = emailContext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.v1 = _emailContext.Messages.Where(x => x.ReceiverEmail == user.Email).Count();

            ViewBag.v2 = _emailContext.SentMessages.Where(x => x.SenderEmail == user.Email).Count();

            ViewBag.v3 = _emailContext.Messages.Where(x => x.ReceiverEmail == user.Email).OrderByDescending(y => y.SendDate).Select(z => z.SendDate).FirstOrDefault();

            ViewBag.v4 = _emailContext.SentMessages.Where(x => x.SenderEmail == user.Email).OrderByDescending(y => y.SendDate).Select(z => z.SendDate).FirstOrDefault();
            return View();
        }
    }
}
