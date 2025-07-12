using QRCoder;
using System.Drawing;

namespace backend.Services.QrCodeServices
{
    public interface IQrCodeService
    {
        Task<Bitmap> GenerateQrCode(string dynamicUrl);
    }
}
