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
    public partial class DZDetail : System.Web.UI.Page
    {
        #region 页面变量
        protected string Title = "", Content = "", PhotoUrl = "";
        protected DateTime IssueTime = DateTime.Now;
        protected string PageTitle = "";
        protected int ClassId = 0;

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
            ClassId = Utils.GetInt(Utils.GetQueryStringValue("ClassId"), 0);
            if (ClassId < 1)
            {
                PageTitle = "文章详细";
            }
            else
            {
                var ClassModel = ArticleClass.GetModel(ClassId);
                #region 根据ClassId获取当前页面的title
                if (ClassModel != null)
                {
                    PageTitle = ClassModel.TypeName;
                }
                else
                {
                    PageTitle = "文章详细";
                }
                #endregion
            }


            int id = Utils.GetInt(Utils.GetQueryStringValue("Id"), 0);
            if (id < 1)
            {
                MessageBox.ShowAndRedirect("文章不存在", "/Default.aspx");
                return;
            }
            var model = BLL.Common.sys.Article.GetModel(id);
            if (model != null)
            {
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