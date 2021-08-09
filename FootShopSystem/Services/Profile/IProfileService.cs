using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootShopSystem.Services.Profile
{
   public interface IProfileService
    {
        public bool AddProductToUserFavourite(string userId, int productId);
    }
}
