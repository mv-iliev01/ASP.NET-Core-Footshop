namespace FootShopSystem.Controllers
{
    using CarRentingSystem.Controllers;
    using FootShopSystem.Data;
    using FootShopSystem.Infrastructures;
    using FootShopSystem.Models.Shoes;
    using FootShopSystem.Services.Designers;
    using FootShopSystem.Services.Shoes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class ShoesController : Controller
    {
        private readonly IShoeService shoes;
        private readonly FootshopDbContext data;
        private readonly IDesignerService designers;

        public ShoesController(
            FootshopDbContext data,
            IShoeService shoes,
            IDesignerService designers)
        {
            this.data = data;
            this.shoes = shoes;
            this.designers = designers;
        }

        public IActionResult Add()
        {
            if (!this.designers.IsDesigner(this.User.Id()))
            {
                return RedirectToAction(nameof(DesignerController.Become), "Dealers");
            }

            return View(new AddShoeFormModel
            {
                Categories = this.shoes.GetShoeCategories(),
                Colors = this.shoes.GetShoeColors(),
                Sizes = this.shoes.GetShoeSizes()
            });

        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddShoeFormModel shoe)
        {
            var designerId = this.designers.IdByUser(this.User.Id());

            if (designerId == 0)
            {
                return RedirectToAction(nameof(DesignerController.Become), "Designers");
            }

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
                this.ModelState.AddModelError(nameof(shoe.SizeId), "Size does not exist!");
            }

            if (!ModelState.IsValid)
            {
                shoe.Categories = this.shoes.GetShoeCategories();
                shoe.Colors = this.shoes.GetShoeColors();
                shoe.Sizes = this.shoes.GetShoeSizes();

                return View(shoe);
            }

            this.shoes.Create(shoe.Brand,
                shoe.Model,
                shoe.Price,
                shoe.ImageUrl,
                shoe.Description,
                shoe.CategoryId,
                shoe.ShoeColorsId,
                shoe.SizeId,
                designerId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllShoesQueryModel query)
        {
            var queryResult = this.shoes.All(
               query.Brand,
               query.SearchTerm,
               query.CurrentPage,
               AllShoesQueryModel.ShoesPerPage);

            var brands = this.shoes.AllShoeBrands();

            query.ShoeCount = queryResult.ShoesCount;
            query.Brands = brands;
            query.Shoes = queryResult.Shoes;

            return View(query);
        }

        public IActionResult Details(int shoeId)
        {
            var shoeModel = this.data
                .Shoes
                .Where(s => s.Id == shoeId)
                .Select(s => s.Model)
                .FirstOrDefault();

            var colors = this.data
                .Shoes
                .Where(s => s.Model == shoeModel)
                .Select(s => new ShoeColorServiceModel
                {
                    Id = s.Id,
                    Name = s.Color.Name,
                    ShoeColorImg = s.ImageUrl
                })
                .ToList();

            var sizes = this.data
            .Shoes
            .Where(s => s.Model == shoeModel)
            .Select(s => new ShoeSizeServiceModel
            {
                Id = s.Id,
                SizeValue = s.Size.SizeValue
            })
            .ToList();

            var details = this.data
                .Shoes
                .Where(s => s.Id == shoeId)
                .Select(s => new ShoeDetailsListingView
                {
                    Id = s.Id,
                    Price = s.Price,
                    Brand = s.Brand,
                    Model = s.Model,
                    ImageUrl = s.ImageUrl,
                    Description = s.Description,
                    Sizes = sizes,
                    Colors = colors
                })
                .FirstOrDefault();

            return View(details);
        }

        public IActionResult MyShoes()
        {
            var myShoes = this.shoes.ByUser(this.User.Id());

            return View(myShoes);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.designers.IsDesigner(this.User.Id()))
            {
                return RedirectToAction(nameof(DesignerController.Become), "Dealers");
            }
            var car = this.shoes.Details(id);

            if (car.UserId != userId)
            {
                return Unauthorized();
            }
            var shoe = this.shoes.Details(id);

            return View(new AddShoeFormModel
            {
                Brand = shoe.Brand,
                Model = shoe.Model,
                Price = shoe.Price,
                ImageUrl = shoe.ImageUrl,
                Description = shoe.Description,
                CategoryId = shoe.CategoryId,
                ShoeColorsId = shoe.ColorId,
                SizeId = shoe.SizeId,
                Categories = this.shoes.GetShoeCategories(),
                Colors = this.shoes.GetShoeColors(),
                Sizes = this.shoes.GetShoeSizes()
            });
        }
        [Authorize]
        [HttpPost]

        public IActionResult Edit(int id, AddShoeFormModel shoe)
        {

            var dealerId = this.designers.IdByUser(this.User.Id());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DesignerController.Become), "Dealers");
            }

            if (!ModelState.IsValid)
            {
                shoe.Categories = this.shoes.GetShoeCategories();

                return View(shoe);
            }

            if (!this.shoes.IsByDealer(id, dealerId))
            {
                return BadRequest();
            }

            var shoeEdited = this.shoes.Edit(
                 id,
                 shoe.Brand,
                 shoe.Model,
                 shoe.Price,
                 shoe.ImageUrl,
                 shoe.Description,
                 shoe.CategoryId,
                 shoe.ShoeColorsId,
                 shoe.SizeId);

            if(!shoeEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}

