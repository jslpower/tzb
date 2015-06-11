using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.SmartWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected static string Domain = System.Configuration.ConfigurationManager.AppSettings["WXDomain"];
        protected string FieldLatitudeJson = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                BMemberAuth.LoginCheck();
                FieldLatitudeJson = JsonConvert.SerializeObject(BBallField.GetList());
            }
        }
    }
}