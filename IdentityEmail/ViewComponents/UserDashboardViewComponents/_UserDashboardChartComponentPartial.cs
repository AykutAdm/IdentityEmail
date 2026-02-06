using IdentityEmail.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityEmail.ViewComponents.UserDashboardViewComponents
{
    public class _UserDashboardChartComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var vm = new DashboardViewModel
            {
                // Şimdilik dummy data, sonra DB’den doldurursun
                Tarihler = new List<string> { "01.02", "02.02", "03.02", "04.02", "05.02" },
                GonderilenMailler = new List<int> { 3, 5, 2, 7, 4 },
                BasariliGirisler = new List<int> { 1, 4, 3, 6, 2 },
                ToplamBasariliGiris = 20,
                Son30GunBasarisizGiris = 2
            };

            return View(vm);
        }
    }
}
