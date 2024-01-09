using Admin.Entities;

namespace Admin.Models
{
    public class CartIndexVM
    {
        public List<Product> Products { get; set; }

        public List<BasketItem> BasketItems { get; set; }

    }
}
