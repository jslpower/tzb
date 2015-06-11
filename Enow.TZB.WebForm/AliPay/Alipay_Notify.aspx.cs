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
    public partial class Alipay_Notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BWebMemberAuth.LoginCheck();
                var model=BWebMemberAuth.GetUserModel();
                string MemberId=model.Id;
                PayAPI.Model.Ali.AliPayTradeNotify notify = PayAPI.Ali.Alipay.Create.GetNotifyAsync();//支付宝的返回通知实体
                if (notify.IsTradeSuccess)
                {
                    foreach (var item in notify.OrderInfo.OrderID)
                    {
                        var PayModel=BMemberWallet.GetModelById(item);
                        if (PayModel != null)
                        {
                            switch (PayModel.TypeId)
                            {
                                case (int)Model.EnumType.财务流水类型.比赛保证金:
                                    bool ret1 = BMatchTeam.UpdatePayInfo(PayModel.BindId, Model.EnumType.支付方式.支付宝支付, true);
                                    bool bllRetCode = BMatchTeam.UpdateValid(PayModel.BindId, Model.EnumType.参赛审核状态.报名费确认中, 0, "", 0);
                                    if (ret1 && bllRetCode)
                                    {
                                        //支付接口回调通知
                                        Response.Write(notify.PayAPICallBackMsg);
                                        Response.End();

                                    }
                                    break;
                                default:
                                    decimal TradeMoney = PayModel.TradeMoney;
                                    //更新充值状态
                                    bool ret = BMemberWallet.UpdatePayState(item);
                                    //更新铁丝币
                                    bool IsSuccess = BMember.UpdateCurrencyNumber(MemberId, Model.EnumType.操作符号.加, TradeMoney);
                                    if (ret && IsSuccess)
                                    {
                                        //支付接口回调通知
                                        Response.Write(notify.PayAPICallBackMsg);
                                        Response.End();

                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(3), "../My/RechargeList.aspx");
                }
            }
        }
    }
}