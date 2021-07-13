namespace FootShopSystem.Data.Models
{
    using static DataConstants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Shoe> Shoes { get; init; } = new List<Shoe>();
    }
}
