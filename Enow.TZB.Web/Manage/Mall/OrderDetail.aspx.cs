using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Mall
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        private string MemberId = "";
        protected string allMoney = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();

                InitPage();

            }
          
        }
        /// <summary>
        /// 计算订单信息
        /// </summary>
        /// <param name="orderid">订单编号</param>
        /// <returns></returns>
        public string Getsum(string orderid)
        {
            var list = BShoppingChart.GetOrdermodels(orderid);
            var sum = 0;//数量
            decimal GoodsFee = 0;//实付
            decimal FreightFee = 0;//运费
            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i].ShoppingNum;
                GoodsFee += list[i].GoodsFee * list[i].ShoppingNum + (list[i].FreightFee != null ? Utils.GetDecimal(list[i].FreightFee.ToString()) : 0);
                FreightFee += (list[i].FreightFee != null ? Utils.GetDecimal(list[i].FreightFee.ToString()) : 0);
            }
            return GoodsFee.ToString();
        }
        private void InitPage()
        {
            string orderid = Request.QueryString["OrderId"];
          
            string orderno = Request.QueryString["no"];
            string addId = Request.QueryString["addr"];
         
            var list = BShoppingChart.GetOrdermodels(orderid);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            litTotal.Text = Getsum(orderid);
            //订单编号
            //litOrderNo.Text = orderno;

            tbl_SendAddress model = BSendAddress.GetModel(addId);
            if (model != null)
            {
                //收货地址
                litAddress.Text = model.Address;
                //物流编号
                if (!string.IsNullOrEmpty(model.LogisticsNo))
                {
                    litaddressNo.Text = model.LogisticsNo;
                }
                else
                {
                    litaddressNo.Text = "暂无";
                }
                litMobile.Text = model.MobilePhone;
            }
        }
      
    }
}