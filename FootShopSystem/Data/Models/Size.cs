namespace FootShopSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Size
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int SizeValue { get; set; }

        public IEnumerable<Shoe> Shoes { get; init; } = new List<Shoe>();
    }
}
