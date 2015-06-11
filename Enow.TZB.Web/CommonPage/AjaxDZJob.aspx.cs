using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using System.Text;

namespace Enow.TZB.App.CommonPage
{
    public partial class AjaxDZJob : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitList();
            }
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        private void InitList()
        {
            #region 查询实体
            MjobSearch SearchModel = new MjobSearch();
            
            SearchModel.IsValid = 1;
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            int types =Utils.GetInt(Utils.GetQueryStringValue("types"),1);
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.JobName = KeyWord;
            }
            SearchModel.jobType = types;
            #endregion
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
            var list = BJob.GetListView(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
            else
            {
                Response.End();
            }

        }
    }
}