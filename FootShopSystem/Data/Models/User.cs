using Microsoft.AspNetCore.Identity;

namespace FootShopSystem.Data.Models
{
    public class User: IdentityUser
    {
        public string Fullname { get; set; }
    }
}
