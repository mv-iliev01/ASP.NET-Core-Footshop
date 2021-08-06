namespace FootShopSystem.Models.Shoes.ViewModels
{
    using FootShopSystem.Data.Models;
    using System.Collections.Generic;
    public class FavouriteShoesListingModel
    {
       public IEnumerable<Shoe> Shoes { get; set; }
    }
}
