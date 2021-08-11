using FootShopSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace FootShopSystem.Test.Mocks
{
    public static class DatabaseMock
    {
        public static FootshopDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<FootshopDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new FootshopDbContext(dbContextOptions);
            }
        }
    }
}
