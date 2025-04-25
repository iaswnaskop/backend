namespace backend.Entities
{
    public class StoreLanguage
    {
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
