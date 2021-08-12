namespace FootShopSystem.Test.Pipeline
{
    using FootShopSystem.Controllers;
    using FootShopSystem.Data.Models;
    using FootShopSystem.Models.Designers;
    using FootShopSystem.Models.Shoes;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

   public class DesignersControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
               => MyPipeline
                   .Configuration()
                   .ShouldMap(request => request
                       .WithPath("/Designer/Become")
                       .WithUser())
                   .To<DesignerController>(c => c.Become())
                   .Which()
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
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Designer/Become")
                    .WithMethod(HttpMethod.Post)
                    .WithFormFields(new
                    {
                        Name = dealerName,
                        PhoneNumber = phoneNumber
                    })
                    .WithUser()
                    .WithAntiForgeryToken())
                .To<DesignerController>(c => c.Become(new BecomeDesignerFormModel
                {
                    Name = dealerName,
                    PhoneNumber = phoneNumber
                }))
                .Which()
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
    }
}

