using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.My
{
    public partial class UpdateTeamMember : System.Web.UI.Page
    {
        protected string MemberId = "";
        protected Model.EnumType.球员角色 MemberRole = Model.EnumType.球员角色.队员;
        protected int MeberSQWZ = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (BWebMemberAuth.IsLoginCheck())
                {
                   // var model = BWebMemberAuth.GetUserModel();
                   //string MemberId = model.Id;
                    string iframeId = Utils.GetQueryStringValue("iframeId");
                    MemberId = Utils.GetQueryStringValue("MemberId");
                    string strErr = "";
                    if (string.IsNullOrWhiteSpace(MemberId))
                    {
                        strErr = CacheSysMsg.GetMsg(17) + "\\n";
                    }
                    if (!string.IsNullOrWhiteSpace(strErr))
                    {
                        MessageBox.ShowAndBoxClose(strErr, iframeId);
                        return;
                    }
                    else
                    {
                        InitModel(MemberId);
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect("请登录", "/Default.aspx");
                }
            }
        }

        /// <summary>
        /// 加载队员队内信息
        /// </summary>
        /// <param name="MemberId"></param>
        private void InitModel(string MemberId)
        {
            string iframeId = Utils.GetQueryStringValue("iframeId");
            var TeamModel = BTeamMember.GetModel(MemberId);
            if (TeamModel!=null)
            {
                this.txtQYHM.Value = TeamModel.DNQYHM.ToString();
                if (!string.IsNullOrWhiteSpace(TeamModel.DNWZ))
                {
                    MeberSQWZ = Convert.ToInt32((Model.EnumType.球员位置)Enum.Parse(typeof(Model.EnumType.球员位置), TeamModel.DNWZ));
                }
                else 
                {
                    MeberSQWZ = (int)Model.EnumType.球员位置.前锋;
                }
                this.hidTMId.Value = TeamModel.Id;
            }
            else
            {
                MessageBox.ShowAndBoxClose(CacheSysMsg.GetMsg(7), iframeId);
                return;
            }
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string iframeId = Request.QueryString["iframeId"];
            string TeamMemberId = Utils.GetFormValue(hidTMId.UniqueID);
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