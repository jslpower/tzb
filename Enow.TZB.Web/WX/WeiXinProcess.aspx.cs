using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Weixin.Mp.Sdk;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX
{
    public partial class WeiXinProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                String postStr = String.Empty;
                if (Request.HttpMethod.ToUpper() == "POST")
                {
                    Response.Clear();
                    string OpenId = BWeiXin.MsgHandler();
                }
                else
                {
                    Response.Write(BWeiXin.Auth());
                    Response.End();
                }
            }
        }
    }
}