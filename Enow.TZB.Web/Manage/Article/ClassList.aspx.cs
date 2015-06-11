using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using Enow.TZB.Model;

namespace Enow.TZB.Web.Manage.Article
{
    public partial class ClassList : System.Web.UI.Page
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
            string doType = "";
            if (!string.IsNullOrEmpty(Request.QueryString["dotype"]))
            {
                 doType = Request.QueryString["dotype"].ToString();
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                InitData();
            }
            if (doType == "delete")
            {
                DeleteClass();
            }
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitData()
        {
            int rowsCount = 0;
            string page = Request.QueryString["Page"];
            string KeyWord = Request.QueryString["KeyWord"];
            if (!string.IsNullOrEmpty(page) && StringValidate.IsInteger(page))
            {
                int.TryParse(page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }

            }
            MarticleTypeSeach SearchModel = new MarticleTypeSeach();
            SearchModel.TypeName = txtTypeName.Text.Trim();
            var list = ArticleClass.GetList(ref rowsCount, intPageSize, CurrencyPage, SearchModel);
            if (rowsCount > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = rowsCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
            }
        }

        /// <summary>
        /// 删除资讯类别方法
        /// </summary>
        private void DeleteClass()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            if (StringValidate.IsInteger(s) == false) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            int bllRetCode = ArticleClass.Delete(int.Parse(s)) == true ? 1 : -99;

            if (bllRetCode == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：已成功删除！"));
            else if (bllRetCode == -99) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));

        }
    }
}