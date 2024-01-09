namespace Admin.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public bool IsMain { get; set; }

        public bool IsHover { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
