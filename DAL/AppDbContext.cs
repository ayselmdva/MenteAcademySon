using Microsoft.EntityFrameworkCore;
using WebApplicationBD.Models;

namespace WebApplicationBD.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }

        public DbSet<Profession> Professions { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
