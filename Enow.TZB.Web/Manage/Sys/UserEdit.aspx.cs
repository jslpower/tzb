using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Sys
{
    /// <summary>
    /// 用户信息修改
    /// </summary>
    public partial class UserEdit : System.Web.UI.Page
    {
        protected string strJs = "";
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
                    this.ltrContactName.Text = model.ContactName;
                    this.txtTel.Text = model.ContactTel;
                    this.ltrUserName.Text = model.UserName;
                    this.txtPwd.Text = "";
                    this.cbIsAllCity.Checked = model.IsAllCity;
                    InitRole(model.RoleId);
                    InitCityList(model.CityList);
                }
                else { ErrMsg(); }
            }
        }
        /// <summary>
        /// 绑定角色组
        /// </summary>
        /// <param name="RoleId"></param>
        private void InitRole(int RoleId)
        {
            this.ddlRole.DataSource = UserRole.GetList("");
            this.ddlRole.DataTextField = "RoleName";
            this.ddlRole.DataValueField = "Id";
            this.ddlRole.DataBind();
            this.ddlRole.Items.Insert(0, new ListItem("请选择角色", "0"));
            if (RoleId > 0)
            {
                for (int i = 0; i < this.ddlRole.Items.Count; i++)
                {
                    if (this.ddlRole.Items[i].Value == RoleId.ToString())
                    {
                        this.ddlRole.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 初始化管理的城市
        /// </summary>
        /// <param name="InitCityList"></param>
        private void InitCityList(string InitCityList) {
            if (!String.IsNullOrWhiteSpace(InitCityList))
            {                
                string[] arrCity = InitCityList.Split(',');
                this.rptList.DataSource = arrCity;
                this.rptList.DataBind();
                for (int i = 0; i < arrCity.Length; i++)
                {
                    int CoutryId = Utils.GetInt(arrCity[i]);
                    if (CoutryId > 0)
                    {
                        var model = BMSysProvince.GetAreaViewModel(CoutryId);
                        if (model != null)
                        {
                            strJs += "pcToobar.init({gID: \"#ddlCountry" + i + "\",pID: \"#ddlProvince" + i + "\",cID: \"#ddlCity" + i + "\",xID: \"#ddlArea" + i + "\",comID: '',gSelect: '" + model.CountryId + "',pSelect: '" + model.ProvinceId + "',cSelect: '" + model.CityId + "',xSelect: '" + CoutryId + "'});\n";
                        }
                        else {
                            strJs += "pcToobar.init({gID: \"#ddlCountry" + i + "\",pID: \"#ddlProvince" + i + "\",cID: \"#ddlCity" + i + "\",xID: \"#ddlArea" + i + "\",comID: '',gSelect: '',pSelect: '',cSelect: '',xSelect: ''});\n";
                        }
                    }
                }
                this.phNoData.Visible = false;
            }
            else
            {
                this.phNoData.Visible = true;
                strJs = "pcToobar.init({gID: \"#ddlCountry\",pID: \"#ddlProvince\",cID: \"#ddlCity\",xID: \"#ddlArea\",comID: '',gSelect: '',pSelect: '',cSelect: '',xSelect: ''});";
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
                string strRoleId = Request.Form["ddlRole"];
                string Tel = this.txtTel.Text;
                string Password = this.txtPwd.Text;
                bool IsAllCity = cbIsAllCity.Checked;
                string CityList = "";
                string[] arrCity = Utils.GetFormValues("ddlArea");
                if (arrCity != null && arrCity.Length > 0)
                {
                    CityList = String.Join(",", arrCity);
                }
                if (StringValidate.IsInteger(strRoleId) == false)
                    strErr = "请选择角色！";
                else
                    RoleId = Convert.ToInt32(strRoleId);
                if (RoleId < 1)
                {
                    strErr = "请选择角色！";
                }
                else
                {
                    var roleModel = UserRole.GetModel(RoleId);
                    if (roleModel != null)
                    {
                        rolePer = roleModel.PerList;
                    }
                    else { strErr = "系统不存在您选择的角色！"; }
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
                    ContactTel = Tel,
                    IsAllCity = IsAllCity,
                    CityList = CityList
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