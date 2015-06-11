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
    public partial class MemberKickedOut : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberApp.LoginCheck();
                
                string iframeId = Request.QueryString["iframeId"];
                string strErr = "";
                string MemberId = Utils.GetQueryStringValue("MemberId");
                if (String.IsNullOrWhiteSpace(MemberId))
                {
                    strErr = CacheSysMsg.GetMsg(17) + "\\n";
                }
                if (!String.IsNullOrWhiteSpace(strErr))
                {
                    MessageBox.ShowAndBoxClose(strErr, iframeId);
                    return;
                }
                else
                {
                    InitModel(MemberId);
                }
            }
        }
        #region 加载球队基本信息
        /// <summary>
        /// 加载球队基本信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitModel(string MemberId)
        {
            string iframeId = Request.QueryString["iframeId"];
            var TeamModel = BTeamMember.GetModel(MemberId);
            if (TeamModel != null)
            {
                this.txtQYHM.Text = TeamModel.DNQYHM.ToString();
                this.SQWZ.Text = TeamModel.DNWZ;
                this.hidTMId.Value = TeamModel.Id;
            }
            else
            {
                MessageBox.ShowAndBoxClose(CacheSysMsg.GetMsg(7), iframeId);
                return;
            }
        }
        #endregion
        /// <summary>
        /// 队员审核通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string iframeId = Request.QueryString["iframeId"];
            string TeamMemberId = Utils.GetFormValue(hidTMId.UniqueID);
            string MemberId = Utils.GetQueryStringValue("MemberId");
            bool IsResult = BTeamMember.UpdateState(TeamMemberId,Model.EnumType.球员审核状态.踢除);
            if (IsResult)
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(18));
            }
            else
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(19));
            }
        }
    }
}