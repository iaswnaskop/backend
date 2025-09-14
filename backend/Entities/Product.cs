namespace backend.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public string? NameEng { get; set; }
        public string? NameDu { get; set; }
        public string? NameFr { get; set; }
        public string? NameIt { get; set; }
        public string? NameEs { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();
    }
}
