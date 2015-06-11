//收银端母版页 2014-11-26
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.ShouYin
{
    /// <summary>
    /// 收银端母版页
    /// </summary>
    public partial class ShouYin : System.Web.UI.MasterPage
    {
        #region attributes
        /// <summary>
        /// 页面标题
        /// </summary>
        protected string ITitle = string.Empty;
        /// <summary>
        /// 球场名称
        /// </summary>
        protected string QiuChangName = string.Empty;
        /// <summary>
        /// 登录用户姓名
        /// </summary>
        protected string YongHuXingMing = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            InitInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            //ManageUserAuth.ManageLoginCheck();
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            var loginYongHuInfo = ManageUserAuth.GetManageUserModel();
            YongHuXingMing = loginYongHuInfo.ContactName;

            QiuChangName = loginYongHuInfo.FieldName;
            ITitle = Page.Title + "-" + QiuChangName + "-收银端-铁子帮管控系统";
        }
        #endregion
    }
}