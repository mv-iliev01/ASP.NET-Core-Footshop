namespace CarRentingSystem.Controllers
{
    using FootShopSystem.Data.Models;
    using FootShopSystem.Infrastructures;
    using FootShopSystem.Models.Designers;
    using FootShopSystem.Services.Designers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DesignerController : Controller
    {
        private readonly IDesignerService user;

        public DesignerController(IDesignerService user)
        {
            this.user = user;
        }

        public IActionResult Become() 
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDesignerFormModel designer)
         {
            var userId = this.User.Id();

            if (this.user.IsDesigner(userId))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(designer);
            }

            var designerData = new Designer
            {
                Name = designer.Name,
                PhoneNumber = designer.PhoneNumber,
                UserId = userId
            };

            this.user.AddDesignerToDb(designerData);

            return RedirectToAction("All", "Shoes");
        }
    }
}


