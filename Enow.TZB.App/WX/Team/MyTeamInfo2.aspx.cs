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
    public partial class MyTeamInfo2 : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;

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
            string doType = Request.QueryString["doType"];
            switch (doType)
            {
               
                case "greet": Greet(); break;

                default: break;
            }

            if (!IsPostBack)
            {
                BMemberApp.LoginCheck();
                InitTeamModel();

                InitIntroduceModel();

                InitLogList();
            }
        }

        /// <summary>
        /// 加载我的日志列表
        /// </summary>
        private void InitLogList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            string TeamId = Model.MCommon.DefaultGuidId;
            var TeamModel = BTeamMember.GetModel(MemberId);
            if (TeamModel != null)
            {
                TeamId = TeamModel.TeamId;
            }
            Model.MMemberArticleSearch SearchModel = new Model.MMemberArticleSearch();
            //SearchModel.MemberId = MemberId;
            SearchModel.TeamId = TeamId;
            SearchModel.IsEnable = true;
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.KeyWords = Server.UrlDecode(KeyWord.Trim());
            }
            var list = BArticle.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptLogList.DataSource = list;
                this.rptLogList.DataBind();
            }
            else
            {
                this.PlaceHolder1.Visible = false;
            }
        }
        private void Greet()
        {
            string s = Utils.GetQueryStringValue("id");


            int bllRetCode = BArticle.UpdateView(s, TZB.Model.发布对象.点赞) == true ? 1 : -99;

            if (bllRetCode == 1)
            {
                //MessageBox.ShowAndRedirect("", "Articles.aspx");
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "成功！"));
            }
            else
            {
                //MessageBox.ShowAndRedirect("", "Articles.aspx"); 
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-99", "失败！"));
            }

        }
        private void InitIntroduceModel()
        {
            var MemberModel = BMemberApp.GetUserModel();
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

        #region 加载球队基本信息
        /// <summary>
        /// 加载球队基本信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitTeamModel()
        {
            var MemberModel = BMemberApp.GetUserModel();
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
                        InitTeamList(TeamId);
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
        private void InitTeamList(string TeamId)
        {
            var list = BTeamMember.GetList(TeamId);
            if (list != null)
            {
                this.rptTeamList.DataSource = list;
                this.rptTeamList.DataBind();
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
                                    ltrOperation.Text = "<div class=\"Rbtn padd02\"><a class=\"MemberCheck\" DataId=\"" + row.MemberId + "\">球员审核</a>\n<a href=\"#\">查看信息</a></div>";
                                    break;
                                case (int)Model.EnumType.球员审核状态.审核通过:
                                    //ltrOperation.Text = "<div class=\"Rbtn\"><a class=\"MemberKickedOut\" DataId=\"" + row.MemberId + "\">踢出球队</a>\n<a class=\"MemberUpate\" DataId=\"" + row.MemberId + "\">更改信息</a>\n<a href=\"javascript:void()\" onclick=\"if (confirm('是否将队长转让给"+row.ContactName+"?')) {window.location.href = 'TeamTransfer.aspx?Id="+row.MemberId+"';}\">队长转让</a>\n<a href=\"#\" >查看信息</a></div>";
                                    ltrOperation.Text = "<div class=\"Rbtn\"><a class=\"MemberKickedOut\" DataId=\"" + row.MemberId + "\">踢出球队</a>\n<a class=\"MemberUpate\" DataId=\"" + row.MemberId + "\">更改信息</a>\n<a href=\"#\"  >查看信息</a></div>";
                                    break;
                                case (int)Model.EnumType.球员审核状态.退出申请:
                                    ltrOperation.Text = "<div class=\"Rbtn padd02\"><a class=\"ExistCheck\" DataId=\"" + row.MemberId + "\">退出审核</a>\n<a href=\"#\">查看信息</a></div>";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        ltrOperation.Text = "<div class=\"Rbtn padd01\"><a href=\"#\">查看信息</a></div>";
                    }
                }
                else
                {
                    ltrOperation.Text = "<div class=\"Rbtn padd01\"><a href=\"#\">查看信息</a></div>";
                }
            }
        }
        #endregion 
    }
}