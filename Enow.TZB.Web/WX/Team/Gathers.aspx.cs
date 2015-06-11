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
    public partial class Gathers : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
       private  string MemberId = "";
       private  string NickName = "", MemberName = "";
       protected string type = "", fee = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BMemberAuth.LoginCheck();
               
               
               
            }
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            InitMember(OpenId);
            string AcceptTeamName = Utils.GetQueryStringValue("name");
            txtGatherName.Text = AcceptTeamName;
            type = hdType.Value;
            fee = hdFee.Value;
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember(string OpenId)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
                NickName = model.NickName;
                MemberName = model.ContactName;
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
          /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AcceptTeamId=Utils.GetQueryStringValue("teamid");
           
            string TeamId = Model.MCommon.DefaultGuidId;
            string TeamName="";
            var TeamModel = BTeamMember.GetModel(MemberId);
            if (TeamModel != null)
            {
                TeamId = TeamModel.TeamId;
                TeamName = TeamModel.TeamName;
            }

            TeamModel = null;

            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            string strErr = "";
           
            if (string.IsNullOrWhiteSpace(txtGatherTime.Text))
            {
                strErr += "约战时间不能为空!/n";

            }
            if (!StringValidate.IsDateTime(txtGatherTime.Text))
            {
                strErr += "约战时间格式不正确!/n";
            }
            if (string.IsNullOrWhiteSpace(txtGatherPlace.Text))
            {
                strErr += "约战地点不能为空!/n";

            }
            if(string.IsNullOrWhiteSpace(type))
            {
                 strErr += "请选择赛制！";
            }
            if(string.IsNullOrWhiteSpace(fee))
            {
                 strErr += "请选择费用类型！";
            }
            if (string.IsNullOrWhiteSpace(txtWarBook.Value))
            {
                strErr += "约战说明不能为空!/n";

            }
            if(TeamId==AcceptTeamId&&TeamName==txtGatherName.Text)
            {
                strErr += "约战球队不能为自己球队!/n";

            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }


            int mType = 0;
            int mFee = 0;
          
            if (!string.IsNullOrEmpty (MemberId))
            {
                int.TryParse(hdType.Value, out mType);
                int.TryParse(hdType.Value, out mFee);
                string Id=Guid.NewGuid().ToString();
                BGathers.Add(new tbl_Gathers 
                {
                    Id =Id ,
                    AcceptTeamName = txtGatherName.Text,
                    WarTime = DateTime.Parse(Convert.ToDateTime(txtGatherTime.Text).ToString("yyyy-MM-dd")),
                    WarPlace =txtGatherPlace.Text,
                    MatchType =mType ,
                    MatchFee = mFee,
                    MatchTarget=txtWarBook.Value,
                    TeamId = TeamId,
                    AcceptTeamId=AcceptTeamId,
                    TeamName=TeamName,
                  
                    MemberId = MemberId,
                    NickName = NickName,
                    MemberName = MemberName,
                    IsAcceptWar=false,
                    IsGatherResult=false
                  

                });
                Response.Redirect("Gathers_Step2.aspx?gatherid=" + Id + "&teamid=" + TeamId + "", true);
               
            }
            else { MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8)); }


        }
    }
}