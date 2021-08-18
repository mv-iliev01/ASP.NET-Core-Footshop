namespace FootShopSystem.Test.Routing
{
    using FootShopSystem.Controllers;
    using FootShopSystem.Models.Shoes;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

   public class ProfileControllerTest
    {
        private const int id = 1;
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

        [Fact]
        public void AddToFavouritesRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap($"Profile/AddToFavourites/{id}")
            .To<ProfileController>(c => c.AddToFavourites(id));

        [Fact]
        public void RemoveFavouriteRouteShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap($"Profile/RemoveFromFavourites/{id}")
           .To<ProfileController>(c => c.RemoveFromFavourites(id));

        [Fact]
        public void FavouritesRoutingShouldBeMapped()
                    => MyRouting
                        .Configuration()
                        .ShouldMap("/Profile/Favourites")
                        .To<ProfileController>(c => c.Favourites(With.Any<AllShoesQueryModel>()));
    }
}
