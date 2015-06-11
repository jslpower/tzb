using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.My
{
    public partial class MyTeam : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        /// 
        protected Model.EnumType.球员角色 MemberRole = Model.EnumType.球员角色.队员;
        protected Model.EnumType.球队审核状态 TeamState = Model.EnumType.球队审核状态.审核中;
        protected string MemberId = "";
        protected string tmId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Master.Page.Title = "我的球队";

                var model = BWebMemberAuth.GetUserModel();
                MemberId = model.Id;
                InitPage(MemberId);
                InitTeamer(MemberId);
                string toType = Utils.GetQueryStringValue("doType").ToLower();
                tmId = Utils.GetQueryStringValue("mid");
                string Mid = Utils.GetQueryStringValue("MemberId");
                switch (toType)
                {
                    case "out"://踢出球队
                        TeamerKickedOut(tmId);
                        break;
                    case "exists"://退出审核
                        TeamerExists(tmId);
                        break;
                    case "disband"://解散球队
                        TeamDisband();
                        break;
                    case "quit":
                        TeamQuit();//退出球队
                        break;
                    case "transfer"://队长转让
                        TeamTransfer(Mid);
                        break;
                    default:
                        break;
                }
            }

        }

        #region 球队转让
        /// <summary>
        /// 球队转让
        /// </summary>
        /// <param name="MemberId"></param>
        private void TeamTransfer(string MemberId)
        {
            if (!string.IsNullOrWhiteSpace(MemberId))
            {
                var MemberModel = BMember.GetModel(MemberId);
                if (MemberModel != null)
                {

                    var model = BWebMemberAuth.GetUserModel();
                    if (model != null)
                    {
                        string TeamId = "";
                        var TeamModel = BTeamMember.GetModel(model.Id);
                        if (TeamModel != null)
                        {
                            TeamId = TeamModel.TeamId;

                            var MemberRole = (Model.EnumType.球员角色)TeamModel.RoleType;
                            if (MemberRole == Model.EnumType.球员角色.队长)
                            {
                                //球队创建人身份变更
                                BTeam.CaptainTransfer(TeamId, MemberId, MemberModel.ContactName);
                                //队长身份变更
                                BTeamMember.UpdateRoleType(TeamId, MemberId, Model.EnumType.球员角色.队长);
                                //队长变更为队员
                                BTeamMember.UpdateRoleType(TeamId, model.Id, Model.EnumType.球员角色.队员);
                                MessageBox.ShowAndRedirect("队长身份转让成功!", "MyTeam.aspx");
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.ShowAndReload("未找到您要转让的队员信息");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndReload("未找到您要转让的队员信息");
                return;
            }
        }

        #endregion


        #region 退出球队
        /// <summary>
        /// 退出球队
        /// </summary>
        private void TeamQuit()
        {
            var MemberModel = BWebMemberAuth.GetUserModel();
            if (MemberModel != null)
            {
                string TeamId = "";
                //查询自己所在的球队
                var TeamModel = BTeamMember.GetModel(MemberModel.Id);
                if (TeamModel != null)
                {
                    TeamId = TeamModel.TeamId;
                    var MemberRole = (Model.EnumType.球员角色)TeamModel.RoleType;
                    if (MemberRole != Model.EnumType.球员角色.队长)
                    {
                        //退出申请
                        var model = BTeam.GetModel(TeamId);
                        if (model != null)
                        {
                            BTeamMember.UpdateState(TeamModel.Id, Model.EnumType.球员审核状态.退出申请);
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(12), "Default.aspx");
                            return;
                        }
                        else
                        {
                            MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(10));
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(12), "Default.aspx");
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(7), "Default.aspx");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "/Register/Register.aspx");
                return;
            }
        }
        #endregion

        #region 解散球队
        /// <summary>
        /// 解散球队
        /// </summary>
        /// <param name="tmId"></param>
        private void TeamDisband()
        {
            var MemberModel = BWebMemberAuth.GetUserModel();
            if (MemberModel != null)
            {
                string TeamId = "";
                //查询自己所在的球队
                var TeamModel = BTeamMember.GetModel(MemberModel.Id);
                if (TeamModel != null)
                {
                    TeamId = TeamModel.TeamId;
                    var MemberRole = (Model.EnumType.球员角色)TeamModel.RoleType;
                    if (MemberRole == Model.EnumType.球员角色.队长)
                    {
                        //解散申请
                        var model = BTeam.GetModel(TeamId);
                        if (model != null)
                        {
                            BTeam.UpdateState(TeamId, 0, "", Model.EnumType.球队审核状态.解散申请,"",0,"");
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(9), "Default.aspx");
                            return;
                        }
                        else
                        {
                            MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(10));
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(11), "Default.aspx");
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(7), "Default.aspx");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "/Register/Register.aspx");
                return;
            }
        }
        #endregion

        #region 退出球队审核
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mid">会员ID</param>
        private void TeamerExists(string tmId)
        {
            if (!string.IsNullOrWhiteSpace(tmId))
            {
                bool IsResult = BTeamMember.UpdateState(tmId, Model.EnumType.球员审核状态.同意退出);
                if (IsResult)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(18), "MyTeam.aspx");
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(19), "MyTeam.aspx");
                }

            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(43), "MyTeam.aspx");
            }
        }
        #endregion

        #region 踢出球队
        /// <summary>
        /// 踢出球队
        /// </summary>
        private void TeamerKickedOut(string tmId)
        {
            if (!string.IsNullOrWhiteSpace(tmId))
            {

                bool IsResult = BTeamMember.UpdateState(tmId, Model.EnumType.球员审核状态.踢除);
                if (IsResult)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(18), "MyTeam.aspx");
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(19), "MyTeam.aspx");
                }

            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(17), "MyTeam.aspx");
            }
        }
        #endregion


        /// <summary>
        /// 加载球队信息
        /// </summary>
        /// <param name="memberId"></param>
        private void InitPage(string memberId)
        {
            MemberId = memberId;
            string TeamId = "";
            //查询自己所在的球队
            var TeamModel = BTeamMember.GetModel(MemberId);
            if (TeamModel != null)
            {
                TeamId = TeamModel.TeamId;
                MemberRole = (Model.EnumType.球员角色)TeamModel.RoleType;
                if (MemberRole == Model.EnumType.球员角色.队长)
                {
                    phHide.Visible = true;
                  //  this.ltrDisband.Text = "<a id=\"disband\" data_id=\""+TeamId+"\" href=\"MyTeam.aspx?doType=disband\" class=\"yellow_btn\">解散球队</a>";
                    this.ltrDisband.Text = "<a id=\"disband\" data_id=\"" + TeamId + "\" href=\"#\" class=\"yellow_btn\">解散球队</a>";
                }
                else
                {
                    phHide.Visible = false;
                    this.ltrDisband.Text = "<a href=\"MyTeam.aspx?doType=quit\" class=\"yellow_btn\">退出球队</a>";
                }
                var model = BTeam.GetModel(TeamId);
                if (model != null)
                {
                    string ErrMsg = "";
                    string Url = "";
                    TeamState = (Model.EnumType.球队审核状态)model.State;
                    switch (model.State)
                    {
                        case (int)Model.EnumType.球队审核状态.审核中:
                        case (int)Model.EnumType.球队审核状态.初审通过:
                            Url = "Default.aspx";
                            ErrMsg = CacheSysMsg.GetMsg(5);
                            break;
                        case (int)Model.EnumType.球队审核状态.初审拒绝:
                        case (int)Model.EnumType.球队审核状态.终审拒绝:
                            Url = "/Team/TeamUpdate.aspx";
                            ErrMsg = CacheSysMsg.GetMsg(46);
                            break;
                        case (int)Model.EnumType.球队审核状态.解散申请:
                            Url = "Default.aspx";
                            ErrMsg = CacheSysMsg.GetMsg(45);
                            break;
                        case (int)Model.EnumType.球队审核状态.解散通过:
                            Url = "/Team/Default.aspx";
                            ErrMsg = CacheSysMsg.GetMsg(7);
                            break;

                    }
                    if (!String.IsNullOrWhiteSpace(ErrMsg))
                    {
                        MessageBox.ShowAndRedirect(ErrMsg, Url);
                        return;
                    }
                    InitTeamer(TeamId);
                    ltrName.Text = model.TeamName;
                    ltrTeamInfo.Text = Utils.GetText2(model.TeamInfo, 90, true);
                    ltrDate.Text = "加入日期:" + model.IssueTime.ToString("yyyy-MM-dd");
                    if (!String.IsNullOrWhiteSpace(model.TeamPhoto))
                        this.ltrImg.Text = "<img src=\"" + model.TeamPhoto + "\">";
                    else
                        this.ltrImg.Text = "<img src=\"/images/qiu-img002.jpg\">";
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(6));
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(7), "/Team/Default.aspx");
                return;
            }


        }

        /// <summary>
        /// 加载球队成员信息
        /// </summary>
        /// <param name="MemberId"></param>
        private void InitTeamer(string TeamId)
        {
            var list = BTeamMember.GetList(TeamId);
            if (list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }

        }

        protected void InitOperation(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = (dt_TeamMember)e.Item.DataItem;
                Literal ltrOperation = (Literal)e.Item.FindControl("ltrOperation");
                if (TeamState == Model.EnumType.球队审核状态.终审通过)
                {
                    if (MemberRole == Model.EnumType.球员角色.队长)//当前用户是队长
                    {
                        if (row.MemberId == MemberId)
                        {
                            //队长是当前用户
                            ltrOperation.Text = "<a class=\"MemberUpate\" DataId=\"" + row.MemberId + "\">修改信息</a>";
                        }

                        else
                        {
                            switch (row.State)
                            {
                                case (int)Model.EnumType.球员审核状态.审核中:
                                    ltrOperation.Text = "<a class=\"MemberCheck\" DataId=\"" + row.MemberId + "\">审核</a>\n<a href=\"#\">查看信息</a>";
                                    break;
                                case (int)Model.EnumType.球员审核状态.审核通过:
                                    ltrOperation.Text = "<a class=\"MemberKickedOut\" href=\"MyTeam.aspx?doType=out&mid=" + row.Id + "\" >踢出球队</a>\n<a class=\"MemberUpate\" DataId=\"" + row.MemberId + "\">修改信息</a>\n<a class=\"CaptainTransfer\" dataId=\"" + row.MemberId + "\" data_name=\"" + row.ContactName + "\">队长转让</a>\n<a href=\"#\">查看信息</a>";
                                    break;
                                case (int)Model.EnumType.球员审核状态.退出申请:
                                    ltrOperation.Text = "<a class=\"ExistCheck\" href=\"MyTeam.aspx?doType=Exists&mid=" + row.Id + "\" >退出审核</a>\n<a href=\"#\">查看信息</a>";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        //ltrOperation.Text = "<a href=\"#\">查看信息</a>";           
                            ltrOperation.Text = "";
             
                    }
                }
                else
                {
                    // ltrOperation.Text = "<a href=\"#\">查看信息</a>";
                    ltrOperation.Text = "";
                }
            }
        }
    }
}