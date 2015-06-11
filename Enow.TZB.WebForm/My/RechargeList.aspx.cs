using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;


namespace Enow.TZB.WebForm.My
{
    public partial class RechargeList : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数,页码
        /// </summary>
        protected int intPageSize =10,CurrencyPage = 1;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Master.Page.Title = "充值记录列表";
                BWebMemberAuth.LoginCheck();
                var model=BWebMemberAuth.GetUserModel();
                InitPage(model.Id);
            }
        }

        private void InitPage(string MemberId)
        {
            #region 列表查询实体
            MWalletSearch searchModel = new MWalletSearch();
            searchModel.BeginDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("begindate"));
            searchModel.EndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("enddate"));
            searchModel.MemberId = MemberId;
            searchModel.IsPay = Model.EnumType.付款状态.已付;
            if (Utils.GetInt(Utils.GetQueryStringValue("TypeId"), -1) >=0)
            {
                searchModel.TypeId = (Model.EnumType.财务流水类型)Utils.GetInt(Utils.GetQueryStringValue("TypeId"));
            }
            #endregion

            int Rowcount = 0;
            string Page = Request.QueryString["Page"];
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                CurrencyPage = int.Parse(Page);
                if (CurrencyPage < 1)
                    CurrencyPage = 1;
            }

            var list = BMemberWallet.GetList(ref Rowcount, intPageSize, CurrencyPage, searchModel);
            if (Rowcount > 0)
            {
                rptRechargeList.DataSource = list;
                rptRechargeList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = intPageSize;
                ExportPageInfo1.intRecordCount = Rowcount;
                ExportPageInfo1.CurrencyPage = CurrencyPage;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
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