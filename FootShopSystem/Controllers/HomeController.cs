namespace FootShopSystem.Controllers
{
    using FootShopSystem.Data;
    using FootShopSystem.Models;
    using FootShopSystem.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly FootshopDbContext data;

        public HomeController(FootshopDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index(HomeShoeListingViewModel query)
        {

            var newestThreeModels = this.data
                .Shoes
                .Select(s => new HomeViewModel
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

            query.Shoes = newestThreeModels;


            return View(query);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
