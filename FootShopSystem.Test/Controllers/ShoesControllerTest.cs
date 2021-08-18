namespace FootShopSystem.Test.Controllers
{
    using FootShopSystem.Controllers;
    using FootShopSystem.Data.Models;
    using FootShopSystem.Models.Shoes;
    using FootShopSystem.Services.Shoes;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static FootShopSystem.Test.Data.Shoes;
    using static FootShopSystem.Test.Data.User;

    public class ShoesControllerTest
    {
        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<ShoesController>
            .Instance(instance => instance
            .WithUser())
                .Calling(c => c.Edit(
                    With.Empty<int>(),
                    With.Empty<AddShoeFormModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void EditPostShouldReturnNotFoundWhenNonAuthorUser()
            => MyController<ShoesController>
                .Instance(instance => instance
                    .WithUser(user => user.WithIdentifier("NonAuthor"))
                    .WithData(GetShoes(1)))
                .Calling(c => c.Edit(1, With.Empty<AddShoeFormModel>()))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DesignerController>(c => c.Become()));

        [Fact]
        public void MyShouldGetCorrectProductsAndReturnView()
            => MyController<ShoesController>
               .Instance()
               .WithUser(u => u.WithIdentifier(UserId))
               .Calling(c => c.MyShoes())
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldHave()
               .Data(data => data
               .WithSet<Shoe>(set =>
               {

               }))
               .AndAlso()
               .ShouldReturn()
               .View();


        [Fact]
        public void GetAddNonSellerShouldBeRedirectedToBecomeSeller()
            => MyController<ShoesController>
                .Instance()
                .WithUser(user => user.WithIdentifier("1"))
                .Calling(c => c.Add())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DesignerController>(c => c.Become()));

        [Fact]
        public void USerAddShouldRedirectToBecome()
         => MyController<ShoesController>
          .Instance()
          .WithUser(u => u.WithIdentifier(UserId))
          .Calling(c => c.Add())
          .ShouldHave()
          .ActionAttributes(a => a
              .RestrictingForAuthorizedRequests())
          .AndAlso()
          .ShouldReturn()
          .Redirect(redirect => redirect
           .To<DesignerController>(c => c.Become()));

        //[Fact]
        //public void GetAddSellerShouldBeRedirectedToCorrectView()
        //   => MyController<ShoesController>
        //       .Instance()
        //       .WithUser(user => user.WithIdentifier(TestUser.Identifier))
        //       .WithData(new Designer
        //       {
        //           UserId = TestUser.Identifier
        //       })
        //       .Calling(c => c.Add())
        //       .ShouldReturn()
        //       .View(result => result
        //            .WithModelOfType<AddShoeFormModel>()
        //            .Passing(model =>
        //            {
        //                model.Categories.FirstOrDefault(c => c.Id == 1).ShouldNotBeNull();
        //            }));


        [Theory]
        [InlineData(null)]
        public void PostAddNonSellerShouldBeRedirectedToBecomeSeller(AddShoeFormModel productFormModel)
         => MyController<ShoesController>
          .Instance()
          .WithUser(u => u.WithIdentifier(UserId))
          .Calling(c => c.Add(productFormModel))
          .ShouldHave()
          .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests()
                .RestrictingForHttpMethod(HttpMethod.Post))
          .AndAlso()
          .ShouldReturn()
          .Redirect(redirect => redirect
           .To<DesignerController>(c => c.Become()));

        [Theory]
        [InlineData(10)]
        public void PostAddProductWithInvalidCategoryShouldReturnView(int cateogryId)
            => MyController<ShoesController>
            .Instance(instance => instance
            .WithUser())
                .Calling(c => c.Add(new AddShoeFormModel
                {
                    CategoryId = cateogryId
                }))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .Redirect();


        [Theory]
        [InlineData(1)]
        public void GetEditCustomerIsRedirectedToSellersBecome(int id)
            => MyController<ShoesController>
                .Instance(instance => instance
                    .WithUser(user => user.WithIdentifier("test")))
                .Calling(c => c.Edit(id))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                   .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DesignerController>(c => c.Become()));

        [Theory]
        [InlineData(0)]
        public void GetEditUserIdIsDifferentAndShouldReturnUnauthorized(int id)
           => MyController<ShoesController>
               .Instance(instance => instance
                   .WithUser(user => user.WithIdentifier(TestUser.Identifier)))
               .Calling(c => c.Edit(id))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                  .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .Redirect();

        [Theory]
        [InlineData(1)]
        public void GetEditSellerShouldReturnView(int id)
           => MyController<ShoesController>
               .Instance(instance => instance
                   .WithUser(user => user.WithIdentifier(TestUser.Identifier)))
               .Calling(c => c.Edit(id))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                  .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .Redirect();
    }
}
