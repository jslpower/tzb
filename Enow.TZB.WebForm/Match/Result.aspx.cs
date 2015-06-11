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
    /// 赛事战报
    /// </summary>
    public partial class Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Id = Utils.GetQueryStringValue("ScheduleId");

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
            var model = BMatchSchedule.GetModel(Id);
            if (model != null)
            {
                Page.Title = model.MatchName + "-" + model.GameName + "-赛事战报";
                this.ltrMatchName.Text = model.MatchName;
                this.ltrGameName.Text = model.GameName;
                this.ltrON.Text = model.OrdinalNumber.ToString();
                this.ltrMatchTime.Text = model.GameStartTime.ToString("yyyy年MM月dd日 HH:mm") + "-" + model.GameEndTime.ToString("HH:mm");
                this.ltrFieldName.Text = model.FieldName;
                var HomeTeam = BTeam.GetModel(model.HomeTeamId);
                if (HomeTeam != null) {
                    ltrHomeTeamPic.Text = "<img src=\"" + HomeTeam.TeamPhoto+ "\" width=\"383\" height=\"206\">";
                    ltrHomeTeamName.Text = HomeTeam.TeamName;
                }
                var AwayTeam = BTeam.GetModel(model.AwayTeamId);
                if (AwayTeam != null)
                {
                    ltrAwayTeamPic.Text = "<img src=\"" + AwayTeam.TeamPhoto + "\" width=\"383\" height=\"206\">";
                    ltrAwayTeamName.Text = AwayTeam.TeamName;
                }
                this.ltrHomeGoals.Text = model.HomeGoals.ToString();
                this.ltrAwayGoals.Text = model.AwayGoals.ToString();
                #region 成员得分
                var TechnicalList = BMatchMemberTechnical.GetList(Id);
                //主队
                var HomeMemberList = BMatchTeamMember.GetListByMatchId(model.MatchId,model.HomeMatchTeamId);
                string TmpStr = "";
                if (HomeMemberList != null) {
                    foreach (var m in HomeMemberList)
                    {
                        TmpStr += "<tr><td>" + m.ContactName + "</td><td>" + m.JerseyNumber.ToString() + "</td>";
                        //查询球员进球成绩
                        if (TechnicalList != null)
                        {
                            var MemberTechnical = TechnicalList.Where(n => n.MatchTeamMemberId == m.TeamMemberId);
                            if (MemberTechnical != null)
                            {
                                var t1 = MemberTechnical.Where(n => n.TypeId == (int)Model.EnumType.进球类型.上半场进球 || n.TypeId == (int)Model.EnumType.进球类型.上半场点球 || n.TypeId == (int)Model.EnumType.进球类型.下半场进球 || n.TypeId == (int)Model.EnumType.进球类型.下半场点球 || n.TypeId == (int)Model.EnumType.进球类型.加时进球).Sum(o => o.Technicals);
                                TmpStr += "<td>" + t1.ToString() + "</td>";
                                var t2 = MemberTechnical.Where(n => n.TypeId == (int)Model.EnumType.进球类型.上半场犯规 || n.TypeId == (int)Model.EnumType.进球类型.下半场犯规).Sum(o => o.Technicals);
                                TmpStr += "<td>" + t2.ToString() + "</td>";
                                var t3 = MemberTechnical.Where(n => n.TypeId == (int)Model.EnumType.进球类型.上半场红牌 || n.TypeId == (int)Model.EnumType.进球类型.下半场红牌).Sum(o => o.Technicals);
                                TmpStr += "<td>" + t3.ToString() + "</td>";
                                var t4 = MemberTechnical.Where(n => n.TypeId == (int)Model.EnumType.进球类型.上半场黄牌 || n.TypeId == (int)Model.EnumType.进球类型.下半场黄牌).Sum(o => o.Technicals);
                                TmpStr += "<td>" + t4.ToString() + "</td>";
                            }
                            else
                            {
                                TmpStr += "<td></td><td></td><td></td><td></td>";
                            }
                        }
                        else
                        {
                            TmpStr += "<td></td><td></td><td></td><td></td>";
                        }
                        //<td>11</td><td></td><td>2</td><td>1</td>
                        TmpStr += "</tr>";
                    }
                }
                this.ltrHomeMember.Text = TmpStr;
                //客队
                var AwayMemberList = BMatchTeamMember.GetListByMatchId(model.MatchId, model.AwayMatchTeamId);
                TmpStr = "";
                if (AwayMemberList != null)
                {
                    foreach (var m in AwayMemberList)
                    {
                        TmpStr += "<tr><td>" + m.ContactName + "</td><td>" + m.JerseyNumber.ToString() + "</td>";
                        //查询球员进球成绩
                        if (TechnicalList != null)
                        {
                            var MemberTechnical = TechnicalList.Where(n => n.MatchTeamMemberId == m.TeamMemberId);
                            if (MemberTechnical != null)
                            {
                                var t1 = MemberTechnical.Where(n => n.TypeId == (int)Model.EnumType.进球类型.上半场进球 || n.TypeId == (int)Model.EnumType.进球类型.上半场点球 || n.TypeId == (int)Model.EnumType.进球类型.下半场进球 || n.TypeId == (int)Model.EnumType.进球类型.下半场点球 || n.TypeId == (int)Model.EnumType.进球类型.加时进球).Sum(o => o.Technicals);
                                TmpStr += "<td>" + t1.ToString() + "</td>";
                                var t2 = MemberTechnical.Where(n => n.TypeId == (int)Model.EnumType.进球类型.上半场犯规 || n.TypeId == (int)Model.EnumType.进球类型.下半场犯规).Sum(o => o.Technicals);
                                TmpStr += "<td>" + t2.ToString() + "</td>";
                                var t3 = MemberTechnical.Where(n => n.TypeId == (int)Model.EnumType.进球类型.上半场红牌 || n.TypeId == (int)Model.EnumType.进球类型.下半场红牌).Sum(o => o.Technicals);
                                TmpStr += "<td>" + t3.ToString() + "</td>";
                                var t4 = MemberTechnical.Where(n => n.TypeId == (int)Model.EnumType.进球类型.上半场黄牌 || n.TypeId == (int)Model.EnumType.进球类型.下半场黄牌).Sum(o => o.Technicals);
                                TmpStr += "<td>" + t4.ToString() + "</td>";
                            }
                            else
                            {
                                TmpStr += "<td></td><td></td><td></td><td></td>";
                            }
                        }
                        else
                        {
                            TmpStr += "<td></td><td></td><td></td><td></td>";
                        }
                        //<td>11</td><td></td><td>2</td><td>1</td>
                        TmpStr += "</tr>";
                    }
                }
                this.ltrAwayMember.Text = TmpStr;
                #endregion
                var ResultArticleModel = BMatchSchedultReport.GetModel(Id);
                if (ResultArticleModel != null) {
                    this.ltrArticleTitle.Text = ResultArticleModel.Title;
                    this.ltrResutlArticleTime.Text = ResultArticleModel.IssueTime.ToString("yyyy-mm-dd HH:mm:ss");
                    this.ltrContentInfo.Text = ResultArticleModel.ContentInfo;
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
    }
}