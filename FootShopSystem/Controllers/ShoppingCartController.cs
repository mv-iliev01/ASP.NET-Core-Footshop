namespace FootShopSystem.Controllers
{
    using FootShopSystem.Data;
    using FootShopSystem.Data.Models;
    using FootShopSystem.Models.Shoes;
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
            var shoe = this.data.Shoes.Where(s => s.Id == id).FirstOrDefault();

            var model = new ShoeDetailsListingView
            {
                Id = shoe.Id,
                Price = shoe.Price,
                Brand = shoe.Brand,
                Model = shoe.Model,
                ImageUrl = shoe.ImageUrl,
                Description = shoe.Description,
                Size=shoe.SizeId,
                Color=shoe.ColorId,

            };

            return View(model); ;
        }
        public IActionResult Cancel()
        {
            return Redirect("/Shoes/All");
        }
    }
}
