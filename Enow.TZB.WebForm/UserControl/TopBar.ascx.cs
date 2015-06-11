using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.UserControl
{
    public partial class TopBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BWebMemberAuth.IsLoginCheck())
            {
                InitBar();
            }
        }

        private void InitBar()
        {
            plnlogin.Visible = false;
            plnLoginDetail.Visible = true;
            var model = BWebMemberAuth.GetUserModel();
            string MemberId = model.Id;

            var UserModel = BMember.GetModel(MemberId);
            lblUserName.Text = UserModel.UserName;
            lblCurr.Text = UserModel.CurrencyNumber.ToString("f2");
            lblScore.Text = UserModel.IntegrationNumber.ToString();
        }
    }
}