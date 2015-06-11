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

namespace Enow.TZB.Web.WX.Job
{
    /// <summary>
    /// /// <summary>
    /// 微信 堂主招聘
    /// </summary>
    /// </summary>
    public partial class TZJob : System.Web.UI.Page
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
                BMemberAuth.LoginCheck();
                //MessageBox.ShowAndRedirect("全球堂主招募尚未开始,\\n敬请期待！", "/WX/Member/Default.aspx");
                //return;
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
            SearchModel.jobType = (int)Model.EnumType.JobType.堂主;
            SearchModel.IsValid = 1;
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.JobName = KeyWord;
            }
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
                PlaceHolder1.Visible = false;
            }

        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string KeyWords = Utils.GetFormValue(this.txtKeyWords.UniqueID);
            if (!String.IsNullOrEmpty(KeyWords) && KeyWords != "岗位搜索")
            {
                Response.Redirect("DZJob.aspx?KeyWord=" + Server.UrlEncode(KeyWords));
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(33));
            }
        }
    }
}