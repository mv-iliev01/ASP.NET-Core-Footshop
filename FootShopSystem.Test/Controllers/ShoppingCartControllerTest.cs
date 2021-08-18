namespace FootShopSystem.Test.Controllers
{
    using FootShopSystem.Controllers;
    using FootShopSystem.Models.Shoes;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ShoppingCartControllerTest
    {

        [Fact]
        public void RemoveShouldReturnView()
           => MyController<ShoppingCartController>
               .Instance()
               .WithUser(user => user.WithIdentifier(TestUser.Identifier))
               .Calling(c => c.Cancel())
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<ShoesController>(c => c.All(With.Any<AllShoesQueryModel>())));
        
    }
}
