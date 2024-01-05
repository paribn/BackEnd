using System.ComponentModel.DataAnnotations;

namespace Admin.Areas.Admin.Models.ColorVM
{
    public class ColorUpdateVM
    {

        [Required(ErrorMessage = "Something went wrong!")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

    }
}
