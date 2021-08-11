using FluentAssertions;
using FootShopSystem.Controllers;
using FootShopSystem.Data.Models;
using FootShopSystem.Models.Home;
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
        public void IndexShouldReturnViewWithCorrectModelAndData()
       => MyMvc
            .Pipeline()
            .ShouldMap("/")
            .To<HomeController>(c => c.Index(null))
            .Which(controller => controller
                 .WithData(Enumerable.Range(0, 10).Select(i => new Shoe()))
            .ShouldReturn()
            .View(view => view
            .WithModelOfType<HomeShoeListingViewModel>()
            .Passing(m => m.Shoes.Should().HaveCount(3))));

        [Fact]
        public void ErrorShouldReturnView()
        {
            var homeController = new HomeController(null);

            var result = homeController.Error();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

        }
    }
}
