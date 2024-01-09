using Admin.Entities;
using X.PagedList;

namespace Admin.Models
{
    public class ShopIndexVM
    {
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Color> Colors { get; set; }

        public IPagedList Products { get; set; }

    }
}