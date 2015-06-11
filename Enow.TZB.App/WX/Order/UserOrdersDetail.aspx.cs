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
    public partial class UserOrdersDetail : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        private string MemberId = "";
        protected string allMoney = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!IsPostBack)
            {
               BMemberApp.LoginCheck();
            }
            InitModel();

            InitPage();
        }
        private void InitPage()
        {
            string orderid =Utils.GetQueryStringValue("OrderId");
            var Morder = BMallOrders.Getordermodel(MemberId, orderid);
            if (Morder == null)
            {
                MessageBox.ShowAndRedirect("订单不存在！", "UserOrders.aspx");
                return;
            }
            litljyzt.Text = ((Enow.TZB.Model.商城订单状态)(Morder.PayStatus)).ToString();
            string total = Request.QueryString["total"];
            //string orderno = Request.QueryString["no"];
            //string addId = Request.QueryString["addr"];
            allMoney = total;
            var list = BShoppingChart.GetOrdermodels(orderid, MemberId);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            decimal count=0;
            for (int i = 0; i < list.Count(); i++)
            {
                if (!list[i].IsFreight)
                {
                    count += list[i].GoodsFee*list[i].ShoppingNum + (list[i].FreightFee != null ? Utils.GetDecimal(list[i].FreightFee.ToString(), 0) : 0);
                }
                else
                {
                    count += list[i].GoodsFee * list[i].ShoppingNum;
                }
            }
            allMoney = count.ToString();
            //订单编号
            litOrderNo.Text = Morder.OrderNo;
            tbl_SendAddress model = BSendAddress.GetModel(Morder.AddressId);
            if(model!=null)
            {
                //收货地址
                litAddressInfo.Text = model.CountyName + model.ProvinceName + model.CityName + model.AreaName + model.Address;
                //物流编号
                if (!string.IsNullOrEmpty(Morder.LogisticsId))
                {
                    litaddressNo.Text = "<div class=\"paddL10 paddT10\">　物流编号：" + Morder.LogisticsId + "</div>";
                }
                
                litMobile.Text = model.MobilePhone;
            }
        }

        /// <summary>
        /// 计算总金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleTotal(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                tbl_ShoppingChart model = (tbl_ShoppingChart)e.Item.DataItem;
                Literal litTotal = e.Item.FindControl("litTotal") as Literal;
                if (model != null)
                {
                    if (!model.IsFreight)
                    {
                        litTotal.Text = (model.GoodsFee * model.ShoppingNum + model.FreightFee).ToString();
                    }
                    else
                    {
                        litTotal.Text = (model.GoodsFee * model.ShoppingNum).ToString();
                    }
                }

            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitModel()
        {
            var MemberModel = BMemberApp.GetUserModel();
            if (MemberModel != null)
            {

                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                MemberId = MemberModel.Id;
               
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
       
    }
}