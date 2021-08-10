namespace FootShopSystem.Controllers
{
    using FootShopSystem.Models;
    using FootShopSystem.Models.Home;
    using FootShopSystem.Services.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly IHomeService service;

        public HomeController(IHomeService service)
        {
            this.service = service;
        }

        public IActionResult Index(HomeShoeListingViewModel query)
        {
            var newestThreeModels = service.GetTopThreeShoeModels();

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
