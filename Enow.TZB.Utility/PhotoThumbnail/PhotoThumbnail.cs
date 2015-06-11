using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Web;

namespace Enow.TZB.Utility
{
    public class PhotoThumbnail
    {
        #region 图片裁剪相关方法
        /// <summary>
        /// 按指定宽高裁剪远程图片，返回文件相对路径
        /// </summary>
        /// <param name="filepath">远程图片路径</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        public static string F1(string filepath, int width, int height, string DIRPATH)
        {
            //string filepath1 = F2(filepath);
            //判断文件夹是否存在，不存在则创建文件夹
            if (!System.IO.File.Exists(HttpContext.Current.Server.MapPath(DIRPATH)))
            {
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(DIRPATH));
            }

            return F2(filepath, width, height, DIRPATH);
        }

        /// <summary>
        /// 下载远程图片，返回本地相对路径
        /// </summary>
        /// <param name="url">远程图片路径</param>
        /// <returns></returns>
        static string F2(string url, int width, int height, string DIRPATH)
        {
            if (string.IsNullOrEmpty(url)) return DIRPATH;
            string filename = System.IO.Path.GetFileName(url);
            if (string.IsNullOrEmpty(filename)) return DIRPATH;

            string filepath = DIRPATH + filename;
            string tofilepath = DIRPATH + System.IO.Path.GetFileNameWithoutExtension(filepath) + "_" + width + "_" + height + System.IO.Path.GetExtension(filepath);
            string destofilepath = HttpContext.Current.Server.MapPath(tofilepath);
            //如果缩略图文件不存在            
            if (System.IO.File.Exists(destofilepath))
            {
                return tofilepath;
            }
            else
            {

                string desfilepath = HttpContext.Current.Server.MapPath(filepath);
                string desdirpath = HttpContext.Current.Server.MapPath(DIRPATH);
                return F3(url, width, height, DIRPATH);
            }
        }

        /// <summary>
        /// 裁剪图片，返回裁剪后的文件路径
        /// </summary>
        /// <param name="filepath">文件相对路径</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        static string F3(string filepath, int width, int height, string DIRPATH)
        {
            if (string.IsNullOrEmpty(filepath)) return DIRPATH;

            string filename = System.IO.Path.GetFileName(filepath);
            if (string.IsNullOrEmpty(filename)) return DIRPATH;

            string desfilepath = HttpContext.Current.Server.MapPath(filepath);

            //  if (!System.IO.File.Exists(desfilepath)) return DIRPATH;

            string tofilepath = DIRPATH + System.IO.Path.GetFileNameWithoutExtension(filepath) + "_" + width + "_" + height + System.IO.Path.GetExtension(filepath);

            string destofilepath = HttpContext.Current.Server.MapPath(tofilepath);

            if (System.IO.File.Exists(destofilepath)) return tofilepath;

            Image img = null;

            try
            {
                img = System.Drawing.Image.FromFile(desfilepath);
            }
            catch
            {

            }

            if (img == null) return DIRPATH;

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = img.Width;
            int oh = img.Height;

            if (img.Width == width && img.Height == height)
            {
                return filepath;
            }

            if ((double)img.Width / (double)img.Height > (double)towidth / (double)toheight)
            {
                oh = img.Height;
                ow = img.Height * towidth / toheight;
                y = 0;
                x = (img.Width - ow) / 2;
            }
            else
            {
                ow = img.Width;
                oh = img.Width * height / towidth;
                x = 0;
                y = (img.Height - oh) / 2;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(img, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(destofilepath);
            }
            catch
            {
                tofilepath = DIRPATH;
            }
            finally
            {
                img.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

            return tofilepath;
        }
        #endregion
    }
}
