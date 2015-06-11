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
    public partial class ClassAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 ManageUserAuth.ManageLoginCheck();
            }
        }

        /// <summary>
        /// 新增资讯类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            string StrTypeName = txtTypeName.Text;
            int typeId = (int)资讯类型.资讯;
            if (string.IsNullOrEmpty(StrTypeName))
            {
                strErr = "资讯类别名称不能为空!";

            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }

            ArticleClass.Add(new tbl_SysType { 
            TypeId=typeId,
            TypeName=StrTypeName
            });
            MessageBox.ShowAndParentReload("添加成功！");
            return;

        }
    }
}