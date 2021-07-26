namespace FootShopSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Designer;

    public class Designer
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DesignerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Shoe> Shoes { get; init; } = new List<Shoe>();
    }
}
