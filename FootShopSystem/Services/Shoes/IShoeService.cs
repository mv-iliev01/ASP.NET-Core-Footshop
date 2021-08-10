namespace FootShopSystem.Services.Shoes
{
    using FootShopSystem.Data.Models;
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

        ShoeDetailsServiceModel Details(int id, string userId);

        IEnumerable<ShoeCategoryServiceModel> AllModels();

        int Create(
            string brand,
            string model,
            double price,
            string imageUrl,
            string description,
            DateTime TimeCreated,
            int categoryId,
            int shoeColorsId,
            int sizeId,
            string userId,
            int designerId);

        bool Edit(
            int id,
            string brand,
            string model,
            double price,
            string imageUrl,
            string description,
            int categoryId,
            int shoeColorsId,
            int sizeId);

        public IEnumerable<ShoeServiceModel> ByUser(string userId);

        public Shoe GetShoe(int id);
        public void RemoveShoe(Shoe shoe);


        public bool CategoryExists(AddShoeFormModel shoe);
        public bool ColorExists(AddShoeFormModel shoe);
        public bool SizeExists(AddShoeFormModel shoe);

        public bool IsByDesigner(int carId, int dealerId);
        public IEnumerable<string> AllShoeBrands();

        public IEnumerable<ShoeCategoryServiceModel> GetShoeCategories();

        public IEnumerable<ShoeColorServiceModel> GetShoeColors();

        public IEnumerable<ShoeSizeServiceModel> GetShoeSizes();

        public IEnumerable<ShoeColorServiceModel> GetDetailsShoeColor(string shoeModel);

        public IEnumerable<ShoeSizeServiceModel> GetDetailsShoeSizes(string shoeModel);

        public ShoeDetailsListingServiceModel GetShoeDetails(int shoeId);

        string GetShoeModel(int shoeId);
    }
}
