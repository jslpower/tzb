using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Team
{
    /// <summary>
    /// 队长转让
    /// </summary>
    public partial class TeamTransfer : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    BMemberAuth.LoginCheck();
                    var AuthModel = BMemberAuth.GetUserModel();
                    string OpenId = AuthModel.OpenId;
                    InitIntroduceModel(OpenId);
                }
            }
        }

        #region 加载球队基本信息
        private void InitIntroduceModel(string openid)
        {
            var MemberModel = BMember.GetModelByOpenId(openid);
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
                    InitMemberList(TeamId, MemberId);
                    if (MemberRole != Model.EnumType.球员角色.队长)
                    {
                        MessageBox.ShowAndRedirect("你不是队长不能操作球队转移", "MyTeamInfo.aspx");
                        return;
                    }
                    var model = BTeam.GetModel(TeamId);
                    if (model != null)
                    {
                        string ErrMsg = "";
                        string Url = "";

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

                        }
                        if (!String.IsNullOrWhiteSpace(ErrMsg))
                        {
                            MessageBox.ShowAndRedirect(ErrMsg, Url);
                            return;
                        }
                        if (model != null)
                        {
                            this.ltrTeamName.Text = model.TeamName;
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
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(7), "Default.aspx");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 加载成员列表
        /// </summary>
        /// <param name="TeamId"></param>
        private void InitMemberList(string TeamId, string MemberId)
        {
            var list = BTeamMember.GetList(TeamId);
            if (list != null)
            {
                var TeamMeberList = list.Where(n => n.MemberId != MemberId);
                this.ddlTeamMember.DataSource = TeamMeberList;
                this.ddlTeamMember.DataTextField = "ContactName";
                this.ddlTeamMember.DataValueField = "MemberId";
                this.ddlTeamMember.DataBind();
            }
        }
        #endregion
        /// <summary>
        /// 转让操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            var MemberModel = BMember.GetModelByOpenId(OpenId);
            string NewTeamHostId = Utils.GetFormValue(ddlTeamMember.UniqueID);
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
                        var NewMemberModel = BMember.GetModel(NewTeamHostId);
                        if (NewMemberModel != null)
                        {
                            //球队创建人身份变更
                            BTeam.CaptainTransfer(TeamId, NewTeamHostId, NewMemberModel.ContactName);
                            //队长身份变更
                            BTeamMember.UpdateRoleType(TeamId, NewTeamHostId, Model.EnumType.球员角色.队长);
                            //队长变更为队员
                            BTeamMember.UpdateRoleType(TeamId, MemberModel.Id, Model.EnumType.球员角色.队员);
                            MessageBox.ShowAndRedirect("队长身份转让成功!", "TeamInfo.aspx");
                            return;
                        }
                        else
                        {
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(7), "WX/Member/Default.aspx");
                            return;
                        }
                    }
                    else
                    {
                        //MessageBox.ShowAndRedirect("操作失败!", "WX/Team/TeamInfo.aspx");
                        MessageBox.ShowAndRedirect("操作失败!", "TeamInfo.aspx");
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(7), "WX/Member/Default.aspx");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
    }
}