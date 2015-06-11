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
    /// 退出球队
    /// </summary>
    public partial class TeamQuit : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var TeamUsid = Utils.GetQueryStringValue("TeamUsid");
                if (!string.IsNullOrEmpty(TeamUsid))
                {
                    Getteamus(TeamUsid);
                }
                else
                {
                    Response.Redirect("/WX/Team/");
                }
            }
        }
        #region 加载球员基本信息
        private void Getteamus(string MemberId)
        {
            var TeamModel = BTeamMember.GetTeamUsModel(MemberId);
            if (TeamModel != null)
            {
                litimage.Text = "<img src=\"" + TeamModel.MobilePhone + "\"/>";
                litname.Text = TeamModel.ContactName;
                litgzjy.Text = ((Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(TeamModel.RoleType)).ToString();
                litjuzhudi.Text = TeamModel.DNQYHM.ToString()+"号";
                Literal1.Text = TeamModel.DNWZ;
                litshouji.Text = TeamModel.IssueTime.ToString("yyyy-MM-dd");
            }
        }
        #endregion
    }
}