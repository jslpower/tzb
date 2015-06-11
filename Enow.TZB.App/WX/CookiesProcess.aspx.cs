using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX
{
    public partial class CookiesProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                string OpenId = Request.QueryString["OpenId"];
                if (!String.IsNullOrWhiteSpace(OpenId))
                {
                    HttpCookie Hc = new HttpCookie("WxOpenId");
                    Hc.Values.Add("OpenId", OpenId);
                    Hc.Expires = DateTime.Now.AddMonths(1);
                    HttpContext.Current.Response.Cookies.Add(Hc);
                    Response.End();
                }
            }
        }
    }
}