using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootShopSystem.Models.Designers
{
    public class BecomeDesignerFormModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 2)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
