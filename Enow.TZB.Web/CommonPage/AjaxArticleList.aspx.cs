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
    public partial class AjaxArticleList : System.Web.UI.Page
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
                int TypeId = Utils.GetInt(Utils.GetQueryStringValue("TypeId"));
                if (TypeId > 0)
                {
                    InitTypeList(TypeId);
                }
            }
        }
        /// <summary>
        /// 加载资讯列表
        /// </summary>
        /// <param name="TypeId"></param>
        private void InitTypeList(int TypeId)
        {
            var model = ArticleClass.GetModel(TypeId);
            if (model != null)
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
                Model.MArticleSearch searchModel = new Model.MArticleSearch();
                #region 查询实体
                searchModel.ClassId = TypeId;
                searchModel.PublicTarget = Model.发布对象.微信;
                searchModel.IsValid = true;
                string KeyWord = Utils.GetQueryStringValue("KeyWord");
                if (!String.IsNullOrWhiteSpace(KeyWord))
                {
                    searchModel.KeyWords = Server.UrlDecode(KeyWord.Trim());
                }
                #endregion
                var list = BLL.Common.sys.Article.GetList(ref rowCounts, intPageSize, CurrencyPage, searchModel);
                if (rowCounts > 0)
                {
                    rptList.DataSource = list;
                    rptList.DataBind();
                }
                #endregion

            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(32));
            }
        }
    }
}