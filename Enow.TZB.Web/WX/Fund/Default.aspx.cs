using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Fund
{
    /// <summary>
    /// 钱包首页
    /// </summary>
    public partial class Default : System.Web.UI.Page
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
                Response.Expires = -1;
                Response.CacheControl = "no-cache";
                Response.Cache.SetNoStore();
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitMember(OpenId);
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember(string OpenId)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                this.ltrUserName.Text = model.ContactName;
                //判断是否设置过支付密码
                bool IsPayPassword = BMemeberPayPassword.IsExist(model.Id);
                if (IsPayPassword==false) {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(58), "/WX/Fund/PayPasswordSet.aspx");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 立即充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                decimal PayMoney = Utils.GetDecimal(Utils.GetFormValue(txtMoney.UniqueID));
                if (PayMoney < 0)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(63));
                    return;
                }
                else
                {
                    //写入待支付
                    string Id = System.Guid.NewGuid().ToString();
                    BMemberWallet.Add(new tbl_MemberWallet
                    {
                        Id = Id,
                        TradeNumber = Id,
                        UserId = 0,
                        UserContactName = "",
                        TypeId = (int)Model.EnumType.财务流水类型.充值,
                        MemberId = model.Id,
                        ContactName = model.ContactName,
                        TradeMoney = PayMoney,
                        IsPayed = '0',
                        PayMemberId = model.Id,
                        PayContactName = model.ContactName,
                        Remark = model.ContactName + "于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "充值 " + PayMoney.ToString("F2") + "元",
                        IssueTime = DateTime.Now
                    });
                    //跳转到支付页
                    //string Sign = BMemberWallet.Sign(Id);
                    string Sign = Id;
                    Response.Redirect("/TenPay/P.aspx?s=" + Sign, true);
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
    }
}