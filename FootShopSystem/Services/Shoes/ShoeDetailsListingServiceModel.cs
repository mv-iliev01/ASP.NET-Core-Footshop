namespace FootShopSystem.Models.Shoes
{
    using FootShopSystem.Services.Shoes;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ShoeDetailsListingServiceModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }


        [DisplayName("Size")]
        public int SizeId { get; init; }
        public IEnumerable<ShoeSizeServiceModel> Sizes { get; set; }

        [DisplayName("Color")]
        public int ColorId { get; init; }
        public IEnumerable<ShoeColorServiceModel> Colors { get; set; }

    }
}
