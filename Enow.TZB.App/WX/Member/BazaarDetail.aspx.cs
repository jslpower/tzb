using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.App.WX.Member
{
    public partial class BazaarDetail : System.Web.UI.Page
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
                BMemberApp.LoginCheck();
                InitModel();
                InitPage();
            }
        }
        private void InitPage()
        {
            string orderid = Utils.GetQueryStringValue("OrderId");
            var Morder = BMallOrders.GetBazaarOrderViewmodel(MemberId, orderid);
            if (Morder == null)
            {
                MessageBox.ShowAndRedirect("订单不存在！", "BazaarOrderList.aspx");
                return;
            }
            litljyzt.Text = ((Enow.TZB.Model.商城订单状态)(Morder.PayStatus)).ToString();
            //string orderno = Request.QueryString["no"];
            //string addId = Request.QueryString["addr"];
            var list = BShoppingChart.GetBazaarOrdermodels(orderid, MemberId);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            decimal count = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                 if (!list[i].CharIsFreight)
                    {
                     count += list[i].GoodsFee*list[i].ShoppingNum + (list[i].CharFreightFee!=null?Utils.GetDecimal(list[i].CharFreightFee.ToString(),0):0);
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
            if (model != null)
            {
                //收货地址
                litAddressInfo.Text = model.CountyName + model.ProvinceName + model.CityName + model.AreaName + model.Address;
                //物流编号
                if (!string.IsNullOrEmpty(Morder.LogisticsId))
                {
                    txtwuliu.Text = Morder.LogisticsId;
                    txtwuliu.Enabled = false;
                    LinkButton1.Visible = false;
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
                        litTotal.Text = (model.GoodsFee*model.ShoppingNum + model.FreightFee).ToString();
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string orderid = Utils.GetQueryStringValue("OrderId");
            if (!string.IsNullOrEmpty(orderid))
            {
                bool retval = BMallOrders.UpdateLogisticsState(orderid, Utils.GetText(Utils.GetFormEditorValue(txtwuliu.UniqueID), 50));
                if (retval)
                {
                    var UserModel = BMemberApp.GetUserModel();
                    int UserId =0;
                    string ContactName = UserModel.ContactName;
                    UserModel = null;
                    BMallOrders.UpdateState(orderid, UserId, ContactName, Model.商城订单状态.已发货);
                    //BMallOrders.UpdateGoodsStockNum(orderid);
                    MessageBox.ShowAndRedirect("操作成功!", "BazaarOrderList.aspx");
                }
                else
                {
                    MessageBox.ShowAndRedirect("订单错误!请重新选择", "BazaarOrderList.aspx");
                }

            }
        }
    }
}