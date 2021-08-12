using FluentAssertions;
using FootShopSystem.Controllers;
using FootShopSystem.Data.Models;
using FootShopSystem.Models.Home;
using FootShopSystem.Models.Profile;
using FootShopSystem.Services.Home;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace FootShopSystem.Test.Controllers
{
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


    }
}
