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
    public partial class TeamInfo : System.Web.UI.Page
    {
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
                    InitModel(OpenId);
                }
                
                
            }
        }
        /// <summary>
        /// 球队列表入口
        /// </summary>
        /// <param name="TeamId"></param>
        private void Getteaminfo(string TeamId)
        {
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
                InitList(TeamId);

            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(6));
                return;
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
                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                MemberId = MemberModel.Id;
                string TeamId = "";
                //查询自己所在的球队
                var TeamModel = BTeamMember.GetModel(MemberId);
                if (TeamModel != null)
                {
                    TeamId = TeamModel.TeamId;
                    MemberRole = (Model.EnumType.球员角色)TeamModel.RoleType;
                    if (!String.IsNullOrWhiteSpace(TeamModel.DNWZ))
                        MeberSQWZ = Convert.ToInt32((Model.EnumType.球员位置)Enum.Parse(typeof(Model.EnumType.球员位置), TeamModel.DNWZ));
                    if (MemberRole == Model.EnumType.球员角色.队长)
                    {
                       
                        //---
                        this.ltrSignOut.Text = "<a href=\"javascript:void()\" class=\"basic_btn\" onclick=\"if (confirm('是否解散球队?')) {window.location.href = 'TeamDisband.aspx';}\">解散球队</a>";
                        //this.hyModifyInfo.Text = "球队信息修改";
                        //this.hyModifyInfo.Visible = true;
                        //---
                    }
                    else
                    {
                      
                        //---
                        this.ltrSignOut.Text = "<a href=\"javascript:void()\" class=\"basic_btn\" onclick=\"if (confirm('是否退出球队?')) {window.location.href = 'TeamQuit.aspx';}\">退出球队</a>";
                        //this.hyModifyInfo.Visible = false;
                        //---
                    }
                    var model = BTeam.GetModel(TeamId);
                    if (model != null)
                    {
                        string ErrMsg = "";
                        string Url = "";
                        TeamState = (Model.EnumType.球队审核状态)model.State;
                        switch (model.State) { 
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
                        InitList(TeamId);
                        //this.ltrTeamName.Text = model.TeamName;
                        //if (!String.IsNullOrWhiteSpace(model.TeamPhoto))
                        //    this.ltrImg.Text = "<img src=\"" + model.TeamPhoto + "\">";
                        //else
                        //    this.ltrImg.Text = "<img src=\"../images/qiu-img002.jpg\">";
                        //this.ltrCreateDate.Text = model.IssueTime.ToString("yyyy-MM-dd");
                        //this.ltrCity.Text = model.CountryName + "-" + model.ProvinceName + "-" + model.CityName;
                        //this.ltrTeamInfo.Text = model.TeamInfo;
                        
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
        #endregion
        #region 加载成员列表
        /// <summary>
        /// 加载成员列表
        /// </summary>
        /// <param name="TeamId"></param>
        private void InitList(string TeamId) {
            var list = BTeamMember.GetList(TeamId);
            if (list != null) {
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
                if (TeamState == Model.EnumType.球队审核状态.终审通过)
                {
                    if (MemberRole == Model.EnumType.球员角色.队长)//当前用户是队长
                    {
                        if (row.MemberId == MemberId)
                        {
                            //队长是当前用户
                            ltrOperation.Text = " <div class=\"Rbtn padd01\"><a class=\"MemberUpate\" DataId=\"" + row.MemberId + "\">更改信息</a></div>";
                        }
                        else
                        {
                            switch (row.State)
                            {
                                case (int)Model.EnumType.球员审核状态.审核中:
                                    ltrOperation.Text = "<div class=\"Rbtn padd02\"><a class=\"MemberCheck\" DataId=\"" + row.MemberId + "\">球员审核</a>\n<a href=\"TeamQuit.aspx?TeamUsid=" + row.MemberId + "\">查看信息</a></div>";
                                    break;
                                case (int)Model.EnumType.球员审核状态.审核通过:
                                    //ltrOperation.Text = "<div class=\"Rbtn\"><a class=\"MemberKickedOut\" DataId=\"" + row.MemberId + "\">踢出球队</a>\n<a class=\"MemberUpate\" DataId=\"" + row.MemberId + "\">更改信息</a>\n<a href=\"javascript:void()\" onclick=\"if (confirm('是否将队长转让给"+row.ContactName+"?')) {window.location.href = 'TeamTransfer.aspx?Id="+row.MemberId+"';}\">队长转让</a>\n<a href=\"#\" >查看信息</a></div>";
                                    ltrOperation.Text = "<div class=\"Rbtn\"><a class=\"MemberKickedOut\" DataId=\"" + row.MemberId + "\">踢出球队</a>\n<a class=\"MemberUpate\" DataId=\"" + row.MemberId + "\">更改信息</a>\n<a href=\"TeamQuit.aspx?TeamUsid=" + row.MemberId + "\"  >查看信息</a></div>";
                                    break;
                                case (int)Model.EnumType.球员审核状态.退出申请:
                                    ltrOperation.Text = "<div class=\"Rbtn padd02\"><a class=\"ExistCheck\" DataId=\"" + row.MemberId + "\">退出审核</a>\n<a href=\"TeamQuit.aspx?TeamUsid=" + row.MemberId + "\">查看信息</a></div>";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        ltrOperation.Text = "<div class=\"Rbtn padd01\"><a href=\"TeamQuit.aspx?TeamUsid=" + row.MemberId + "\">查看信息</a></div>";
                    }
                }
                else
                {
                    ltrOperation.Text = "<div class=\"Rbtn padd01\"><a href=\"TeamQuit.aspx?TeamUsid=" + row.MemberId + "\">查看信息</a></div>";
                }
            }
        }
        #endregion 
    }
}