namespace FootShopSystem.Models.Shoes.ViewModels
{
    using System.Collections.Generic;
    public class FavouriteShoesListingModel
    {
       public IEnumerable<FavouriteShoeViewModel> Shoes { get; set; }
    }
}
