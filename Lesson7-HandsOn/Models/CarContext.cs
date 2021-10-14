using Microsoft.EntityFrameworkCore;

namespace Lesson7_HandsOn.Models{
    public class CarContext : DbContext{
        public CarContext(DbContextOptions<CarContext> options) : base(options){}

         public DbSet<Car> Cars { get; set; }
     }
}
