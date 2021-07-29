namespace FootShopSystem.Controllers
{
    using FootShopSystem.Data;
    using FootShopSystem.Infrastructures;
    using FootShopSystem.Models.Profile;
    using FootShopSystem.Services.Designers;
    using FootShopSystem.Services.Shoes;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class ProfileController : Controller
    {
        private readonly IShoeService shoes;
        private readonly FootshopDbContext data;
        private readonly IDesignerService designers;

        public ProfileController(
            IDesignerService designers,
            FootshopDbContext data,
            IShoeService shoes)
        {
            this.designers = designers;
            this.shoes = shoes;
            this.data = data;
        }

        public IActionResult AccountPage()
        {
            var myShoesCount = shoes.ByUser(this.User.Id()).Count();
            //var username = this.data.Customers.Select(c => c.Username).FirstOrDefault(); ;
            //var email = this.data.Customers.Select(c => c.Email).FirstOrDefault();
            //var purchasesCount = this.data.Customers.Select(c => c.PurchasesCount).Count();

            return View(new ProfileDataViewModel
            {
                MyShoeCount = myShoesCount,
                //Username = username,
                //Email = email,
                //PurchasesCount = purchasesCount
            });
        }
    }
}
