﻿namespace FootShopSystem.Data.Models
{
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


        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        public int SizeId { get; set; }
        public Size Size { get; set; }
    }
}