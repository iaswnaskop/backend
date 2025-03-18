namespace backend.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();
    }
}
