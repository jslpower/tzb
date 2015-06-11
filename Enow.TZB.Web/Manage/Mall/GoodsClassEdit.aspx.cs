using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Mall
{
    public partial class GoodsClassEdit : System.Web.UI.Page
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

                var model = BGoodsClass.GetModel(int.Parse(Id));
                if (model != null)
                {
                    txtTypeName.Text = model.ClassName;
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
        /// 保存商品分类信息
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
                    strErr = "商品类别名称不能为空！";
                }
            }

            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }

            bool IsSucess = BGoodsClass.Update(new tbl_GoodsClass
            {
                Id = int.Parse(Id),
                ClassName = txtTypeName.Text.Trim()
            });
            if (IsSucess)
            {
                MessageBox.ShowAndParentReload("商品分类修改成功！");
            }
            else
            {
                MessageBox.ShowAndReturnBack("商品分类修改失败！");
                return;
            }

        }
    }
}