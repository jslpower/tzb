using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Install
{
    public partial class Install : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("sk") == "get")
            {
                this.ltrSerialNumber.Text = Adpost.Common.License.License.GetSerialNumber();
                return;
            }
            this.ltrSerialNumber.Text = Adpost.Common.License.License.GetSerialNumber();
            if (!Page.IsPostBack)
            {
                string LicenseNumber = System.Configuration.ConfigurationManager.AppSettings["License"];
                string TimeSN = System.Configuration.ConfigurationManager.AppSettings["EyouSoftUD"];
                if (Adpost.Common.License.License.CheckLicense(LicenseNumber) == true && Adpost.Common.License.License.SystemExipre(TimeSN) == "true")
                {
                    Response.Redirect("/", true);
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateConfig_Click(object sender, System.EventArgs e)
        {
            string strErr = "";
            string LicenseNumber = this.txtSnNumber.Text;
            string TimeSN = this.txtTimeSN.Text;
            if (LicenseNumber == null || String.Empty == LicenseNumber)
                strErr = "请填写注册码!";
            else
                LicenseNumber = LicenseNumber.Trim();
            if (String.IsNullOrEmpty(TimeSN))
                strErr = "请填写注册码2!";
            else
                TimeSN = TimeSN.Trim();
            if (Adpost.Common.License.License.CheckLicense(LicenseNumber) == false)
                strErr = "注册码错误!";
            if (Adpost.Common.License.License.SystemExipre(TimeSN) != "true")
                strErr = "注册码2错误!";
            if (strErr != "")
            {
                MessageBox.Show( strErr);
                return;
            }
            else
            {
                ConfigClass.SetConfigKeyValue("License", LicenseNumber);
                ConfigClass.SetConfigKeyValue("EyouSoftUD", TimeSN);
                //Adpost.Finawin.Utility.AppsettingConfigUtils.UpdateAppConfig("License", LicenseNumber);
                MessageBox.ShowAndRedirect( "配置成功!", "/");
                return;
            }
        }
    }
}