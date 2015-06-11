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
    /// 加入球队
    /// </summary>
    public partial class SignUp : System.Web.UI.Page
    {
        public string ltrTeamName = "";
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberApp.LoginCheck();
                string strErr = "";
                string TeamId = Request.QueryString["TeamId"];
                if (String.IsNullOrWhiteSpace(TeamId))
                {
                    strErr = CacheSysMsg.GetMsg(42);
                }                
                if (!String.IsNullOrWhiteSpace(strErr))
                {
                    MessageBox.ShowAndReturnBack(strErr);
                    return;
                }
                else
                {
                    InitModel(TeamId);
                }
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitModel(string TeamId)
        {
            var MemberModel = BMemberApp.GetUserModel();
            if (MemberModel != null)
            {
               var TeamMemberModel = BTeamMember.GetModel(MemberModel.Id);
               if (TeamMemberModel !=null)
               {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(15), "/WX/Member/Default.aspx");
                    return;
                }
                this.ltrMemeberCityName.Text = MemberModel.ProvinceName + "-" + MemberModel.CityName;
                var model = BTeam.GetModel(TeamId);
                if (model != null)
                {
                    ltrTeamName = model.TeamName;
                    UserHome1.Userhometitle = ltrTeamName;
                    if (!String.IsNullOrWhiteSpace(model.TeamPhoto))
                        this.ltrImg.Text = "<img src=\"" + model.TeamPhoto + "\">";
                    else
                        this.ltrImg.Text = "<img src=\"../images/qiu-img002.jpg\">";
                    this.ltrCreateDate.Text = model.IssueTime.ToString("yyyy-MM-dd");
                    this.ltrCity.Text = model.ProvinceName + "-" + model.CityName;
                    this.ltrTeamInfo.Text = model.TeamInfo;
                    this.ltrTeamName2.Text = model.TeamName;
                    this.ltrCreateName.Text = model.MemberName;
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(6));
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
        /// 加入球队
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            BMemberApp.LoginCheck();
            string TeamId = Request.QueryString["TeamId"];
            int RoleType = Utils.GetInt(Utils.GetFormValue("hidType"));
            string SQWZ = Convert.ToString((Model.EnumType.球员位置)Utils.GetInt(Utils.GetFormValue("SQWZ")));
            int QYHM = Utils.GetInt(Utils.GetFormValue("txtQYHM"));
            string JRYS = Utils.GetFormValue("txtRemark");
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                BTeamMember.Add(new tbl_TeamMember
                {
                    Id = System.Guid.NewGuid().ToString(),
                    TeamId = TeamId,
                    MemberId = model.Id,
                    RoleType = RoleType,
                    SQWZ = SQWZ,
                    SQQYHM = QYHM,
                    JRYS = JRYS,
                    State = (int)Model.EnumType.球队审核状态.审核中,
                    IssueTime = DateTime.Now
                });
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(16), "/WX/Member/Default.aspx");
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8));
            }
        }
    }
}