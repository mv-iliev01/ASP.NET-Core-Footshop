using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootShopSystem.Data.Models
{
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
