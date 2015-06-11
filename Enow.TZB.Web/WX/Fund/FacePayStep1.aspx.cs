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
    /// 当面付支付密码确认
    /// </summary>
    public partial class FacePayStep1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitMember(OpenId);
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember(string OpenId)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                //判断是否设置过支付密码
                bool IsPayPassword = BMemeberPayPassword.IsExist(model.Id);
                if (IsPayPassword == false)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(58), "/WX/Fund/PayPasswordSet.aspx");
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
        /// 支付密码确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                string CuPassword = "";
                string Password = Utils.GetFormValue(txtPayPassword.UniqueID);
                if (String.IsNullOrWhiteSpace(Password))
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(59));
                    return;
                }
                else
                {
                    HashCrypto CrypTo = new HashCrypto();
                    Password = CrypTo.SHAEncrypt(Password.Trim());
                    CrypTo.Dispose();
                }
                //取得原始支付密码
                var PassModel = BMemeberPayPassword.GetModel(model.Id);
                if (PassModel != null)
                {
                    CuPassword = PassModel.PayPassword;
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(58), "/WX/Fund/PayPasswordSet.aspx");
                    return;
                }
                if (CuPassword != Password)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(61));
                    return;
                }
                //跳转生成支付密码
                Response.Redirect("FaceToPay.aspx", true);
                return;
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
    }
}