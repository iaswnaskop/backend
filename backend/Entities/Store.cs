namespace backend.Entities
{
    public class Store
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
