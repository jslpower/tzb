using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayAPI.Model.Tencent;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.PayTest
{
    /// <summary>
    /// 微信支付测试页
    /// </summary>
    public partial class P : System.Web.UI.Page
    {
        protected WxPayAPI.Model.Tencent.TenPayTrade TenPayTradeModel = new WxPayAPI.Model.Tencent.TenPayTrade();
        protected PrePay _TenPayTradeModel = new PrePay();
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
                #region 验证
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                string s = Utils.GetQueryStringValue("s");
                if (!String.IsNullOrWhiteSpace(s))
                {
                    InitMember(OpenId, s.Trim());
                    /*
                    string Id = BMemberWallet.UnSign(s.Trim());
                    if (Id != "-1")
                    {
                        
                    }
                    else
                    {
                        Response.Redirect("/WX/Member/?Msg=" + Server.UrlEncode(CacheSysMsg.GetMsg(65)), true);
                        //MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(65), "/WX/Member/");
                        Response.End();
                        return;
                    }
                     * */
                }
                else
                {
                    Response.Redirect("/WX/Member/?Msg=" + Server.UrlEncode(CacheSysMsg.GetMsg(65)), true);
                    //MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(65), "/WX/Member/");
                    Response.End();
                    return;
                }
                #endregion
            }
        }
        /// <summary>
        /// 处理支付信息
        /// </summary>
        /// <param name="OpenId">OpenId</param>
        /// <param name="Id">支付编号</param>
        private void InitMember(string OpenId, string Id)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    Response.Redirect("/WX/Member/Step2.aspx", true);
                    //MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    Response.End();
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                //取得支付信息
                var PayModel = BMemberWallet.GetModelById(Id);
                if (PayModel != null)
                {
                    if (PayModel.TypeId == (int)Model.EnumType.财务流水类型.比赛保证金)
                    {
                        this.phNoView.Visible = false;
                    }
                    this.ltrUserName.Text = PayModel.ContactName;
                    this.ltrPoint.Text = PayModel.TradeMoney.ToString("F2");
                    this.ltrMoney.Text = PayModel.TradeMoney.ToString("F2");
                    #region 初始化支付信息
                    WxPayAPI.Tencent.Tenpay pay = new WxPayAPI.Tencent.Tenpay();
                    TenPayTradeModel.OPENID = OpenId;
                    TenPayTradeModel.Totalfee = PayModel.TradeMoney;
                    TenPayTradeModel.UserIP = StringValidate.GetRemoteIP();
                    switch ((Model.EnumType.财务流水类型)PayModel.TypeId)
                    {
                        case Model.EnumType.财务流水类型.充值:
                            TenPayTradeModel.OrderInfo.Body = PayModel.ContactName + " 微信充值:" + PayModel.TradeMoney.ToString("F2") + "铁丝币，共计：" + PayModel.TradeMoney.ToString("F2") + "元";
                            break;
                        case Model.EnumType.财务流水类型.线下充值:
                            TenPayTradeModel.OrderInfo.Body = "充值操作人：" + PayModel.UserContactName + ":" + PayModel.ContactName + " 充值:" + PayModel.TradeMoney.ToString("F2") + "铁丝币，共计：" + PayModel.TradeMoney.ToString("F2") + "元";
                            break;
                    }
                    _TenPayTradeModel = pay.Create_url(TenPayTradeModel);
                    //更新订单流水号
                    bool IsUpdateState = BMemberWallet.UpdateTradeNumber(Id, TenPayTradeModel.OutTradeNo);
                    if (IsUpdateState == false)
                    {
                        Response.Redirect("/WX/Member/?Msg=" + Server.UrlEncode(CacheSysMsg.GetMsg(66)), true);
                        //MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(66), "/WX/Member/");
                        Response.End();
                        return;
                    }
                    #endregion
                }
                else
                {
                    Response.Redirect("/WX/Member/?Msg=" + Server.UrlEncode(CacheSysMsg.GetMsg(65)), true);
                    //MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(65), "/WX/Member/");
                    Response.End();
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                Response.End();
                return;
            }
        }
    }
}