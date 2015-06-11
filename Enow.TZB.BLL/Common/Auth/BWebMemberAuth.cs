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
    /// 用户验证
    /// </summary>
    public class BWebMemberAuth
    {
        #region 变量定义
        private static string _RedirectUrl = "/Default.aspx";
        /// <summary>
        /// 转向地址
        /// </summary>
        public static string RedirectUrl
        {
            get { return _RedirectUrl; }
            set { _RedirectUrl = value; }
        }
        private const string _AuthorName = "TZBMemberLogin";
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
        #region 用户登陆
        /// <summary>
        /// 登陆验证
        /// </summary>        
        /// <param name="UserName">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns>1:登陆成功  -1:用户名或密码不正确 2账号未审核 3账号已禁用</returns>
        public static int UserLogin(string UserName, string Password,out tbl_Member UserInfo)
        {
            using (FWDC rdc = new FWDC())
            {
                Enow.TZB.Utility.HashCrypto CrypTo = new Enow.TZB.Utility.HashCrypto();
                Password = CrypTo.MD5Encrypt(Password);
                CrypTo.Dispose();
                var model = rdc.tbl_Member.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);
                if (model != null)
                {
                    UserInfo = model;
                    //if (model.State == (int)Model.EnumType.会员状态.审核中)
                    //{
                    //    return 2;
                    //}
                    //else if (model.State==(int)Model.EnumType.会员状态.拒绝)
                    //{
                    //    return 3;
                    //}
                    //else
                    //{
                        WriteCookieInfo(model);
                        return 1;
                    //}
                }
                else
                {
                    UserInfo = null;
                    return -1;
                }
            }
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
            tbl_Member model = GetUserModel();
            if (model == null)
                HttpContext.Current.Response.Redirect(RedirectUrl, true);

        }
        /// <summary>
        /// 验证用户是否成功登陆
        /// </summary>
        /// <returns>true:已登陆 false:未登陆</returns>
        public static bool IsLoginCheck()
        {
            tbl_Member model = GetUserModel();
            if (model == null)
                return false;
            else
                return true;

        }
        /// <summary>
        /// 验证用户是否成功登陆
        /// </summary>
        /// <param name="url">跳转的URL</param>
        public static void LoginCheck(string url)
        {
            tbl_Member model = GetUserModel();
            if (model == null)
                HttpContext.Current.Response.Redirect(url, true);
        }
        /// <summary>
        /// 取得登陆用户的凭据数据
        /// </summary>
        /// <returns>返回登陆用户的实体</returns>
        public static tbl_Member GetUserModel()
        {
            HttpCookie hc = HttpContext.Current.Request.Cookies[AuthorName];
            if (hc != null)
            {
                try
                {
                    bool IsLoginState = true;
                    tbl_Member model = new tbl_Member();
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
                    if (hc["UserName"] != null && String.Empty != hc["UserName"])
                    {
                        model.UserName = crypto.DeRC2Encrypt(hc["UserName"]);
                    }
                    else
                    {
                        IsLoginState = false;
                    }
                    if (hc["ContactName"] != null && String.Empty != hc["ContactName"])
                    {
                        model.ContactName = crypto.DeRC2Encrypt(hc["ContactName"]);
                    }
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

        #region 保存用户凭证
        /// <summary>
        /// 保存用户登陆凭证
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <param name="cookieName">凭证名称</param>
        private static void WriteCookieInfo(tbl_Member model)
        {
            Enow.TZB.Utility.HashCrypto CrypTo = new Enow.TZB.Utility.HashCrypto();
            CrypTo.Key = Key;
            CrypTo.IV = IV;
            HttpCookie Hc = new HttpCookie(AuthorName);
            Hc.Values.Add("Id", CrypTo.RC2Encrypt(model.Id));
            Hc.Values.Add("UserName", CrypTo.RC2Encrypt(model.UserName));
            Hc.Values.Add("ContactName", CrypTo.RC2Encrypt(model.ContactName));
            HttpContext.Current.Response.Cookies.Add(Hc);
            CrypTo.Dispose();
            CrypTo = null;
            model = null;
        }
        #endregion
    }
}
