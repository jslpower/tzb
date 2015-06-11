using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Team
{
    public partial class TeamView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    InitPage(Id);
                }
                else
                {
                    MessageBox.ShowAndRedirect("未找到要查看的球队", "Default.aspx");
                }
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="Id"></param>
        private void InitPage(string Id)
        {

            var model = BTeam.GetViewModel(Id);
            if (model!=null)
            {
                lblTeamName.Text = model.TeamName;
                lblMemberName.Text = model.ContactName;
                lblMobile.Text = model.MobilePhone;
                lblAreaName.Text = model.CountryName + "-" + model.ProvinceName + "-" + model.CityName + "-" + model.AreaName;
                lblIssueDate.Text = model.IssueTime.ToString("yyyy-MM-dd");
                lblState.Text = ((Model.EnumType.球队审核状态)model.State).ToString();
                LtrTeamInfo.Text = model.TeamInfo;
                LtrTeamPhoto.Text = "<img src="+model.TeamPhoto+" width=400px height=400px />";
            }
        }
    }
}