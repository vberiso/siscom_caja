using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZXing.QrCode;

namespace Gfd.Siscom.Web.HtmlTargets
{

    public class TagQRCode
    {
        public static string Qr(string value)
        {

            var width = 195; // width of the Qr Code
            var height = 195; // height of the Qr Code
            var margin = 2;

            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions { Height = height, Width = width, Margin = margin }
            };
            var pixelData = qrCodeWriter.Write(value);
            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGBC:\Users\OEM\Documents\Siscom\caja2\SOAPAP\Model\
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                    pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //output.TagName = "img";
                //output.Attributes.Clear();
                //output.Attributes.Add("width", width);
                //output.Attributes.Add("height", height);
                //output.Attributes.Add("alt", alt);
                //output.Attributes.Add("src",
                //String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
                //output.Attributes.Add("style", "margin-top: 7px; margin-bottom:-15px; margin-left:-10px;");
                return $@"<img width='{width}' height='{height}' src='{ String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray()))}' alt='qr' style = 'margin-top: 7px; margin-bottom:-15px; margin-left:-10px;') /> ";

            }
        }

        public static string barcode(string value)
        {


            var width = 450; // width of the Bar Code

            var height = 80; // height of the Bar Code
            var margin = 2;

            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.CODE_128,
                Options = new QrCodeEncodingOptions { Height = height, Width = width, Margin = margin }
            };
            var pixelData = qrCodeWriter.Write(value.PadLeft(13, '0'));
            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                    pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //output.TagName = "img";
                //output.Attributes.Clear();
                //output.Attributes.Add("width", width);
                //output.Attributes.Add("height", height);
                //output.Attributes.Add("alt", alt);
                //output.Attributes.Add("src",
                //String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
                //output.Attributes.Add("style", "margin-top: -10px; margin-bottom:-127px; display: inline-block;");
                return $@"<img width='{width}' height='{height}' src='{ String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray()))}' alt='qr' style = 'margin-top: 7px; margin-bottom:-15px; margin-left:-10px;') /> ";

            }



        }
    }
}




    
