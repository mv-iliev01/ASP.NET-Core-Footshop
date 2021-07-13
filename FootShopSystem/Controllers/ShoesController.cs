namespace FootShopSystem.Controllers
{
    using FootShopSystem.Data;
    using FootShopSystem.Data.Models;
    using FootShopSystem.Models.Shoes;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class ShoesController : Controller
    {
        private readonly FootshopDbContext data;

        public ShoesController(FootshopDbContext data)
            => this.data = data;

        public IActionResult Add() => View(new AddShoeFormModel
        {
            Categories = this.GetShoeCategories(),
            Colors = this.GetShoeColors(),
            Sizes = this.GetShoeSizes()
        });

        [HttpPost]
        public IActionResult Add(AddShoeFormModel shoe)
        {

            if (!this.data.Categories.Any(s => s.Id == shoe.CategoryId))
            {
                this.ModelState.AddModelError(nameof(shoe.CategoryId), "Category does not exist!");
            }

            if (!this.data.Colors.Any(s => s.Id == shoe.ShoeColorsId))
            {
                this.ModelState.AddModelError(nameof(shoe.ShoeColorsId), "Color does not exist!");
            }

            if (!this.data.Sizes.Any(s => s.Id == shoe.SizeId))
            {
                this.ModelState.AddModelError(nameof(shoe.SizeId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                shoe.Categories = this.GetShoeCategories();
                shoe.Colors = this.GetShoeColors();
                shoe.Sizes = this.GetShoeSizes();

                return View(shoe);
            }

            var shoeData = new Shoe
            {
                Brand = shoe.Brand,
                Model = shoe.Model,
                Price = shoe.Price,
                ImageUrl = shoe.ImageUrl,
                Description = shoe.Description,
                CategoryId = shoe.CategoryId,
                ColorId=shoe.ShoeColorsId,
                SizeId=shoe.SizeId
            };

            this.data.Shoes.Add(shoeData);

            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<ShoeCategoryViewModel> GetShoeCategories()
        => this.data
            .Categories
            .Select(c => new ShoeCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        private IEnumerable<ShoeColorViewModel> GetShoeColors()
        => this.data
            .Colors
            .Select(c => new ShoeColorViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        private IEnumerable<ShoeSizeViewModel> GetShoeSizes()
        => this.data
            .Sizes
            .Select(s => new ShoeSizeViewModel
            {
                Id = s.Id,
                SizeValue = s.SizeValue
            })
            .ToList();
    }
}
