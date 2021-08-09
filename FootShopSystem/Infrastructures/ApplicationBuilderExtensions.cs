namespace FootShopSystem.Infrastructures
{
    using FootShopSystem.Data;
    using FootShopSystem.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static WebConstants;
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var services = scopedServices.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedAdministrator(services);

            return app;
        }
        
        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<FootshopDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<FootshopDbContext>();

            if (data.Categories.Any())
            {
                return;
            }
            data.Categories.AddRange(new[]
            {
                new Category { Name = "Boots" },
                new Category { Name = "Trainers" },
                new Category { Name = "Sandals" }
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


            //var shoe = new Shoe
            //{
            //    Brand = "Nike",
            //    Model = "Airmax",
            //    Price = 200,
            //    ImageUrl = "https://www.sportvision.bg/files/thumbs/files/images/slike_proizvoda/media/CW1/CW1626-002/images/thumbs_800/CW1626-002_800_800px.jpg",
            //    Description = "The best shoe ever created you should try it !",
            //    TimeCreated = DateTime.Now,
            //    CategoryId = 1,
            //    ColorId = 1,
            //    SizeId = 1
            //};

            //data.Shoes.Add(shoe);

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                         return;
                    }
                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@abv.bg";
                    const string adminPassword = "admin123";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
