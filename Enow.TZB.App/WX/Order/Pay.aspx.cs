using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Order
{
    public partial class Pay : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        private string MemberId = "";
        string orderid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Expires = -1;
                Response.CacheControl = "no-cache";
                Response.Cache.SetNoStore();
            }
            InitMember();
            Getordermodel();
        }
        /// <summary>
        /// 初始化订单信息
        /// </summary>
        public void Getordermodel()
        {
            orderid = Request.QueryString["OrderId"];
            string goodid = Request.QueryString["GoodsId"];
            GetOrderInfo(orderid);
            
        }
        /// <summary>
        /// 计算订单信息
        /// </summary>
        /// <param name="orderid">订单编号</param>
        /// <returns></returns>
        public void GetOrderInfo(string orderid)
        {
            var list = BShoppingChart.GetOrdermodels(orderid, MemberId);
            if (list.Count<=0)
            {
                MessageBox.ShowAndRedirect("请先选择商品!", "/WX/Mall/Mall_Type.aspx");
                return;
            }
            var sum = 0;//数量
            decimal GoodsFee = 0;//实付
            decimal FreightFee = 0;//运费
            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i].ShoppingNum;
                GoodsFee += list[i].GoodsFee * list[i].ShoppingNum + (list[i].FreightFee != null ? Utils.GetDecimal(list[i].FreightFee.ToString()) : 0);
                FreightFee += (list[i].FreightFee != null ? Utils.GetDecimal(list[i].FreightFee.ToString()) : 0) ;
            }
            litTotal.Text = (GoodsFee).ToString() + "元";
            litGoodsFee.Text = (GoodsFee - FreightFee).ToString() + "元";
            litFreightFee.Text = (FreightFee).ToString() + "元";
            spanMoney.Text = litTotal.Text;
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember()
        {
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
                //判断是否设置过支付密码
                //bool IsPayPassword = BMemeberPayPassword.IsExist(model.Id);
                //if (IsPayPassword == false)
                //{
                //    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(58), "/WX/Fund/PayPasswordSet.aspx");
                //    return;
                //}
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 立即支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                decimal PayMoney = Utils.GetDecimal(litTotal.Text);
                if (PayMoney < 0)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(63));
                    return;
                }
                else
                {
                    //写入待支付
                    //string Id = System.Guid.NewGuid().ToString();
                    BMemberWallet.Add(new tbl_MemberWallet
                    {
                        Id = orderid,
                        TradeNumber = orderid,
                        UserId = 0,
                        UserContactName = "",
                        TypeId = (int)Model.EnumType.财务流水类型.消费,
                        MemberId = model.Id,
                        ContactName = model.ContactName,
                        TradeMoney = PayMoney,
                        IsPayed = '0',
                        PayMemberId = model.Id,
                        PayContactName = model.ContactName,
                        Remark = model.ContactName + "于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "支付 " + PayMoney.ToString("F2") + "元"+"订单Id,"+orderid,
                        IssueTime = DateTime.Now
                    });
                    //跳转到支付页
                    //string Sign = BMemberWallet.Sign(Id);
                    string Sign = orderid;
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