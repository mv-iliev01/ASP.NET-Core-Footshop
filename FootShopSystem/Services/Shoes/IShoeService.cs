namespace FootShopSystem.Services.Shoes
{
    using FootShopSystem.Models.Shoes;
    using System;
    using System.Collections.Generic;
    public interface IShoeService
    {
        ShoeQueryServiceModel All(
            string brand,
            string searchTerm,
            int currentPage,
            int shoesPerPage);

        ShoeDetailsServiceModel Details(int id);

        int Create(
            string brand,
            string model,
            int price,
            string imageUrl,
            string description,
            DateTime TimeCreated,
            int categoryId,
            int shoeColorsId,
            int sizeId,
            int designerId);

        bool Edit(
            int id,
            string brand,
            string model,
            int price,
            string imageUrl,
            string description,
            int categoryId,
            int shoeColorsId,
            int sizeId);

        IEnumerable<ShoeServiceModel> ByUser(string userId);

        bool IsByDesigner(int carId, int dealerId);
        IEnumerable<string> AllShoeBrands();

        IEnumerable<ShoeCategoryServiceModel> GetShoeCategories();

        IEnumerable<ShoeColorServiceModel> GetShoeColors();

        IEnumerable<ShoeSizeServiceModel> GetShoeSizes();

        IEnumerable<ShoeColorServiceModel> GetDetailsShoeColor(string shoeModel);

        public IEnumerable<ShoeSizeServiceModel> GetDetailsShoeSizes(string shoeModel);

        public ShoeDetailsListingServiceModel GetShoeDetails(int shoeId,
            IEnumerable<ShoeSizeServiceModel> sizes,
            IEnumerable<ShoeColorServiceModel> colors);

        string GetShoeModel(int shoeId);
    }
}
