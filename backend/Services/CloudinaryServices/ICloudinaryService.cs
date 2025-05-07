namespace backend.Services.CloudinaryServices
{
    public interface ICloudinaryService
    {

        Task<string> UploadPhotoAsync(IFormFile file, string afm);
        Task<List<string>> UploadPhotosAsync(List<IFormFile> files, string afm);
        Task<bool> DeletePhotoAsync(string publicId);
        Task<bool> DeletePhotosAsync(List<string> publicIds);
    }
}
