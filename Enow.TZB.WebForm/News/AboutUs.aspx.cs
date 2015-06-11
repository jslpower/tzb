using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using Enow.TZB.Model;

namespace Enow.TZB.WebForm.News
{
    public partial class AboutUs : System.Web.UI.Page
    {
        #region 页面变量
        protected string  Content = "", PhotoUrl = "";
        protected DateTime IssueTime = DateTime.Now;
        protected string PageTitle = "";
        protected int Id = 0;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            Id = Utils.GetInt(Utils.GetQueryStringValue("Id"), 0);
            if (Id < 1)
            {
                Id = 56;
            }
            var model = BLL.Common.sys.Article.GetModel(Id);
            if (model != null)
            {
                PageTitle = model.Title;
                this.Header.Title = model.Title;
                Title = model.Title;
                Content = model.ContentInfo;
                IssueTime = model.IssueTime;
                if (!string.IsNullOrWhiteSpace(model.PhotoUrl))
                {
                    ltrPhoto.Text = "<img src=\" " + model.PhotoUrl + " \" width=\"540\" height=\"340\">";
                }
            }
            else
            {
                MessageBox.ShowAndRedirect("文章不存在", "/Default.aspx");
            }


        }
    }
}