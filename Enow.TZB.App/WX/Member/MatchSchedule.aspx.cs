using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.Model;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Member
{
    /// <summary>
    /// 赛事赛程
    /// </summary>
    public partial class MatchSchedule : System.Web.UI.Page
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
        /// 加载赛事信息
        /// </summary>
        /// <param name="Id"></param>
        private void InitMatchInfo(string Id)
        {
            var MemberMatchModel = BMatchTeamMember.GetMemberMatchModel(Id);
            if (MemberMatchModel != null)
            {
                if (MemberMatchModel.State < (int)Model.EnumType.参赛审核状态.已获参赛权)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(134), "MatchDetail.aspx?Id=" + Id);
                    return;
                }
                else
                {
                    this.ltrMatchName.Text = MemberMatchModel.MatchName;
                    InitMatchGame(MemberMatchModel.MatchId);
                    if (MemberMatchModel.RoleType == (int)Model.EnumType.球员角色.队长)
                    {
                        if (BMatchTeamCode.IsBallot(MemberMatchModel.MatchTeamId) == true)
                        {
                            this.phIsBallot.Visible = false;
                        }
                        else { this.phIsBallot.Visible = true; }
                    }
                    else
                    {
                        this.phIsBallot.Visible = false;
                    }
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
        /// 加载赛事赛程信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitMatchSchedule(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
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
        /// <summary>
        /// 抽签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnBallot_Click(object sender, EventArgs e)
        {
            BMemberApp.LoginCheck();
            string Id = Utils.GetQueryStringValue("Id");
            if (!String.IsNullOrWhiteSpace(Id))
            {
                var MemberMatchModel = BMatchTeamMember.GetMemberMatchModel(Id);
                if (MemberMatchModel != null)
                {
                    var model = BMatch.GetModel(MemberMatchModel.MatchId);
                    if (model != null)
                    {                       
                        if (MemberMatchModel.RoleType == (int)Model.EnumType.球员角色.队长)
                        {
                            string MatchCode = "";
                            BMatchTeamCode.UpdateBallot(MemberMatchModel.MatchTeamId, ref MatchCode);
                            bool bllRetCode = BMatchTeam.UpdateValid(MemberMatchModel.MatchTeamId, Model.EnumType.参赛审核状态.已抽签, 0, "", 0);
                            MessageBox.ShowAndRedirect(string.Format(CacheSysMsg.GetMsg(133), MatchCode), "MatchTeamBallotResult.aspx?Id=" + Id);
                        }
                    }
                    else
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                    }
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
    }
}