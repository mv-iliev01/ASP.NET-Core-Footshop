namespace FootShopSystem.Test.Routing
{
    using FootShopSystem.Controllers;
    using FootShopSystem.Models.Designers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class DesignerControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Designer/Become")
                .To<DesignerController>(c => c.Become());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Designer/Become")
                    .WithMethod(HttpMethod.Post))
                .To<DesignerController>(c => c.Become(With.Any<BecomeDesignerFormModel>()));
    }
}

