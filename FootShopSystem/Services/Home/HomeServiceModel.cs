namespace FootShopSystem.Services.Home
{
    using System;
    public class HomeServiceModel
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
