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
    public partial class Recharge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BWebMemberAuth.LoginCheck();
                var model = BWebMemberAuth.GetUserModel();
                this.Master.Page.Title = "会员充值";
                InitPage(model.Id);
            }
        }

        /// <summary>
        /// 绑定会员信息
        /// </summary>
        /// <param name="MemberId"></param>
        private void InitPage(string MemberId)
        {
            var UserModel = BMember.GetModel(MemberId);
            if (UserModel != null)
            {
                txtContractName.Text = UserModel.ContactName;
                txtUserName.Text = UserModel.UserName;
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/Default.aspx");
            }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            var model = BWebMemberAuth.GetUserModel();
            var UserModel = BMember.GetModel(model.Id);
            if (UserModel != null)
            {
                decimal PayMoney = Utils.GetDecimal(Utils.GetFormValue(txtCurrNo.UniqueID));
                if (PayMoney <= 0)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(63));
                    return;
                }
                else
                {
                    //写入待支付记录
                    string Id = Guid.NewGuid().ToString();
                    BMemberWallet.Add(new tbl_MemberWallet
                    {
                        Id = Id,
                        TradeNumber = Id,
                        UserId = 0,
                        UserContactName = "",
                        TypeId = (int)Model.EnumType.财务流水类型.充值,
                        MemberId = model.Id,
                        ContactName = UserModel.ContactName,
                        TradeMoney = PayMoney,
                        IsPayed = '0',
                        PayMemberId = UserModel.Id,
                        PayContactName = UserModel.ContactName,
                        IssueTime = DateTime.Now

                    });


                    Response.Redirect("../AliPay/Pay.aspx?OrderId=" + Id, true);
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/Default.aspx");
            }
           
        }
    }
}