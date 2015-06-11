//收银端页面基类 2014-11-26
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.ShouYin
{
    /// <summary>
    /// 收银端页面基类
    /// </summary>
    public class ShouYinYeMian: System.Web.UI.Page
    {
        #region attributes
        ManagerList _YongHuInfo = null;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public ManagerList YongHuInfo { get { return _YongHuInfo; } }
        #endregion

        #region protected members
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ManageUserAuth.SYLoginCheck();
            _YongHuInfo = ManageUserAuth.GetManageUserModel();
        }

        /// <summary>
        /// OnPreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        /// <summary>
        /// Response.Clear();Response.Write(s);Response.End();
        /// </summary>
        /// <param name="s">输出字符串</param>
        protected void RCWE(string s)
        {
            Enow.TZB.Utility.Utils.RCWE(s);
        }

        /// <summary>
        /// register scripts
        /// </summary>
        /// <param name="script"></param>
        protected void RegisterScript(string script)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
        #endregion
    }
}