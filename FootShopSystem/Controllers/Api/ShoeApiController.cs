namespace CarRentingSystem.Controllers.Api
{
    using FootShopSystem.Models.Api.Shoes;
    using FootShopSystem.Services.Shoes;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/shoes")]
    public class CarsApiController : ControllerBase
    {
        private readonly IShoeService shoes;

        public CarsApiController(IShoeService shoes)
            => this.shoes = shoes;

        [HttpGet]
        public ShoeQueryServiceModel All([FromQuery] AllShoesApiRequestModel query)
            => this.shoes.All(
                query.Brand,
                query.SearchTerm,
                query.CurrentPage,
                query.ShoesPerPage);
    }
}
