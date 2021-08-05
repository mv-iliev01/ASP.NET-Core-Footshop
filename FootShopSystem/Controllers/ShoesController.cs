namespace FootShopSystem.Controllers
{
    using CarRentingSystem.Controllers;
    using FootShopSystem.Data;
    using FootShopSystem.Data.Models;
    using FootShopSystem.Infrastructures;
    using FootShopSystem.Models.Shoes;
    using FootShopSystem.Services.Designers;
    using FootShopSystem.Services.Shoes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class ShoesController : Controller
    {
        private readonly IShoeService shoes;
        private readonly IDesignerService designers;
        private readonly FootshopDbContext data;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public ShoesController(
            FootshopDbContext data,
            IShoeService shoes,
            IDesignerService designers,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            this.data = data;
            this.shoes = shoes;
            this.designers = designers;
            this.signInManager = signInManager;
            this.userManager = userManager;
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
                shoe.TimeCreated,
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
            var shoeModel = this.shoes.GetShoeModel(shoeId);

            var colors = this.shoes.GetDetailsShoeColor(shoeModel);

            var sizes = this.shoes.GetDetailsShoeSizes(shoeModel);

            var details = this.shoes.GetShoeDetails(shoeId, sizes, colors);

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
                return RedirectToAction(nameof(DesignerController.Become), "Designers");
            }
            var shoeD = this.shoes.Details(id);

            if (shoeD.UserId != userId)
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

            var designerId = this.designers.IdByUser(this.User.Id());

            if (designerId == 0)
            {
                return RedirectToAction(nameof(DesignerController.Become), "Dealers");
            }

            if (!ModelState.IsValid)
            {
                shoe.Categories = this.shoes.GetShoeCategories();

                return View(shoe);
            }

            if (!this.shoes.IsByDesigner(id, designerId))
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

            if (!shoeEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        public IActionResult Favourites()
        {
            var userId = this.User.Id();
            var user = new User();
            var shoes = user.FavouriteShoes.ToList();


            return View(shoes);
        }
    }
}

