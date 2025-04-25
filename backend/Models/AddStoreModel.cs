namespace backend.Models
{
    public class AddStoreModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
        public string AFM { get; set; }
        public string ImageURL { get; set; }
        public string? Email { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? TikTok { get; set; }
        public string? TripAdvisor { get; set; }
        public string? GoogleBusiness { get; set; }
        public List<int> TypeId { get; set; }
        public List<int> LanguageId { get; set; }
    }
}
