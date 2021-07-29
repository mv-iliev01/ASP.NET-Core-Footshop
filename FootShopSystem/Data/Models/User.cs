using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace FootShopSystem.Data.Models
{
    public class User: IdentityUser
    {
        public string Fullname { get; set; }

        public int ShoesMade { get; set; }
        public int PurchasesCount { get; set; }

        public IEnumerable<Shoe> FavouriteShoes { get; set; }

    }
}
