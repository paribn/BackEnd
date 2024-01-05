using Admin.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Admin.Areas.Admin.Models.ProductVM
{
    public class ProductUpdateVM
    {

        [Required(ErrorMessage = "Something went wrong!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MinLength(5, ErrorMessage = "Name can't be less than 5 characters!")]
        [MaxLength(10, ErrorMessage = "Name can't be more than 10 characters!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Price can't be less than 0!")]
        public decimal? Price { get; set; }


        [Required(ErrorMessage = "Count is required!")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Price can't be less than 0!")]
        public int Count { get; set; }

        [ValidateNever]
        public string Description { get; set; }

        [ValidateNever]
        public string? ImageName { get; set; }


        [Required(ErrorMessage = "Photo is required!")]
        public IEnumerable<IFormFile> Photos { get; set; }

        public IEnumerable<ProductImage> ProductImages { get; set; }

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
