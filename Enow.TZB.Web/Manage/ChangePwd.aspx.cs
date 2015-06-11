using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.Manage
{
    public partial class ChangePwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitUserInfo();
            }
        }
        /// <summary>
        /// 错误提醒
        /// </summary>
        private void ErrMsg()
        {
            MessageBox.ResponseScript("alert('未找到您要修改的信息!');parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();");
            return;
        }
        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitUserInfo()
        {
            ManagerList model = ManageUserAuth.GetManageUserModel();
            if (model == null)
            {
                ErrMsg();
            }
            else
            {
                this.ltrContactName.Text = model.ContactName;
                this.ltrUserName.Text = model.UserName;
            }
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            ManagerList model = ManageUserAuth.GetManageUserModel();
            if (model == null)
            {
                ErrMsg();
            }
            else
            {
                string Password = this.txtPwd.Text;
                if (!String.IsNullOrEmpty(Password))
                {
                    HashCrypto CrypTo = new HashCrypto();
                    Password = CrypTo.MD5Encrypt(Password.Trim());
                    CrypTo.Dispose();
                }
                if (!String.IsNullOrEmpty(strErr))
                {
                    MessageBox.ShowAndReturnBack(strErr);
                    return;
                }
                bool IsSucess = SysUser.UpdatePassword(model.Id, Password);
                if (IsSucess)
                    MessageBox.ShowAndRedirect("修改成功！", "/Manage/");
                else
                    MessageBox.ShowAndReturnBack("修改失败！");
                return;
            }
        }
    }
}