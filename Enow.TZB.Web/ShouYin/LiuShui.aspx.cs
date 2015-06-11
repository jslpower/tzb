using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.ShouYin
{
    public partial class LiuShui :ShouYinYeMian
    {
        #region attributes
        /// <summary>
        /// 球场名称
        /// </summary>
        protected string QiuChangName = string.Empty;
        /// <summary>
        /// 每页记录数
        /// </summary>
        protected int PageSize = 5;
        /// <summary>
        /// 页序号
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        protected int RecordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            QiuChangName = YongHuInfo.FieldName;
            InitPage();
        }

        #region 加载订单流水记录
        private void InitPage()
        {
            phOrderDetail.Visible = true;
            var SearchModel = new Model.MSummarySearch();
            SearchModel.BallFieldId = YongHuInfo.FieldId;
            SearchModel.IssueBeginTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtBeginTime"));
            SearchModel.IssueEndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
            PageIndex = UtilsCommons.GetPadingIndex();
            var list = BLL.Order.BOrder.GetCashierFlow(ref RecordCount, PageSize, PageIndex, SearchModel);
            if (RecordCount > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
            else
            {
                phEmptyDetail.Visible = true;
            }

        }
        #endregion
    }
}