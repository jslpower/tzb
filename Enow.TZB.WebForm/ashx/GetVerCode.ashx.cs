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
    /// GetVerCode 的摘要说明
    /// </summary>
    public class GetVerCode : IHttpHandler
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
        /// 获取注册验证码
        /// </summary>
        private void getVerCode(string MobilePhone)
        {
            BLL.BMember bll = new BLL.BMember();
            BLL.BMemberVerCode bllcode = new BLL.BMemberVerCode();
            ///防止手机号码多次注册
            if (bll.IsExistsPhone(MobilePhone))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", CacheSysMsg.GetMsg(21), ""));
            }
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
            //发送验证码至手机
            string v = BSMS.VerCodeSend(MobilePhone, ValidCode);
            if (v == "1")
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(25), ValidCode));
            }
            else {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", v));
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