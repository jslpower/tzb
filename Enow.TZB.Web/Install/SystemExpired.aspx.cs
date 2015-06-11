using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Install
{
    public partial class SystemExpired : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnUpdateConfig_Click(object sender, System.EventArgs e)
        {
            string strErr = "";
            string LicenseNumber = this.txtSnNumber.Text;
            if (LicenseNumber == null || String.Empty == LicenseNumber)
                strErr = "请填写时间注册码!";
            else
                LicenseNumber = LicenseNumber.Trim();
            if (Adpost.Common.License.License.SystemExipre(LicenseNumber) != "true")
                strErr = "时间注册码错误!";
            if (strErr != "")
            {
                MessageBox.Show( strErr);
                return;
            }
            else
            {
                ConfigClass.SetConfigKeyValue("EyouSoftUD", LicenseNumber);
                //Adpost.Finawin.Utility.AppsettingConfigUtils.UpdateAppConfig("FinwinUD", LicenseNumber);
                MessageBox.ShowAndRedirect( "配置成功!", "/");
                return;
            }
        }
    }
}