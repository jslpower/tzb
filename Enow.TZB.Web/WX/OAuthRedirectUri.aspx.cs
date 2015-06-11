using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using Weixin.Mp.Sdk;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX
{
    public partial class OAuthRedirectUri : System.Web.UI.Page
    {
        string Appid = "wx33bbf1c5c1d5a10b";
        string appsecret = "4700b1876dbe932b1f57047b312343c5";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer=true;
            Response.CacheControl="no-cache";    //禁止代理服务器缓存本页面
            Response.Expires = -1000;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string Code = Request.QueryString["code"].ToString();
                    //获得Token  
                    OAuth_Token Model = Get_token(Code);
                    string OpenId = Model.openid;
                    //Response.Write(Model.access_token);                    
                    //OAuthUser OAuthUser_Model = Get_UserInfo(Model.access_token, Model.openid);
                    //Response.Write("用户OPENID:" + OAuthUser_Model.openid + "<br>用户昵称:" + OAuthUser_Model.nickname + "<br>性别:" + OAuthUser_Model.sex + "<br>所在省:" + OAuthUser_Model.province + "<br>所在市:" + OAuthUser_Model.city + "<br>所在国家:" + OAuthUser_Model.country + "<br>头像地址:" + OAuthUser_Model.headimgurl + "<br>用户特权信息:" + OAuthUser_Model.privilege);
                    string BaseUrl = Request.QueryString["BaseUrl"];
                    if (!String.IsNullOrEmpty(OpenId))
                    { 
                        //写登陆凭证并 转向
                        BMemberAuth.UserLogin(OpenId);
                        Response.Redirect(BaseUrl, true);
                        return;
                    }
                    else {
                        Response.Redirect("/WX/Notice/NoAttention.aspx",true);
                        return;
                    }
                }
            }
        }

        //获得Token  
        protected OAuth_Token Get_token(string Code)
        {
            string Str = GetJson("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + Appid + "&secret=" + appsecret + "&code=" + Code + "&grant_type=authorization_code");
            OAuth_Token Oauth_Token_Model = JsonHelper.ParseFromJson<OAuth_Token>(Str);
            return Oauth_Token_Model;
        }
        //刷新Token  
        protected OAuth_Token refresh_token(string REFRESH_TOKEN)
        {
            string Str = GetJson("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid=" + Appid + "&grant_type=refresh_token&refresh_token=" + REFRESH_TOKEN);
            OAuth_Token Oauth_Token_Model = JsonHelper.ParseFromJson<OAuth_Token>(Str);
            return Oauth_Token_Model;
        }
        //获得用户信息  
        protected OAuthUser Get_UserInfo(string REFRESH_TOKEN, string OPENID)
        {
            // Response.Write("获得用户信息REFRESH_TOKEN:" + REFRESH_TOKEN + "||OPENID:" + OPENID);  
            string Str = GetJson("https://api.weixin.qq.com/sns/userinfo?access_token=" + REFRESH_TOKEN + "&openid=" + OPENID);
            OAuthUser OAuthUser_Model = JsonHelper.ParseFromJson<OAuthUser>(Str);
            return OAuthUser_Model;
        }
        protected string GetJson(string url)
        {
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            wc.Encoding = Encoding.UTF8;
            string returnText = wc.DownloadString(url);

            if (returnText.Contains("errcode"))
            {
                //可能发生错误  
            }
            //Response.Write(returnText);  
            return returnText;
        }  
    }
}