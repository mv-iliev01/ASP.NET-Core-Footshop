namespace FootShopSystem.Controllers
{
    using FootShopSystem.Data;
    using FootShopSystem.Infrastructures;
    using FootShopSystem.Models.Profile;
    using FootShopSystem.Models.Shoes.ViewModels;
    using FootShopSystem.Services.Designers;
    using FootShopSystem.Services.Shoes;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Security.Claims;

    public class ProfileController : Controller
    {
        private readonly IShoeService shoes;
        private readonly FootshopDbContext data;

        public ProfileController(
            FootshopDbContext data,
            IShoeService shoes)
        {
            this.shoes = shoes;
            this.data = data;
        }

        public IActionResult AccountPage()
        {
            var myShoesCount = shoes.ByUser(this.User.Id()).Count();

            var purchasesCount = this.data.Users.Select(c => c.PurchasesCount).Count();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var user = this.data.Users.Where(u => u.Id == userId).FirstOrDefault();
            var favouriteShoes = user.FavouriteShoes.Count();


            return View(new ProfileDataViewModel
            {
                FavouriteShoesCount=favouriteShoes,
                MyShoeCount = myShoesCount,
                Username = userName,
                Email = userEmail,
                
                PurchasesCount = purchasesCount
            });
        }

    }
}
