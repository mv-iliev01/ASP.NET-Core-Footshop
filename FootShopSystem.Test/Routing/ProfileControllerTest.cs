namespace FootShopSystem.Test.Routing
{
    using FootShopSystem.Controllers;
    using FootShopSystem.Models.Shoes;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

   public class ProfileControllerTest
    {
        [Fact]
        public void GetAccountPageShouldBeMapped()
        => MyRouting
            .Configuration()
            .ShouldMap("/Profile/AccountPage")
            .To<ProfileController>(p => p.AccountPage());

        [Fact]
        public void GetAddToFavouritesShouldBeMapped()
        => MyRouting
            .Configuration()
            .ShouldMap("/Profile/Favourites")
            .To<ProfileController>(p => p.Favourites(With.Any<AllShoesQueryModel>()));
    }
}
