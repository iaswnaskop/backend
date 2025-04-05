namespace backend.Entities
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<StoreType> StoreTypes { get; set; } = new List<StoreType>();
    }
}
