using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using PayAPI;

namespace Enow.TZB.WebForm.AliPay
{
    public partial class Pay : System.Web.UI.Page
    {
        List<string> OrderList = new List<string>();
        protected readonly string AliPayAccount = System.Configuration.ConfigurationManager.AppSettings["AlipayAccount"];
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Response.Expires = -1;
                Response.CacheControl = "no-cache";
                Response.Cache.SetNoStore();
                BWebMemberAuth.LoginCheck();
                var model = BWebMemberAuth.GetUserModel();
                string OrderId = Utils.GetQueryStringValue("OrderId");
                if (!string.IsNullOrWhiteSpace(OrderId))
                {
                    OrderList.Add(OrderId);
                    InitPage(model.Id, OrderId);
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(65), "/My/Default.aspx");
                    Response.End();
                    return;
                }

            }

        }

        private void InitPage(string MemberId, string OrderId)
        {
            var model = BMember.GetModel(MemberId);
            if (model != null)
            {
                //检查会员状态
                BMember.StateCheck((Model.EnumType.会员状态)model.State, "/My/Update.aspx");
                //支付信息
                var PayModel = BMemberWallet.GetModelById(OrderId);
                if (PayModel != null)
                {
                    if (PayModel.TradeMoney <= 0)
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(63));
                        return;
                    }
                    if (PayModel.TypeId == (int)Model.EnumType.财务流水类型.比赛保证金)
                    {
                        //初始化支付宝支付信息
                        string url = "";
                        PayAPI.Model.Ali.AliPayTrade trade = new PayAPI.Model.Ali.AliPayTrade();
                        trade.OrderInfo.OrderID = OrderList;
                        trade.OrderInfo.Subject = "姓名:" + model.ContactName + ",交纳赛事保证金:" + PayModel.TradeMoney;
                        trade.OrderInfo.Body = "姓名：" + model.ContactName + ",交纳赛事保证金:" + PayModel.TradeMoney + "";
                        trade.Totalfee = PayModel.TradeMoney;
                        trade.IsRoyalty = false;
                        trade.RoyaltyType = PayAPI.Model.Ali.RoyaltyType.平级分润;
                        trade.SellerAccount = AliPayAccount;   //卖家账号           
                        trade.ShowUrl = "";  //展示页面
                        PayAPI.Model.Attach attach = new PayAPI.Model.Attach();
                        attach.Key = "RegistrationFee";
                        attach.Value = "交纳赛事保证金";
                        trade.AttachList.Add(attach);
                        //构造url
                        url = PayAPI.Ali.Alipay.Create.Create_url(trade);
                        if (!string.IsNullOrWhiteSpace(url))
                        {
                            Response.Redirect(url);
                        }
                        else
                        {
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(3), "../my/Recharge.aspx");
                        }
                    }
                    else
                    {
                        //初始化支付宝支付信息
                        string url = "";
                        PayAPI.Model.Ali.AliPayTrade trade = new PayAPI.Model.Ali.AliPayTrade();
                        trade.OrderInfo.OrderID = OrderList;
                        trade.OrderInfo.Subject = "用户姓名:" + model.ContactName + "";
                        trade.OrderInfo.Body = "用户姓名：" + model.ContactName + ",充值金额:" + PayModel.TradeMoney + "";
                        trade.Totalfee = PayModel.TradeMoney;
                        trade.IsRoyalty = false;
                        trade.RoyaltyType = PayAPI.Model.Ali.RoyaltyType.平级分润;
                        trade.SellerAccount = AliPayAccount;   //卖家账号           
                        trade.ShowUrl = "";  //展示页面
                        PayAPI.Model.Attach attach = new PayAPI.Model.Attach();
                        attach.Key = "Recharge";
                        attach.Value = "充值";
                        trade.AttachList.Add(attach);
                        //构造url
                        url = PayAPI.Ali.Alipay.Create.Create_url(trade);
                        if (!string.IsNullOrWhiteSpace(url))
                        {
                            Response.Redirect(url);
                        }
                        else
                        {
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(3), "../my/Recharge.aspx");
                        }
                    }
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/Default.aspx");
            }
        }
    }
}