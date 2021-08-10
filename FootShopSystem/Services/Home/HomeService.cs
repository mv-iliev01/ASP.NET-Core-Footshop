namespace FootShopSystem.Services.Home
{
    using FootShopSystem.Data;
    using System.Collections.Generic;
    using System.Linq;
    public class HomeService : IHomeService
    {
        private readonly FootshopDbContext data;

        public HomeService(FootshopDbContext data)
        {
            this.data = data;
        }

        public List<HomeServiceModel> GetTopThreeShoeModels()
        {
            return this.data
               .Shoes
               .Select(s => new HomeServiceModel
               {
                   Id = s.Id,
                   ImageUrl = s.ImageUrl,
                   Brand = s.Brand,
                   Model = s.Model,
                   Color = s.Color.Name,
                   Price = s.Price,
                   TimeCreated = s.TimeCreated
               })
               .OrderByDescending(sh => sh.TimeCreated)
               .Take(3)
               .ToList();
        }
    }
}
