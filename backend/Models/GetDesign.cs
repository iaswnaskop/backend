namespace backend.Models
{
    public class GetDesign
    {
        public int Id { get; set; }
        public string? BgColor { get; set; }
        public string? BgURL { get; set; }
        public string? Font { get; set; }
        public virtual DesignModel DesignModel { get; set; } = null!;
    }
}
