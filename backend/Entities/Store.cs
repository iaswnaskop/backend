namespace backend.Entities
{
    public class Store
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
        public  string AFM { get; set; }
        public  string ImageURL { get; set; }
        public string? Email { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? TikTok { get; set; }
        public string? TripAdvisor { get; set; }
        public string? GoogleBusiness { get; set; }
        public int DesignId { get; set; }
        public string? PurchasingManager { get; set; }
        public string? WifiName { get; set; }
        public string? WifiPassword { get; set; }
        public string? StorePhone { get; set; }
        public bool Cash { get; set; }
        public bool Card { get; set; }
        public bool PayPal { get; set; }
        public bool BitCoin { get; set; }
        public bool IRIS { get; set; }

        public bool IsActive { get; set; } = true;
        //public virtual Design Design { get; set; } = null!;
        public string? QRCodeURL { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<StoreType> StoreTypes { get; set; } = new List<StoreType>();
        public ICollection<Design> Design { get; set; } = new List<Design>();
        public ICollection<StoreLanguage> StoreLanguages { get; set; } = new List<StoreLanguage>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public List<Promo> Promos { get; internal set; }
    }
}
