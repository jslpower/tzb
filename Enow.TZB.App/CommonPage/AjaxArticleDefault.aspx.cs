using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using System.Text;

namespace Enow.TZB.App.CommonPage
{
    public partial class AjaxArticleDefault : System.Web.UI.Page
    {
        public string ltrTypeName = "";
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = 50;
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
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
            #region 文章列表
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
            MArticleSearch searchModel = new MArticleSearch();
            #region 查询实体
            searchModel.ClassId = TypeId;
            searchModel.PublicTarget = 发布对象.APP;
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
    }
}