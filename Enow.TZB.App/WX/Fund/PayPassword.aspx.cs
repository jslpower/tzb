using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;
using Enow.TZB.SMS;

namespace Enow.TZB.Web.WX.Fund
{
    /// <summary>
    /// 修改支付密码
    /// </summary>
    public partial class PayPassword : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
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
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
        /// <summary>
        /// 更新支付密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                string CuPassword = "";
                string OldPassword = Utils.GetFormValue(txtOldPassword.UniqueID);
                string Password = Utils.GetFormValue(txtPayPassword.UniqueID);
                if (String.IsNullOrWhiteSpace(Password) || String.IsNullOrWhiteSpace(OldPassword))
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(59));
                    return;
                }
                else {
                    HashCrypto CrypTo = new HashCrypto();
                    Password = CrypTo.SHAEncrypt(Password.Trim());
                    OldPassword = CrypTo.SHAEncrypt(OldPassword.Trim());
                    CrypTo.Dispose();
                }
                //取得原始支付密码
                var PassModel = BMemeberPayPassword.GetModel(model.Id);
                if (PassModel != null) {
                    CuPassword = PassModel.PayPassword;
                } else {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(58), "/WX/Fund/PayPasswordSet.aspx");
                    return;
                }
                if (CuPassword != OldPassword)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(61));
                    return;
                }
                var rv = BMemeberPayPassword.Update(model.Id,Password);
                if (rv)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(60), "Default.aspx");
                }
                else {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(62), "/WX/Fund/PayPasswordSet.aspx");
                    return;
                    }
                return;
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 支付密码重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {

            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                if (!String.IsNullOrWhiteSpace(model.MobilePhone))
                {
                    ///获取6位随机密码
                    string Password = MakeValidCode();                    
                    //发送验证码至手机
                    string v = BSMS.Send(model.MobilePhone, String.Format(CacheSysMsg.GetMsg(159), Password));
                    //密码加密
                    HashCrypto CrypTo = new HashCrypto();
                    Password = CrypTo.SHAEncrypt(Password.Trim());
                    CrypTo.Dispose();
                    //更新支付密码
                    var rv = BMemeberPayPassword.Update(model.Id, Password);
                    if (rv)
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(160), "Default.aspx");
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(62), "/WX/Fund/PayPasswordSet.aspx");
                        return;
                    }
                }
                else {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(158));
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 生成6位数字随机验证码
        /// </summary>
        /// <returns></returns>
        private static string MakeValidCode()
        {
            char[] str = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string num = "";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                num += str[r.Next(0, str.Length)].ToString();
            }

            return num;
        }
    }
}