namespace backend.Models
{
    public class UpdateStorePhotos
    {
        public IFormFile Logo { get; set; }
        public IFormFile BgPhoto { get; set; }
        public string? ImageURL { get; set; }
        public string? DesignBackgroundURL { get; set; }
    }
}
