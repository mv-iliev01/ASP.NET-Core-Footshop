
namespace FootShopSystem.Models.Home
{
    using System;
    public class HomeViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; init; }
        public string Brand { get; init; }
        public string Model { get; init; }
        public string Color { get; init; }
        public double Price { get; init; }
        public DateTime TimeCreated { get; init; }

    }
}
