namespace FootShopSystem.Models.Api.Shoes
{
    public class AllShoesApiRequestModel
    {
        public string Brand { get; init; }

        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int ShoesPerPage { get; init; } = 10;

    }
}


