using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.CommonPage
{
    public partial class AjaxUserOrderList : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected decimal total = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitMember();
                InitList();
            }
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
                    //您未填写实名信息,\n请补充填写！
                    Response.End();
                }
                
                MemberId = model.Id;
            }
            else
            {
                Response.End();
            }
        }
        /// <summary>
        /// 加载订单列表
        /// </summary>
        private void InitList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }

            Model.MallorderQuery SearchModel = new Model.MallorderQuery();
            SearchModel.PayStatus = Utils.GetInt(Utils.GetQueryStringValue("type"), -1);
            SearchModel.MemberId = MemberId;
            SearchModel.IsDelete = false;

            var list = BMallOrders.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }


        }
        /// <summary>
        /// 处理操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleOperation(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                tbl_MallOrder model = (tbl_MallOrder)e.Item.DataItem;
                Literal litOperation = e.Item.FindControl("litOperation") as Literal;
                if (model != null)
                {
                    if (model.PayStatus == (int)Enow.TZB.Model.商城订单状态.审核无效)
                    {
                        litOperation.Text = " <a href=\"javascript:void(0);\" onclick=\"PageJsDataObj.DeleteOrder('" + model.OrderId + "','" + model.AddressId + "')\" class=\"gray_btn\">删除订单" +
                  " </a>";
                    }

                    if (model.PayStatus == (int)Enow.TZB.Model.商城订单状态.未支付)
                    {
                        litOperation.Text = " <a href=\"Pay.aspx?OrderId=" + model.OrderId + "\"  class=\"gray_btn\">支付</a>";
                    }

                }

            }
        }
        /// <summary>
        /// 查询图片信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string Getimgurl(string orderid)
        {
            var list = BShoppingChart.GetOrdermodels(orderid, MemberId);
            return list.Count > 0 ? list[0].GoodsPhoto : "";
        }
        /// <summary>
        /// 计算订单信息
        /// </summary>
        /// <param name="orderid">订单编号</param>
        /// <returns></returns>
        public string Getsum(string orderid)
        {
            var list = BShoppingChart.GetOrdermodels(orderid, MemberId);
            var sum = (from temp in list select temp.ShoppingNum).Sum();//数量
            decimal GoodsFee = (from temp in list select temp.GoodsFee).Sum(od => (Utils.GetDecimal(od.ToString(), 0)));//实付
            decimal FreightFee = (from temp in list select temp.FreightFee).Sum(od => (od != null ? Utils.GetDecimal(od.ToString(), 0) : 0));//运费
            total = GoodsFee + FreightFee;
            return string.Format("共{0}件商品  实付：¥{1}  (含运费{2}元)", sum, GoodsFee + FreightFee, FreightFee);
        }
    }
}