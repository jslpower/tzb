using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Enow.TZB.WebForm.Notice
{
    public partial class Notice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Page.Title = "温馨提示";
        }
    }
}