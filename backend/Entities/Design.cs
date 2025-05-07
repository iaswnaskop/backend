namespace backend.Entities
{
    public class Design
    {
        public int Id { get; set; }
        public string? BgColor { get; set; }
        public string? BgURL { get; set; }
        public string? Font { get; set; }
        public int DesignModelId { get; set; }
        public virtual DesignModel DesignModel { get; set; } = null!;

    }
}
