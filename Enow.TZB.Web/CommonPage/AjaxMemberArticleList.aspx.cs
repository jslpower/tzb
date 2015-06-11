using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.CommonPage
{
    /// <summary>
    /// 会员日志异步分页列表加载
    /// </summary>
    public partial class AjaxMemberArticleList : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Utils.GetInt(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
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
                    InitList();
            }
        }
        /// <summary>
        /// 加载资讯列表
        /// </summary>
        private void InitList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            #region 文章列表
            #region 查询实体
            Model.MMemberArticleSearch SearchModel = new Model.MMemberArticleSearch();
            string MemberId = Utils.GetQueryStringValue("MemberId");
            SearchModel.MemberId = MemberId;
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.KeyWords = Server.UrlDecode(KeyWord.Trim());
            }
            #endregion
            var list = BArticle.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (rowCounts > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
            #endregion
        }
    }
}