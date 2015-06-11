using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.CommonPage
{
    public partial class ValidateCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getNumbers(4);
        }
        //呈现页面后台
        private void getNumbers(int len)
        {
            ValidateNumberAndChar.CurrentLength = len;
            ValidateNumberAndChar.CurrentNumber = ValidateNumberAndChar.CreateValidateNumber(ValidateNumberAndChar.CurrentLength);
            string number = ValidateNumberAndChar.CurrentNumber;
            ValidateNumberAndChar.CreateValidateGraphic(this, number);
            HttpCookie a = new HttpCookie("FinaWinValidCode", number);
            Response.Cookies.Add(a);

        }
    }
}