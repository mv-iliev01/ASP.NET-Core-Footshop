namespace FootShopSystem.Controllers
{
    using FootShopSystem.Models.Shoes;
    using FootShopSystem.Services.Profile;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartController : Controller
    {
        private readonly IProfileService service;

        public ShoppingCartController(IProfileService service)
        {
            this.service = service;
        }

        public IActionResult Cart(int id)
        {
            var shoe = this.service.GetShoe(id);

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
