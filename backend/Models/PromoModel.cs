namespace backend.Models
{
    public class PromoModel
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }
        public string PromoUrl { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public int? StoreId { get; set; }
    }
}
