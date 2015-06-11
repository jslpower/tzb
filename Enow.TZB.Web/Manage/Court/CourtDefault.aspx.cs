using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;


namespace Enow.TZB.Web.Manage.Court
{
    /// <summary>
    /// 球场管理
    /// </summary>
    public partial class CourtDefault : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype").ToLower();

            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                if (ManageUserAuth.CheckAdminAuth((int)RoleEnum.场所新增) == true)
                {
                    this.PhAdd.Visible = true;
                }
                else { this.PhAdd.Visible = false; }
                if (ManageUserAuth.CheckAdminAuth((int)RoleEnum.场所修改) == true)
                {
                    this.phUpdate.Visible = true;
                }
                else { this.phUpdate.Visible = false; }
                if (ManageUserAuth.CheckAdminAuth((int)RoleEnum.场所删除) == true)
                {
                    this.phDel.Visible = true;
                }
                else { this.phDel.Visible = false; }
                InitPage();
            }

            if (dotype=="delete")
            {
                DeleteCourt();
            }
        }

        /// <summary>
        /// 列表
        /// </summary>
        private void InitPage()
        {
            int Rowcount = 0;
            string Page = Request.QueryString["Page"];
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                CurrencyPage = int.Parse(Page);
                if (CurrencyPage < 1)
                    CurrencyPage = 1;
            }
            MBallFieldSearch searchModel = new MBallFieldSearch();
            searchModel.KeyWord = Utils.GetQueryStringValue("CourtName");
            #region 城市权限控制
            var ManageModel = ManageUserAuth.GetManageUserModel();
            if (ManageModel != null)
            {
                searchModel.IsAllCity = ManageModel.IsAllCity;
                searchModel.CityLimitList = ManageModel.CityList;
            }
            ManageModel = null;
            #endregion
            var list = BBallField.GetCourList(ref Rowcount, intPageSize, CurrencyPage, searchModel);
            if (Rowcount>0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                this.ExportPageInfo1.LinkType = 3;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = Rowcount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
            }
        }

        private void DeleteCourt()
        {
            string id = Utils.GetQueryStringValue("id");
            if (!string.IsNullOrEmpty(id))
            {
                int bllRetCode =BBallField.Delete(id)== true ? 1 : -99;

            if (bllRetCode == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：已成功删除！"));
            else if (bllRetCode == -99) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：信息不存在！"));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));
            }
        }
    }
}