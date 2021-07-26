namespace FootShopSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class User : IdentityUser
    {
        [MaxLength(UserNameMaxLength)]
        public string FullName { get; set; }
    }
}
