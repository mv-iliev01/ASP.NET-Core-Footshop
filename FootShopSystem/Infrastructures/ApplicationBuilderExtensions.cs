using FootShopSystem.Data;
using FootShopSystem.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FootShopSystem.Infrastructures
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<FootshopDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(FootshopDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }
            data.Categories.AddRange(new[]
            {
                new Category { Name = "Boots" },
                new Category { Name = "Trainers" },
                new Category { Name = "Sandals" },
                new Category { Name = "Open Top" }
            });
            data.Colors.AddRange(new[]
            {
                new Color { Name = "Red" },
                new Color { Name = "Blue" },
                new Color { Name = "Green" },
                new Color { Name = "Orange" },
                new Color { Name = "Yellow" },
                new Color { Name = "Brown" },
                new Color { Name = "Grey" },
                new Color { Name = "Black" },
                new Color { Name = "White" },
                new Color { Name = "Pink" },
                new Color { Name = "Purple" },
            });

            data.Sizes.AddRange(new[]
            {
            new Size { SizeValue = 35 },
            new Size { SizeValue = 36 },
            new Size { SizeValue = 37 },
            new Size { SizeValue = 38 },
            new Size { SizeValue = 39 },
            new Size { SizeValue = 40 },
            new Size { SizeValue = 41 },
            new Size { SizeValue = 42 },
            new Size { SizeValue = 43 },
            new Size { SizeValue = 44 },
            new Size { SizeValue = 45 },
            });

            data.SaveChanges();
        }
    }
}
