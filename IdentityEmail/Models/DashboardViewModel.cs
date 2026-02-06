namespace IdentityEmail.Models
{
    public class DashboardViewModel
    {
        public List<string> Tarihler { get; set; } = new();
        public List<int> GonderilenMailler { get; set; } = new();
        public List<int> BasariliGirisler { get; set; } = new();

        public int ToplamBasariliGiris { get; set; }
        public int Son30GunBasarisizGiris { get; set; }
    }
}
