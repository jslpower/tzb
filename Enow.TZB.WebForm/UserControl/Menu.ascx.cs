using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.UserControl
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected int HeadMenuIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            HeadMenuIndex = Utils.GetInt(Utils.GetQueryStringValue("HmId"), 0);
        }
    }
}