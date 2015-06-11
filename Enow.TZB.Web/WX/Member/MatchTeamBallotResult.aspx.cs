#region 命名空间
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.Model;
using Enow.TZB.BLL;
#endregion

namespace Enow.TZB.Web.WX.Member
{
    /// <summary>
    /// 赛程抽签结果
    /// </summary>
    public partial class MatchTeamBallotResult : System.Web.UI.Page
    {
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
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitMember(OpenId);
                string Id = Utils.GetQueryStringValue("Id");
                if (!String.IsNullOrWhiteSpace(Id))
                {
                    InitMatchInfo(Id);
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember(string OpenId)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 加载赛事信息
        /// </summary>
        /// <param name="Id"></param>
        private void InitMatchInfo(string Id)
        {
            var MemberMatchModel = BMatchTeamMember.GetMemberMatchModel(Id);
            if (MemberMatchModel != null)
            {
                if (MemberMatchModel.State < (int)Model.EnumType.参赛审核状态.已获参赛权) {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(134), "MatchDetail.aspx?Id=" + Id);
                    return;
                }
                else
                {
                    if (BMatchTeamCode.IsBallot(MemberMatchModel.MatchTeamId) == false)
                    {
                        Response.Redirect("MatchSchedule.aspx?Id=" + Id, true);
                        return;
                    }
                    this.ltrMatchName.Text = MemberMatchModel.MatchName;
                    InitMatchGame(MemberMatchModel.MatchId);
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
        /// <summary>
        /// 加载赛程列表
        /// </summary>
        /// <param name="MatchId"></param>
        private void InitMatchGame(string MatchId)
        {
            this.rptGameList.DataSource = BMatchGame.GetList(MatchId);
            this.rptGameList.DataBind();
        }
        /// <summary>
        /// 显示球队对战名称
        /// </summary>
        /// <param name="MatchTeamId"></param>
        /// <param name="TeamName"></param>
        /// <param name="MatchTeamCode"></param>
        /// <returns></returns>
        protected string TeamCodeView(string MatchTeamId, string TeamName, string MatchTeamCode)
        {
            if (MatchTeamId == "0" || String.IsNullOrEmpty(MatchTeamId)) { return MatchTeamCode; }
            else
            {
                if (BMatchTeamCode.IsBallot(MatchTeamId) == true)
                    return TeamName;
                else
                    return MatchTeamCode;
            }
        }
        /// <summary>
        /// 对战表显示
        /// </summary>
        /// <param name="HomeTeamName"></param>
        /// <param name="HomeMatchTeamCode"></param>
        /// <param name="HomeIsBallot"></param>
        /// <param name="AwayTeamName"></param>
        /// <param name="AwayMatchTeamCode"></param>
        /// <param name="AwayIsBallot"></param>
        /// <param name="PublishState"></param>
        /// <returns></returns>
        protected string MatchTeamScheduleView(string ScheduleId, string HomeTeamName, string HomeMatchTeamCode, bool HomeIsBallot, string AwayTeamName, string AwayMatchTeamCode, bool AwayIsBallot, int PublishState)
        {
            string str = "<div class=\"cent\">";
            if (HomeIsBallot)
            {
                str = str + HomeTeamName;
            }
            else
            {
                str = str + HomeMatchTeamCode;
            }
            str = str + "<br />VS<br />";
            if (AwayIsBallot)
            {
                str = str + AwayTeamName;
            }
            else
            {
                str = str + AwayMatchTeamCode;
            }
            str = str + "</div>";
            if (PublishState == 1) {
                str = str + "<div class=\"user-bi floatR\"><span><a href=\"/WX/Match/Result.aspx?Id=" + Utils.GetQueryStringValue("Id") + "&ScheduleId=" + ScheduleId + "\">查看战报</a></span>&nbsp;&nbsp;&nbsp;&nbsp;</div><div class=\"clearfix\"></div>";
            }
            return str;
        }
        /// <summary>
        /// 加载赛事赛程信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitMatchSchedule(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = (tbl_MatchGame)e.Item.DataItem;
                Repeater rptScheduleList = (Repeater)e.Item.FindControl("rptScheduleList");
                string GameId = row.Id;
                string MatchId = row.MatchId;
                row = null;
                rptScheduleList.DataSource = BMatchSchedule.GetList(new MMatchScheduleSearch
                {
                    MatchId = MatchId,
                    GameId = GameId,
                    GameState = Model.EnumType.赛程状态.发布
                });
                rptScheduleList.DataBind();
            }
        }
    }
}