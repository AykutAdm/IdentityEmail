using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityEmail.Context
{
    public class EmailContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-D0QM5NB\\SQLEXPRESS;initial Catalog=EmailDb;Integrated Security=True;TrustServerCertificate=True;");
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<SentMessage> SentMessages { get; set; }
    }
}
