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
    /// 球队信息
    /// </summary>
    public partial class MyTeamInfo : System.Web.UI.Page
    {
        /// <summary>
        /// 会员关注列表
        /// </summary>
        private List<string> strlist = null;
        /// <summary>
        /// 球队编号
        /// </summary>
        protected string dhurl = "";
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected Model.EnumType.球员角色 MemberRole = Model.EnumType.球员角色.队员;
        protected Model.EnumType.球队审核状态 TeamState = Model.EnumType.球队审核状态.审核中;
        protected int MeberSQWZ = 0;
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void Page_Load(object sender, EventArgs e)
        {
            Getgzlist();
            if (!IsPostBack)
            {
                var teamid = Utils.GetQueryStringValue("TeamId");
                if (!string.IsNullOrEmpty(teamid))
                {
                    UserHome1.Userhometitle = "球队信息";
                    dhurl = "?TeamId=" + teamid;
                    Getteaminfo(teamid);

                }
                else
                {
                    BMemberAuth.LoginCheck();
                    var AuthModel = BMemberAuth.GetUserModel();
                    string OpenId = AuthModel.OpenId;
                    UserHome1.Userhometitle = "我的球队";
                    InitIntroduceModel(OpenId);
                }

            }
        }
        #region 会员关注列表信息
        /// <summary>
        /// 获取用户关注列表
        /// </summary>
        private void Getgzlist()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel != null)
            {
                var model = BMember.GetModelByOpenId(AuthModel.OpenId);
                strlist = BOfferpat.GetStrlist(model.Id, 3);
            }
            
        }
        /// <summary>
        /// 查询是否已关注
        /// </summary>
        /// <param name="Usid"></param>
        /// <returns></returns>
        protected string Selgzyf(string Usid)
        {
            if (strlist != null && strlist.Contains(Usid))
            {
                return "取消关注";
            }
            return "关注";

        }
        #endregion
        /// <summary>
        /// 球队列表入口
        /// </summary>
        /// <param name="TeamId"></param>
        private void Getteaminfo(string TeamId)
        {

            //---
            this.hyTransfer.Visible = false;
            this.hyModifyInfo.Visible = false;
            //---

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
                    this.ltrTeamName.InnerText = model.TeamName;
                    if (!String.IsNullOrWhiteSpace(model.TeamPhoto))

                        this.teamimg.InnerHtml = "<img class='qiudui_xximg' src=\"" + model.TeamPhoto + "\">";
                    else
                        this.teamimg.InnerHtml = " <img class='qiudui_xximg' src=\"../images/qiudui-img.jpg\"/>";
                    //"<img src=\"../images/qiu-img002.jpg\">";

                    this.ltrTeamInfo.InnerText = model.TeamInfo;
                }
            }


        }
        /// <summary>
        /// 会员入口
        /// </summary>
        /// <param name="openid"></param>
        private void InitIntroduceModel(string openid)
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
                        if (model != null)
                        {
                            this.ltrTeamName.InnerText = model.TeamName;
                            if (!String.IsNullOrWhiteSpace(model.TeamPhoto))

                                this.teamimg.InnerHtml = "<img class='qiudui_xximg' src=\"" + model.TeamPhoto + "\">";
                            else
                                this.teamimg.InnerHtml = " <img class='qiudui_xximg' src=\"../images/qiudui-img.jpg\"/>";
                            //"<img src=\"../images/qiu-img002.jpg\">";

                            this.ltrTeamInfo.InnerText = model.TeamInfo;
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