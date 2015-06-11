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
    /// 设置支付密码
    /// </summary>
    public partial class PayPasswordSet : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 设置密码
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
                string Password = Utils.GetFormValue(txtPayPassword.UniqueID);
                if (String.IsNullOrWhiteSpace(Password))
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(59));
                    return;
                }
                else {
                    HashCrypto CrypTo = new HashCrypto();
                    Password = CrypTo.SHAEncrypt(Password.Trim());
                    CrypTo.Dispose();
                }
                BMemeberPayPassword.Add(new tbl_MemberPayPassword {
                    Id = System.Guid.NewGuid().ToString(),
                    MemberId=model.Id,
                    PayPassword = Password,
                    IsEnable = true,
                    IssueTime = DateTime.Now
                });
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(60), "Default.aspx");
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