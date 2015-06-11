using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enow.TZB.SMS
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public class SMSClass
    {

        /// <summary>
        /// 门牌号
        /// </summary>
        private static string SmsAccount = System.Configuration.ConfigurationManager.AppSettings["SmsAccount"];
        /// <summary>
        /// 登陆密码
        /// </summary>
        private static string SmsPassword = System.Configuration.ConfigurationManager.AppSettings["SmsPassword"];
        /// <summary>
        /// 授权码
        /// </summary>
        private static string SerialNumber = System.Configuration.ConfigurationManager.AppSettings["SerialNumber"];
        /// <summary>
        /// 通道号
        /// </summary>
        private static string SmsChannelCode = System.Configuration.ConfigurationManager.AppSettings["SmsChannelCode"];
        /// <summary>
        /// 签名
        /// </summary>
        private static string SmsSignature = System.Configuration.ConfigurationManager.AppSettings["SmsSignature"];
        /// <summary>
        /// 验证短信发送
        /// </summary>
        /// <param name="MobilePhone"></param>
        /// <param name="SmsMsg"></param>
        /// <returns></returns>
        public static string Send(string MobilePhone, string SmsMsg)
        {
            string state = "通道异常";
            SmsService.SmsService SMS = new SmsService.SmsService();
            try
            {
                string smsid = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string pwd = GetMD5(SmsPassword + SerialNumber);
                //加密结果长度必须是32位的，不足32位，前面加0不足
                if (pwd.Length < 32)
                {
                    for (int i = 0; i < 32 - pwd.Length; i++)
                    {
                        pwd = "0" + pwd;
                    }
                }
                string strReturnValue = SMS.SendSms3(SmsAccount, pwd, MobilePhone, SmsMsg + SmsSignature, SmsChannelCode, smsid, "1");
                switch (strReturnValue)
                {
                    case "0":
                        state = "1";
                        break;
                    default:
                        state = strReturnValue;
                        break;
                }
                return state;
            }
            catch (Exception ee)
            {
                return state;
                //MessageBox.Show(ee.Message);
            }
        }
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5(string str)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
            byte[] bytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
