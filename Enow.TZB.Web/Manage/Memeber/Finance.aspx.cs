using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Memeber
{
    /// <summary>
    /// 铁丝财务管理
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
            int IsPayed = Utils.GetInt(Utils.GetQueryStringValue("IsPayed"), 1);
            int TypeId = Utils.GetInt(Utils.GetQueryStringValue("TypeId"), -1);
            string ContractName = Utils.GetQueryStringValue("ContractName");
            string MobileNo = Utils.GetQueryStringValue("MobilePhone");
            string NickName = Utils.GetQueryStringValue("NickName");
            int PerId = Utils.GetInt(Utils.GetQueryStringValue("cid"));
            Model.MWalletViewSearch SearchModel = new Model.MWalletViewSearch();
            #region 城市权限控制
            var ManageModel = ManageUserAuth.GetManageUserModel();
            if (ManageModel != null)
            {
                SearchModel.IsAllCity = ManageModel.IsAllCity;
                SearchModel.CityLimitList = ManageModel.CityList;
            }
            #endregion
            switch (PerId) { 
                case (int)Model.RoleEnum.铁丝充值管理:
                    SearchModel.PerClass = Model.EnumType.财务流水类型.充值;
                    break;
                case (int)Model.RoleEnum.铁丝消费管理:
                    SearchModel.PerClass = Model.EnumType.财务流水类型.消费;
                    break;
                case (int)Model.RoleEnum.铁丝退款管理:
                    SearchModel.PerClass = Model.EnumType.财务流水类型.消费退款;
                    break;
                case (int)Model.RoleEnum.比赛保证金管理:
                    SearchModel.PerClass = Model.EnumType.财务流水类型.比赛保证金;
                    break;
            }
            if (TypeId > 0) {
                SearchModel.TypeId = (Model.EnumType.财务流水类型)TypeId; 
            }
            SearchModel.IsPay = (Model.EnumType.付款状态)IsPayed;
            SearchModel.ContactName = ContractName;
            SearchModel.MobilePhone = MobileNo;
            SearchModel.NickName = NickName;
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
            int TypeId = Utils.GetInt(Utils.GetFormValue("ddlTypeId"), -1);
            int IsPayed = Utils.GetInt(Utils.GetFormValue("IsPayed"), 1);
            string ContractName = Utils.GetFormValue(txtContractName.UniqueID);
            string MobileNo = Utils.GetFormValue(txtMobile.UniqueID);
            string NickName = Utils.GetFormValue(txtNickName.UniqueID);
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&TypeId=" + TypeId + "&IsPayed=" + IsPayed + "&MobilePhone=" + MobileNo.Trim() + "&ContractName=" + Server.UrlEncode(ContractName) + "&NickName=" + NickName, true);
        }
    }
}