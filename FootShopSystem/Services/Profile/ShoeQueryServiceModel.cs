using FootShopSystem.Services.Shoes;
using System.Collections.Generic;

namespace FootShopSystem.Services.Profile
{
    public class ShoeQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int ShoesPerPage { get; init; }

        public int TotalShoes { get; set; }

        public List<ShoeServiceModel> Shoes { get; init; }
    }
}
