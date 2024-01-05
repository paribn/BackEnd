namespace Admin.Entities
{
    public class ProductColor
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int ColorId { get; set; }
        public Color? Colors { get; set; }
    }
}
