using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.basic
{
    public partial class ProvinceEdit : System.Web.UI.Page
    {
        protected int CountryId = 0;
        protected string dotype = "";
        protected int Id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            dotype = Utils.GetQueryStringValue("dotype").ToLower();
            Id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                if (dotype == "update" && Id > 0)
                {
                    InitPage(Id);
                }

            }
        }

        protected void InitPage(int Id)
        {
            var model = BMSysProvince.GetProvinceModel(Id);
            if (model != null)
            {
                txtProvinceName.Text = model.Name;
                txtJP.Text = model.JP;
                txtQP.Text = model.QP;
                txtEnName.Text = model.EnName;
                CountryId = model.CountryId;
            }
        }

        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
           
            if (Utils.GetInt(Utils.GetFormValue("selCountry")) == 0)
            {
                strErr = "请选择国家";
            }
            if (dotype == "add")
            {
                if (string.IsNullOrWhiteSpace(txtProvinceName.Text) || BMSysProvince.IsExistsProvince(Utils.GetInt(Utils.GetFormValue("selCountry")),txtProvinceName.Text.Trim()))
                {
                    strErr = "省份名称已存在，请勿重复录入！";
                }
            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }
            if (dotype == "add")
            {
                tbl_SysProvince model = new tbl_SysProvince();
                model.CountryId = Utils.GetInt(Utils.GetFormValue("selCountry"));
                model.Name = txtProvinceName.Text.Trim();
                model.QP = txtQP.Text.Trim();
                model.JP = txtJP.Text.Trim();
                model.EnName = txtEnName.Text.Trim();
                BMSysProvince.AddProvince(model);
                MessageBox.ShowAndParentReload("新增成功！");
                return;

            }
            else if (dotype == "update")
            {
                tbl_SysProvince Model = new tbl_SysProvince();
                if (Id>0)
                {
                    Model.ProvinceId = Id;
                    Model.CountryId = Utils.GetInt(Utils.GetFormValue("selCountry"));
                    Model.Name = txtProvinceName.Text.Trim();
                    Model.EnName = txtEnName.Text.Trim();
                    Model.QP = txtQP.Text.Trim();
                    Model.JP = txtJP.Text.Trim();
                    bool IsSucess = BMSysProvince.UpdateProvince(Model); 
                    if (IsSucess)
                    {
                        MessageBox.ShowAndParentReload("修改成功！");
                    }
                    else
                    {
                        MessageBox.ShowAndReturnBack("修改失败！");
                        return;
                    }
                }
               
            }
        }
    }
}