using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Sys
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public partial class UserLoginEvnet : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                InitList();
            }
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitList()
        {
            int rowsCount = 0;
            string Page = Request.QueryString["Page"];
            Model.EnumType.EventType TypeId = Model.EnumType.EventType.登陆日志;
            var list = EventLog.GetList(ref rowsCount, intPageSize, CurrencyPage, TypeId);
            if (rowsCount > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                this.ExportPageInfo1.LinkType = this.ExportPageInfo2.LinkType = 3;
                this.ExportPageInfo1.intPageSize = this.ExportPageInfo2.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = this.ExportPageInfo2.intRecordCount = rowsCount;
                this.ExportPageInfo1.CurrencyPage = this.ExportPageInfo2.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = this.ExportPageInfo2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                this.ExportPageInfo1.UrlParams = this.ExportPageInfo2.UrlParams = Request.QueryString;

            }
        }
    }
}