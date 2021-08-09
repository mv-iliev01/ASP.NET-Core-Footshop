using FootShopSystem.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace FootShopSystem.Data
{
    public class FootshopDbContext : IdentityDbContext<User>
    {
        public FootshopDbContext(DbContextOptions<FootshopDbContext> options)
            : base(options)
        {
        }
        public DbSet<Shoe> Shoes { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Size> Sizes{ get; init; }
        public DbSet<Color> Colors { get; init; }
        public DbSet<Designer> Designers { get; init; }
        public DbSet<Favourite> Favourites { get; init; }

          
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
                .Entity<Favourite>()
                .HasKey(f => new { f.ShoeId, f.UserId });

            builder
                .Entity<Shoe>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Shoes)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Shoe>()
                .HasOne(p => p.Designer)
                .WithMany(s => s.Shoes)
                .HasForeignKey(p => p.DesignerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Shoe>()
                .HasOne(p => p.Color)
                .WithMany(s => s.Shoes)
                .HasForeignKey(p => p.ColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Shoe>()
                .HasOne(p => p.Size)
                .WithMany(s => s.Shoes)
                .HasForeignKey(p => p.SizeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                 .Entity<Designer>()
                 .HasOne<User>()
                 .WithOne()
                 .HasForeignKey<Designer>(s => s.UserId)
                 .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
