namespace FootShopSystem.Test.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using FootShopSystem.Data.Models;

    public static class Shoes
    {
        public static IEnumerable<Shoe> GetTenShoes
                => Enumerable.Range(0, 10).Select(i => new Shoe { });
    }
}
