using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.Model;
using Enow.TZB.BLL;

namespace Enow.TZB.WebForm.Team
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Utils.GetQueryStringValue("id");
                if (BWebMemberAuth.IsLoginCheck())
                {
                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        InitPage(id);
                    }
                    else
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                    }
                }
                else
                {
                    InitPage(id);
                    this.phJoin.Visible = true;
                    this.PhSignUp.Visible = false;
                }
            }
        }

        /// <summary>
        /// 加载球队信息
        /// </summary>
        /// <param name="id"></param>
        private void InitPage(string id)
        {
            this.Master.Page.Title = "加入球队";
            var model = BTeam.GetModel(id);
            if (model!=null)
            {
                ltrTeamName.Text = model.TeamName;
                ltrTimeAndArea.Text = model.IssueTime.ToString("yyyy年MM月dd日") + " " + model.CountryName + "-" + model.ProvinceName + "-" + model.CityName + model.AreaName + "  创始人："+model.MemberName;
                ltrTeamInfo.Text = model.TeamInfo;
                ltrImg.Text = "<img src="  + model.TeamPhoto + " width=\"720px\">";
                //ltrCity.Text = model.CountryName + "-" + model.ProvinceName + "-" + model.CityName + model.AreaName;
                //ltrContactName.Text = model.MemberName;
                //ltrTname.Text = model.TeamName;
               
            }
            var MemberModel = BWebMemberAuth.GetUserModel();
            if (MemberModel != null)
            {
                var UserModel = BMember.GetModel(MemberModel.Id);
                BMember.StateCheck((Model.EnumType.会员状态)UserModel.State, "/");
                var TeamModel = BTeamMember.GetModel(MemberModel.Id);

                if (TeamModel != null)
                {
                    PhSignUp.Visible = false;
                }
                else
                {
                    PhSignUp.Visible = true;
                }
            }
        }

        /// <summary>
        /// 加入球队
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var model = BWebMemberAuth.GetUserModel();
            string TeamId = Utils.GetQueryStringValue("id");
            int RoleType = Utils.GetInt(Utils.GetFormValue("hidType"));
            string SQWZ = Convert.ToString((Model.EnumType.球员位置)Utils.GetInt(Utils.GetFormValue("SQWZ")));
            int QYHM =Utils.GetInt(Utils.GetFormValue(txtQYHM.UniqueID));
            string JRYS = Utils.GetFormValue("txtRemark");
            if (model != null)
            {
                
                    if (!string.IsNullOrWhiteSpace(model.ContactName))
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
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(16), "/My/Default.aspx");
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect("请填写真实信息!", "/My/Update.aspx");
                    }
              
            }
            else
            {
                MessageBox.ShowAndRedirect("请登录!", "/Default.aspx");
            }
        }
    }
}