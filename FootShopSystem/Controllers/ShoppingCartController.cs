namespace FootShopSystem.Controllers
{
    using FootShopSystem.Models.Shoes;
    using FootShopSystem.Services.Profile;
    using FootShopSystem.Services.Shoes;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartController : Controller
    {
        private readonly IProfileService service;
        private readonly IShoeService shoeService;

        public ShoppingCartController(IProfileService service,
            IShoeService shoeService)
        {
            this.service = service;
            this.shoeService = shoeService;
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
        public IActionResult BuyNow(int shoeId)
        {
            var shoe = this.service.GetShoe(shoeId);
            shoeService.RemoveShoe(shoe);

            return Redirect("/");
        }
        public IActionResult Cancel()
        {
            return Redirect("/Shoes/All");
        }
    }
}
