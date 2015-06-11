using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.CommonPage
{
    public partial class validatecode : System.Web.UI.Page
    {
        protected string CookieName = "SYS_TieLv_VC";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ValidateCodeName"]))
            {
                CookieName = Request.QueryString["ValidateCodeName"];
            }
            getNumbers(4, this.Page);
        }

        private void getNumbers(int len, Page page)
        {
            ValidateNumberAndChar.CurrentLength = len;
            ValidateNumberAndChar.CurrentNumber = ValidateNumberAndChar.CreateValidateNumber(ValidateNumberAndChar.CurrentLength);
            string number = ValidateNumberAndChar.CurrentNumber;
            ValidateNumberAndChar.CreateValidateGraphic(page, number);
            Response.Cookies[CookieName].Value = number;
        }


    }
}