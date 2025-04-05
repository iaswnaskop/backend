namespace backend.Entities
{
    public class ProductDetail
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        public string? NameEng { get; set; }

        public string? DescriptionEng { get; set; }
        public string? NameDu { get; set; }

        public string? DescriptionDu { get; set; }
        public string? NameFr { get; set; }

        public string? DescriptionFr { get; set; }
        public string? NameIt { get; set; }

        public string? DescriptionIt { get; set; }
        public string? NameEs { get; set; }

        public string? DescriptionEs { get; set; }

        public string? ImageUrl { get; set; }

        public double Price { get; set; }

        public bool Available { get; set; }

        public int StoreId { get; set; }

        public int ProductId { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        public virtual Product Product { get; set; } = null!;

        public virtual Store Store { get; set; } = null!;
    }
}
