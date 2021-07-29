namespace FootShopSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class Shoe
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime TimeCreated{ get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; init; }

        public int ColorId { get; set; }
        public Color Color { get; init; }

        public int SizeId { get; set; }
        public Size Size { get; init; }

        public int DesignerId { get; init; }
        public Designer Designer { get; init; }

        public int UserId { get; init; }
        public User User { get; init; }

    }
}
