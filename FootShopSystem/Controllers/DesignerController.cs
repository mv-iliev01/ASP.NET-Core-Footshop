﻿namespace CarRentingSystem.Controllers
{
    using FootShopSystem.Data;
    using FootShopSystem.Data.Models;
    using FootShopSystem.Infrastructures;
    using FootShopSystem.Models.Designers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class DesignerController : Controller
    {
        private readonly FootshopDbContext data;

        public DesignerController(FootshopDbContext data)
            => this.data = data;


        public IActionResult Become() 
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDesignerFormModel designer)
         {
            var userId = this.User.Id();

            var userIdAlreadyDealer = this.data
                .Designers
                .Any(d => d.UserId == userId);

            if (userIdAlreadyDealer)
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

            this.data.Designers.Add(designerData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Shoes");
        }
    }
}


