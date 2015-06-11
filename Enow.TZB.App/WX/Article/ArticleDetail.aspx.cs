using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Article
{
    public partial class ArticleDetail : System.Web.UI.Page
    {
        public string wztitle = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int articleId = Utils.GetInt(Utils.GetQueryStringValue("Id"));
                if (articleId > 0)
                {
                    InitArticle(articleId);
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
        }

        protected void InitArticle(int id)
        {
            var model = BLL.Common.sys.Article.GetModel(id);
            if (model!=null&&model.ClassId>0)
            {
                var classModel = ArticleClass.GetModel(model.ClassId);
                wztitle = classModel.TypeName;
                UserHome1.Userhometitle = wztitle;
                ltrTitle.Text = model.Title;
                ltrContent.Text = model.ContentInfo;
                //更新文章浏览数量
                BLL.Common.sys.Article.UpdateView(model.ID, Model.发布对象.APP);
            }
        }
    }
}