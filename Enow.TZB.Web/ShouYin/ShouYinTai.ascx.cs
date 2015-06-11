using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Enow.TZB.Web.ShouYin
{
    public partial class ShouYinTai : System.Web.UI.UserControl
    {
        #region attributes
        string _Display = "block";
        /// <summary>
        /// style.display default:block 
        /// </summary>
        public string Display { get { return _Display; } set { _Display = value; } }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}