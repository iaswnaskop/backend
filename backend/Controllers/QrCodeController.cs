using backend.Services.CloudinaryServices;
using backend.Services.StoreServices;
using backend.Services.QrCodeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace backend.Controllers
{
    //[Authorize]
    [Route("api/")]
    [ApiController]
    public class QrCodeController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IQrCodeService _qrCodeService;
        public QrCodeController(IStoreService storeService, ICloudinaryService cloudinaryService, IQrCodeService qrCodeService)
        {
            _storeService = storeService;
            _cloudinaryService = cloudinaryService;
            _qrCodeService = qrCodeService;
        }
        [HttpGet("generate-qr/{storeId}")]
        public async Task<ActionResult<string>> GenerateQr(int storeId)
        {
            var storeInfo = await _storeService.GetStoreByStoreId(storeId);
            if (storeInfo == null)
            {
                return NotFound("Store not found");
            }

            // Δημιουργία δυναμικού URL
            // string cleanStoreName = storeInfo.Name.Replace(" ", "_");
            //string encodedId = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(storeInfo.Id.ToString()));
            //string dynamicUrl = $"https://example.com/store/{cleanStoreName}?data={encodedId}";


            string dynamicUrl = $"https://www.google.com/";
            var qrCodeImage = await _qrCodeService.GenerateQrCode(dynamicUrl);
            if (qrCodeImage == null)
            {
                return BadRequest("Failed to generate QR code");
            }

            var qrCodeURL = await _cloudinaryService.UploadPhotoAsync(qrCodeImage, storeInfo.AFM);
            return qrCodeURL;
        }
    }
}
