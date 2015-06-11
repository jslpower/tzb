using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Cashier
{
    /// <summary>
    /// 收银员管理
    /// </summary>
    public partial class Default : System.Web.UI.Page
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
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");

            switch (doType)
            {
                case "Disabled": DisabledData(); break;
                case "Enable": EnableData(); break;
                default: break;
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                if (ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.收银员新增) == true)
                {
                    this.PhAdd.Visible = true;
                }
                else { this.PhAdd.Visible = false; }
                if (ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.收银员修改) == true)
                {
                    this.phUpdate.Visible = true;
                }
                else { this.phUpdate.Visible = false; }
                if (ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.收银员启用禁用) == true)
                {
                    this.phEnable.Visible = true;
                }
                else { this.phEnable.Visible = false; }
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
            string keyword = Request.QueryString["KeyWord"];
            string UserName = Utils.GetQueryStringValue("UserName");
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                CurrencyPage = int.Parse(Page);
                if (CurrencyPage < 1)
                    CurrencyPage = 1;
            }
            Model.MManagerSearch SearchModel = new Model.MManagerSearch();
            #region 城市权限控制
            var ManageModel = ManageUserAuth.GetManageUserModel();
            if (ManageModel != null)
            {
                SearchModel.IsAllCity = ManageModel.IsAllCity;
                SearchModel.CityLimitList = ManageModel.CityList;
            }
	    ManageModel = null;
            #endregion
        SearchModel.UserName = UserName;
                SearchModel.KeyWord = keyword;
                SearchModel.IsCashier = true;

                var list = SysUser.GetCashierList(ref rowsCount, intPageSize, CurrencyPage, SearchModel);
            if (rowsCount > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                this.ExportPageInfo1.LinkType = 3;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = rowsCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;

            }
        }
        #region 禁用/启用
        /// <summary>
        /// 启用操作
        /// </summary>
        private void EnableData()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            if (StringValidate.IsInteger(s) == false) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            bool bllRetCode = SysUser.Enable(Convert.ToInt32(s));

            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        /// <summary>
        /// 禁用操作
        /// </summary>
        private void DisabledData()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            if (StringValidate.IsInteger(s) == false) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            bool bllRetCode = SysUser.Disabled(Convert.ToInt32(s));

            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        #endregion
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string KeyWord = this.txtKeyWord.Text;
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&KeyWord=" + Server.UrlEncode(KeyWord) + "&UserName=" + Server.UrlEncode(this.txtuserName.Text), true);
        }
    }
}