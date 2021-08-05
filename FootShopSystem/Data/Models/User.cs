namespace FootShopSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    public class User : IdentityUser
    {
        public string Fullname { get; set; }

        public int ShoesMade { get; set; }
        public int PurchasesCount { get; set; }

        public ICollection<Shoe> FavouriteShoes { get; set; } = new List<Shoe>();

    }
}
