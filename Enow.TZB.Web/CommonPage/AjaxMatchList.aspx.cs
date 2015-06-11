using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.CommonPage
{
    public partial class AjaxMatchList : System.Web.UI.Page
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
                BMemberAuth.LoginCheck();
                InitList();
            }
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        void InitList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }            
            Model.MMatch SearchModel = new Model.MMatch();
            if (!String.IsNullOrWhiteSpace(KeyWord) && KeyWord != "赛事搜索")
            {
                SearchModel.MatchName = Server.UrlDecode(KeyWord.Trim());
            }
            var list = BMatch.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list != null && list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
        }
    }
}