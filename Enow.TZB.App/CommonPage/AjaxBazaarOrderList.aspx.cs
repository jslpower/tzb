using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
namespace Enow.TZB.App.CommonPage
{
    public partial class AjaxBazaarOrderList : System.Web.UI.Page
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
                    Response.End();
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
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

            SearchModel.GoodsMemberId = MemberId;
            SearchModel.PayStatus =(int)Enow.TZB.Model.商城订单状态.已支付;
            var list = BMallOrders.GetBazaarOrderList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
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
            var sum = 0;//数量
            decimal GoodsFee = 0;//实付
            decimal FreightFee = 0;//运费
            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i].ShoppingNum;
                GoodsFee += list[i].GoodsFee * list[i].ShoppingNum + (list[i].FreightFee != null ? Utils.GetDecimal(list[i].FreightFee.ToString()) : 0);
                FreightFee += (list[i].FreightFee != null ? Utils.GetDecimal(list[i].FreightFee.ToString()) : 0);
            }
            return string.Format("共{0}件商品  实付：¥{1}  (含运费{2}元)", sum, GoodsFee, FreightFee);
        }
    }
}