using backend.Services.QrCodeServices;
using QRCoder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class QrCodeService : IQrCodeService
{
    public async Task<Bitmap> GenerateQrCode(string dynamicUrl)
    {
        using var qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(dynamicUrl, QRCodeGenerator.ECCLevel.Q);

        using var qrCode = new QRCoder.PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qrCode.GetGraphic(20);

        Bitmap? qrCodeImage = ConvertByteArrayToBitmap(qrCodeBytes);
        if (qrCodeImage == null)
        {
            throw new InvalidOperationException("Failed to generate QR code image.");
        }

        qrCodeImage = ConvertToNonIndexedFormat(qrCodeImage);

        // Προσθήκη λογότυπου στο κέντρο με τετράγωνο background
        Bitmap logoImage = await DownloadImageAsync("https://res.cloudinary.com/dixh4rsqv/image/upload/v1747996182/loudlink-logo-qr_bnachg.png");
        if (logoImage != null)
        {
            int logoHeight = qrCodeImage.Height / 5;
            int logoSize = qrCodeImage.Width / 5;
            int logoX = (qrCodeImage.Width - logoSize) / 2;
            int logoY = (qrCodeImage.Height - logoHeight) / 2;

            using Graphics graphics = Graphics.FromImage(qrCodeImage);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. Σχεδίαση λευκού background πίσω από το logo
            using Brush whiteBrush = new SolidBrush(Color.White);
            graphics.FillRectangle(whiteBrush, logoX, logoY, logoSize, logoHeight);

            // 2. Σχεδίαση του logo πάνω στο background
            graphics.DrawImage(logoImage, logoX, logoY, logoSize, logoHeight);
        }

        return qrCodeImage;
    }


    private async Task<Bitmap?> DownloadImageAsync(string imageUrl)
    {
        try
        {
            using var client = new HttpClient();
            var imageBytes = await client.GetByteArrayAsync(imageUrl);
            using var memoryStream = new MemoryStream(imageBytes);
            return new Bitmap(memoryStream);
        }
        catch
        {
            return null;
        }
    }

    private Bitmap? ConvertByteArrayToBitmap(byte[] imageBytes)
    {
        try
        {
            using var memoryStream = new MemoryStream(imageBytes);
            return new Bitmap(memoryStream);
        }
        catch
        {
            return null;
        }
    }

    private Bitmap ConvertToNonIndexedFormat(Bitmap sourceBitmap)
    {
        var newBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, PixelFormat.Format32bppArgb);
        using (var graphics = Graphics.FromImage(newBitmap))
        {
            graphics.DrawImage(sourceBitmap, 0, 0);
        }
        return newBitmap;
    }
}
