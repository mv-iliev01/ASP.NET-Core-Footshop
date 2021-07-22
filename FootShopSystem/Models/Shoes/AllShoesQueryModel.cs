namespace FootShopSystem.Models.Shoes
{
    using FootShopSystem.Services.Shoes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllShoesQueryModel
    {
        public const int ShoesPerPage = 6;

        public string Brand { get; init; }

        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int ShoeCount { get; set; }

        public ShoeSorting Sorting { get; init; }

        public IEnumerable<ShoeServiceModel> Shoes { get; set; }
    }
}
