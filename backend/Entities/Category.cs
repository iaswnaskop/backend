namespace backend.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();

        public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}
