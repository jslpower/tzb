using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Cashier
{
    public partial class UserEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitUserInfo();
            }
        }
        /// <summary>
        /// 错误提醒
        /// </summary>
        private void ErrMsg()
        {
            MessageBox.ResponseScript("alert('未找到您要修改的信息!');parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();");
            return;
        }
        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitUserInfo()
        {
            string Id = Request.QueryString["id"];
            if (String.IsNullOrEmpty(Id) || StringValidate.IsInteger(Id) == false)
            {
                ErrMsg();
            }
            else
            {
                var model = SysUser.GetModel(Convert.ToInt32(Id));
                if (model != null)
                {
                    this.txtContactName.Text = model.ContactName;
                    this.txtTel.Text = model.ContactTel;
                    this.ltrUserName.Text = model.UserName;
                    this.txtPwd.Text = "";
                    InitFieldList(model.FieldId);
                }
                else { ErrMsg(); }
            }
        }
        /// <summary>
        /// 绑定球场
        /// </summary>
        /// <param name="RoleId"></param>
        private void InitFieldList(string Field)
        {
            this.ddlFieldId.DataSource = BBallField.GetList();
            this.ddlFieldId.DataTextField = "FieldName";
            this.ddlFieldId.DataValueField = "Id";
            this.ddlFieldId.DataBind();
            this.ddlFieldId.Items.Insert(0, new ListItem("请选择球场", "0"));
            if (Field != "0")
            {
                for (int i = 0; i < this.ddlFieldId.Items.Count; i++)
                {
                    if (this.ddlFieldId.Items[i].Value == Field)
                    {
                        this.ddlFieldId.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            string rolePer = string.Empty;
            string Id = Request.QueryString["id"];
            if (String.IsNullOrEmpty(Id) || StringValidate.IsInteger(Id) == false)
            {
                ErrMsg();
            }
            else
            {
                int RoleId = 0;
                string Tel = this.txtTel.Text;
                string ContactName = this.txtContactName.Text;
                string Password = this.txtPwd.Text;
                string FieldId = Utils.GetFormValue(ddlFieldId.UniqueID);
                string FieldName = "";
                if (FieldId == "0")
                {
                    strErr = "请选择球场！";
                }
                else
                {
                    var FieldModel = BBallField.GetModel(FieldId);
                    if (FieldModel != null)
                    {
                        FieldName = FieldModel.FieldName;
                    }
                    else
                    {
                        strErr = "系统不存在您选择的球场！";
                    }
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
                bool IsSucess = SysUser.Update(new ManagerList
                {
                    Id = Convert.ToInt32(Id),
                    Password = Password,
                    RoleId = RoleId,
                    PermissionList = rolePer,
                    ContactName = ContactName,
                    ContactTel = Tel,
                    FieldId = FieldId,
                    FieldName = FieldName
                });
                if (IsSucess)
                    MessageBox.ShowAndParentReload("修改成功！");
                else
                    MessageBox.ShowAndReturnBack("修改失败！");
                return;
            }
        }
    }
}