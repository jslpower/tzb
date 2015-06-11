using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.basic
{
    public partial class CityEdit : System.Web.UI.Page
    {
        protected int CountryId = 0;
        protected int ProvinceID = 0;
        protected string dotype = "";
        protected int Id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            dotype = Utils.GetQueryStringValue("dotype").ToLower();
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
            var model = BMSysProvince.GetCityModel(Id);
            if (model != null)
            {
                txtCityName.Text = model.Name;
                txtJP.Text = model.JP;
                txtQP.Text = model.QP;
                txtEnName.Text = model.EnName;
                ProvinceID = model.ProvinceId;
                CountryId = BMSysProvince.GetProvinceModel(ProvinceID).CountryId;
            }
        }

        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";

            if (Utils.GetInt(Utils.GetFormValue("selCountry")) == 0)
            {
                strErr = "请选择国家";
            }
            if (Utils.GetInt(Utils.GetFormValue("selProvince")) == 0)
            {
                strErr = "请选择省份";
            }
            if (dotype == "add")
            {
                if (string.IsNullOrWhiteSpace(txtCityName.Text) || BMSysProvince.IsExistsCity(Utils.GetInt(Utils.GetFormValue("selProvince")), txtCityName.Text.Trim()))
                {
                    strErr = "城市名称已存在，请勿重复录入！";
                }
            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }
            if (dotype == "add")
            {
                tbl_SysCity model = new tbl_SysCity();
                model.ProvinceId = Utils.GetInt(Utils.GetFormValue("selProvince"));
                model.Name = txtCityName.Text.Trim();
                model.JP = txtJP.Text.Trim();
                model.QP = txtQP.Text.Trim();
                model.EnName = txtEnName.Text.Trim();
                BMSysProvince.AddCity(model);
                MessageBox.ShowAndParentReload("新增成功！");
                return;
            }
            else if (dotype == "update")
            {
                tbl_SysCity model = new tbl_SysCity();
                if (Id>0)
                {
                    model.CityId = Id;
                    model.ProvinceId = Utils.GetInt(Utils.GetFormValue("selProvince"));
                    model.Name = txtCityName.Text.Trim();
                    model.JP = txtJP.Text.Trim();
                    model.QP = txtQP.Text.Trim();
                    model.EnName = txtEnName.Text.Trim();
                    bool IsSucess = BMSysProvince.UpdateCity(model);
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