using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Court
{
    public partial class TransactionFlow : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        protected int TypeId, CountryId, ProvinceId, CityId, AreaId, intYear, intMonth;
        protected string FieldId;
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
                InitDate();
                InitList();
            }
        }
        /// <summary>
        /// 初始化日期
        /// </summary>
        private void InitDate()
        {
            intYear = Utils.GetInt(Utils.GetQueryStringValue("Year"));
            intMonth = Utils.GetInt(Utils.GetQueryStringValue("Month"));
            int yStart = 2013;
            for (int i = yStart; i <= DateTime.Now.Year; i++)
            {
                this.ddlYear.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
            }
            if (intYear > 0)
            {
                this.ddlYear.Items.FindByValue(intYear.ToString()).Selected = true;
            }
            else
            {
                this.ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
                intYear = DateTime.Now.Year;
            }
            for (int i = 1; i <= 12; i++)
            {
                this.ddlMonth.Items.Add(new ListItem(i.ToString() + "月", i.ToString()));
            }
            if (intMonth > 0)
            {
                this.ddlMonth.Items.FindByValue(intMonth.ToString()).Selected = true;
            }
            else
            {
                this.ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
                intMonth = DateTime.Now.Month;
            }
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitList()
        {
            int Rowcount = 0;
            string Page = Request.QueryString["Page"];
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                CurrencyPage = int.Parse(Page);
                if (CurrencyPage < 1)
                    CurrencyPage = 1;
            }
            Model.MCourStatisticsSearch searchModel = new Model.MCourStatisticsSearch();
            TypeId = Utils.GetInt(Utils.GetQueryStringValue("TypeId"));
            FieldId = Utils.GetQueryStringValue("FieldId");
            CountryId = Utils.GetInt(Utils.GetQueryStringValue("CountryId"));
            ProvinceId = Utils.GetInt(Utils.GetQueryStringValue("ProvinceId"));
            CityId = Utils.GetInt(Utils.GetQueryStringValue("CityId"));
            AreaId = Utils.GetInt(Utils.GetQueryStringValue("AreaId"));
            intYear = Utils.GetInt(Utils.GetQueryStringValue("Year"));
            intMonth = Utils.GetInt(Utils.GetQueryStringValue("Month"));
            if (intYear == 0)
                intYear = DateTime.Now.Year;
            if (intMonth == 0)
                intMonth = DateTime.Now.Month;
            searchModel.FieldTypeId = TypeId;
            searchModel.FieldId = FieldId;
            searchModel.CountryId = CountryId;
            searchModel.ProvinceId = ProvinceId;
            searchModel.CityId = CityId;
            searchModel.AreaId = AreaId;
            searchModel.Year = intYear;
            searchModel.Month = intMonth;
            #region 城市权限控制
            var ManageModel = ManageUserAuth.GetManageUserModel();
            if (ManageModel != null)
            {
                searchModel.IsAllCity = ManageModel.IsAllCity;
                searchModel.CityLimitList = ManageModel.CityList;
            }
            ManageModel = null;
            #endregion
            var list = BLL.Order.BOrder.GetCashierFlow(ref Rowcount, intPageSize, CurrencyPage, searchModel);
            if (Rowcount > 0)
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
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int TypeId = Utils.GetInt(Utils.GetFormValue("ddlTypeId"));
            string FieldId = Utils.GetFormValue("ddlFieldId");
            int CountryId = Utils.GetInt(Utils.GetFormValue("ddlCountry"));
            int ProvinceId = Utils.GetInt(Utils.GetFormValue("ddlProvince"));
            int CityId = Utils.GetInt(Utils.GetFormValue("ddlCity"));
            int AreaId = Utils.GetInt(Utils.GetFormValue("ddlArea"));
            int Year = Utils.GetInt(Utils.GetFormValue(ddlYear.UniqueID));
            int Month = Utils.GetInt(Utils.GetFormValue(ddlMonth.UniqueID));
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&TypeId=" + TypeId + "&FieldId=" + FieldId + "&CountryId=" + CountryId + "&ProvinceId=" + ProvinceId + "&CityId=" + CityId + "&AreaId=" + AreaId + "&Year=" + Year + "&Month=" + Month, true);
        }
    }
}