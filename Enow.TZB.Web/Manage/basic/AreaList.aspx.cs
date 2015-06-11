using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.basic
{
    public partial class AreaList : System.Web.UI.Page
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
            string dotype = Utils.GetQueryStringValue("dotype");
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"));

            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitPage();
            }
            if (dotype=="delete"&&id>0)
            {
                DleteArea(id);
            }

        }

        protected void InitPage()
        {

            MAreaSearch SearchModel = new MAreaSearch();
            int rowCounts = 0;
            SearchModel.Name = Utils.GetQueryStringValue("AreaName");
            string Page = Request.QueryString["Page"];

            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            var list = BMSysProvince.AreaListByPage(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (rowCounts > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = intPageSize;
                ExportPageInfo1.intRecordCount = rowCounts;
                ExportPageInfo1.CurrencyPage = CurrencyPage;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
        }
        /// <summary>
        /// 删除区县
        /// </summary>
        /// <param name="id"></param>
        private void DleteArea(int id)
        {
            int bllRetCode =BMSysProvince.DeleteArea(id)== true ? 1 : -99;

            if (bllRetCode == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：已成功删除！"));
            else if (bllRetCode == -99) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));

        }
    }
}