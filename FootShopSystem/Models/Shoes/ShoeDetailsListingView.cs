namespace FootShopSystem.Models.Shoes
{
    using FootShopSystem.Services.Shoes;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ShoeDetailsListingView
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Size { get; init; }
        public int Color { get; init; }

    }
}
