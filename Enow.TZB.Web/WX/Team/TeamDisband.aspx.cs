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
    /// 球队解散申请
    /// </summary>
    public partial class TeamDisband : System.Web.UI.Page
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
              //  InitModel(OpenId);
            }
        }
        #region 加载球队基本信息
        /// <summary>
        /// 加载球队基本信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitModel(string OpenId)
        {
            var MemberModel = BMember.GetModelByOpenId(OpenId);
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
                            BTeam.UpdateState(TeamId,0,"", Model.EnumType.球队审核状态.解散申请,"",0,"");
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(9), "/WX/Member/Default.aspx");
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
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            string Reason = Utils.GetFormValue(txtRemark.UniqueID);
            if (string.IsNullOrWhiteSpace(Reason))
            {
                MessageBox.ShowAndReturnBack("请填写解散原因!");
                return;
            }
            var MemberModel = BMember.GetModelByOpenId(OpenId);
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
                            BTeam.UpdateState(TeamId, 0, "", Model.EnumType.球队审核状态.解散申请,Reason, 0, "");
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(9), "/WX/Member/Default.aspx");
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
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
    }
}