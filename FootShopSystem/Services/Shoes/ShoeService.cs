namespace FootShopSystem.Services.Shoes
{
    using FootShopSystem.Data;
    using System.Collections.Generic;
    using System.Linq;
    public class ShoeService : IShoeService
    {
        private readonly FootshopDbContext data;

        public ShoeService(FootshopDbContext data)
            => this.data = data;

        public ShoeQueryServiceModel All(
            string brand,
            string searchTerm,
            int currentPage,
            int shoesPerPage)
        {
            var shoesQuery = this.data
                .Shoes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                shoesQuery = shoesQuery.Where(s => s.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                shoesQuery = shoesQuery.Where(s =>
                  (s.Brand + " " + s.Model).ToLower().Contains(searchTerm.ToLower()));
            };

            var shoeCount = this.data.Shoes.Count();

            var shoes = shoesQuery
                .Skip((currentPage - 1) * shoesPerPage)
                .Take(shoesPerPage)
                .OrderByDescending(s => s.Size.SizeValue)
                .Select(s => new ShoeServiceModel
                {
                    Id = s.Id,
                    Brand = s.Brand,
                    Model = s.Model,
                    Price = s.Price,
                    ImageUrl = s.ImageUrl
                })
                .ToList();


            return new ShoeQueryServiceModel
            {
                Shoes = shoes
            };
        }

        public IEnumerable<string> AllShoeBrands()
            => this.data
                .Shoes
                .Select(s => s.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

    }
}
