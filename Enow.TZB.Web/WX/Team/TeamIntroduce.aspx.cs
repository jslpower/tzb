using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Team
{
    public partial class TeamIntroduce : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected Model.EnumType.球员角色 MemberRole = Model.EnumType.球员角色.队员;
        protected Model.EnumType.球队审核状态 TeamState = Model.EnumType.球队审核状态.审核中;
   

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitModel(OpenId);
            }
        }
        private void InitModel(string openid)
        {
             var MemberModel = BMember.GetModelByOpenId(openid);
             if (MemberModel != null)
             {
                 BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                 MemberId = MemberModel.Id;
                 string TeamId = "";
                 //查询自己所在的球队
                 var TeamModel = BTeamMember.GetModel(MemberId);
                 if (TeamModel != null)
                 {
                     TeamId = TeamModel.TeamId;
                     MemberRole = (Model.EnumType.球员角色)TeamModel.RoleType;
                    
                     if (MemberRole == Model.EnumType.球员角色.队长)
                     {
                         
                         //---
                         this.hyTransfer.Text = "队长转让";
                         this.hyTransfer.Visible = true;
                         this.hyModifyInfo.Text = "修改";
                         this.hyModifyInfo.Visible = true;
                         //---
                     }
                     else
                     {
                         
                         //---
                         this.hyTransfer.Visible = false;
                         this.hyModifyInfo.Visible = false;
                         //---
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
                         if(model !=null)
                         {
                             this.ltrTeamName.InnerText  = model.TeamName;
                             if (!String.IsNullOrWhiteSpace(model.TeamPhoto))

                                 this.teamimg.InnerHtml = "<img class='qiudui_xximg' src=\"" + model.TeamPhoto + "\">";
                             else
                                 this.teamimg.InnerHtml = " <img class='qiudui_xximg' src=\"../images/qiudui-img.jpg\"/>";
                                     //"<img src=\"../images/qiu-img002.jpg\">";
                           
                             this.ltrTeamInfo.InnerText  = model.TeamInfo;
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
    }
}