using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using System.Data;

namespace Enow.TZB.Web.Manage.Match
{
    public partial class BallotList : System.Web.UI.Page
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
            string doType = Utils.GetQueryStringValue("doType");
            switch (doType)
            {
                case "Ballot":
                    TeamBallot();
                    return;
                    break;
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                InitList();
            }
        }
        #region 初始化
        /// <summary>
        /// 绑定列表
        /// </summary>
        private void InitList()
        {
            int rowsCount = 0;
            string Page = Request.QueryString["Page"];
            string keyword = Utils.GetQueryStringValue("KeyWord");
            string matchName = Utils.GetQueryStringValue("MatchName");
            DateTime? StartDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("StartDate"));
            DateTime? EndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EndDate"));
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                CurrencyPage = int.Parse(Page);
                if (CurrencyPage < 1)
                    CurrencyPage = 1;
            }
            MMatchTeamSearch searchModel = new MMatchTeamSearch();
            searchModel.State = Model.EnumType.参赛审核状态.已获参赛权;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                searchModel.TeamName = keyword;
            }
            if (!string.IsNullOrWhiteSpace(matchName))
            {
                searchModel.MatchName = matchName;
            }
            if (StartDate.HasValue)
            {
                searchModel.StartDate = StartDate;
            }
            if (EndDate.HasValue)
            {
                searchModel.EndDate = EndDate;
            }
            List<dt_MatchTeam> list = BMatchTeam.GetList(ref rowsCount, intPageSize, CurrencyPage, searchModel);
            if (rowsCount > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                this.ExportPageInfo1.LinkType = 3;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = rowsCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string KeyWord = this.txtKeyWord.Text;
            string MatchName = this.txtMatch.Text;
            string StartDate = this.txtStartDate.Text;
            string EndDate = this.txtEndDate.Text;
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&MatchName=" + Server.UrlEncode(MatchName) + "&KeyWord=" + Server.UrlEncode(KeyWord) + "&StartDate=" + StartDate + "&EndDate=" + EndDate, true);
        }
        #endregion
        #region 团队抽签
        /// <summary>
        /// 团队抽签
        /// </summary>
        private void TeamBallot()
        {
            string id = Utils.GetQueryStringValue("id");
            if (!String.IsNullOrWhiteSpace(id)) {
                string[] arrId = id.Split(',');
                string MatchCode = "";
                foreach (string s in arrId)
                {
                    BMatchTeamCode.UpdateBallot(s, ref MatchCode);
                    BMatchTeam.UpdateValid(s, Model.EnumType.参赛审核状态.已抽签, 0, "", 0);
                }
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            }
        }
        #endregion
    }
}