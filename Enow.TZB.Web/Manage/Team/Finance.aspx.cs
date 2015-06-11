using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Team
{
    /// <summary>
    /// 球队财务统计
    /// </summary>
    public partial class Finance : System.Web.UI.Page
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
            #region 查询条件
            string ContractName = Utils.GetQueryStringValue("ContractName");
            string TeamName = Utils.GetQueryStringValue("TeamName");
            string MatchName = Utils.GetQueryStringValue("MatchName");
            int PerId = Utils.GetInt(Utils.GetQueryStringValue("cid"));
            Model.MTeamWalletViewSearch SearchModel = new Model.MTeamWalletViewSearch();
            #region 城市权限控制
            var ManageModel = ManageUserAuth.GetManageUserModel();
            if (ManageModel != null)
            {
                SearchModel.IsAllCity = ManageModel.IsAllCity;
                SearchModel.CityLimitList = ManageModel.CityList;
            }
            ManageModel = null;
            #endregion
            SearchModel.ContactName = ContractName;
            SearchModel.TeamName = TeamName;
            SearchModel.MatchName = MatchName;
            #endregion
            string Page = Request.QueryString["Page"];
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                CurrencyPage = int.Parse(Page);
                if (CurrencyPage < 1)
                    CurrencyPage = 1;
            }
            var list = BMemberWallet.GetViewList(ref rowsCount, intPageSize, CurrencyPage, SearchModel);
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
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string ContractName = Utils.GetFormValue(txtContractName.UniqueID);
            string TeamName = Utils.GetFormValue(txtTeamName.UniqueID);
            string MatchName = Utils.GetFormValue(txtMatchName.UniqueID);
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&ContractName=" + Server.UrlEncode(ContractName) + "&TeamName=" + Server.UrlEncode(TeamName) + "&MatchName=" + Server.UrlEncode(MatchName), true);
        }
    }
}