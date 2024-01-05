using System.ComponentModel.DataAnnotations;

namespace Admin.Areas.Admin.Models.CategoryVM
{
    public class CategoryAddVM
    {
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(3, ErrorMessage = "Name can't be less than 3 characters!")]
        [MaxLength(25, ErrorMessage = "Name can't be more than 25 characters!")]
        public string? Name { get; set; }

    }
}
