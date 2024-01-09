using Admin.Entities;

namespace Admin.Models
{
    public class CheckoutIndexVM
    {
        public List<Product> Products { get; set; }

        public List<BasketItem> BasketItems { get; set; }

        public List<CartCheckout> Checkouts { get; set; }

        //public CartCheckout CartCheckout { get; set; }

    }
}
