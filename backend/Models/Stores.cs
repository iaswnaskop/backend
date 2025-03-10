namespace backend.Models
{
    public class Stores
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public ICollection<StoreProduct> StoreProducts { get; set; } = new List<StoreProduct>();
    }
}
