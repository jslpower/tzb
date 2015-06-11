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
    public partial class AjaxVotelist : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        public string dizhiurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Getusermodel();
                InitList();
            }
        }
        private void Getusermodel()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel==null)
            {
                Response.End();
            }

        }
        /// <summary>
        /// 加载商品列表
        /// </summary>
        private void InitList()
        {
            int rowCounts = 0;
            dizhiurl = Utils.GetQueryStringValue("tzurl");
            string Page = Request.QueryString["Page"];
            string title = Utils.GetQueryStringValue("title");
            int types = Utils.GetInt(Utils.GetQueryStringValue("types"), 1);
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            Model.MVoteQuery SearchModel = new Model.MVoteQuery();
            SearchModel.title = title;
            SearchModel.ColumnID = types;
            var list = BVote.GetList(ref rowCounts, 100, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }

        }
    }
}