namespace backend.Entities
{
    public class StoreType
    {
        public int StoreId { get; set; }
        public Store Store { get; set; } = null!;
        public int TypeId { get; set; }
        public Type Type { get; set; } = null!;
    }
}
