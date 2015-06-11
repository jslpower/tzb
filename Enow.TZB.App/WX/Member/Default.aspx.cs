using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Member
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberApp.LoginCheck();
                InitMember();
                string msg = Utils.GetQueryStringValue("Msg");
                if (!String.IsNullOrWhiteSpace(msg)) {
                    MessageBox.Show(Server.UrlDecode(msg));
                }
                
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember()
        {
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                string usimg = BMemberApp.Getusimg(model.Id);
                if (!string.IsNullOrEmpty(usimg))
                {
                    this.ltrHead.Text = "<img src=\"" + usimg + "\" width=\"85\" border=\"0\" />";
                }
                else
                {
                    this.ltrHead.Text = "<img src=\"../images/player.gif\" />";
                }
                if (model.Jobtyoe != null && model.Jobtyoe != 0)
                {
                    Literal1.Text = "<div class=\"user-item user-item02 font14 R_jiantou\" onclick=\"location.href='/WX/Member/Articles.aspx?types=" + model.Jobtyoe + "'\"><i class=\"u-ico08\"></i>" + (Enow.TZB.Model.EnumType.JobType)Utils.GetInt(model.Jobtyoe.ToString()) + "日志</div>";
                }
                this.ltrNickName.Text = model.ContactName;
                this.ltrPoint.Text = model.IntegrationNumber.ToString();
                this.ltrTitle.Text = BMemberHonor.GetTitle(model.HonorNumber);
                this.ltrMsgNumber.Text = BMessage.MsgCount(model.Id).ToString();
                //this.ltrMoney.Text = model.CurrencyNumber.ToString("F2");
                //查询自己所在的球队
                var TeamMemberModel = BTeamMember.GetModel(model.Id);
                if (TeamMemberModel != null)
                {
                    var TeamModel = BTeam.GetModel(TeamMemberModel.TeamId);
                    if (TeamModel != null)
                    {
                        this.ltrTeamName.Text = TeamModel.TeamName;
                    }
                   
                }
                else
                {
                    this.ltrTeamName.Text = "无";
                }

            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
    }
}