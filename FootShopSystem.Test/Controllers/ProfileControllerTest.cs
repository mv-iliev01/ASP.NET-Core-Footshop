namespace FootShopSystem.Test.Controllers
{
    using FootShopSystem.Controllers;
    using FootShopSystem.Models.Profile;
    using FootShopSystem.Test.Data;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    public class ProfileControllerTest
    {
        [Fact]
        public void AccountPageShouldWorkAsExpected()
            => MyController<ProfileController>
                .Instance(controller => controller
                    .WithData(User.GetUser()))
                .Calling(c => c.AccountPage())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Get)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<ProfileDataViewModel>(dealers => dealers
                        .Any(d => d.FavouriteShoesCount == 5 &&
                        d.Email == "user@abv.bg" &&
                        d.MyShoeCount == 2 &&
                        d.Username == "Mimo" &&
                        d.Role == "Designer")))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<ProfileDataViewModel>());



    }
}
