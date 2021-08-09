using FootShopSystem.Data;
using FootShopSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootShopSystem.Services.Profile
{
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
    }
}
