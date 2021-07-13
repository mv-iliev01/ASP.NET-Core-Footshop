using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FootShopSystem.Data.Models
{
    using static DataConstants;
    public class Color
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(ColorNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Shoe> Shoes { get; init; } = new List<Shoe>();
    }
}
