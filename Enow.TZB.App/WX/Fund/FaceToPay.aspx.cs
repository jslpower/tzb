using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Fund
{
    /// <summary>
    /// 生成支付二维码
    /// </summary>
    public partial class FaceToPay : System.Web.UI.Page
    {
        private string QrCodePath = System.Configuration.ConfigurationManager.AppSettings["QRCodeFilePath"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitMember();
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void InitMember()
        {
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                string QrCodeInfo = model.Id +"||"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                QrCodeInfo = BMemberWallet.Sign(QrCodeInfo);
                this.imgPayQrCode.ImageUrl = "/Ashx/GetQrCode.ashx?QCode=" + QrCodeInfo + "&R=" + System.Guid.NewGuid().ToString();
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
    }
}