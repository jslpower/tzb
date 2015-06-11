using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Match
{
    /// <summary>
    /// 赛事报名
    /// </summary>
    public partial class SignUp : System.Web.UI.Page
    {
        protected int PlayersMin = 0, PlayersMax = 0, BabysMin = 0, BabysMax = 0;
        protected int MaxJoinNumber = 0;
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
                DoConfirm();
                Response.End(); 
                return;
            }
            if (!IsPostBack)
            {
                BMemberApp.LoginCheck();
                string MatchId = Request.QueryString["Id"];
                if (!String.IsNullOrWhiteSpace(MatchId))
                {
                    InitMatch(MatchId);
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(49), "/WX/Match/List.aspx");
                    return; 
                }                
            }
        }
        /// <summary>
        /// 保存参赛 信息
        /// </summary>
        private void DoConfirm()
        {
            var matchid = Utils.GetQueryStringValue("Id");
            var match = BMatch.GetModel(matchid);
            if (match == null)
            {
                Response.Write("{\"ret\":\"0\",\"msg\":\"该赛事不存在！\"}");
                return;
            }
            else
            {
                string FieldId = Utils.GetFormValue(ddlFieldId.UniqueID);
                #region 判断
                if (String.IsNullOrWhiteSpace(FieldId) || FieldId == "0")
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"请选择比赛球场！\"}");
                    return;
                }
                if (match.SignBeginDate > DateTime.Now)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"该赛事报名还未开始，请留意报名通知！\"}");
                    return;
                }
                if (match.SignEndDate < DateTime.Now)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"该赛事报名已结束！\"}");
                    return;
                }
                var AuthModel = BMemberAuth.GetUserModel();
                if (AuthModel == null)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"该用户不存在！\"}");
                    return;
                }
                var MemberModel = BMember.GetModelByOpenId(AuthModel.OpenId);
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
                if (match.IsCityLimit && match.CityId != ballteam.CityId)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"球队不在同一城市不能报名！\"}");
                    return;
                }
                #endregion

                string[] HidTMid = Utils.GetFormValues("hidTMId");
                List<tbl_MatchTeamMember> MTMList = new List<tbl_MatchTeamMember>();
                string matchteamid = Guid.NewGuid().ToString();
                //参赛队伍信息
                BMatchTeam.Add(new tbl_MatchTeam()
                {
                    Id = matchteamid,
                    MatchId = matchid,
                    TeamId = TeamModel.TeamId,
                    TeamName = TeamModel.TeamName,
                    TeamOwner = MemberModel.ContactName,
                    JoinNumber = HidTMid.Length,
                    DepositMoney = match.RegistrationFee,
                    DepositOverage = match.RegistrationFee,
                    State = (int)Enow.TZB.Model.EnumType.参赛审核状态.资格审核中,
                    IssueTime = DateTime.Now
                });
                //队长参赛信息
                MTMList.Add(new tbl_MatchTeamMember()
                {
                    Id = Guid.NewGuid().ToString(),
                    MatchTeamId = matchteamid,
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
                                MatchTeamId = matchteamid,
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
                    BMatchTeam.Update(matchteamid, Model.EnumType.参赛审核状态.资格审核中, MTMList.Count(), FieldId);
                    BMatchTeamMember.Add(MTMList);
                    Response.Write("{\"ret\":\"1\",\"msg\":\"报名成功！\"}");
                }
                else
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"报名失败！\"}");
                }
            }
        }
        #region 加载赛事信息
        /// <summary>
        /// 加载赛事信息
        /// </summary>
        /// <param name="Id"></param>
        private void InitMatch(string Id)
        {
            var model = BMatch.GetModel(Id);
            if (model != null)
            {                

                if (model.SignBeginDate > DateTime.Now)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"该赛事报名还未开始，请留意报名通知！\"}");
                    return;
                }
                if (model.SignEndDate < DateTime.Now)
                {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"该赛事报名已结束！\"}");
                    return;
                }
                if (model.SignUpNumber >= model.TeamNumber) {
                    Response.Write("{\"ret\":\"0\",\"msg\":\"参赛队伍已满，请参加其他比赛！\"}");
                    return;
                }
                PlayersMin = model.PlayersMin;
                PlayersMax = model.PlayersMax;
                BabysMin = model.BayMin;
                if (model.BayMax.HasValue)
                    BabysMax = model.BayMax.Value;
                MaxJoinNumber = model.PlayersMax;
                this.ltrMatchName.Text = model.MatchName;
                this.ltrStartDate.Text = model.BeginDate.ToString("yyyy-MM-dd HH:mm:ss");
                this.ltrCityName.Text = model.CityName + "-" + model.AreaName;
                this.ltrFee.Text = model.RegistrationFee.ToString("F0");
                this.ltrTeamNumber.Text = "<span id=\"spanSignUpNumber\">1</span>/" + model.PlayersMax;
                InitModel(Id);
                var list = BMatchField.GetList(Id);
                if (list.Count() > 0)
                {
                    this.ddlFieldId.DataSource = list;
                    this.ddlFieldId.DataTextField = "FieldName";
                    this.ddlFieldId.DataValueField = "FieldId";
                    this.ddlFieldId.DataBind();
                    this.ddlFieldId.Items.Insert(0, new ListItem("请选择球场", "0"));
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(49), "/WX/Member/Default.aspx");
                return;
            }
        }
        #endregion
        #region 加载球队基本信息
        /// <summary>
        /// 加载球队基本信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitModel(string MatchId)
        {
            var MemberModel = BMemberApp.GetUserModel();
            if (MemberModel != null)
            {
                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                string MemberId = MemberModel.Id;
                string TeamId = "";
                //查询自己所在的球队
                var TeamModel = BTeamMember.GetModel(MemberId);
                if (TeamModel != null)
                {
                    TeamId = TeamModel.TeamId;
                    var MemberRole = (Model.EnumType.球员角色)TeamModel.RoleType;
                    if (MemberRole != Model.EnumType.球员角色.队长)
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(48), "/WX/Member/Default.aspx");
                        return;
                    }
                    var model = BTeam.GetModel(TeamId);
                    if (model != null)
                    {
                        string ErrMsg = "";
                        string Url = "";
                        var TeamState = (Model.EnumType.球队审核状态)model.State;
                        switch (model.State)
                        {
                            case (int)Model.EnumType.球队审核状态.审核中:
                            case (int)Model.EnumType.球队审核状态.初审通过:
                                Url = "/WX/Member/Default.aspx";
                                ErrMsg = CacheSysMsg.GetMsg(5);
                                break;
                            case (int)Model.EnumType.球队审核状态.初审拒绝:
                            case (int)Model.EnumType.球队审核状态.终审拒绝:
                                Url = "/WX/Team/TeamUpdate.aspx";
                                ErrMsg = CacheSysMsg.GetMsg(46);
                                break;
                            case (int)Model.EnumType.球队审核状态.解散申请:
                                Url = "/WX/Member/Default.aspx";
                                ErrMsg = CacheSysMsg.GetMsg(45);
                                break;
                            case (int)Model.EnumType.球队审核状态.解散通过:
                                Url = "/WX/Member/Default.aspx";
                                ErrMsg = CacheSysMsg.GetMsg(7);
                                break;
                            //case (int)Model.EnumType.球队审核状态.终审通过:
                            //    break;
                        }
                        if (!String.IsNullOrWhiteSpace(ErrMsg))
                        {
                            MessageBox.ShowAndRedirect(ErrMsg, Url);
                            return;
                        }
                        //判断是否参赛
                        if (BMatchTeam.IsExists(MatchId, TeamId) == true)
                        {
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(54), "/WX/Member/Default.aspx");
                            return;
                        }
                        else
                        {
                            InitList(TeamId);
                        }
                    }
                    else
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(6));
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(7), "/WX/Member/Default.aspx");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
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
                    ltrOperation.Text = string.Format("<a href=\"javascript:void(0)\" class=\"gray join\" dataroletype=\"{1}\" dataId=\"{0}\">未参加</a>", row.MemberId,row.RoleType);
                }
            }
        }
        #endregion 
    }
}