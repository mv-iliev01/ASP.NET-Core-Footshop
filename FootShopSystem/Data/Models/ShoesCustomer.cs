namespace FootShopSystem.Data.Models
{
    public class ShoesCustomer
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; }
    }
}
