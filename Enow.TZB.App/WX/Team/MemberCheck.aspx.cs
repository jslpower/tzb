﻿using System;
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
    public partial class MemberCheck : System.Web.UI.Page
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
                this.txtQYHM.Value = TeamModel.SQQYHM.ToString();
                if (!String.IsNullOrWhiteSpace(TeamModel.DNWZ))
                MeberSQWZ = Convert.ToInt32((Model.EnumType.球员位置)Enum.Parse(typeof(Model.EnumType.球员位置), TeamModel.DNWZ)); 
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
            string MobilePhone = "";
            var MemberModel = BMember.GetModel(MemberId);
            if (MemberModel != null) {
                MobilePhone = MemberModel.MobilePhone;
            } else {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(43));
            }
            bool IsResult = BTeamMember.Update(new tbl_TeamMember {
                Id = TeamMemberId,
                DNQYHM = QYHM,
                DNWZ = SQWZ,
                State = (int)Model.EnumType.球员审核状态.审核通过
            });
            if (IsResult)
            {
                /*
                if (MemberModel.CountryId == 1 && MemberModel.ProvinceId != 190 && MemberModel.ProvinceId != 191 && MemberModel.ProvinceId != 988)
                {
                    //发送短信
                    SMSClass.Send(MobilePhone, CacheSysMsg.GetMsg(28));
                }
                 * */
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(13));
            }
            else {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(14));
            }
        }
        /// <summary>
        /// 拒绝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefuse_Click(object sender, EventArgs e)
        {
            string iframeId = Request.QueryString["iframeId"];
            string TeamMemberId = Utils.GetFormValue(hidTMId.UniqueID);
            string MemberId = Utils.GetQueryStringValue("MemberId");
            string SQWZ = Convert.ToString((Model.EnumType.球员位置)Utils.GetInt(Utils.GetFormValue("SQWZ")));
            int QYHM = Utils.GetInt(Utils.GetFormValue("txtQYHM"));
            bool IsResult = BTeamMember.Update(new tbl_TeamMember
            {
                Id = TeamMemberId,
                DNQYHM = QYHM,
                DNWZ = SQWZ,
                State = (int)Model.EnumType.球员审核状态.拒绝
            });
            if (IsResult)
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(13));
            }
            else
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(14));
            }
        }
    }
}