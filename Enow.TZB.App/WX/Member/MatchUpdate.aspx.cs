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
    /// 参赛信息修改
    /// </summary>
    public partial class MatchUpdate : System.Web.UI.Page
    {
        protected int PlayersMin = 0, PlayersMax = 0, BabysMin = 0, BabysMax = 0;
        protected int MaxJoinNumber = 0,hidPalyerNumber = 1,hidBabyNumber = 0;
        /// <summary>
        /// 参赛队员列表
        /// </summary>
        protected List<dt_MatchTeamMember> JoinList = null;
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("dotype") == "doconfirm")
            {
                Response.Clear();
                Save();
                Response.End();
                return;
            }
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
        #region 保存
        /// <summary>
        /// 保存参赛 信息
        /// </summary>
        private void Save()
        {
            var Id = Utils.GetQueryStringValue("Id");            
            var MemberMatchModel = BMatchTeamMember.GetMemberMatchModel(Id);
            if (MemberMatchModel == null)
            {
                Response.Write("{\"ret\":\"0\",\"msg\":\"该赛事不存在！\"}");
                return;
            }
            else
            {
                string FieldId = Utils.GetFormValue(ddlFieldId.UniqueID);
                string MatchTeamId = MemberMatchModel.MatchTeamId;
                #region 判断
                if (String.IsNullOrWhiteSpace(FieldId) || FieldId == "0")
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"请选择比赛球场！\"}");
                    return;
                }
                if (MemberMatchModel.SignBeginDate > DateTime.Now)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"该赛事报名还未开始，请留意报名通知！\"}");
                    return;
                }
                if (MemberMatchModel.SignEndDate < DateTime.Now)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"该赛事报名已结束！\"}");
                    return;
                }

                var MemberModel = BMemberApp.GetUserModel();
                if (MemberModel == null)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"该会员不存在！\"}");
                    return;
                }
                var TeamModel = BTeamMember.GetModel(MemberModel.Id);
                if (TeamModel == null)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"该成员不存在！\"}");
                    return;
                }
                if (TeamModel.RoleType != (int)Model.EnumType.球员角色.队长)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"非队长不能报名！\"}");
                    return;
                }
                var ballteam = BTeam.GetModel(TeamModel.TeamId);
                if (ballteam == null)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"该球队不存在！\"}");
                    return;
                }
                if (MemberMatchModel.IsCityLimit && MemberMatchModel.CityId != ballteam.CityId)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"球队不在同一城市不能报名！\"}");
                    return;
                }
                #endregion
                //删除原参赛人员信息
                BMatchTeamMember.Delete(MatchTeamId);
                string[] HidTMid = Utils.GetFormValues("hidTMId");
                List<tbl_MatchTeamMember> MTMList = new List<tbl_MatchTeamMember>();
                //队长参赛信息
                MTMList.Add(new tbl_MatchTeamMember()
                {
                    Id = Guid.NewGuid().ToString(),
                    MatchTeamId = MatchTeamId,
                    RoleType = TeamModel.RoleType,
                    TeamMemberId = TeamModel.Id,
                    MemberId = TeamModel.MemberId,
                    JerseyNumber = TeamModel.DNQYHM,
                    PlayerPosition = TeamModel.DNWZ,
                    IssueTime = DateTime.Now
                });
                //队员参赛信息
                foreach (string TMId in HidTMid)
                {
                    if (TMId != "0")
                    {
                        var m = BTeamMember.GetModel(TMId);
                        if (m != null)
                        {
                            MTMList.Add(new tbl_MatchTeamMember()
                            {
                                Id = Guid.NewGuid().ToString(),
                                MatchTeamId = MatchTeamId,
                                RoleType = m.RoleType,
                                TeamMemberId = m.Id,
                                MemberId = m.MemberId,
                                JerseyNumber = m.DNQYHM,
                                PlayerPosition = m.DNWZ,
                                IssueTime = DateTime.Now
                            });
                        }
                    }
                }

                if (MTMList != null && MTMList.Count > 0)
                {
                    //二次报名更新参赛队伍审核状态和参赛人数
                    BMatchTeam.Update(MatchTeamId, Model.EnumType.参赛审核状态.资格审核中, MTMList.Count(), FieldId);
                    BMatchTeamMember.Add(MTMList);
                    Response.Write("{\"ret\":\"1\",\"msg\":\"报名成功！\"}");
                }
                else
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"报名失败！\"}");
                }
            }
        }
        #endregion
        #region 赛事初始化
        /// <summary>
        /// 加载赛事信息
        /// </summary>
        /// <param name="Id"></param>
        private void InitMatchInfo(string Id)
        {
            var MemberMatchModel = BMatchTeamMember.GetMemberMatchModel(Id);
            if (MemberMatchModel != null)
            {
                if (MemberMatchModel.State > (int)Model.EnumType.参赛审核状态.资格审核通过) {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(56),"/WX/Member/");
                    return;
                }
                var model = BMatch.GetModel(MemberMatchModel.MatchId);
                if (model != null)
                {
                    this.MasterOrganizer.Text = model.MasterOrganizer;
                    this.CoOrganizers.Text = model.CoOrganizers;
                    this.Organizer.Text = model.Organizer;
                    this.Sponsors.Text = model.Sponsors;
                    this.ltrSignUpTime.Text = model.SignBeginDate.ToString("yyyy-MM-dd") + " 至 " + model.SignEndDate.ToString("yyyy-MM-dd");
                    this.BreakTime.Text = model.BreakTime.ToString();
                    this.SignUpNumber.Text = model.SignUpNumber.ToString() + "/" + model.PlayersMax.ToString();
                    this.TotalTime.Text = model.TotalTime.ToString();
                    this.BayMax.Text = model.BayMax.ToString();
                    this.BayMin.Text = model.BayMin.ToString();
                    this.MaxAge.Text = model.MaxAge.ToString();
                    this.MinAge.Text = model.MinAge.ToString();
                    this.Remark.Text = model.Remark;
                    this.ltrTeamNumber.Text = "<span id=\"spanSignUpNumber\">" + MemberMatchModel.JoinNumber.ToString() + "</span>" + "/" + model.PlayersMax.ToString();
                    JoinList = BMatchTeamMember.GetListByMatchId(MemberMatchModel.MatchId, MemberMatchModel.MatchTeamId);
                    InitList(MemberMatchModel.TeamId);

                    PlayersMin = model.PlayersMin;
                    PlayersMax = model.PlayersMax;
                    BabysMin = model.BayMin;
                    if (model.BayMax.HasValue)
                        BabysMax = model.BayMax.Value;
                    MaxJoinNumber = model.PlayersMax;
                    this.ltrFieldAddress.Text = MemberMatchModel.FieldAddress;
                    var list = BMatchField.GetList(MemberMatchModel.MatchId);
                    if (list.Count() > 0)
                    {
                        this.ddlFieldId.DataSource = list;
                        this.ddlFieldId.DataTextField = "FieldName";
                        this.ddlFieldId.DataValueField = "FieldId";
                        this.ddlFieldId.DataBind();
                        this.ddlFieldId.Items.Insert(0, new ListItem("请选择球场", "0"));
                        if (!String.IsNullOrWhiteSpace(MemberMatchModel.MatchFieldId)) {
                            this.ddlFieldId.Items.FindByValue(MemberMatchModel.MatchFieldId).Selected = true;
                        }
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
        #endregion
        #region 加载成员列表
        /// <summary>
        /// 加载成员列表
        /// </summary>
        /// <param name="TeamId"></param>
        private void InitList(string TeamId)
        {
            var list = BTeamMember.GetCheckList(TeamId);
            if (list != null)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
        /// <summary>
        /// 检查会员是否加入
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        private bool JoinCheck(string MemberId) {
            var m = JoinList.FirstOrDefault(n => n.MemberId == MemberId);
            if (m != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 绑定球员列表操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitOperation(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = (dt_TeamMember)e.Item.DataItem;
                Literal ltrOperation = (Literal)e.Item.FindControl("ltrOperation");
                if (row.RoleType == (int)Model.EnumType.球员角色.队长)//队长
                {
                    ltrOperation.Text = "<a>已参加</a>";
                }
                else
                {
                    if (JoinCheck(row.MemberId)==true) {
                        ltrOperation.Text = string.Format("<a href=\"javascript:void(0)\" class=\"join\" dataroletype=\"{1}\" dataId=\"{0}\">已参加</a><input type=\"hidden\" id=\"hidTMId\" name=\"hidTMId\" value=\"{0}\" />", row.MemberId, row.RoleType);
                        if (row.RoleType == (int)Model.EnumType.球员角色.队员)
                        {
                            hidPalyerNumber = hidPalyerNumber + 1;
                        }
                        else {
                            hidBabyNumber = hidBabyNumber + 1;
                        }
                    }
                    else
                    {
                        ltrOperation.Text = string.Format("<a href=\"javascript:void(0)\" class=\"gray join\" dataroletype=\"{1}\" dataId=\"{0}\">未参加</a><input type=\"hidden\" id=\"hidTMId\" name=\"hidTMId\" value=\"0\" />", row.MemberId, row.RoleType);
                    }
                }
            }
        }
        #endregion 
    }
}