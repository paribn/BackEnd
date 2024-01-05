﻿using System.ComponentModel.DataAnnotations;

namespace Admin.Areas.Admin.Models.BrandVM
{
    public class BrandUpdateVM
    {
        [Required(ErrorMessage = "Something went wrong!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MinLength(3, ErrorMessage = "Name can't be less than 3 characters!")]
        [MaxLength(15, ErrorMessage = "Name can't be more than 15 characters!")]
        public string Name { get; set; }
    }
}
