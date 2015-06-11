using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.AboutWar
{
    public partial class AboutWarTeam : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected string GatherId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetLoadqiudui();
            }
        }
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
        private void GetLoadqiudui()
        {
            var Id = Utils.GetQueryStringValue("ID");
            var model = BAboutWar.GetModel(Id);
            if (model != null)
            {
                InitTeamList(model.MainID);
                var usmodel = BAboutWar.GetQdid(model.MainID);
                if (usmodel != null)
                    litTeamName.Text = usmodel.TeamName;
            }
            else
            {
                Response.Redirect("Default.aspx");
            }

        }
    }
}