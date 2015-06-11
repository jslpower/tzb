using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using System.Data;

namespace Enow.TZB.Web.Manage.Job
{
    public partial class OfferList : System.Web.UI.Page
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
        /// 省市联动下拉框初始化
        /// </summary>
        protected string CId = "", PId = "", CSId = "", AId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitPage();
            }

        }

        /// <summary>
        /// 绑定应聘信息列表
        /// </summary>
        protected void InitPage()
        {
            #region 查询实体
            MOfferSearch SearchModel = new MOfferSearch();
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("JobName")))
            {
                SearchModel.JobName = Utils.GetQueryStringValue("JobName");
            }
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("UserName")))
            {
                SearchModel.UserName = Utils.GetQueryStringValue("UserName");
            }
            if (Utils.GetInt(Utils.GetQueryStringValue("OfferState")) > 0)
            {
                SearchModel.OfferState = Utils.GetInt(Utils.GetQueryStringValue("OfferState"));
            }
            if (Utils.GetInt(Utils.GetQueryStringValue("Country"), 0) > 0)
            {
                SearchModel.CountryId = Utils.GetInt(Utils.GetQueryStringValue("Country"));
            }
            if (Utils.GetInt(Utils.GetQueryStringValue("Province"), 0) > 0)
            {
                SearchModel.ProvinceId = Utils.GetInt(Utils.GetQueryStringValue("Province"));
            }
            if (Utils.GetInt(Utils.GetQueryStringValue("City"), 0) > 0)
            {
                SearchModel.CityId = Utils.GetInt(Utils.GetQueryStringValue("City"));
            }
            if (Utils.GetInt(Utils.GetQueryStringValue("Area"), 0) > 0)
            {
                SearchModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("Area"));
            }
            SearchModel.Mobile = Utils.GetQueryStringValue("Mobile");
            SearchModel.StartIssueTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startDate"));
            SearchModel.EndIssueTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endDate"));
            #endregion

            int RowsCount = 0;
            string Page = Request.QueryString["Page"];

            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            var list = BOffer.GetList(ref RowsCount, intPageSize, CurrencyPage, SearchModel);
            if (RowsCount > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = intPageSize;
                ExportPageInfo1.intRecordCount = RowsCount;
                ExportPageInfo1.CurrencyPage = CurrencyPage;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }

        }

        /// <summary>
        /// 绑定招聘状态下拉框
        /// </summary>
        protected string InitOfferState(string str)
        {
            System.Text.StringBuilder strState = new System.Text.StringBuilder();
            Array array = Enum.GetValues(typeof(Model.EnumType.应聘状态));
            strState.Append("<option value='-1' selected='selected'>-请选择-</option>");
            foreach (var arr in array)
            {
                int value = (int)Enum.Parse(typeof(Model.EnumType.应聘状态), arr.ToString());
                string text = Enum.GetName(typeof(Model.EnumType.应聘状态), arr);

                if (value.ToString().Equals(str))
                {
                    strState.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", value, text);
                }
                else
                {
                    strState.AppendFormat("<option value='{0}'>{1}</option>", value, text);
                }

            }
            return strState.ToString();
        }

        #region 导出Excel
        protected void btnExport_Click(object sender, EventArgs e)
        {
            #region 查询实体
            MOfferSearch SearchModel = new MOfferSearch();
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("JobName")))
            {
                SearchModel.JobName = Utils.GetQueryStringValue("JobName");
            }
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("UserName")))
            {
                SearchModel.UserName = Utils.GetQueryStringValue("UserName");
            }
            if (Utils.GetInt(Utils.GetQueryStringValue("OfferState")) > 0)
            {
                SearchModel.OfferState = Utils.GetInt(Utils.GetQueryStringValue("OfferState"));
            }
            if (Utils.GetInt(Utils.GetQueryStringValue("Country"), 0) > 0)
            {
                SearchModel.CountryId = Utils.GetInt(Utils.GetQueryStringValue("Country"));
            }
            if (Utils.GetInt(Utils.GetQueryStringValue("Province"), 0) > 0)
            {
                SearchModel.ProvinceId = Utils.GetInt(Utils.GetQueryStringValue("Province"));
            }
            if (Utils.GetInt(Utils.GetQueryStringValue("City"), 0) > 0)
            {
                SearchModel.CityId = Utils.GetInt(Utils.GetQueryStringValue("City"));
            }
            if (Utils.GetInt(Utils.GetQueryStringValue("Area"), 0) > 0)
            {
                SearchModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("Area"));
            }
            SearchModel.Mobile = Utils.GetQueryStringValue("Mobile");
            SearchModel.StartIssueTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startDate"));
            SearchModel.EndIssueTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endDate"));
            #endregion

            int RowsCount = 0;
           int Index=1;
            var list = BOffer.GetList(ref RowsCount, 9999, 1, SearchModel);
            if (RowsCount > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("编号", typeof(System.Int32));
                dt.Columns.Add("申请职位", typeof(System.String));
                dt.Columns.Add("就职区域", typeof(System.String));
                dt.Columns.Add("用户名", typeof(System.String));
                dt.Columns.Add("手机号码", typeof(System.String));
                dt.Columns.Add("姓名", typeof(System.String));
                dt.Columns.Add("身份证号", typeof(System.String));
                dt.Columns.Add("地址", typeof(System.String));
                dt.Columns.Add("Email", typeof(System.String));
                dt.Columns.Add("工作年限", typeof(System.String));
                dt.Columns.Add("球龄", typeof(System.String));
                dt.Columns.Add("专业", typeof(System.String));
                dt.Columns.Add("应聘状态", typeof(System.String));
                dt.Columns.Add("申请日期", typeof(System.String));

                foreach (var lst in list)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = Index++;
                    dr[1] = lst.JobName;
                    dr[2] = lst.CountryName + "-" + lst.ProvinceName + "-" + lst.CityName + "-" + lst.AreaName;
                    dr[3] = lst.UserName;
                    dr[4] = lst.MobilePhone;
                    dr[5] = lst.ContactName;
                    dr[6] = lst.PersonalId;
                    dr[7] = lst.Address;
                    dr[8] = lst.Email;
                    dr[9] = lst.WorkYear;
                    dr[10]=lst.BallYear;
                    dr[11] = lst.Specialty;
                    dr[12] = (Model.EnumType.应聘状态)lst.OfferState;
                    dr[13] = lst.IssueTime.ToString("yyyy-MM-dd");
                    dt.Rows.Add(dr);
                }
                NPOIHelper.TableToExcelForXLSAny(dt, "应聘信息报表");
            }
        }
        #endregion
    }
}