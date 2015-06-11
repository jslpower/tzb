using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Utility;
using Enow.TZB.SMS;

namespace Enow.TZB.BLL
{
    public enum 短信类型
    {
        验证码短信,
        业务短信
    }
    public class BSMS
    {
        private static string VerMsg = "您正在注册铁子帮官方账号，校验码{0}，请在30分钟内按页面提示提交校验码。";
        /// <summary>
        /// 验证短信发送
        /// </summary>
        /// <param name="MobilePhone"></param>
        /// <param name="VerCode"></param>
        /// <returns></returns>
        public static string VerCodeSend(string MobilePhone, string VerCode)
        {
            int SendCount = BMemberVerCode.TodayVerCodeSendCount(MobilePhone);
            if (SendCount < 3)
            {
                string SmsMsg = String.Format(VerMsg, VerCode);
                if (IsExistsPhone(MobilePhone) == false)
                {
                    AddSmsLog(短信类型.验证码短信, MobilePhone, SmsMsg);
                    return SMSClass.Send(MobilePhone, SmsMsg);
                }
                else
                {
                    return "号码在黑名单";
                }
            }
            else
            {
                return "同一手机一天只能发送三次验证码!";
            }
        }
        /// <summary>
        /// 验证短信发送
        /// </summary>
        /// <param name="MobilePhone"></param>
        /// <param name="SmsMsg"></param>
        /// <returns></returns>
        public static string Send(string MobilePhone, string SmsMsg)
        {
            if (IsExistsPhone(MobilePhone) == false)
            {
                int SendCount = BMemberVerCode.TodaySendCount(MobilePhone);
                if (SendCount < 20)
                {
                    AddSmsLog(短信类型.业务短信,MobilePhone, SmsMsg);
                    return SMSClass.Send(MobilePhone, SmsMsg);
                }
                else
                {
                    return "同一手机一天最多发送20条信息!";
                }
            }
            else
            {
                return "号码在黑名单";
            }
        }

        /// <summary>
        /// 判断手机号码是否在黑名单列表中
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool IsExistsPhone(string mobile)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SmsBlackPhone.FirstOrDefault(n => n.MobilePhone == mobile);
                if (m != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 添加短信发送历史
        /// </summary>
        /// <param name="MobilePhone"></param>
        /// <param name="SmsMsg"></param>
        public static void AddSmsLog(短信类型 SmsType,string MobilePhone, string SmsMsg)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_SMSLog.InsertOnSubmit(new tbl_SMSLog {
                Id = System.Guid.NewGuid().ToString(),
                TypeId = (int)SmsType,
                RemoteIp = StringValidate.GetRemoteIP(),
                MobilePhone = MobilePhone,
                SmsContent = SmsMsg,
                SendTime = DateTime.Now
                });
                rdc.SubmitChanges();
            }
        }
    }
}
