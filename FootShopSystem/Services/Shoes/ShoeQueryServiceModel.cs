namespace FootShopSystem.Services.Shoes
{
    using System.Collections.Generic;
    
    public class ShoeQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int CarsPerPage { get; init; }

        public int ShoesCount { get; init; }
        public IEnumerable<ShoeServiceModel> Shoes { get; init; }
    }
}
