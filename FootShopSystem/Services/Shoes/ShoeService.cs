namespace FootShopSystem.Services.Shoes
{
    using FootShopSystem.Data;
    using FootShopSystem.Data.Models;
    using FootShopSystem.Models.Shoes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class ShoeService : IShoeService
    {
        private readonly FootshopDbContext data;

        public ShoeService(FootshopDbContext data)
            => this.data = data;

        public ShoeQueryServiceModel All(
            string brand,
            string searchTerm,
            int currentPage,
            int shoesPerPage)
        {
            var shoesQuery = this.data
                .Shoes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                shoesQuery = shoesQuery.Where(s => s.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                shoesQuery = shoesQuery.Where(s =>
                  (s.Brand + " " + s.Model).ToLower().Contains(searchTerm.ToLower()));
            };

            var shoeCount = this.data.Shoes.Count();

            var shoes = GetShoes(shoesQuery
                .Skip((currentPage - 1) * shoesPerPage)
                .Take(shoesPerPage)
                .OrderByDescending(s => s.Size.SizeValue));



            return new ShoeQueryServiceModel
            {
                Shoes = shoes
            };
        }


        public IEnumerable<ShoeServiceModel> ByUser(string userId)
            => GetShoes(this.data
                .Shoes
                .Where(c => c.Designer.UserId == userId));

        private static IEnumerable<ShoeServiceModel> GetShoes(IQueryable<Shoe> shoesQuery)
       => shoesQuery
            .Select(s => new ShoeServiceModel
            {
                Id = s.Id,
                Brand = s.Brand,
                Model = s.Model,
                Price = s.Price,
                ImageUrl = s.ImageUrl
            })
            .ToList();

        public IEnumerable<string> AllShoeBrands()
            => this.data
                .Shoes
                .Select(s => s.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public IEnumerable<ShoeCategoryServiceModel> GetShoeCategories()
        => this.data
            .Categories
            .Select(c => new ShoeCategoryServiceModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        public IEnumerable<ShoeColorServiceModel> GetShoeColors()
         => this.data
            .Colors
            .Select(c => new ShoeColorServiceModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        public IEnumerable<ShoeSizeServiceModel> GetShoeSizes()
         => this.data
            .Sizes
            .Select(s => new ShoeSizeServiceModel
            {
                Id = s.Id,
                SizeValue = s.SizeValue
            })
            .ToList();

        public string GetShoeModel(int shoeId)
            => this.data
                 .Shoes
                 .Where(s => s.Id == shoeId)
                .Select(s => s.Model)
                .FirstOrDefault();

        public IEnumerable<ShoeColorServiceModel> GetDetailsShoeColor(string shoeModel)
        => this.data
                .Shoes
                .Where(s => s.Model == shoeModel)
                .Select(s => new ShoeColorServiceModel
                {
                    Id = s.Id,
                    Name = s.Color.Name,
                    ShoeColorImg = s.ImageUrl
                })
                .ToList();
        public IEnumerable<ShoeSizeServiceModel> GetDetailsShoeSizes(string shoeModel)
        => this.data
            .Shoes
            .Where(s => s.Model == shoeModel)
            .Select(s => new ShoeSizeServiceModel
            {
                Id = s.Id,
                SizeValue = s.Size.SizeValue
            })
            .ToList();


        public ShoeDetailsListingServiceModel GetShoeDetails(int shoeId)
        {
            var shoeModel = GetShoeModel(shoeId);
            var colors = GetDetailsShoeColor(shoeModel);
            var sizes = GetDetailsShoeSizes(shoeModel);


            var result = this.data
                  .Shoes
                  .Where(s => s.Id == shoeId)
                  .Select(s => new ShoeDetailsListingServiceModel
                  {
                      Id = s.Id,
                      Price = s.Price,
                      Brand = s.Brand,
                      Model = s.Model,
                      ImageUrl = s.ImageUrl,
                      Description = s.Description,
                      Sizes = sizes.ToList(),
                      Colors = colors.ToList()
                  })
                  .FirstOrDefault();

            return result;
        }

        public ShoeDetailsServiceModel Details(int id, string userId)
        {
            var shoeModel = GetShoeModel(id);
            var colors = GetDetailsShoeColor(shoeModel);
            var sizes = GetDetailsShoeSizes(shoeModel);

            var user = this.data
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var shoe = user
                .FavouriteShoes
                .FirstOrDefault(s => s.Id == id);

            return this
                .data
                     .Shoes
                     .Where(s => s.Id == id)
                     .Select(s => new ShoeDetailsServiceModel
                     {
                         Id = s.Id,
                         Brand = s.Brand,
                         Model = s.Model,
                         ImageUrl = s.ImageUrl,
                         Price = s.Price,
                         Description = s.Description,
                         DesignerId = s.Designer.Id,
                         DesignerName = s.Designer.Name,
                         UserId = s.Designer.UserId,
                         Colors = colors.ToList(),
                         Sizes = sizes.ToList(),
                         isFav = shoe == null ? false : true

                     })
                     .FirstOrDefault();
        }


        public int Create(
            string brand,
            string model,
            double price,
            string imageUrl,
            string description,
            DateTime TimeCreated,
            int categoryId,
            int shoeColorsId,
            int sizeId,
            string userId,
            int designerId)
        {
            var shoeData = new Shoe
            {
                Brand = brand,
                Model = model,
                Price = price,
                ImageUrl = imageUrl,
                Description = description,
                TimeCreated = DateTime.UtcNow,
                CategoryId = categoryId,
                ColorId = shoeColorsId,
                SizeId = sizeId,
                UserId = userId,
                DesignerId = designerId

            };

            this.data.Shoes.Add(shoeData);
            this.data.SaveChanges();

            return shoeData.Id;
        }

        public bool Edit(
            int id,
            string brand,
            string model,
            double price,
            string imageUrl,
            string description,
            int categoryId,
            int shoeColorsId,
            int sizeId)
        {
            var shoeData = this.data
                .Shoes
                .Find(id);

            shoeData.Brand = brand;
            shoeData.Model = model;
            shoeData.Price = price;
            shoeData.ImageUrl = imageUrl;
            shoeData.Description = description;
            shoeData.CategoryId = categoryId;
            shoeData.ColorId = shoeColorsId;
            shoeData.SizeId = sizeId;


            this.data.SaveChanges();

            return true;
        }


        public bool IsByDesigner(int shoeId, int designerId)
        => this.data
                .Shoes
                .Any(c => c.Id == shoeId && c.DesignerId == designerId);

        public bool CategoryExists(AddShoeFormModel shoe)
        {
            return this.data.Categories.Any(s => s.Id == shoe.CategoryId);
        }

        public bool ColorExists(AddShoeFormModel shoe)
        {
            return this.data.Colors.Any(s => s.Id == shoe.ShoeColorsId);
        }

        public bool SizeExists(AddShoeFormModel shoe)
        {
            return this.data.Sizes.Any(s => s.Id == shoe.SizeId);
        }

        public Shoe GetShoe(int id)
        {
            return this.data.Shoes.Where(s => s.Id == id).FirstOrDefault();
        }

        public void RemoveShoe(Shoe shoe)
        {
            this.data.Shoes.Remove(shoe);

            this.data.SaveChanges();
        }
    }
}

