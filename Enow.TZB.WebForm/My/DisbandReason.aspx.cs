using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.My
{
    public partial class DisbandReason : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string teamId = Utils.GetQueryStringValue("teamId");
            string DisbandReason = Utils.GetFormValue(txtDisbandReason.UniqueID);
            if (!string.IsNullOrWhiteSpace(teamId))
            {
                BTeam.UpdateState(teamId, 0, "", Model.EnumType.球队审核状态.解散申请, DisbandReason,0,"");
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(9));
                return;
            }
            else
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(19));
            }
        }
    }
}