using System.IO;
using PdfSharp.Drawing;   
using PdfSharp.Pdf;      
using QRCoder;

namespace BLL
{
    public class PdfGeneratorService
    {
        public byte[] GeneratePrescriptionPdf(string pacjent, string zalecenia, string qrText)
        {

            var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);


            var pngQr = new PngByteQRCode(qrData);
            byte[] qrPngBytes = pngQr.GetGraphic(
                pixelsPerModule: 10,           
                darkColorRgba: new byte[] { 0, 0, 0, 255 },      
                lightColorRgba: new byte[] { 255, 255, 255, 255 }, 
                drawQuietZones: true
            );


            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);


            var font = new XFont("Arial", 14); 


            gfx.DrawString($"Recepta dla: {pacjent}", font, XBrushes.Black, new XPoint(50, 50));
            gfx.DrawString($"Zalecenia: {zalecenia}", font, XBrushes.Black, new XPoint(50, 100));
            gfx.DrawString("Kod QR:", font, XBrushes.Black, new XPoint(50, 150));

         
            using (var ms = new MemoryStream(qrPngBytes))
            {
                var img = XImage.FromStream(ms);
                gfx.DrawImage(img, 50, 180, 150, 150);
            }

           
            using var outStream = new MemoryStream();
            document.Save(outStream, false);
            return outStream.ToArray();
        }
    }
}
