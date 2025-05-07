using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace backend.Services.CloudinaryServices
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var settings = config.Value;
            _cloudinary = new Cloudinary(new Account(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret
            ));
        }
        public Task<string> UploadPhotoAsync(IFormFile file, string afm)
        {
            throw new NotImplementedException();
        }
        public async Task<List<string>> UploadPhotosAsync(List<IFormFile> files, string afm)
        {
            var urls = new List<string>();

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                    continue;  // ή throw, ανάλογα τι θέλεις

                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = afm,
                    Transformation = new Transformation()
                        .FetchFormat("auto")
                        .Chain()
                };

                var result = await _cloudinary.UploadAsync(uploadParams);

                if (result.Error != null)
                    throw new ApplicationException($"Cloudinary upload error: {result.Error.Message}");

                urls.Add(result.SecureUrl.AbsoluteUri);
            }

            return urls;
        
        }
        public Task<bool> DeletePhotoAsync(string publicId)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeletePhotosAsync(List<string> publicIds)
        {
            throw new NotImplementedException();
        }
    }
   
}
