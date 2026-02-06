using Microsoft.AspNetCore.Mvc;

namespace IdentityEmail.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutTopbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
