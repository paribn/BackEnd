using Admin.Entities;

namespace Admin.Models
{
    public class ShopIndexVM
    {
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Color> Colors { get; set; }

        public List<Product> Products { get; set; }
    }
}