using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Fund
{
    /// <summary>
    /// 账单
    /// </summary>
    public partial class PayBill : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = 50;
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitMember();
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
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
                InitList(model.Id);
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitList(string MemberId)
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
            Model.MWalletSearch SearchModel = new Model.MWalletSearch();
            SearchModel.MemberId = MemberId;
            SearchModel.IsPay= Model.EnumType.付款状态.已付;
            var list = BMemberWallet.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
        /// <summary>
        /// 根据流水类型显示正负号
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        protected string BillOperation(Model.EnumType.财务流水类型 Type)
        {
            string str = "";
            switch (Type)
            {
                case Model.EnumType.财务流水类型.充值:
                case Model.EnumType.财务流水类型.代充:
                case Model.EnumType.财务流水类型.线下充值:
                case Model.EnumType.财务流水类型.消费退款:
                case Model.EnumType.财务流水类型.转账收入:
                    str = "+";
                    break;
                case Model.EnumType.财务流水类型.消费:
                case Model.EnumType.财务流水类型.代消费:
                case Model.EnumType.财务流水类型.线下消费:
                case Model.EnumType.财务流水类型.转账支出:
                    str = "-";
                    break;
            }
            return str;
        }
    }
}