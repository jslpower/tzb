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
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Getuser();
        }
        private void Getuser()
        {
            var model = BMemberApp.GetUserModel();
            if (model!=null)
            {
                LinkButton1.Visible = true;
                littxt.Text = "<a class=\"whitelink\" href=\"Member/Default.aspx\">您好！" + model.ContactName + "</a>";
            }
            else
            {
                littxt.Text = "<a class=\"whitelink\" href=\"login.aspx\">登录</a>  ";
                litright.Text = "<a class=\"whitelink\" href=\"Register/Step1.aspx\">注册</a>";
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            BMemberApp.Logout("index.aspx");
        }
    }
}