using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Admin.Areas.Admin.Models
{
    public class AccountSingInVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pasword is required")]
        [DataType(DataType.Password)]
        [MaxLength(255, ErrorMessage = "pasword can't be more than 255 characters")]
        public string Password { get; set; }

        [ValidateNever]
        public string ErrorMessage { get; set; }

        public bool RememberMe { get; set; }
    }
}
