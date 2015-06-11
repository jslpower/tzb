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
    public partial class ClassEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitClass();
            }
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitClass()
        {
            string Id = Request.QueryString["Id"].ToString();
            if (string.IsNullOrEmpty(Id) && StringValidate.IsInteger(Id))
            {
                ErrMsg();
            }
            else
            {

                var model = ArticleClass.GetModel(int.Parse(Id));
                if (model != null)
                {
                    txtTypeName.Text = model.TypeName;
                }
            }
        }
        /// <summary>
        /// 错误提醒
        /// </summary>
        private void ErrMsg()
        {
            MessageBox.ShowAndBoxClose("未找到您要修改的信息!", Request.QueryString["iframeId"]);
            return;
        }

        /// <summary>
        /// 保存资讯类别信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            string Id = Request.QueryString["id"];
            if (String.IsNullOrEmpty(Id) || StringValidate.IsInteger(Id) == false)
            {
                ErrMsg();

            }
            else
            {
                if (string.IsNullOrEmpty(txtTypeName.Text))
                {
                    strErr = "资讯类别名称不能为空！";
                }
            }

            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }

            bool IsSucess = ArticleClass.Update(new tbl_SysType
            {
                Id=int.Parse(Id),
                TypeName = txtTypeName.Text.Trim()
            });
            if (IsSucess)
            {
                MessageBox.ShowAndParentReload("修改成功！");
            }
            else
            {
                MessageBox.ShowAndReturnBack("修改失败！");
                return;
            }

        }
    }
}