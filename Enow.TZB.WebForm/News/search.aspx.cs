using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;


namespace Enow.TZB.WebForm.News
{
    public partial class search : System.Web.UI.Page
    {
        #region 页面变量
        protected int pageSize = 20, pageIndex = 1, recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitList();
            }
        }


        #region 加载搜索数据
        private void InitList()
        {
            string keywords = Utils.GetQueryStringValue("keywords");
            if (string.IsNullOrWhiteSpace(keywords))
            {
                MessageBox.ShowAndRedirect("关键字为空", "/Default.aspx");
            }

            MArticleSearch searchModel = new MArticleSearch();

            searchModel.IsValid = true;
            searchModel.PublicTarget = Model.发布对象.网站;
            searchModel.KeyWords = Server.UrlDecode(keywords);
            UtilsCommons.Paging(pageSize, ref pageIndex, recordCount);
            string Page = Request.QueryString["Page"];
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                pageIndex = int.Parse(Page);
                if (pageIndex < 1)
                    pageIndex = 1;
            }

            var list = BLL.Common.sys.Article.GetList(ref recordCount, pageSize, pageIndex, searchModel);
            if (recordCount > 0)
            {
                rptNewsList.DataSource = list;
                rptNewsList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = pageSize;
                ExportPageInfo1.intRecordCount = recordCount;
                ExportPageInfo1.CurrencyPage = pageIndex;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
        }
        #endregion
    }
}