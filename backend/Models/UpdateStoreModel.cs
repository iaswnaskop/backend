namespace backend.Models
{
    public class UpdateStoreModel
    {
        //public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
        public required string  AFM { get; set; }
        public string? ImageURL { get; set; }
        public string? Email { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? TikTok { get; set; }
        public string? TripAdvisor { get; set; }
        public string? GoogleBusiness { get; set; }

        public string? PurchasingManager { get; set; }
        public string? WifiName { get; set; }
        public string? WifiPassword { get; set; }
        public string? StorePhone { get; set; }
        public string? QRCodeURL { get; set; }
        public bool? Cash { get; set; }
        public bool? Card { get; set; }
        public bool? PayPal { get; set; }
        public bool? BitCoin { get; set; }
        public bool? IRIS { get; set; }
        public bool? IsActive { get; set; }
        public List<int> Languages { get; set; } = new List<int>();
        //public ICollection<Schedule> Schedule { get; set; } = new List<Schedule>();
        public List<ScheduleModel> ScheduleModel { get; set; } = new List<ScheduleModel>();
        //public virtual ICollection<StoreLanguage> StoreLanguage { get; set; } = new List<StoreLanguage>();
       // public List<int> LanguageId { get; set; }
        //public int DesignModelId { get; set; }
        //public string? DesignColor { get; set; }
        //public string? DesignFont { get; set; }
        public IFormFile? Logo { get; set; }
        
       
    }

    public class ScheduleModel
    {
       
        public int DayOfWeek { get; set; }
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
        public bool IsClosed { get; set; } = false;
    }
}
