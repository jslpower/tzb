using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.My
{
    public partial class ChangePayPwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BWebMemberAuth.LoginCheck();
                var model = BWebMemberAuth.GetUserModel();
                string MemberId = model.Id;
                InitPage(MemberId);
            }

        }

        private void InitPage(string MemberId)
        {
            this.Master.Page.Title = "修改支付密码";
            var model = BMember.GetModel(MemberId);
            if (model != null)
            {
                if (string.IsNullOrWhiteSpace(model.ContactName))
                {
                    MessageBox.ShowAndRedirect("您的实名信息填写不完整，请补充!", "update.aspx");
                    return;
                }
                //取得原始支付密码
                var PassModel = BMemeberPayPassword.GetModel(MemberId);
                if (PassModel != null)
                {
                    phPaypwdAdd.Visible = false;
                    phPayPwdEdit.Visible = true;
                }
                else
                {
                    phPaypwdAdd.Visible = false;
                    phPaypwdAdd.Visible = true;
                }

            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/Default.aspx");
                return;
            }
        }



        /// <summary>
        /// 设置支付密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var model = BWebMemberAuth.GetUserModel();
            string MemberId = model.Id;
            var UserModel = BMember.GetModel(MemberId);
            if (UserModel != null)
            {
                string PayPwd = Utils.GetFormValue(txtPayPwd.UniqueID);
                if (string.IsNullOrWhiteSpace(PayPwd))
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(59));
                    return;
                }
                else
                {
                    HashCrypto CrypTo = new HashCrypto();
                    PayPwd = CrypTo.SHAEncrypt(PayPwd.Trim());
                    CrypTo.Dispose();
                }
                BMemeberPayPassword.Add(new tbl_MemberPayPassword
                {
                    Id = System.Guid.NewGuid().ToString(),
                    MemberId = model.Id,
                    PayPassword = PayPwd,
                    IsEnable = true,
                    IssueTime = DateTime.Now
                });
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(60), "Default.aspx");
                return;
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/Default.aspx");
                return;
            }
        }

        /// <summary>
        /// 修改支付密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
          var model=BWebMemberAuth.GetUserModel();
          string MemberId = model.Id;
          var Usermodel = BMember.GetModel(MemberId);
          if (Usermodel != null)
            {
                string CuPassword = "";
                string OldPassword = Utils.GetFormValue(txtOldPayPwd.UniqueID);
                string Password = Utils.GetFormValue(txtNewPayPwd.UniqueID);
                if (String.IsNullOrWhiteSpace(Password) || String.IsNullOrWhiteSpace(OldPassword))
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(59));
               
                    return;
                }
                else
                {
                    HashCrypto CrypTo = new HashCrypto();
                    Password = CrypTo.SHAEncrypt(Password.Trim());
                    OldPassword = CrypTo.SHAEncrypt(OldPassword.Trim());
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
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(58), "ChangePayPwd.aspx");
                    return;
                }
                if (CuPassword != OldPassword)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(61));
                    return;
                }
                var rv = BMemeberPayPassword.Update(model.Id, Password);
                if (rv)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(60), "Default.aspx");
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(62), "ChangePayPwd.aspx");
                    return;
                }
                return;
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/Default.aspx");
                return;
            }
        }
    }
}