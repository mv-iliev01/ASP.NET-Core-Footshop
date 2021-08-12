namespace FootShopSystem.Test.Services
{
    using FootShopSystem.Data;
    using FootShopSystem.Data.Models;
    using FootShopSystem.Services.Home;
    using FootShopSystem.Test.Mocks;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class HomeServiceTest
    {
        //[Fact]
        //public void GetTopThreeShoeModelsShouldTakeNewestThreeShoes()
        //{
        //    using var data = SetShoesData();
        //    var homeService = new HomeService(data);
        //    var shoes = homeService.GetTopThreeShoeModels();

        //    var shoe1 = new HomeServiceModel { Id = 1, TimeCreated=DateTime.Now };
        //    var shoe2 = new HomeServiceModel { Id = 2, TimeCreated=DateTime.Now };
        //    var shoe3 = new HomeServiceModel { Id = 3, TimeCreated=DateTime.Now };
          
        //    var list = new List<HomeServiceModel>();
        //    list.Add(shoe1);
        //    list.Add(shoe2);
        //    list.Add(shoe3);

        //    Assert.Equal(list, shoes);
        //}

        public HomeService SetDbAndService(FootshopDbContext data)
        {
            var service = new HomeService(data);

            return service;
        }
        public FootshopDbContext SetShoesData()
        {
            var data = DatabaseMock.Instance;

            data.Shoes.AddRange(new[]
            {
                new Shoe {Id=1,TimeCreated=DateTime.Now},
                new Shoe {Id=2,TimeCreated=DateTime.Now},
                new Shoe {Id=3,TimeCreated=DateTime.Now},
                new Shoe {Id=4,TimeCreated=DateTime.Now},
                new Shoe {Id=5,TimeCreated=DateTime.Now},
            });
            data.SaveChanges();

            return data;
        }
    }
}
