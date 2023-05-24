using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudioWebApp.Models;

namespace StudioWebApp.DAL
{
    public class StudioDbContext:IdentityDbContext<AppUser>
    {
        public StudioDbContext(DbContextOptions<StudioDbContext> options):base(options)
        {

        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
