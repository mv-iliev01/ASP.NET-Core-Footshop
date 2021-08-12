namespace FootShopSystem.Controllers
{
    using FootShopSystem.Data.Models;
    using FootShopSystem.Infrastructures;
    using FootShopSystem.Models.Shoes;
    using FootShopSystem.Services.Designers;
    using FootShopSystem.Services.Shoes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ShoesController : Controller
    {
        private readonly IShoeService shoes;
        private readonly IDesignerService designers;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public ShoesController(
            IShoeService shoes,
            IDesignerService designers,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
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
        public IActionResult Add(AddShoeFormModel shoeModel)
        {
            var designerId = this.designers.IdByUser(this.User.Id());

            var userId = this.User.Id();

            if (designerId == 0)
            {
                return RedirectToAction(nameof(DesignerController.Become), "Designers");
            }

            if (!shoes.CategoryExists(shoeModel))
            {
                this.ModelState.AddModelError(nameof(shoeModel.CategoryId), "Category does not exist!");
            }

            if (!shoes.ColorExists(shoeModel))
            {
                this.ModelState.AddModelError(nameof(shoeModel.ShoeColorsId), "Color does not exist!");
            }

            if (!shoes.SizeExists(shoeModel))
            {
                this.ModelState.AddModelError(nameof(shoeModel.SizeId), "Size does not exist!");
            }

            if (!ModelState.IsValid)
            {
                shoeModel.Categories = this.shoes.GetShoeCategories();
                shoeModel.Colors = this.shoes.GetShoeColors();
                shoeModel.Sizes = this.shoes.GetShoeSizes();

                return View(shoeModel);
            }

            this.shoes.Create(
                shoeModel.Brand,
                shoeModel.Model,
                shoeModel.Price,
                shoeModel.ImageUrl,
                shoeModel.Description,
                shoeModel.TimeCreated,
                shoeModel.CategoryId,
                shoeModel.ShoeColorsId,
                shoeModel.SizeId,
                userId,
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

        [Authorize]
        public IActionResult Details(int id)
        {
            var userId = this.User.Id();

            var shoe = this.shoes.Details(id, userId);

            return View(shoe);
        }

        [Authorize]
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
            var shoeD = this.shoes.Details(id, userId);

            if (shoeD.UserId != userId)
            {
                return Unauthorized();
            }
            var shoe = this.shoes.Details(id, userId);

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

        [Authorize]
        public IActionResult DeleteShoe(int id)
        {
            var shoe = shoes.GetShoe(id);

            shoes.RemoveShoe(shoe);
                
            return Redirect("/Shoes/All");
        }
    }
}

