using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootShopSystem.Models.Shoes
{
    public class ListingShoeViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
