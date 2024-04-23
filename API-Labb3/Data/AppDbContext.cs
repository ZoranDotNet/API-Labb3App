using API_Labb3.Entity;
using Microsoft.EntityFrameworkCore;

namespace API_Labb3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Link> Links { get; set; }
    }
}
