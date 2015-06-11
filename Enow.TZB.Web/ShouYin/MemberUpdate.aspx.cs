using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.ShouYin
{
    public partial class MemberUpdate : ShouYinYeMian
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitUserInfo();
            }
        }
        /// <summary>
        /// 错误提醒
        /// </summary>
        private void ErrMsg()
        {
            MessageBox.ShowAndRedirect("未找到您要修改的信息","/");
            return;
        }
        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitUserInfo()
        {
            var _YongHuInfo = ManageUserAuth.GetManageUserModel();
            int Id = _YongHuInfo.Id;
            if (Id>0)
            {
                var model = SysUser.GetModel(Id);
                if (model != null)
                {
                    this.ltrUserName.Text = model.UserName;
                    this.ltrContactName.Text = model.ContactName;
                    this.txtContactTel.Text = model.ContactTel;
                }
                else { ErrMsg(); } 
            }
            else
            {
                ErrMsg();
            }
        }
        /// <summary>
        /// 保存用户信息修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
           var _YongHuInfo = ManageUserAuth.GetManageUserModel();
            int Id = _YongHuInfo.Id;
            if (Id>0)
            {
                string Tel = Utils.GetFormValue(txtContactTel.UniqueID);
                string OldPaword = Utils.GetFormValue(txtOldPassword.UniqueID);
                string Password = Utils.GetFormValue(txtPwd.UniqueID);
                if (String.IsNullOrWhiteSpace(Tel)) {
                    strErr = "请填写手机号码！";
                }
                if (!String.IsNullOrEmpty(OldPaword))
                {
                    HashCrypto CrypTo = new HashCrypto();
                    OldPaword = CrypTo.MD5Encrypt(OldPaword.Trim());
                    CrypTo.Dispose();
                }
                var Model = SysUser.GetModel(Id);
                if (Model != null)
                {
                    if (Model.Password != OldPaword)
                    {
                        strErr = "您输入的原始密码不正确！";
                    }
                }
                else
                {
                    strErr = "未找到您要修改的用户信息！";
                }
                if (!String.IsNullOrEmpty(Password))
                {
                    HashCrypto CrypTo = new HashCrypto();
                    Password = CrypTo.MD5Encrypt(Password.Trim());
                    CrypTo.Dispose();
                }
                if (!String.IsNullOrEmpty(strErr))
                {
                    MessageBox.ShowAndReturnBack(strErr);
                    return;
                }
                bool IsSucess = SysUser.UpdateBasicInfo(Id, Tel, Password);
                if (IsSucess)
                    MessageBox.ShowAndRedirect("修改成功！", "Default.aspx");
                else
                    MessageBox.ShowAndReturnBack("修改失败！");
                return;
            }
            else
            {
                ErrMsg();
            }
        }
    }
}