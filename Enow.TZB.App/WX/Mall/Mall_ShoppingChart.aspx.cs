using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Mall
{
    public partial class Mall_ShoppingChart : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = 999;
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            InitMember();
            var action = Utils.GetQueryStringValue("action").ToLower();
            switch (action)
            {
                case "delete":
                    CharDelete();
                    break;
            }
            if (!IsPostBack)
            {
                InitList();
            }
        }
        private void CharDelete()
        {
            var CharID = Utils.GetQueryStringValue("charid");
            if (!string.IsNullOrEmpty(CharID))
            {
                bool retbol = BShoppingChart.Delete(CharID,MemberId);
               if (retbol)
               {
                   Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功！"));
               }
               else
               {
                   Utils.RCWE(UtilsCommons.AjaxReturnJson("0","删除失败！"));
               }
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
                        litTotal.Text = (model.GoodsFee + model.FreightFee).ToString();
                    }
                    else
                    {
                        litTotal.Text = (model.GoodsFee).ToString();
                    }
                }

            }
        }
        /// <summary>
        /// 加载购物列表
        /// </summary>
        private void InitList()
        {
            if (!string.IsNullOrEmpty(MemberId))
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
                Model.MMallGoodsSearch SearchModel = new Model.MMallGoodsSearch();
                SearchModel.MemberId = MemberId;

                if (!string.IsNullOrEmpty(txtGoodsName.Text))
                {
                    SearchModel.GoodsName = Utils.InputText(txtGoodsName.Text);
                }
                var list = BShoppingChart.GetShoppingGoodsList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
                if (list.Count() > 0)
                {
                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                }
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }

        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void InitMember()
        {
            BMemberApp.LoginCheck();
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            InitList();
        }
    }
}