using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.Job
{
    public partial class JobList : System.Web.UI.Page
    {
        #region 页面变量
        protected int pageSize = 20, pageIndex = 1, recordCount = 0;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        #region 绑定招聘列表页
        private void InitPage()
        {

            MjobSearch jobModel = new MjobSearch();
            jobModel.IsValid = 1;
            jobModel.startDate = DateTime.Now;
            jobModel.endDate = DateTime.Now;
            var list = BJob.GetListView(ref recordCount, pageSize, pageIndex, jobModel);
            if (recordCount > 0)
            {
                rptJobList.DataSource = list;
                rptJobList.DataBind();
            }
        }
        #endregion
    }
}