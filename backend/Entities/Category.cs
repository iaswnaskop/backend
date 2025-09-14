namespace backend.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? NameEng { get; set; } = null!;
        public string? DescriptionEng { get; set; } = null!;
        public string? NameDu { get; set; } = null!;
        public string? DescriptionDu { get; set; } = null!;
        public string? NameFr { get; set; } = null!;
        public string? DescriptionFr { get; set; } = null!;
        public string? NameIt { get; set; } = null!;
        public string? DescriptionIt { get; set; } = null!;
        public string? NameEs { get; set; } = null!;
        public string? DescriptionEs { get; set; } = null!;

        public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();

        public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}
