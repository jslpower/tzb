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
    public partial class MemberUpdate : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected Model.EnumType.球员角色 MemberRole = Model.EnumType.球员角色.队员;
        protected int MeberSQWZ = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
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
               
                this.txtQYHM.Value = TeamModel.DNQYHM.ToString();
                if (!string.IsNullOrEmpty(TeamModel.DNWZ))
                {
                    MeberSQWZ = Convert.ToInt32((Model.EnumType.球员位置)Enum.Parse(typeof(Model.EnumType.球员位置), TeamModel.DNWZ));
                }
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
            string SQWZ = Convert.ToString((Model.EnumType.球员位置)Utils.GetInt(Utils.GetFormValue("SQWZ")));
            int QYHM = Utils.GetInt(Utils.GetFormValue("txtQYHM"));
            bool IsResult = BTeamMember.UpdateBaseInfo(new tbl_TeamMember
            {
                Id = TeamMemberId,
                DNQYHM = QYHM,
                DNWZ = SQWZ
            });
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