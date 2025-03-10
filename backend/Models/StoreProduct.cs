namespace backend.Models
{
    public class StoreProduct
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public virtual Stores Store { get; set; }
        public virtual Products Product { get; set; }  
    }
}
