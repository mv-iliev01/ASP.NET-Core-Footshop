namespace FootShopSystem.Services.Home
{
    using System.Collections.Generic;
    public interface IHomeService
    {
        List<HomeServiceModel> GetTopThreeShoeModels();
    }
}
