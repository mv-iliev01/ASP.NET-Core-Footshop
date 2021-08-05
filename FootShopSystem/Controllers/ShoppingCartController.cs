namespace FootShopSystem.Controllers
{
    using FootShopSystem.Data;
    using FootShopSystem.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class ShoppingCartController : Controller
    {

        private readonly FootshopDbContext data;

        public ShoppingCartController(FootshopDbContext data)
        {
            this.data = data;
        }

        public IActionResult Cart(int id)
        {

            var user = new User();

            var shoe = this.data
                .Shoes
                .Where(s => s.Id == id)
                .FirstOrDefault();

            var shoes = user.FavouriteShoes;
            shoes.Add(shoe);

            return View(shoes);
        }
    }
}
