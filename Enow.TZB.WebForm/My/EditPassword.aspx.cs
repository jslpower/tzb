using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.My
{
    public partial class EditPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BWebMemberAuth.LoginCheck();
                this.Master.Page.Title = "修改密码";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var model = BWebMemberAuth.GetUserModel();
            string memberId = model.Id;
            string strOldPwd = Utils.GetFormValue(txtOldPassword.UniqueID);
            string strNewPassword = Utils.GetFormValue(txtNewPassword.UniqueID);
            string strSurePassword = Utils.GetFormValue(txtSurePassword.UniqueID);
            var UserModel = BMember.GetModel(memberId);
            if (UserModel != null)
            {

                HashCrypto has = new HashCrypto();

                strOldPwd = has.MD5Encrypt(strOldPwd);
                if (strOldPwd != UserModel.Password)
                {
                    MessageBox.ShowAndReturnBack("新密码与原密码不一致!");

                    return;
            
                }

                bool IsSuccess = BMember.ChangePassword(memberId, has.MD5Encrypt(strNewPassword));

                if (IsSuccess)
                {
                    MessageBox.ShowAndRedirect("操作成功", "Default.aspx");
                }
                else
                {
                    MessageBox.ShowAndRedirect("操作失败", "EditPassword.aspx");
                }

            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/Default.aspx");
            }
        }


    }
}