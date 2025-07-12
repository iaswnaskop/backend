using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using static QRCoder.PayloadGenerator;
using System.Drawing;

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

        public async Task<string> UploadPhotoAsync(IFormFile file, string afm)
        {
            var url = string.Empty;
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

            url = result.SecureUrl.AbsoluteUri;

            return url;
        }

        public async Task<List<string>> UploadPhotosAsync(List<IFormFile> files, string afm)
        {
            var urls = new List<string>();

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                    continue;

                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream),
                    Folder = afm,
                    Transformation = new Transformation()
                        .FetchFormat("avif")
                        .Quality("auto") 
                        .Chain(),
                    DisplayName = file.Name,
                    PublicId = file.Name
                };

                var result = await _cloudinary.UploadAsync(uploadParams);

                if (result.Error != null)
                    throw new ApplicationException($"Cloudinary upload error: {result.Error.Message}");

                urls.Add(result.SecureUrl.AbsoluteUri);
            }

            return urls;
        }

        public async Task<string> UploadPhotoAsync(Bitmap qrCodeImage, string? afm)
        {
            using var stream = new MemoryStream();
            qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Position = 0;

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(afm, stream),
                Folder = afm,
                Transformation = new Transformation()
                    .FetchFormat("auto")
                    .Chain()
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
                throw new ApplicationException($"Cloudinary upload error: {result.Error.Message}");

            return result.SecureUrl.AbsoluteUri;
        }
    }
   
}
