namespace FootShopSystem.Models.Home
{
    using FootShopSystem.Services.Home;
    using System.Collections.Generic;
    public class HomeShoeListingViewModel
    {
        public IEnumerable<HomeServiceModel> Shoes { get; set; }
    }
}
