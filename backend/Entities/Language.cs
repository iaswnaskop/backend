namespace backend.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string CountryLang { get; set; }
        public string Abbr { get; set; }
        public virtual ICollection<StoreLanguage> StoreLanguages { get; set; } = new List<StoreLanguage>();
    }
}

