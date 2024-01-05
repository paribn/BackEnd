using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Admin.Areas.Admin.Models.ProductVM
{
    public class ProductAddVM
    {
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(3, ErrorMessage = "Name can't be less than 3 characters!")]
        [MaxLength(25, ErrorMessage = "Name can't be more than 25 characters!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Price can't be less than 0!")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Count is required!")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Price can't be less than 0!")]
        public int Count { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Photo is required!")]
        public IEnumerable<IFormFile> Photos { get; set; }

        public int ProductCategoryId { get; set; }
        public int ProductColorId { get; set; }

        public int ProductBrandId { get; set; }

        [ValidateNever]
        public List<SelectListItem> Category { get; set; }

        [ValidateNever]
        public List<SelectListItem> Brand { get; set; }

        [ValidateNever]
        public List<SelectListItem>? Color { get; set; }



    }
}
