using FootShopSystem.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace FootShopSystem.Test.Routing
{
    public class ShoppingCartControllerTest
    {
        private const int ShoeId = 5;

        [Fact]
        public void GetCartShouldBeMapped()
        => MyRouting
            .Configuration()
            .ShouldMap(controller => controller
            .WithPath($"/ShoppingCart/Cart/{ShoeId}")
            .WithMethod(HttpMethod.Get))
            .To<ShoppingCartController>(c => c.Cart(ShoeId));

    }
}
