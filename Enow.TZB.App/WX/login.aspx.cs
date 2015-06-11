using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.SMS;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.App.WX
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var member = BMemberApp.GetUserModel();
                if (member!=null)
                {
                    Response.Redirect("Member/Default.aspx");
                }
            }
        }

        protected void btn_dl_Click(object sender, EventArgs e)
        {
            var usname = Utils.InputText(Utils.GetFormValue(txtusname.UniqueID));
            var password = Utils.InputText(Utils.GetFormValue(txtuspassword.UniqueID));
            //has.MD5Encrypt(strpwd),
            if (string.IsNullOrEmpty(usname)||string.IsNullOrEmpty(password))
            {
                MessageBox.ShowAndParentReload("请输入账号密码！");
            }
            HashCrypto has = new HashCrypto();
            int retin= BMemberApp.UserLogin(usname, has.MD5Encrypt(password));
            if (retin==1)
            {
                MessageBox.ShowAndRedirect("登录成功！", "Member/Default.aspx");
            }
            else
            {
                MessageBox.ShowAndParentReload("账号或密码错误！");
            }
        }
    }
}