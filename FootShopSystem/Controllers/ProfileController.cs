namespace FootShopSystem.Controllers
{
    using FootShopSystem.Data;
    using FootShopSystem.Infrastructures;
    using FootShopSystem.Models.Profile;
    using FootShopSystem.Models.Shoes;
    using FootShopSystem.Services.Profile;
    using FootShopSystem.Services.Shoes;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Security.Claims;

    public class ProfileController : Controller
    {
        private readonly IShoeService shoes;
        private readonly IProfileService profile;

        public ProfileController(
            IShoeService shoes,
            IProfileService profile)
        {
            this.shoes = shoes;
            this.profile = profile;
        }

        public IActionResult AccountPage()
        {
            var myShoesCount = shoes.ByUser(this.User.Id()).Count();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userName = User.FindFirstValue(ClaimTypes.Name);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var isDesigner = this.profile.IsDesigner(userId);

            var favouriteShoes = this.profile.GetFavouriteShoesCount(userId);

            return View(new ProfileDataViewModel
            {
                FavouriteShoesCount = favouriteShoes,
                MyShoeCount = myShoesCount,
                Username = userName,
                Role = isDesigner ? "Designer" : "Customer",
                Email = userEmail,
            });
        }

        public IActionResult AddToFavourites(int id)
        {
            var userId = this.User.Id();

            this.profile.AddProductToUserFavourite(userId, id);

            return RedirectToAction(nameof(ShoesController.All), "Shoes");
        }
        public IActionResult Favourites([FromQuery] AllShoesQueryModel query)
        {
            var userId = this.User.Id();

            var queryResult = this.profile.All(
               query.Brand,
               query.SearchTerm,
               query.CurrentPage,
               query.ShoeCount,
               userId);

            var brands = this.shoes.AllModels().Select(c => c.Name);

            query.Brands = brands;
            query.ShoeCount = queryResult.TotalShoes;
            query.Shoes = queryResult.Shoes;

            return View(query);
        }

        public IActionResult RemoveFromFavourites(int id)
        {
            var userId = this.User.Id();

            var shoe = this.profile.GetShoe(id);
            var user = this.profile.GetUser(userId);

            user.FavouriteShoes.Remove(shoe);

            return Redirect("/Profile/Favourites");
        }

    }
}
