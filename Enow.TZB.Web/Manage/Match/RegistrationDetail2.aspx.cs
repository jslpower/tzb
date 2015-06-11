using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Match
{
    /// <summary>
    /// 支付审核
    /// </summary>
    public partial class RegistrationDetail2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitList();
            }

        }

        #region 加载审核信息
        private void InitList()
        {
            string Id = Utils.GetQueryStringValue("id");//MatchTeamId
            string Mid = Utils.GetQueryStringValue("Mid");//MatchId
            if (string.IsNullOrWhiteSpace(Id))
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(31));
                return;
            }
            if (string.IsNullOrWhiteSpace(Mid))
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(31));
                return;
            }

            #region 绑定赛事相关数据
            tbl_Match model = BMatch.GetModel(Mid);
            if (model != null)
            {
                lblMatchName.Text = model.MatchName;
                lblMatchArea.Text = model.CountryName + "-" + model.ProvinceName + "-" + model.CityName;
                lblSignDate.Text = model.SignBeginDate.ToString("yyyy-MM-dd") + "---" + model.SignEndDate.ToString("yyyy-MM-dd");
                lblMatchDate.Text = model.BeginDate.ToString("yyyy-MM-dd") + "---" + model.EndDate.ToString("yyyy-MM-dd");
                lblSignCount.Text = model.SignUpNumber + "/" + model.TeamNumber;
                //lblLimitCity.Text = model.IsCityLimit == true ? "是" : "否";
                ltrFee1.Text = model.RegistrationFee.ToString("F2");
                lblFee.Text = model.EarnestMoney.ToString("F2");
                lblMaster.Text = model.MasterOrganizer;
                lblCoor.Text = model.CoOrganizers;
                lblOrganizer.Text = model.Organizer;
                lblSponsors.Text = model.Sponsors;
            }
            #endregion


            #region 绑定球队相关信息
            tbl_MatchTeam Teammodel = BMatchTeam.GetTeamModel(Id);
            lblTeamName.Text = Teammodel.TeamName;
            lblOnwerName.Text = Teammodel.TeamOwner;
            lblTeamCount.Text = Teammodel.JoinNumber.ToString();
            this.ltrFieldName.Text = Teammodel.FieldName;
            this.ltrFieldAddress.Text = Teammodel.FieldAddress;

            #endregion

            #region 绑定参赛球员信息

            var list = BMatchTeamMember.GetListByMatchId(Mid, Id);
            if (list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();

            }
            #endregion
        }
        #endregion
        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnEnabled_Click(object sender, EventArgs e)
        {
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int userId = UserModel.Id;
            string contractName = UserModel.ContactName;
            UserModel = null;
            string id = Utils.GetQueryStringValue("Id");
            if (string.IsNullOrWhiteSpace(id))
            {

                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(31));
                return;
            }

            var model = BMatchTeam.GetModel(id);
            if (model != null)
            {
                if (model.PayType == (int)Model.EnumType.支付方式.线下支付)
                {
                    BMatchTeam.UpdatePayInfo(id, Model.EnumType.支付方式.线下支付, true);
                    //更改支付流水状态
                    BMemberWallet.UpdatePayStateByBindId(id);
                }
                bllRetCode = BMatchTeam.UpdateValid(id, Model.EnumType.参赛审核状态.已获参赛权, userId, contractName, 0);
            }
            else
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(31));
                return;
            }
            if (bllRetCode)
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(18));
                return;
            }
            else
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(31));
                return;
            }
        }
    }
}