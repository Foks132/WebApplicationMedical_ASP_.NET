using QRCoder;
using System.Drawing;
using ZXing;

namespace WebApplicationMedical.Models
{
    public class QrCode
    {

        /// <summary>
        /// Создаёт QR-код из входящей строки
        /// </summary>
        /// <param name="stringCode"></param>
        /// <returns>Строка base64</returns>
        public string CrateQrCode(string stringCode)
        {
            if (!string.IsNullOrWhiteSpace(stringCode))
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(stringCode, QRCodeGenerator.ECCLevel.L, true);
                PngByteQRCode pngByteQRCode = new PngByteQRCode(qRCodeData);

                byte[] qrBitmap = pngByteQRCode.GetGraphic(20);
                return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(qrBitmap));
            }
            return null;
        }

        public string ReadQrCode(IFormFile qrCode)
        {
            if (qrCode != null)
            {
                try
                {
                    MemoryStream memoryStream = new MemoryStream();
                    qrCode.CopyTo(memoryStream);
                    Bitmap qrCodeBitmap = new Bitmap(memoryStream);
                    BarcodeReader barcodeReader = new BarcodeReader();
                    Result result = barcodeReader.Decode(qrCodeBitmap);
                    if (result == null)
                    {
                        return null;
                    }
                    return result.ToString();

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return null;
        }
    }
}
