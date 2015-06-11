using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.TZB.SMS;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.Ashx
{
    /// <summary>
    /// 密码重置
    /// </summary>
    public class PassVerCodeReset : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string MobilePhone = context.Request.QueryString["MobilePhone"];
            context.Response.ContentType = "text/plain";
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.Cache.SetNoStore();
            getVerCode(MobilePhone);
            context.Response.End();
        }
        /// <summary>
        /// 获取重置验证码
        /// </summary>
        private void getVerCode(string MobilePhone)
        {
            BLL.BMember bll = new BLL.BMember();
            var MemberModel = bll.GetModelByPhone(MobilePhone);
            if (MemberModel == null)//未找到用户
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", CacheSysMsg.GetMsg(41), ""));
                return;
            }
            BLL.BMemberVerCode bllcode = new BLL.BMemberVerCode();
            ///获取6位随机验证码
            string ValidCode = MakeValidCode();
            BLL.BMemberVerCode.Add(new BLL.tbl_MemberVerCode
            {
                Id = Guid.NewGuid().ToString(),
                MobilePhone = MobilePhone,
                VerCode = ValidCode,
                IsUsed = false,
                IssueTime = DateTime.Now
            });
            if (MemberModel.CountryId == 1 && MemberModel.ProvinceId != 190 && MemberModel.ProvinceId != 191 && MemberModel.ProvinceId != 988)
            {
                //发送验证码至手机
                string v = BSMS.VerCodeSend(MobilePhone, ValidCode);
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(25), ValidCode));
            }
            else
            {
                //发邮件
                string sender = System.Configuration.ConfigurationManager.AppSettings["MailSendUserName"];
                string sendPass = System.Configuration.ConfigurationManager.AppSettings["MailSendPassword"];
                Enow.TZB.Utility.Email.QQMail.sendMail(MemberModel.Email, sender, sender, sendPass, "铁子帮会员绑定校验码", "您正在注册铁子帮官方账号，校验码" + ValidCode + "，请在30分钟内按页面提示提交校验码。");
                Utils.RCWE(UtilsCommons.AjaxReturnJson("2", CacheSysMsg.GetMsg(25), ValidCode));
            }
        }
        /// <summary>
        /// 生成6位数字随机验证码
        /// </summary>
        /// <returns></returns>
        private static string MakeValidCode()
        {
            char[] str = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string num = "";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                num += str[r.Next(0, str.Length)].ToString();
            }

            return num;
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