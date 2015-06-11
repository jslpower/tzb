using System;
using System.Text;
using System.Security.Cryptography;

namespace Enow.TZB.Utility
{
    /// <summary>
    /// 加密模块
    /// Author:陈志仁 2007-11-06 
    /// </summary>
    public class HashCrypto : IDisposable, IHashCrypto
    {
        private bool _IsDispose = false;
        #region 初始化
        public HashCrypto()
        { }
        /// <summary>
        /// 垃圾回收
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._IsDispose)
            {
                if (disposing)
                {
                    GC.Collect();
                }
            }
            _IsDispose = true;

        }
        ~HashCrypto()
        {
            Dispose(false);
        }
        #endregion

        #region 常量和变量

        private string _Key = null;
        private string _IV = null;

        #endregion

        #region 构造函数
        /// <summary>
        /// 加密 密钥
        /// </summary>
        public string Key
        {
            set { _Key = value; }
            get { return _Key; }
        }
        /// <summary>
        /// 初始向量
        /// </summary>
        public string IV
        {
            set { _IV = value; }
            get { return _IV; }
        }
        #endregion

        #region 全局方法
        #region MD5

        /// <summary>
        /// 32位MD5 加密
        /// </summary>
        /// <param name="inputString">要加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public string MD5Encrypt(string inputString)
        {
            if (inputString == null || inputString == "")
            {
                throw new Exception("要加密的字符串不能为空");
            }
            try
            {
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
                return BitConverter.ToString(hashMD5.ComputeHash(Encoding.Default.GetBytes(inputString))).Replace("-", "").ToLower();
            }
            catch (Exception e)
            {
                throw new Exception("加密过程中发生错误:" + e.Message);
            }

        }
        /// <summary>
        /// 16位MD5 加密
        /// </summary>
        /// <param name="inputString">要加密的字符串</param>
        /// <param name="hashLength">加密长度</param>
        /// <returns>返回加密后的字符串</returns>
        public string MD5Encrypt16(string inputString)
        {
            if (inputString == null || inputString == "")
            {
                throw new Exception("要加密的字符串不能为空");
            }
            return MD5Encrypt(inputString).Substring(8, 16);
        }
        #endregion
        #region SHA
        /// <summary>
        /// SHA算法，默认为SHA1
        /// </summary>
        /// <param name="inputString">要加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public string SHAEncrypt(string inputString)
        {
            if (inputString == null || inputString == "")
            {
                throw new Exception("要加密的字符串不能为空");
            }
            try
            {
                SHA1CryptoServiceProvider hashSHA = new SHA1CryptoServiceProvider();
                return BitConverter.ToString(hashSHA.ComputeHash(Encoding.Default.GetBytes(inputString))).Replace("-", "").ToLower();
            }
            catch (Exception e)
            {
                throw new Exception("加密过程中发生错误:" + e.Message);
            }
        }
        /// <summary>
        /// 重载SHA算法 默认 256
        /// </summary>
        /// <param name="inputString">要加密的字符串</param>
        /// <param name="HashLength">加密长度 可分为 128,256,384,512 这几种长度</param>
        /// <returns>返回加密后的字符串</returns>
        public string SHAEncrypt(string inputString, int HashLength)
        {
            try
            {
                switch (HashLength)
                {
                    case 128:
                        return SHAEncrypt(inputString);
                    case 384:
                        SHA384Managed hashSHA384 = new SHA384Managed();
                        return BitConverter.ToString(hashSHA384.ComputeHash(Encoding.Default.GetBytes(inputString))).Replace("-", "").ToLower();
                    case 512:
                        SHA512Managed hashSHA512 = new SHA512Managed();
                        return BitConverter.ToString(hashSHA512.ComputeHash(Encoding.Default.GetBytes(inputString))).Replace("-", "").ToLower();
                    default:
                        SHA256Managed hashSHA256 = new SHA256Managed();
                        return BitConverter.ToString(hashSHA256.ComputeHash(Encoding.Default.GetBytes(inputString))).Replace("-", "").ToLower();
                }
            }
            catch (Exception e)
            {
                throw new Exception("SHA加密过程中发生错误:" + e.Message);
            }
        }
        #endregion
        #region DES
        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="Values">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string DESEncrypt(string Values)
        {
            try
            {
                DESCryptoServiceProvider DesHash = new DESCryptoServiceProvider();
                DesHash.Mode = CipherMode.CBC;
                DesHash.Padding = PaddingMode.PKCS7;
                if (String.IsNullOrEmpty(this._Key))
                {
                    DesHash.GenerateKey();
                    _Key = Encoding.ASCII.GetString(DesHash.Key);
                }
                else
                {
                    if (_Key.Length > 8)
                    {
                        _Key = _Key.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _Key.Length; i--)
                            _Key += "0";
                    }
                }
                if (String.IsNullOrEmpty(this._IV))
                {
                    DesHash.GenerateIV();
                    _IV = Encoding.ASCII.GetString(DesHash.IV);
                }
                else
                {
                    if (_IV.Length > 8)
                    {
                        _IV = _IV.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _IV.Length; i--)
                            _IV += "0";
                    }
                }
                DesHash.Key = Encoding.ASCII.GetBytes(this._Key);
                DesHash.IV = Encoding.ASCII.GetBytes(this._IV);
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(Values);
                ICryptoTransform cTransform = DesHash.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in resultArray)
                {
                    sb.AppendFormat("{0:X2}", b);
                }
                return sb.ToString();
                /*
                  byte[] byt = Encoding.UTF8.GetBytes(Values);
                ICryptoTransform ct = DesHash.CreateEncryptor(Encoding.UTF8.GetBytes(this._Key), Encoding.UTF8.GetBytes(this._IV));
                DesHash.Clear();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return  Convert.ToBase64String(ms.ToArray());
                 */
            }
            catch (Exception e)
            {
                throw new Exception("加密数据出错,详细原因：" + e.Message);
            }
        }
        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="Values">加密后的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string DeDESEncrypt(string Values)
        {
            try
            {
                DESCryptoServiceProvider DesHash = new DESCryptoServiceProvider();
                DesHash.Mode = CipherMode.CBC;
                DesHash.Padding = PaddingMode.PKCS7;
                if (String.IsNullOrEmpty(this._Key))
                {
                    DesHash.GenerateKey();
                    _Key = Encoding.ASCII.GetString(DesHash.Key);
                }
                else
                {
                    if (_Key.Length > 8)
                    {
                        _Key = _Key.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _Key.Length; i--)
                            _Key += "0";
                    }
                }
                if (String.IsNullOrEmpty(this._IV))
                {
                    DesHash.GenerateIV();
                    _IV = Encoding.ASCII.GetString(DesHash.IV);
                }
                else
                {
                    if (_IV.Length > 8)
                    {
                        _IV = _IV.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _IV.Length; i--)
                            _IV += "0";
                    }
                }
                DesHash.Key = Encoding.ASCII.GetBytes(this._Key);
                DesHash.IV = Encoding.ASCII.GetBytes(this._IV);

                int halfInputLength = Values.Length / 2;
                byte[] toEncryptArray = new byte[halfInputLength];
                for (int x = 0; x < halfInputLength; x++)
                {
                    int i = (Convert.ToInt32(Values.Substring(x * 2, 2), 16));
                    toEncryptArray[x] = (byte)i;
                }

                ICryptoTransform ct = DesHash.CreateDecryptor();
                byte[] resultArray = ct.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                DesHash.Clear();
                /*
                byte[] buffer = Convert.FromBase64String(Values);
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
                System.IO.StreamReader sw = new System.IO.StreamReader(cs);
                return sw.ReadToEnd();
                 * */
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception e)
            {
                throw new Exception("解密数据出错,详细原因：" + e.Message);
            }
        }
        #endregion
        #region 3 DES(tripleDES)
        /// <summary>
        /// 24位Key 3 DES数据加密
        /// </summary>
        /// <param name="Values">加密后的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string TripleDesEncrypt(string Values)
        {
            try
            {
                TripleDES des3 = new TripleDESCryptoServiceProvider();
                des3.Mode = CipherMode.CBC;
                des3.Padding = PaddingMode.PKCS7;
                //Key 的长充必须为 16或24字节
                if (String.IsNullOrEmpty(this._Key))
                {
                    des3.GenerateKey();
                    _Key = Encoding.ASCII.GetString(des3.Key);
                }
                else
                {
                    if (_Key.Length > 24)
                    {
                        _Key = _Key.Substring(0, 24);
                    }
                    else
                    {
                        for (int i = 24; i < _Key.Length; i--)
                            _Key += "0";
                    }
                    //if (_Key.Length != 16 && _Key.Length != 24)
                    //    throw new Exception("加密数据出错,详细原因：Key的长度不为 16 或 24 byte.");
                }
                if (String.IsNullOrEmpty(this._IV))
                {
                    des3.GenerateIV();
                    _IV = Encoding.ASCII.GetString(des3.IV);
                }
                else
                {
                    if (_IV.Length > 8)
                    {
                        _IV = _IV.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _IV.Length; i--)
                            _IV += "0";
                    }
                    //if (_IV.Length != 8)
                    //    throw new Exception("加密数据出错,详细原因：IV的长度不为 8 byte.");
                }

                des3.Key = Encoding.ASCII.GetBytes(this._Key);
                des3.IV = Encoding.ASCII.GetBytes(this._IV);
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(Values);
                ICryptoTransform ct = des3.CreateEncryptor();
                byte[] resultArray = ct.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in resultArray)
                {
                    sb.AppendFormat("{0:X2}", b);
                }
                des3.Clear();
                return sb.ToString();
                /*
                byte[] byt = Encoding.UTF8.GetBytes(Values);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Convert.ToBase64String(ms.ToArray());
                 * */
            }
            catch (Exception e)
            {
                throw new Exception("加密数据出错,详细原因：" + e.Message);
            }
        }
        /// <summary>
        /// 24位 3 DES 解密
        /// </summary>
        /// <param name="Values">加密后的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string DeTripleDesEncrypt(string Values)
        {
            try
            {                
                TripleDES des3 = new TripleDESCryptoServiceProvider();
                des3.Mode = CipherMode.CBC;
                des3.Padding = PaddingMode.PKCS7;
                //Key 的长充必须为 16或24字节
                if (String.IsNullOrEmpty(this._Key))
                {
                    des3.GenerateKey();
                    _Key = Encoding.ASCII.GetString(des3.Key);
                }
                else
                {
                    if (_Key.Length > 24)
                    {
                        _Key = _Key.Substring(0, 24);
                    }
                    else
                    {
                        for (int i = 24; i < _Key.Length; i--)
                            _Key += "0";
                    }
                    //if (_Key.Length != 16 && _Key.Length != 24)
                    //    throw new Exception("加密数据出错,详细原因：Key的长度不为 16 或 24 byte.");
                }
                if (String.IsNullOrEmpty(this._IV))
                {
                    des3.GenerateIV();
                    _IV = Encoding.ASCII.GetString(des3.IV);
                }
                else
                {
                    if (_IV.Length > 8)
                    {
                        _IV = _IV.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _IV.Length; i--)
                            _IV += "0";
                    }
                    //if (_IV.Length != 8)
                    //    throw new Exception("加密数据出错,详细原因：IV的长度不为 8 byte.");
                }
                des3.Key = Encoding.ASCII.GetBytes(this._Key);
                des3.IV = Encoding.ASCII.GetBytes(this._IV);
                ICryptoTransform ct = des3.CreateDecryptor();

                int halfInputLength = Values.Length / 2;
                byte[] toEncryptArray = new byte[halfInputLength];
                for (int x = 0; x < halfInputLength; x++)
                {
                    int i = (Convert.ToInt32(Values.Substring(x * 2, 2), 16));
                    toEncryptArray[x] = (byte)i;
                }

                byte[] resultArray = ct.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                des3.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
                /*
                byte[] buffer = Convert.FromBase64String(Values);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
                ICryptoTransform ct = des3.CreateDecryptor(Encoding.ASCII.GetBytes(this._Key), Encoding.ASCII.GetBytes(this._IV));
                des3.Clear();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
                System.IO.StreamReader sw = new System.IO.StreamReader(cs);
                return sw.ReadToEnd();
                 * */
            }
            catch (Exception e)
            {
                throw new Exception("解密数据出错,详细原因：" + e.Message);
            }
        }
        /// <summary>
        /// 16位Key 3 DES数据加密
        /// </summary>
        /// <param name="Values">加密后的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string ShortKeyTripleDesEncrypt(string Values)
        {
            try
            {
                TripleDES des3 = new TripleDESCryptoServiceProvider();
                des3.Mode = CipherMode.CBC;
                byte[] byt;
                //Key 的长充必须为 16或24字节
                if (null == this._Key || String.Empty == this._Key)
                {
                    des3.GenerateKey();
                    _Key = Encoding.ASCII.GetString(des3.Key);
                }
                else
                {
                    if (_Key.Length > 16)
                    {
                        _Key = _Key.Substring(0, 16);
                    }
                    else
                    {
                        for (int i = 16; i < _Key.Length; i--)
                            _Key += "0";
                    }
                    //if (_Key.Length != 16 && _Key.Length != 24)
                    //    throw new Exception("加密数据出错,详细原因：Key的长度不为 16 或 24 byte.");
                }
                if (null == this._IV || String.Empty == this._IV)
                {
                    des3.GenerateIV();
                    _IV = Encoding.ASCII.GetString(des3.IV);
                }
                else
                {
                    if (_IV.Length > 8)
                    {
                        _IV = _IV.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _IV.Length; i--)
                            _IV += "0";
                    }
                    //if (_IV.Length != 8)
                    //    throw new Exception("加密数据出错,详细原因：IV的长度不为 8 byte.");
                }
                //return _Key + "：" + Encoding.ASCII.GetBytes(_Key).Length.ToString() + "<br>"+ _IV + "：" + Encoding.ASCII.GetBytes(this._IV).Length.ToString();
                byt = Encoding.UTF8.GetBytes(Values);
                ICryptoTransform ct = des3.CreateEncryptor(Encoding.ASCII.GetBytes(this._Key), Encoding.ASCII.GetBytes(this._IV));
                des3.Clear();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                throw new Exception("加密数据出错,详细原因：" + e.Message);
            }
        }
        /// <summary>
        /// 16位 3 DES 解密
        /// </summary>
        /// <param name="Values">加密后的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string ShortKeyDeTripleDesEncrypt(string Values)
        {
            try
            {
                byte[] buffer = Convert.FromBase64String(Values);
                TripleDES des3 = new TripleDESCryptoServiceProvider();
                des3.Mode = CipherMode.CBC;
                //Key 的长充必须为 16或24字节
                if (null == this._Key || String.Empty == this._Key)
                {
                    des3.GenerateKey();
                    _Key = Encoding.ASCII.GetString(des3.Key);
                }
                else
                {
                    if (_Key.Length > 16)
                    {
                        _Key = _Key.Substring(0, 16);
                    }
                    else
                    {
                        for (int i = 16; i < _Key.Length; i--)
                            _Key += "0";
                    }
                    //if (_Key.Length != 16 && _Key.Length != 24)
                    //    throw new Exception("加密数据出错,详细原因：Key的长度不为 16 或 24 byte.");
                }
                if (null == this._IV || String.Empty == this._IV)
                {
                    des3.GenerateIV();
                    _IV = Encoding.ASCII.GetString(des3.IV);
                }
                else
                {
                    if (_IV.Length > 8)
                    {
                        _IV = _IV.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _IV.Length; i--)
                            _IV += "0";
                    }
                    //if (_IV.Length != 8)
                    //    throw new Exception("加密数据出错,详细原因：IV的长度不为 8 byte.");
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
                ICryptoTransform ct = des3.CreateDecryptor(Encoding.ASCII.GetBytes(this._Key), Encoding.ASCII.GetBytes(this._IV));
                des3.Clear();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
                System.IO.StreamReader sw = new System.IO.StreamReader(cs);
                return sw.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("解密数据出错,详细原因：" + e.Message);
            }
        }
        #endregion
        #region Rijndael
        /// <summary>
        /// Rijndael数据加密 Key 为 32字节, IV 为 16字节
        /// </summary>
        /// <param name="Values">加密后的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string RijndaelEncrypt(string Values)
        {
            try
            {
                Rijndael rijndael = new RijndaelManaged();
                rijndael.Mode = CipherMode.CBC;
                byte[] byt;
                //Key 和 IV 为 16或32字节
                if (null == this._Key)
                {
                    rijndael.GenerateKey();
                    _Key = Encoding.ASCII.GetString(rijndael.Key);
                }
                else
                {
                    if (_Key.Length > 32)
                    {
                        _Key = _Key.Substring(0, 32);
                    }
                    else
                    {
                        for (int i = 32; i < _Key.Length; i--)
                            _Key += "0";
                    }
                    //if (_Key.Length != 32)
                    //    throw new Exception("加密数据出错,详细原因：Key的长度不为 32 byte.");
                }
                if (null == this._IV)
                {
                    rijndael.GenerateIV();
                    _IV = Encoding.ASCII.GetString(rijndael.IV);
                }
                else
                {
                    if (_IV.Length > 16)
                    {
                        _IV = _IV.Substring(0, 16);
                    }
                    else
                    {
                        for (int i = 16; i < _IV.Length; i--)
                            _IV += "0";
                    }
                    //if (_IV.Length != 16)
                    //    throw new Exception("加密数据出错,详细原因：IV的长度不为 16 byte.");
                }
                //return _Key + "：" + Encoding.ASCII.GetBytes(_Key).Length.ToString() + "<br>"+ _IV + "：" + Encoding.ASCII.GetBytes(this._IV).Length.ToString();
                byt = Encoding.UTF8.GetBytes(Values);
                ICryptoTransform ct = rijndael.CreateEncryptor(Encoding.ASCII.GetBytes(this._Key), Encoding.ASCII.GetBytes(this._IV));
                rijndael.Clear();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                throw new Exception("加密数据出错,详细原因：" + e.Message);
            }
        }
        /// <summary>
        /// Rijndael数据加密 Key 为 32字节, IV 为 16字节
        /// </summary>
        /// <param name="Values">加密后的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string RijndaelEncryptForUrl(string Values)
        {
            string str = RijndaelEncrypt(Values);
            string NewBase64Str = str.Replace("+", "*").Replace("/", "-").Substring(0,str.Length-2);
            return NewBase64Str;
        }
        /// <summary>
        /// Rijndael数据加密 Key 为 32字节, IV 为 16字节
        /// </summary>
        /// <param name="Values">加密后的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string DeRijndaelEncryptForUrl(string Values)
        {
            if (!string.IsNullOrEmpty(Values))
            {
                string Base64Str = Values.Replace("*", "+").Replace("-", "/")+"==";
                return DeRijndaelEncrypt(Base64Str);
            }
            else {
                return string.Empty;
            }
        }
        /// <summary>
        /// Rijndael 解密
        /// </summary>
        /// <param name="Values">加密后的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string DeRijndaelEncrypt(string Values)
        {
            try
            {
                byte[] buffer = Convert.FromBase64String(Values);
                Rijndael rijndael = new RijndaelManaged();
                rijndael.Mode = CipherMode.CBC;
                if (null == this._Key)
                {
                    rijndael.GenerateKey();
                    _Key = Encoding.ASCII.GetString(rijndael.Key);
                }
                else
                {
                    if (_Key.Length > 32)
                    {
                        _Key = _Key.Substring(0, 32);
                    }
                    else
                    {
                        for (int i = 32; i < _Key.Length; i--)
                            _Key += "0";
                    }
                    //if (_Key.Length != 32)
                    //    throw new Exception("加密数据出错,详细原因：Key的长度不为 32 byte.");
                }
                if (null == this._IV)
                {
                    rijndael.GenerateIV();
                    _IV = Encoding.ASCII.GetString(rijndael.IV);
                }
                else
                {
                    if (_IV.Length > 16)
                    {
                        _IV = _IV.Substring(0, 16);
                    }
                    else
                    {
                        for (int i = 16; i < _IV.Length; i--)
                            _IV += "0";
                    }
                    //if (_IV.Length != 16)
                    //    throw new Exception("加密数据出错,详细原因：IV的长度不为 16 byte.");
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
                ICryptoTransform ct = rijndael.CreateDecryptor(Encoding.ASCII.GetBytes(this._Key), Encoding.ASCII.GetBytes(this._IV));
                rijndael.Clear();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
                System.IO.StreamReader sw = new System.IO.StreamReader(cs);
                return sw.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("解密数据出错,详细原因：" + e.Message);
            }
        }
        #endregion
        #region RC2 Key:16位 IV:8位
        /// <summary>
        /// RC2 数据加密
        /// </summary>
        /// <param name="Values">加密后的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string RC2Encrypt(string Values)
        {
            try
            {
                RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();
                rc2CSP.Mode = CipherMode.CBC;
                byte[] byt;
                //Key 和 IV 为 16或24字节
                if (null == this._Key)
                {
                    rc2CSP.GenerateKey();
                    _Key = Encoding.ASCII.GetString(rc2CSP.Key);
                }
                else
                {
                    if (_Key.Length > 16)
                    {
                        _Key = _Key.Substring(0, 16);
                    }
                    else
                    {
                        for (int i = 16; i < _Key.Length; i--)
                            _Key += "0";
                    }
                    //if (_Key.Length != 16)
                    //    throw new Exception("加密数据出错,详细原因：Key的长度不为 16 byte.");
                }
                if (null == this._IV)
                {
                    rc2CSP.GenerateIV();
                    _IV = Encoding.ASCII.GetString(rc2CSP.IV);
                }
                else
                {
                    if (_IV.Length > 8)
                    {
                        _IV = _IV.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _IV.Length; i--)
                            _IV += "0";
                    }
                    //if (_IV.Length != 8)
                    //    throw new Exception("加密数据出错,详细原因：IV的长度不为 8 byte.");
                }
                //return _Key + "：" + Encoding.ASCII.GetBytes(_Key).Length.ToString() + "<br>"+ _IV + "：" + Encoding.ASCII.GetBytes(this._IV).Length.ToString();
                byt = Encoding.UTF8.GetBytes(Values);
                ICryptoTransform ct = rc2CSP.CreateEncryptor(Encoding.ASCII.GetBytes(this._Key), Encoding.ASCII.GetBytes(this._IV));
                rc2CSP.Clear();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                throw new Exception("加密数据出错,详细原因：" + e.Message);
            }
        }
        /// <summary>
        /// RC2 解密
        /// </summary>
        /// <param name="Values">加密后的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string DeRC2Encrypt(string Values)
        {
            try
            {
                byte[] buffer = Convert.FromBase64String(Values);
                RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();
                rc2CSP.Mode = CipherMode.CBC;
                if (null == this._Key)
                {
                    rc2CSP.GenerateKey();
                    _Key = Encoding.ASCII.GetString(rc2CSP.Key);
                }
                else
                {
                    if (_Key.Length > 16)
                    {
                        _Key = _Key.Substring(0, 16);
                    }
                    else
                    {
                        for (int i = 16; i < _Key.Length; i--)
                            _Key += "0";
                    }
                    //if (_Key.Length != 16)
                    //    throw new Exception("加密数据出错,详细原因：Key的长度不为 16 byte.");
                }
                if (null == this._IV)
                {
                    rc2CSP.GenerateIV();
                    _IV = Encoding.ASCII.GetString(rc2CSP.IV);
                }
                else
                {
                    if (_IV.Length > 8)
                    {
                        _IV = _IV.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _IV.Length; i--)
                            _IV += "0";
                    }
                    //if (_IV.Length != 8)
                    //    throw new Exception("加密数据出错,详细原因：IV的长度不为 8 byte.");
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
                ICryptoTransform ct = rc2CSP.CreateDecryptor(Encoding.ASCII.GetBytes(this._Key), Encoding.ASCII.GetBytes(this._IV));
                rc2CSP.Clear();
                CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
                System.IO.StreamReader sw = new System.IO.StreamReader(cs);
                return sw.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("解密数据出错,详细原因：" + e.Message);
            }
        }
        #endregion
        #endregion
    }
}
