
namespace FootShopSystem.Test.Routing
{
    using FootShopSystem.Controllers;
    using FootShopSystem.Models.Shoes;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class ShoesControllerTest
    {
        private const int ShoeId = 5;

        [Fact]
        public void GetAddShouldBeMapped()
        => MyRouting
            .Configuration()
            .ShouldMap("/Shoes/Add")
            .To<ShoesController>(s => s.Add());

        [Fact]
        public void PostAddShouldBeMapped()
        => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Shoes/Add")
            .WithMethod(HttpMethod.Post))
            .To<ShoesController>(s => s.Add(With.Any<AddShoeFormModel>()));

        [Fact]
        public void GetAllShoesShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(controller => controller
            .WithPath("/Shoes/All")
            .WithMethod(HttpMethod.Get))
            .To<ShoesController>(s => s.All(With.Any<AllShoesQueryModel>()));


        [Fact]
        public void GetDetailsShouldBeMapped()
        => MyRouting
            .Configuration()
            .ShouldMap($"/Shoes/Details/{ShoeId}")
            .To<ShoesController>(s => s.Details(ShoeId));

        [Fact]
        public void GetMyShoesShouldBeMapped()
        => MyRouting
            .Configuration()
            .ShouldMap("/Shoes/MyShoes")
            .To<ShoesController>(s => s.MyShoes());

        [Fact]
        public void GetEditShouldBeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap(controller => controller
           .WithPath($"/Shoes/Edit/{ShoeId}")
           .WithMethod(HttpMethod.Get))
           .To<ShoesController>(s => s.Edit(ShoeId));

        [Fact]
        public void PostEditShouldBeMapped()
       => MyRouting
           .Configuration()
           .ShouldMap(request => request
           .WithPath($"/Shoes/Edit/{ShoeId}")
           .WithMethod(HttpMethod.Post))
           .To<ShoesController>(s => s.Edit(ShoeId,With.Any<AddShoeFormModel>()));

    }
}
