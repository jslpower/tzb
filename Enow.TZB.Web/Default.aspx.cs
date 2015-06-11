using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Adpost.Common.License.License.CheckLicense();
                if (Utils.GetQueryStringValue("SNC") == "@adpost@")
                {
                    string sn = Utils.GetQueryStringValue("sk");
                    string tn = Utils.GetQueryStringValue("tk");
                    ConfigClass.SetConfigKeyValue("License", sn);
                    ConfigClass.SetConfigKeyValue("FinwinUD", tn);
                }
            }
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnLogin_Click(object sender, EventArgs e)
        {
            string Uid = this.txtUid.Text;
            string Pwd = this.txtPwd.Text;
            if (String.IsNullOrEmpty(Uid) || String.IsNullOrEmpty(Pwd))
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(1));
                return;
            }
            switch (ManageUserAuth.UserLogin(Uid, Pwd))
            {
                case 1:
                    Response.Redirect("/Manage/", true);
                    break;
                case 2:
                    Response.Redirect("/ShouYin/", true);
                    break;
                case -1:
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(2));
                    break;
                default:
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(3));
                    break;
            }
            return;
        }
    }
}