
namespace FootShopSystem.Test.Data
{
    using FootShopSystem.Data.Models;
    using FootShopSystem.Models.Profile;
    public static class User
    {
        public static string UserId = "test";
        public static ProfileDataViewModel GetUser()
                => new ProfileDataViewModel
                {
                    FavouriteShoesCount = 5,
                    MyShoeCount = 2,
                    Username = "Mimo",
                    Role = "Designer",
                    Email = "user@abv.bg"
                };
        public static Designer GetDesigner()
        {
            var designer = new Designer
            {
                Id = 1,
                Name = "DesignerOOd",
                PhoneNumber = "0897827242",
                UserId = "test"
            };

            var user = new FootShopSystem.Data.Models.User
            {
                Id = "test",
                UserName = "mimo"
            };

            return designer;
        }
    }
}
