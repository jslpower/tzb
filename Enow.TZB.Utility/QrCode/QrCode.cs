using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.IO;
using ZXing.QrCode;
using ZXing;
using System.Drawing;

namespace Enow.TZB.Utility
{
    /// <summary>
    /// 二维码生成
    /// </summary>
    public class QrCode
    {
        #region 生成二维码

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="data">生成的数据</param>
        /// <returns></returns>
        public static string CreateZxingCode(string data,string QrCodePath,int Width,int Height)
        {
            if (data.Length > 128)
            {
                return "内容不能超过128个字符";
            }
            var options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = Width,
                Height = Height
            };

            BarcodeWriter writer = null;
            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;

            Bitmap bitmap = writer.Write(data);

            string file = QrCodePath + DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            string path = QrCodePath + DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM");
            string filename = HttpContext.Current.Server.MapPath(file);
            string filepath = HttpContext.Current.Server.MapPath(path);

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            //保存图片
            bitmap.Save(filename);

            return file;
        }
        /// <summary>
        /// 生成二维码图片数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static byte[] CreateZxingCode(string data, int Width, int Height)
        {
            if (data.Length > 128)
            {
                return null;
            }
            var options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = Width,
                Height = Height
            };

            BarcodeWriter writer = null;
            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;

            Bitmap bitmap = writer.Write(data);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return stream.ToArray();
        }
        /// <summary>
        /// 输出二维码图片数据
        /// </summary>
        /// <param name="containsPage"></param>
        /// <param name="data"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public static void CreateZxingCodeResponse(string data, int Width, int Height)
        {
            if (data.Length > 128)
            {
                return;
            }
            var options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = Width,
                Height = Height
            };

            BarcodeWriter writer = null;
            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;

            Bitmap bitmap = writer.Write(data);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //输出图片
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "image/jpeg";
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());            
        }
        #endregion
    }
}
