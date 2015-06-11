using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.Match
{
    /// <summary>
    /// 赛事详情页
    /// </summary>
    public partial class Detail : System.Web.UI.Page
    {
        //图片裁剪后保存的文件夹
        protected const string DIRPATH = "/ufiles/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Id = Utils.GetQueryStringValue("id");

                if (!string.IsNullOrWhiteSpace(Id))
                {
                    InitPage(Id);

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
        private void InitPage(string Id)
        {
            this.Master.Page.Title = "联赛杯赛-赛事详情";
            var model = BMatch.GetModel(Id);
            if (model != null)
            {
                ltrMatchName.Text = model.MatchName;
                ltrSignDate.Text = model.SignBeginDate.ToString("yyyy年MM月dd日") + " 至 " + model.SignEndDate.ToString("yyyy年MM月dd日");
                ltrMatchDate.Text = model.BeginDate.ToString("yyyy年MM月dd日") + " 至 " + model.EndDate.ToString("yyyy年MM月dd日");
                ltrMatchArea.Text = model.CountryName + "-" + model.ProvinceName + "-" + model.CityName + "-" + model.AreaName;
                ltrEarnestMoney.Text = model.EarnestMoney.ToString("C2") + "元"; ;
                ltrDepositMoney.Text = model.RegistrationFee.ToString("C2") + "元";
                ltrMasterOrganizer.Text = model.MasterOrganizer.ToString();
                if (!string.IsNullOrWhiteSpace(model.CoOrganizers))
                {
                    ltrCoOrganizers.Text = "协办方：" + model.CoOrganizers.ToString();
                }
                else
                {
                    ltrCoOrganizers.Text = "&nbsp;&nbsp;";
                }
                if (!string.IsNullOrWhiteSpace(model.Organizer))
                {
                    ltrOrganizer.Text = "承办方：" + model.Organizer.ToString();
                }
                else
                {
                    ltrOrganizer.Text = "&nbsp;&nbsp;";
                }
                if (!string.IsNullOrWhiteSpace(model.Sponsors))
                {

                    ltrSponsors.Text = "赞助方：" + model.Sponsors.ToString();
                }
                else
                {
                    ltrSponsors.Text = "&nbsp;&nbsp;";
                }

                ltrSignUpNumber.Text = model.PlayersMin.ToString() + "/" + model.PlayersMax.ToString() + "人";
                ltrBayNumber.Text = model.BayMin.ToString() + "/" + model.BayMax.ToString() + "人";
                ltrTotalTime.Text = model.TotalTime.ToString() + "分钟";
                ltrBreakTime.Text = model.BreakTime.ToString() + "分钟";
                ltrRemark.Text = model.Remark;
                if (!string.IsNullOrWhiteSpace(model.MatchPhoto))
                {
                    LtrMatchPhoto.Text = "<img src=" + model.MatchPhoto + " width=\"211px\" height=\"131px\">";
                }


                if (model.SignBeginDate <= DateTime.Now && model.SignEndDate >= DateTime.Now)
                {
                    var MemberModel = BWebMemberAuth.GetUserModel();
                    if (MemberModel != null)
                    {
                        //查询自己所在的球队
                        var TeamModel = BTeamMember.GetModel(MemberModel.Id);

                        if (TeamModel != null)
                        {
                            //判断是否参赛
                            if (BMatchTeam.IsExists(Id, TeamModel.TeamId) == false)
                            {
                                this.PhSignUp.Visible = true;
                            }
                            else
                            {
                                this.PhSignUp.Visible = false;
                            }
                            TeamModel = null;
                        }
                    }
                }
                else
                {
                    this.PhSignUp.Visible = false;
                }
                InitMatchTeamList(Id);
                InitMatchGame(Id);
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
        /// <summary>
        /// 加载球队列表信息
        /// </summary>
        private void InitMatchTeamList(string MatchId)
        {
            var List = BMatchTeam.GetMatchTeamList(MatchId);
            this.rptList.DataSource = List;
            this.rptList.DataBind();
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
        protected string MatchTeamScheduleView(string ScheduleId, string HomeTeamName, string HomeMatchTeamCode, bool HomeIsBallot, string AwayTeamName, string AwayMatchTeamCode, bool AwayIsBallot, int PublishState, int HomeGoals, int AwayGoals)
        {
            string str = "";
            if (HomeIsBallot)
            {
                str = str + HomeTeamName;
            }
            else
            {
                str = str + HomeMatchTeamCode;
            }
            if (PublishState == 1)
            {
                str = str + "&nbsp;&nbsp;" + HomeGoals.ToString() + "-" + AwayGoals.ToString() + "&nbsp;&nbsp;";
            }
            else { str = str + "&nbsp;VS&nbsp;"; }
            if (AwayIsBallot)
            {
                str = str + AwayTeamName;
            }
            else
            {
                str = str + AwayMatchTeamCode;
            }
            return str;
        }
        /// <summary>
        /// 战报显示
        /// </summary>
        /// <param name="PublishState"></param>
        /// <returns></returns>
        protected string ResultView(string ScheduleId, int PublishState)
        {
            string str = "";
            if (PublishState == 1)
            {
                str = str + "<a href=\"Result.aspx?ScheduleId=" + ScheduleId + "\">战报</a>";
            }
            return str;
        }
    }
}