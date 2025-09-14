using System.Drawing;

namespace backend.Services.CloudinaryServices
{
    public interface ICloudinaryService
    {

        Task<string> UploadPhotoAsync(IFormFile file, string afm);
        Task<List<string>> UploadPhotosAsync(List<IFormFile> files, string afm);
        Task<List<string>> UploadPromoPhotos(List<IFormFile> files, string afm);
        
        Task<string> UploadPhotoAsync(Bitmap qrCodeImage, string? afm);
        Task<string> UpdatePhoto(IFormFile image, string afm);

        Task<string> UploadProductPhoto(IFormFile file, string afm, int productId);
    }
}
