namespace FootShopSystem.Controllers
{
    using FootShopSystem.Models;
    using FootShopSystem.Services.Home;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using static WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly IHomeService service;
        private readonly IMemoryCache cache;

        public HomeController(IHomeService service,
            IMemoryCache cache)
        {
            this.service = service;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var mostRecentProducts = this.cache.Get<List<HomeServiceModel>>(MostRecenetProductsCacheKey);

            if (mostRecentProducts == null)
            {
                mostRecentProducts = service.GetTopThreeShoeModels();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(MostRecenetProductsCacheKey, mostRecentProducts, cacheOptions);
            }

            return View(mostRecentProducts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
