namespace FootShopSystem.Test.Services
{
    using FootShopSystem.Data;
    using FootShopSystem.Data.Models;
    using FootShopSystem.Services.Designers;
    using FootShopSystem.Test.Mocks;
    using System.Linq;
    using Xunit;
    public class DesignerServiceTest
    {
        private const string UserId = "TestUserId";
        [Fact]
        public void IsDesignerShouldReturnTrueWhenUserIsDesinger()
        {
            //Arrange
            using var data = this.GetDesignerData();

            var designerService = new DesignerService(data);

            //Act
            var result = designerService.IsDesigner(UserId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsDesignerShouldReturnFalseWhenUserIsNotDesinger()
        {
            //Arrange
            using var data = this.GetDesignerData();

            var designerService = new DesignerService(data);

            //Act
            var result = designerService.IsDesigner("AnotherUserId");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddDesignerToDataBaseShouldWorkProperly()
        {
            using var data = DatabaseMock.Instance;
            var designerService = new DesignerService(data);

            var designer = new Designer { UserId = UserId };
            designerService.AddDesignerToDb(designer);

            Assert.True(data.Designers.Any(d => d.UserId == UserId));
        }

        [Fact]
        public void GetDesignerIdProperly()
        {
            using var data = DatabaseMock.Instance;
            var designerService = new DesignerService(data);

            data.Designers.Add(new Designer { UserId = UserId, Id = 5 });
            data.SaveChanges();

            var result = designerService.IdByUser(UserId);

            Assert.Equal(5, result);
        }

        public FootshopDbContext GetDesignerData()
        {
            var data = DatabaseMock.Instance;

            data.Designers.Add(new Designer { UserId = UserId });
            data.SaveChanges();

            return data;
        }
    }
}
