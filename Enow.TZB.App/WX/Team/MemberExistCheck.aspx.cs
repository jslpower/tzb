using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using Enow.TZB.SMS;

namespace Enow.TZB.Web.WX.Team
{
    /// <summary>
    /// 退出审核
    /// </summary>
    public partial class MemberExistCheck : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
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
            var TeamModel = BTeamMember.GetModelNoState(MemberId);
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
            string MobilePhone = "";
            var MemberModel = BMember.GetModel(MemberId);
            if (MemberModel != null)
            {
                MobilePhone = MemberModel.MobilePhone;
            }
            else
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(43));
            }
            MemberModel = null;
            bool IsResult = BTeamMember.UpdateState(TeamMemberId, Model.EnumType.球员审核状态.同意退出);
            if (IsResult)
            {
                /*
                //发送短信
                SMSClass.Send(MobilePhone, CacheSysMsg.GetMsg(30));
                 * */
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(18));
            }
            else
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(19));
            }
        }
    }
}