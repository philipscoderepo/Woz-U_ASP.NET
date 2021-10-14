using Microsoft.EntityFrameworkCore;

namespace Lesson6_HandsOn.Models{
    public class AlienContext : DbContext{

        public AlienContext(DbContextOptions<AlienContext> options) : base(options) {

        }
        public DbSet<Alien> Alien { get; set; }
    }
}