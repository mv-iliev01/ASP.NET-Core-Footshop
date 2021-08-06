using System.Collections.Generic;

namespace FootShopSystem.Services.Shoes
{
    public class ShoeDetailsServiceModel : ShoeServiceModel
    {
        public string Description { get; init; }

        public int DesignerId { get; init; }

        public string DesignerName { get; init; }

        public int CategoryId { get; init; }
        public int ColorId { get; init; }
        public int SizeId { get; init; }

        public string UserId { get; init; }

        public bool isFav { get; set; }

        public IEnumerable<ShoeSizeServiceModel> Sizes { get; set; }
        
        public IEnumerable<ShoeColorServiceModel> Colors { get; set; }
    }
}
