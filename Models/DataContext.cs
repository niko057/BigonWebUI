
using BigonWebUI.Models.Entities;
using BigonWebUI.Models.Persistences.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BigonWebUI.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }

        public DbSet<Color> Colors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }





      

    }
}
