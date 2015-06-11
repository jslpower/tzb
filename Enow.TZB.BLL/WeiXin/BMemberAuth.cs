using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Data.Linq.SqlClient;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 会员用户认证
    /// </summary>
    public class BMemberAuth
    {
        #region 变量定义
        private static string _RedirectUrl = "/WX/Notice/NoAttention.aspx";
        /// <summary>
        /// 转向地址
        /// </summary>
        public static string RedirectUrl
        {
            get { return _RedirectUrl; }
            set { _RedirectUrl = value; }
        }
        private const string _AuthorName = "MemberLogin";
        /// <summary>
        /// 获得数据验证的key
        /// </summary>
        public static string AuthorName
        {
            get { return _AuthorName; }
        }
        private const string _Key = "12$#@!#@5tr%u8wsfr543$,23ve7w%$#";
        /// <summary>
        /// 获得数据验证的key
        /// </summary>
        public static string Key
        {
            get { return _Key; }
        }
        private const string _IV = "!54~1)e74&%3+-q#";
        /// <summary>
        /// 获得数据验证的IV
        /// </summary>
        public static string IV
        {
            get { return _IV; }
        }
        #endregion
        #region 会员用户登陆
        /// <summary>
        /// 微信用户登陆
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public static int UserLogin(string OpenId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberWeiXin.FirstOrDefault(u => u.OpenId == OpenId);
                if (model != null)
                {
                    WriteCookieInfo(model);
                    return 1;
                }
                else
                {
                    WriteCookieInfo(new tbl_MemberWeiXin{
                        Id="",
                        OpenId = OpenId,
                        NickName = "",
                        HeadPhoto = ""
                    });
                    return -1;
                }
            }
        }
        /// <summary>
        /// 验证用户名是否存在
        /// </summary>        
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public static bool IsUserLogin(string UserName)
        {
            using (FWDC rdc = new FWDC())
            {
                tbl_Member model = rdc.tbl_Member.FirstOrDefault(u => u.UserName == UserName && u.State == (int)Model.EnumType.会员状态.通过);
                if (model != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion 
        #region 保存管理用户凭证
        /// <summary>
        /// 保存用户登陆凭证
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <param name="cookieName">凭证名称</param>
        private static void WriteCookieInfo(tbl_MemberWeiXin model)
        {
            Enow.TZB.Utility.HashCrypto CrypTo = new Enow.TZB.Utility.HashCrypto();
            CrypTo.Key = Key;
            CrypTo.IV = IV;
            HttpCookie Hc = new HttpCookie(AuthorName);
            Hc.Values.Add("Id", CrypTo.RC2Encrypt(model.Id.ToString()));
            Hc.Values.Add("OpenId", CrypTo.RC2Encrypt(model.OpenId.ToString()));
            //Hc.Values.Add("NickName", CrypTo.RC2Encrypt(model.NickName));
            //Hc.Values.Add("HeadPhoto", CrypTo.RC2Encrypt(model.HeadPhoto));
            //Hc.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Current.Response.Cookies.Add(Hc);
            CrypTo.Dispose();
            CrypTo = null;
            model = null;
        }
        #endregion
        #region 用户退出
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="Url">退出后转向网址</param>
        public static void Logout(string Url)
        {
            HttpContext.Current.Response.Cookies[AuthorName].Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Redirect(Url, true);
        }
        #endregion
        #region 用户验证及取得已登陆的用户信息
        /// <summary>
        /// 验证用户是否成功登陆
        /// </summary>
        public static void LoginCheck()
        {
            tbl_MemberWeiXin model = GetUserModel();
            if (model == null)
                HttpContext.Current.Response.Redirect(RedirectUrl, true);

        }
        /// <summary>
        /// 验证用户是否成功登陆
        /// </summary>
        /// <returns>true:已登陆 false:未登陆</returns>
        public static bool IsLoginCheck()
        {
            var model = GetUserModel();
            if (model == null)
                return false;
            else
                return true;

        }
        /// <summary>
        /// 取得登陆用户的凭据数据
        /// </summary>
        /// <returns>返回登陆用户的实体</returns>
        public static tbl_MemberWeiXin GetUserModel()
        {
            HttpCookie hc = HttpContext.Current.Request.Cookies[AuthorName];
            if (hc != null)
            {
                try
                {
                    bool IsLoginState = true;
                    tbl_MemberWeiXin model = new tbl_MemberWeiXin();
                    Enow.TZB.Utility.HashCrypto crypto = new Enow.TZB.Utility.HashCrypto();
                    crypto.Key = Key;
                    crypto.IV = IV;
                    if (hc["Id"] != null && String.Empty != hc["Id"])
                    {
                        model.Id = crypto.DeRC2Encrypt(hc["Id"]);
                    }
                    else
                    {
                        IsLoginState = false;
                    }

                    if (hc["OpenId"] != null && string.Empty != hc["OpenId"])
                    {
                        model.OpenId = crypto.DeRC2Encrypt(hc["OpenId"]);
                    }
                    else
                    {
                        IsLoginState = false;
                    }
                    /*
                    if (hc["NickName"] != null && String.Empty != hc["NickName"])
                    {
                        model.NickName = crypto.DeRC2Encrypt(hc["NickName"]);
                    }
                    if (hc["HeadPhoto"] != null && String.Empty != hc["HeadPhoto"])
                    {
                        model.HeadPhoto = crypto.DeRC2Encrypt(hc["HeadPhoto"]);
                    }
                     * */
                    if (IsLoginState)
                        return model;
                    else
                        return null;
                }
                catch { return null; }
            }
            else { return null; }
        }
        #endregion
    }
}
