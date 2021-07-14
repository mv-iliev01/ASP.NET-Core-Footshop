namespace FootShopSystem.Models.Shoes
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ShoeDetailsListingView
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }


        [DisplayName("Size")]
        public int SizeId { get; init; }
        public IEnumerable<ShoeSizeViewModel> Sizes { get; set; }

    }
}
