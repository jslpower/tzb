using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayAPI.Model.Tencent;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.TenPay
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WxPayAPI.Tencent.Tenpay pay = new WxPayAPI.Tencent.Tenpay();
                var model = pay.GetNotifyAsync();
                if (model.IsTradeSuccess)
                {
                    bool rv = false;
                    var WalletModel = BMemberWallet.GetModel(model.OutTradeNo);
                    {
                        switch (WalletModel.TypeId)
                        {
                            case (int)Model.EnumType.财务流水类型.比赛保证金:
                                BMatchTeam.UpdatePayInfo(WalletModel.BindId, Model.EnumType.支付方式.微信支付, true);
                                bool bllRetCode = BMatchTeam.UpdateValid(WalletModel.BindId, Model.EnumType.参赛审核状态.报名费确认中, 0, "", 0);
                                break;
                            case (int)Model.EnumType.财务流水类型.消费:
                                //修改订单状态为已支付
                                string remark = WalletModel.Remark;
                                string[] r = remark.Split(',');
                                string orderid=r[r.Length-1];
                                //修改订单状态为已支付
                                BMallOrders.UpdatePayState(orderid);
                                if (WalletModel.Id != null)
                                {
                                    BMallOrders.UpdatePayState(WalletModel.Id);
                                }
                                BMemberWallet.UpdatePayState(model.OutTradeNo);
                                break;
                            default:
                                //更新支付状态
                                rv = BMemberWallet.UpdatePayState(model.OutTradeNo);
                                break;
                        }
                    }
                    if (rv)
                    {
                        Response.Clear();
                        Response.Write("<xml><return_code>SUCCESS</return_code></xml>");
                        Response.End();
                        return;
                    }
                    else
                    {
                        Log.WLog("验证支付凭据支付失败!!", "/Log/Pay.txt");
                        return;
                    }
                }
                else
                {
                    Log.WLog("支付失败!!", "/Log/Pay.txt");
                    return;
                }
            }
        }
    }
}