namespace FootShopSystem.Test.Controllers
{
    using FootShopSystem.Controllers;
    using FootShopSystem.Models.Profile;
    using FootShopSystem.Models.Shoes;
    using FootShopSystem.Test.Data;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    public class ProfileControllerTest
    {
        private const string UserId = "Test";

        [Fact]
        public void CustomerShouldCreateObjectCorrectlyAndShouldReturnView()
              => MyController<ProfileController>
               .Instance()
               .WithUser(u => u.WithIdentifier(TestUser.Identifier))
               .Calling(c => c.AccountPage())
               .ShouldHave()
               .ActionAttributes(a => a
                   .RestrictingForAuthorizedRequests())
                .ValidModelState()
               .AndAlso()
               .ShouldReturn()
               .View(view =>
                     view.WithModelOfType<ProfileDataViewModel>());

       

        //[Theory]
        //[InlineData("test", "test", Models.ProductsSorting.Anime, 1)]
        //public void FavouritesShouldReturnAllFavouriteProductsAndReturnView(
        //            string category,
        //            string searchTerm,
        //            ProductsSorting sorting,
        //            int currPage)
        //        => MyController<ProfileController>
        //            .Instance()
        //            .WithUser(user => user.WithIdentifier(TestUser.Identifier))
        //            .Calling(c => c.Favourites(new ProductsSearchQueryModel()
        //            {
        //                Category = category,
        //                SearchTerm = searchTerm,
        //                Sorting = sorting,
        //                Categories = null,
        //                TotalProducts = 3,
        //                CurrentPage = currPage,
        //                Products = new System.Collections.Generic.List<ProductServiceModel>(),
        //            }))
        //            .ShouldReturn()
        //            .View(view => view
        //                .WithModelOfType<ProductsSearchQueryModel>());

    }
}
