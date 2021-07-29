namespace FootShopSystem.Models.Shoes
{
    using System.ComponentModel;
    using static Data.DataConstants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FootShopSystem.Services.Shoes;
    using System;

    public class AddShoeFormModel
    {
        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; init; }


        [Range(MinPrice, MaxPrice)]
        public int Price { get; init; }

        [DisplayName("Image Url")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength
            , MinimumLength = DescriptionMinLength,
            ErrorMessage = "The filed Description must be a text with minimum length of {2}.")]
        public string Description { get; init; }
        public DateTime TimeCreated { get; init; }

        [DisplayName("Category")]
        public int CategoryId { get; init; }

        [DisplayName("Color")]
        public int ShoeColorsId { get; init; }

        [DisplayName("Size")]
        public int SizeId { get; init; }

        public IEnumerable<ShoeCategoryServiceModel> Categories { get; set; }
        public IEnumerable<ShoeColorServiceModel> Colors { get; set; }
        public IEnumerable<ShoeSizeServiceModel> Sizes { get; set; }

    }
}

