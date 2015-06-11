using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Enow.TZB.Utility
{
    public class Log
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="s">内容</param>
        /// <param name="path">相对路径，确保目录实际存在</param>
        public static void WLog(string s, string path)
        {
            string fPath = Utils.GetMapPath(path);
            string extension = System.IO.Path.GetExtension(fPath);
            if (!string.IsNullOrEmpty(extension))
            {
                fPath = fPath.Substring(0, fPath.LastIndexOf(extension)) + "." + DateTime.Today.ToString("yyyyMMdd") + extension;
            }
            if (!File.Exists(fPath))
            {
                FileStream fs = File.Create(fPath);
                fs.Close();
            }
            try
            {
                var sw = new StreamWriter(fPath, true, System.Text.Encoding.UTF8);
                sw.Write(DateTime.Now.ToString("o") + "\t" + s + "\r\n");
                sw.Close();
            }
            catch { }
        }
    }
}
