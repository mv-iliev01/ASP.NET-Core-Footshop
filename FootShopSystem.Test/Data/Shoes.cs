using FootShopSystem.Data.Models;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FootShopSystem.Test.Data
{
    public static class Shoes
    {
        public static IEnumerable<Shoe> GetTenShoes
                => Enumerable.Range(0, 10).Select(i => new Shoe { });

        public static List<Shoe> GetShoes(int count, bool isPublic = true, bool sameUser = true)
        {
            var user = new FootShopSystem.Data.Models.User
            {
                Id = TestUser.Identifier,
                UserName = TestUser.Username
            };

            var articles = Enumerable
                .Range(1, count)
                .Select(i => new Shoe
                {
                    Id = i,
                    Brand=$"Nike {i}",
                    User = sameUser ? user : new FootShopSystem.Data.Models.User
                    {
                        Id = $"Author Id {i}",
                        UserName = $"Author {i}"
                    }
                })
                .ToList();

            return articles;
        }
    }
}
