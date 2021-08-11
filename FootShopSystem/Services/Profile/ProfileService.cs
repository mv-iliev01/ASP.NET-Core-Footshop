namespace FootShopSystem.Services.Profile
{
    using FootShopSystem.Data;
    using FootShopSystem.Data.Models;
    using FootShopSystem.Services.Shoes;
    using System.Collections.Generic;
    using System.Linq;

    public class ProfileService : IProfileService
    {
        private readonly FootshopDbContext data;

        public ProfileService(FootshopDbContext data)
        {
            this.data = data;
        }

        public bool AddProductToUserFavourite(string userId, int shoeId)
        {
            var fav = new Favourite()
            {
                UserId = userId,
                ShoeId = shoeId
            };

            this.data.Favourites.Add(fav);
            this.data.SaveChanges();

            return true;
        }

        public ShoeQueryServiceModel All(
            string category,
            string searchTerm,
            int currentPage,
            int productsPerPage,
            string userId)
        {

            var shoesQuery = this.GetCustomerFavouriteProducts(userId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                shoesQuery = shoesQuery.Where(s => s.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                shoesQuery = shoesQuery.Where(s =>
                    s.Brand.ToLower().Contains(searchTerm.ToLower()) ||
                    s.Model.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalShoes = shoesQuery.Count();

            var shoes = shoesQuery
                .Select(s => new ShoeServiceModel
                {
                    Id = s.Id,
                    Brand = s.Brand,
                    Model = s.Model,
                    ImageUrl = s.ImageUrl,
                    Price = s.Price
                })
                .ToList();

            var categories = this.data
                .Shoes
                .Select(c => c.Category.Name)
                .Distinct()
                .ToList();

            return new ShoeQueryServiceModel
            {
                TotalShoes = totalShoes,
                CurrentPage = currentPage,
                Shoes = shoes,
                ShoesPerPage = productsPerPage
            };
        }

        public IEnumerable<Shoe> GetCustomerFavouriteProducts(string userId)
        => this.data
            .Favourites
            .Where(f => f.UserId == userId)
            .Select(s => s.Shoe);

        public int GetFavouriteShoesCount(string userId)
        => this.data
            .Favourites
            .Where(f => f.UserId == userId)
            .Select(f => f.Shoe).Count();

        public Shoe GetShoe(int id)
            => this.data
            .Shoes
            .Where(s => s.Id == id)
            .FirstOrDefault();

        public User GetUser(string userId)
        => this.data
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

        public bool IsDesigner(string userId)
        => this.data
            .Designers
            .Any(d => d.UserId == userId);

    }
}
