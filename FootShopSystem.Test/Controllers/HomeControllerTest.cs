namespace FootShopSystem.Test.Controllers
{
    using FootShopSystem.Controllers;
    using FootShopSystem.Models.Profile;
    using FootShopSystem.Services.Home;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using Xunit;
    using static FootShopSystem.Test.Data.Shoes;
    using static FootShopSystem.WebConstants.Cache;
    public class HomeControllerTest
    {
        [Fact]
        public void GetAccountPageShouldBeForAuthorizedUsersAndReturnView()
        => MyController<ProfileController>
               .Instance(controller => controller
               .WithUser())
               .Calling(c => c.AccountPage())
               .ShouldReturn()
               .View(view => view.WithModelOfType<ProfileDataViewModel>());

        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
            .Instance(controller => controller
                .WithData(GetTenShoes))
            .Calling(c => c.Index())
            .ShouldHave()
            .MemoryCache(cache => cache
                .ContainingEntry(entry => entry
                    .WithKey(MostRecenetProductsCacheKey)
                    .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                    .WithValueOfType<List<HomeServiceModel>>()))
            .AndAlso()
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<List<HomeServiceModel>>());

    }
}
