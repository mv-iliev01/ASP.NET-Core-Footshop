namespace FootShopSystem.Data.Models
{
    public class Favourite
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int ShoeId { get; set; }

        public Shoe Shoe { get; set; }
    }
}
