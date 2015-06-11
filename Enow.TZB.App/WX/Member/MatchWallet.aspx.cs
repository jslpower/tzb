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
    public partial class MatchWallet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                string msg = Utils.GetQueryStringValue("Msg");
                if (!String.IsNullOrWhiteSpace(msg))
                {
                    MessageBox.Show(Server.UrlDecode(msg));
                }
                InitMember();
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

                if (!String.IsNullOrWhiteSpace(model.MemberPhoto))
                    {
                        this.ltrHead.Text = "<img src=\"" + model.MemberPhoto + "\" width=\"85\" border=\"0\" />";
                    }
                    else
                    {
                        this.ltrHead.Text = "<img src=\"../images/player.gif\" />";
                    }
                        litltsbnum.Text = model.CurrencyNumber.ToString("F2");


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