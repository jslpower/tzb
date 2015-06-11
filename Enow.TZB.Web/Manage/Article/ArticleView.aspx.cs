using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Article
{
    public partial class ArticleView : System.Web.UI.Page
    {
        int Id = Utils.GetInt(Utils.GetQueryStringValue("id"));
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageUserAuth.ManageLoginCheck();
            if (Id>0)
            {
                InitPage(Id);
            }
        }

        protected void InitPage(int id)
        {
            var model = BLL.Common.sys.Article.GetModel(id);
            if (model!=null)
            {
                lblTitle.Text = model.Title;
                lblType.Text = GetTypeName(model.ClassId);
                lblTarget.Text = ((Model.发布对象)model.PublishTarget).ToString();
                lblStatus.Text = (bool)model.IsValid ? "已审核" : "未审核";
                lblOperatorName.Text = model.OperatorName;
                ltrContent.Text = model.ContentInfo;
            }
        }

        /// <summary>
        /// 根据TypeID返回类型名称
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        protected string GetTypeName(int ClassId)
        {
            string ReturnValue = "";

            MarticleTypeSeach searchModel = new MarticleTypeSeach();
            searchModel.id = ClassId;
            int rowsCount = 0;
            var list = ArticleClass.GetList(ref rowsCount, 1, 1, searchModel);
            if (rowsCount > 0)
            {
                ReturnValue = list[0].TypeName.ToString();
            }

            return ReturnValue;
        }
    }

  
}