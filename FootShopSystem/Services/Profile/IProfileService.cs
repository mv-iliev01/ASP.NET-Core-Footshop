using FootShopSystem.Data.Models;
using FootShopSystem.Services.Shoes;
using System.Collections.Generic;

namespace FootShopSystem.Services.Profile
{
    public interface IProfileService
    {
        public bool AddProductToUserFavourite(string userId, int productId);

        public ShoeQueryServiceModel All(
            string category,
            string searchTerm,
            int currentPage,
            int productsPerPage,
            string userId);

        public IEnumerable<Shoe> GetCustomerFavouriteProducts(string userId);
    }
}
