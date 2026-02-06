using Microsoft.AspNetCore.Mvc;

namespace IdentityEmail.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
