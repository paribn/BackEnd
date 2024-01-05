using System.ComponentModel.DataAnnotations;

namespace Admin.Areas.Admin.Models.BrandVM
{
    public class BrandAddVM
    {
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(3, ErrorMessage = "Name can't be less than 3 characters!")]
        [MaxLength(15, ErrorMessage = "Name can't be more than 15 characters!")]
        public string Name { get; set; }

    }
}
