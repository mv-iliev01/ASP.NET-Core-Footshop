﻿using FootShopSystem.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace FootShopSystem.Data
{
    public class FootshopDbContext : IdentityDbContext
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
        public DbSet<User> Users { get; init; }
    }
}
