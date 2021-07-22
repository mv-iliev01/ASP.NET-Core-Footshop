using System.Collections.Generic;

namespace FootShopSystem.Services.Shoes
{
    public interface IShoeService
    {
        ShoeQueryServiceModel All(
            string brand,
            string searchTerm,
            int currentPage,
            int shoesPerPage);

        IEnumerable<string> AllShoeBrands();
    }
}
