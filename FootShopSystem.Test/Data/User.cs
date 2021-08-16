
namespace FootShopSystem.Test.Data
{
    using FootShopSystem.Models.Profile;
    public static class User
    {
        public static ProfileDataViewModel GetUser()
                => new ProfileDataViewModel
                {
                    FavouriteShoesCount = 5,
                    MyShoeCount = 2,
                    Username = "Mimo",
                    Role = "Designer",
                    Email = "user@abv.bg"
                };
    }
}
