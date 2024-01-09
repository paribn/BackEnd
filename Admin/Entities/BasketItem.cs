using Admin.Models;

namespace Admin.Entities
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public int Count { get; set; }

        public int CartId { get; set; }
        public Product Product { get; set; }
        public CartIndexVM cartIndex { get; set; }
    }
}
