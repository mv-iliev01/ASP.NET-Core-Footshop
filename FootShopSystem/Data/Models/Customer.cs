namespace FootShopSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Customer;

    public class Customer
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(CustomerNameMaxLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }
        public int PurchasesCount { get; set; }

        public ICollection<ShoesCustomer> ShoesCustomers { get; set; }

    }
}
