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


namespace Enow.TZB.Web.WX.Article
{
    /// <summary>
    /// 文章更表
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        public string ltrTypeName = "";
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
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
            if (!IsPostBack) {
                int TypeId = Utils.GetInt(Utils.GetQueryStringValue("TypeId"));
                if (TypeId > 0) {
                    InitTypeList(TypeId);
                } else {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(32));
                }
            }
        }
        /// <summary>
        /// 加载资讯列表
        /// </summary>
        /// <param name="TypeId"></param>
        private void InitTypeList(int TypeId) {
            var model = ArticleClass.GetModel(TypeId);
            if (model != null) {
                this.Title = model.TypeName;
                ltrTypeName = model.TypeName;
                UserHome1.Userhometitle = ltrTypeName;
                bool IsView = false;
                if (!String.IsNullOrWhiteSpace(model.TypePhoto))
                {
                    this.ltrTypePhoto.Text = "<img src=\"" + model.TypePhoto + "\" />";
                    IsView = true;
                }
                if (!String.IsNullOrWhiteSpace(model.Remark))
                {
                    this.ltrTypeRemark.Text = model.Remark;
                    IsView = true;
                }
                if (IsView)
                    this.phHead.Visible = true;
                else
                    this.phHead.Visible = false;
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

            } else {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(32));
            }
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string KeyWords = Utils.GetFormValue(this.txtKeyWords.UniqueID);
            if (!String.IsNullOrEmpty(KeyWords))
            {
                string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
                Response.Redirect(uri + "&TypeId=" + Utils.GetQueryStringValue("TypeId") + "&KeyWord=" + Server.UrlEncode(KeyWords.Trim()), true);
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(57));
            }
        }
    }
}