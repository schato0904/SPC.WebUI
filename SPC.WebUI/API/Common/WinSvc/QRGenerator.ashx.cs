using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using DevExpress.BarCodes;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;

namespace SPC.WebUI.API.Common.WinSvc
{
    /// <summary>
    /// QRGenerator의 요약 설명입니다.
    /// </summary>
    public class QRGenerator : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string sSize = context.Request.QueryString["pSIZE"] ?? "57";
            int nSize = int.Parse(sSize);
            
            using (MemoryStream memoryImage = new MemoryStream())
            {
                BarCode barCode = new BarCode();
                barCode.Symbology = Symbology.QRCode;
                barCode.CodeText = context.Request.QueryString["pVAL"];
                barCode.BackColor = Color.White;
                barCode.ForeColor = Color.Black;
                barCode.RotationAngle = 0;
                barCode.Module = 1;
                barCode.DpiX = 72F;
                barCode.DpiY = 72F;
                barCode.AutoSize = true;
                barCode.CodeBinaryData = Encoding.UTF8.GetBytes(barCode.CodeText);
                barCode.Options.QRCode.CompactionMode = QRCodeCompactionMode.Byte;
                barCode.Options.QRCode.ErrorLevel = QRCodeErrorLevel.Q;
                barCode.Options.QRCode.ShowCodeText = false;

                barCode.Save(memoryImage, ImageFormat.Jpeg);
                memoryImage.Seek(0, SeekOrigin.Begin);

                Bitmap startBitmap = new Bitmap(memoryImage); // write CreateBitmapFromBytes  

                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(ImageToByteArray(RezizeImage(startBitmap, nSize, nSize)));
            }
        }

        private Bitmap RezizeImage(Bitmap startBitmap, int width, int height)
        {
            if (startBitmap.Height == height && startBitmap.Width == width) return startBitmap;

            Bitmap newBitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                graphics.Clear(Color.Transparent);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(startBitmap, new Rectangle(0, 0, width, height), new Rectangle(0, 0, startBitmap.Width, startBitmap.Height), GraphicsUnit.Pixel);
            }

            return newBitmap;
        }

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}