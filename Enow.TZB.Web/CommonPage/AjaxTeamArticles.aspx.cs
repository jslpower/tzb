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
    public partial class AjaxTeamArticles : System.Web.UI.Page
    {
        protected string dhurl = "";
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitLogList();
            }
        }
        /// <summary>
        /// 加载我的日志列表
        /// </summary>
        private void InitLogList()
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
            string TeamId = Model.MCommon.DefaultGuidId;
            var teamid = Utils.GetQueryStringValue("TeamId");
            if (!string.IsNullOrEmpty(teamid))
            {
                TeamId = teamid;
            }
            else
            {
                var TeamModel = BTeamMember.GetModel(MemberId);
                if (TeamModel != null)
                {
                    TeamId = TeamModel.TeamId;
                }
            }

            Model.MMemberArticleSearch SearchModel = new Model.MMemberArticleSearch();
            //SearchModel.MemberId = MemberId;
            SearchModel.TeamId = TeamId;
            SearchModel.IsEnable = true;
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.KeyWords = Server.UrlDecode(KeyWord.Trim());
            }
            var list = BArticle.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptLogList.DataSource = list;
                this.rptLogList.DataBind();
            }
        }
    }
}