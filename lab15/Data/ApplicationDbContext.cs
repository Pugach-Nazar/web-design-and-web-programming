using lab15.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lab15.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Seller> Sellers { get; set;}
        public DbSet<Category> Categories { get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Device>()
                .HasOne(d => d.Manufacturer)
                .WithMany(m => m.Devices)
                .HasForeignKey(d => d.ManufacturerId);

            builder.Entity<Device>()
                .HasOne(d => d.Seller)
                .WithMany(s => s.Devices)
                .HasForeignKey(d => d.SellerId);

            builder.Entity<Device>()
                .HasOne(d => d.Category)
                .WithMany(c => c.Devices)
                .HasForeignKey(d => d.CategoryId);
        }
    }
}