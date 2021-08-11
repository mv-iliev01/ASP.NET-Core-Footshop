namespace FootShopSystem.Services.Profile
{
    using FootShopSystem.Data.Models;
    using System.Collections.Generic;
    public interface IProfileService
    {
        public bool AddProductToUserFavourite(string userId, int productId);
        public bool IsDesigner(string userId);
        public ShoeQueryServiceModel All(
            string category,
            string searchTerm,
            int currentPage,
            int productsPerPage,
            string userId);

        public User GetUser(string userId);
        public Shoe GetShoe(int id);
        public int GetFavouriteShoesCount(string userId);
        public IEnumerable<Shoe> GetCustomerFavouriteProducts(string userId);
    }
}
