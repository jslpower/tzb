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
    public partial class RefuseReason : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype");

            switch (dotype)
            {
                case "Disabled":
                    phShenghe.Visible = true;
                    phdisband.Visible = false;
                    break;
                case "Disabled2":
                    phShenghe.Visible = true;
                    phdisband.Visible = false;
                    break;
                case "NoDisband":
                    phShenghe.Visible = false;
                    phdisband.Visible = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 初审/终审拒绝
        /// </summary>
        /// <param name="teamId">球队编号</param>
        /// <param name="TypeId">拒绝类型 1:初审  2：终审</param>
        private bool DisabledData(string teamId, int TypeId)
        {
            string TeamCreateName = "";
            string TeamCreateMobilePhone = "";
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            int ReasonId = Utils.GetInt(Utils.GetFormValue("ddlReason"), 0);
            string ContactName = UserModel.ContactName;
            UserModel = null;

            if (string.IsNullOrEmpty(teamId))
            {
                return false;
            }
            #region 取得球队信息
            var model = BTeam.GetViewModel(teamId);
            if (model != null)
            {
                TeamCreateName = model.ContactName;
                TeamCreateMobilePhone = model.MobilePhone;
            }
            else
            {
                return false;
            }
            #endregion
            switch (TypeId)
            {
                case 1:
                    bllRetCode = BTeam.UpdateState(teamId, UserId, ContactName, Model.EnumType.球队审核状态.初审拒绝, "", ReasonId, "");
                    break;
                case 2:
                    bllRetCode = BTeam.UpdateState(teamId, UserId, ContactName, Model.EnumType.球队审核状态.终审拒绝, "", ReasonId, "");
                    //发送短信
                    //SMSClass.Send(TeamCreateMobilePhone, CacheSysMsg.GetMsg(28));
                    break;
            }
            if (bllRetCode)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 拒绝解散操作
        /// </summary>
        private bool NoDisband(string teamId)
        {
            string TeamCreateName = "";
            string TeamCreateMobilePhone = "";
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            string ContactName = UserModel.ContactName;
            UserModel = null;
            string Reason = Utils.GetFormValue(txtDisband.UniqueID);

            if (string.IsNullOrEmpty(teamId))
            {
                return false;
            }
            #region 取得球队信息
            var model = BTeam.GetViewModel(teamId);
            if (model != null)
            {
                TeamCreateName = model.ContactName;
                TeamCreateMobilePhone = model.MobilePhone;
            }
            else
            {
                return false;
            }
            #endregion
            bllRetCode = BTeam.UpdateState(teamId, UserId, ContactName, Model.EnumType.球队审核状态.解散拒绝, "", 0, Reason);
            if (bllRetCode)
                return true;
            else
                return false;
        }

        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype");
            string teamId = Utils.GetQueryStringValue("Id");
            bool ret = false;
            switch (dotype)
            {
                case "Disabled":

                    ret = DisabledData(teamId, 1);//初审拒绝
                    break;
                case "Disabled2":
                  
                 ret=   DisabledData(teamId, 2);//终审拒绝
                    break;
                case "NoDisband":
                 
                  ret=  NoDisband(teamId);//解散拒绝
                    break;
                default:
                    break;
            }
            if (ret)
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(18));
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(31));
            }
        }
    }
}