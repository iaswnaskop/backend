namespace backend.Entities
{
    public class SuggestProduct
    {
        public int Id { get; set; }

        public int ProductDetailId { get; set; }
        public ProductDetail ProductDetail { get; set; } = null!;

        public int SuggestedProductDetailId { get; set; }
        public ProductDetail SuggestedProductDetail { get; set; } = null!;
    }
}
