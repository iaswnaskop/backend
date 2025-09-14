namespace backend.Entities
{
    public class Promo
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? PromoUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; } = null!;
    }
}
