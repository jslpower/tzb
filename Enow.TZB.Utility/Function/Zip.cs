using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Enow.TZB.Utility
{
    /// <summary>
    /// 压缩、解压缩
    /// </summary>
    public class Zip
    {
        public static bool FileZip(List<string> filenames, string zipFileName)
        {
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFileName)))
                {
                    s.SetLevel(5); // 0 - store only to 9 - means best compression
                    byte[] buffer = new byte[4096];
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                    return true;
                }
        }
    }
}
