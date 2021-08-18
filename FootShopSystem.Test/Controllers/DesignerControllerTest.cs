namespace FootShopSystem.Test.Controllers
{
    using FootShopSystem.Controllers;
    using FootShopSystem.Data.Models;
    using FootShopSystem.Models.Designers;
    using FootShopSystem.Models.Shoes;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    public class DesignerControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
           => MyController<DesignerController>
               .Instance()
               .Calling(c => c.Become())
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View();

        [Theory]
        [InlineData("Designer", "+35988888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string dealerName,
            string phoneNumber)
            => MyController<DesignerController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Become(new BecomeDesignerFormModel
                {
                    Name = dealerName,
                    PhoneNumber = phoneNumber
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Designer>(dealers => dealers
                        .Any(d =>
                            d.Name == dealerName &&
                            d.PhoneNumber == phoneNumber &&
                            d.UserId == TestUser.Identifier)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ShoesController>(c => c.All(With.Any<AllShoesQueryModel>())));

        [Theory]
        [InlineData("Designer", "+35988888")]
        public void PostBecomeSellerToBecomeSellerShouldReturnBadRequest(
            string name,
            string phoneNumber)
            => MyController<DesignerController>
                .Instance(controller => controller
                    .WithUser(user => user.WithIdentifier(TestUser.Identifier)))
                .Calling(c => c.Become(new BecomeDesignerFormModel
                {
                    Name = name,
                    PhoneNumber = phoneNumber
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Redirect();
    }
}
