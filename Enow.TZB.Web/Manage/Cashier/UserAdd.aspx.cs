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
    /// <summary>
    /// 收银员添加
    /// </summary>
    public partial class UserAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageUserAuth.ManageLoginCheck();
            if (!IsPostBack)
            {
                InitFieldList();
            }
        }

        /// <summary>
        /// 绑定角色组
        /// </summary>
        /// <param name="RoleId"></param>
        private void InitFieldList()
        {
            this.ddlFieldId.DataSource = BBallField.GetList();
            this.ddlFieldId.DataTextField = "FieldName";
            this.ddlFieldId.DataValueField = "Id";
            this.ddlFieldId.DataBind();
            this.ddlFieldId.Items.Insert(0, new ListItem("请选择球场", "0"));

        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string Contact_Name = txtContactName.Text.Trim();
            string Tel = txtTel.Text.Trim();
            string User_Name = txtUserName.Text.Trim();
            string User_Pwd = txtPwd.Text.Trim();

            string strErr = "";
            string rolePer = string.Empty;
            int RoleId = 0;
            string FieldId = Utils.GetFormValue(ddlFieldId.UniqueID);
            string FieldName="";
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
            if (!String.IsNullOrEmpty(User_Pwd))
            {
                HashCrypto CrypTo = new HashCrypto();
                User_Pwd = CrypTo.MD5Encrypt(User_Pwd.Trim());
                CrypTo.Dispose();
            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }

            SysUser.Add(new ManagerList
            {
                RoleId = RoleId,
                EmployeeId = 0,
                UserName = User_Name,
                Password = User_Pwd,
                ContactName = Contact_Name,
                ContactTel = Tel,
                PermissionList = "",
                IsEnable = true,
                LastLoginIp = null,
                LastLoginTime = DateTime.Now,
                IssueTime = DateTime.Now,
                FieldId = FieldId,
                FieldName = FieldName
            });
            MessageBox.ShowAndParentReload("添加成功！");
            return;
        }
    }
}