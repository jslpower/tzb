using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.SMS;
using Enow.TZB.Utility;
using Enow.TZB.BLL;
using Newtonsoft.Json;

using Weixin.Mp.Sdk;
using Weixin.Mp.Sdk.Request;
using Weixin.Mp.Sdk.Response;
using Weixin.Mp.Sdk.Domain;
using System.Web.Script.Serialization;
using Weixin.Mp.Sdk.Util;

namespace Enow.TZB.Web
{
    public partial class Test : System.Web.UI.Page
    {
        private const string _Key = "12$#@!#@5tr%u8wsfr543$,23ve7w%$#";
        private const string _IV = "!54~1)e74&%3+-q#";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Server.UrlEncode("/WX/Mall/Mall_Type_List.aspx?type=3")+"<br />");
            var WxModel = new Weixin.Mp.Sdk.Domain.User
            {
                City = "杭州",
                Country = "中华人民共和国",
                HeadImgUrl = "http://weixin.qq.com/sdlfjslf",
                Language = "zh-cn",
                NickName = "微信昵称",
                OpenId = "微信OpenId",
                Province = "浙江",
                Sex = "男",
                SubScribe = "",
                SubscribeTime = ""
            };
            Response.Write(JsonConvert.SerializeObject(WxModel));
            if (Utility.Utils.GetQueryStringValue("zl") == "scewm") ShengChengErWeiMa();
            Response.Write(DateTime.Parse("2014-12-1 00:00:00").AddMonths(1).ToString() + "<br />");
            HashCrypto CrypTo = new HashCrypto();
            string Password = CrypTo.MD5Encrypt("12345");
            CrypTo.Dispose();
            Response.Write(Password+"<br />");
            string tId = System.Guid.NewGuid().ToString();
            HashCrypto cpt = new HashCrypto();
            cpt.Key = _Key;
            cpt.IV = _IV;
            string Estr = cpt.DESEncrypt(tId);
            string RStr = cpt.DeDESEncrypt(Estr);
            cpt.Dispose();
            bool r = RStr == tId;
            Response.Write("加密后：" + Estr + ",解密后："+RStr);
            Response.Write("<br>数据比对，原数据：" + tId + ",解密后：" + RStr + ",比对结果:" + r);

            string OpenId = Request.QueryString["OpenId"];
            if(!String.IsNullOrWhiteSpace(OpenId)){
            BMemberAuth.UserLogin(OpenId);
            Response.Redirect("/WX/Member/", true);
            }
        }
        /// <summary>
        /// 短信发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string phone = this.txtMobile.Text;
            string Code = this.txtVerNumber.Text;
            var V = BSMS.VerCodeSend(phone, Code);
            Response.Write(V.ToString());
        }

        void ShengChengErWeiMa()
        {
            string huiYuanId = "fcdc50e8-2831-4934-b439-ce52a8a54562";
            string s = huiYuanId + "||" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            s = Enow.TZB.BLL.BMemberWallet.Sign(s);
            Response.Write(s);
            //Response.End();
            Utility.Utils.RCWE("<img src=\"/Ashx/GetQrCode.ashx?QCode=" + s+ "\">");
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            Enow.TZB.Utility.Email.QQMail.sendMail("8332251@qq.com", "tiezibang@tie-zi.com", "tiezibang@tie-zi.com", "tzb2014", "测试", "测试内容！");
        }
    }
}