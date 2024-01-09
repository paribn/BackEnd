using Admin.Entities;

namespace Admin.Controllers
{
    public class OrderIndexVM
    {
        public List<Product> Products { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
