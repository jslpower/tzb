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
    public partial class AreaEdit : System.Web.UI.Page
    {
        protected int CountryId = 0;
        protected int ProvinceID = 0;
        protected int CityId = 0;
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
            var model = BMSysProvince.GetAreaModel(Id);
            if (model != null)
            {
                txtAreaName.Text = model.Name;
                txtJP.Text = model.JP;
                txtQP.Text = model.QP;
                txtEnName.Text = model.EnName;
                CityId = model.CityId;
                ProvinceID = BMSysProvince.GetCityModel(CityId).ProvinceId;
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
            if (Utils.GetInt(Utils.GetFormValue("selCity")) == 0)
            {
                strErr = "请选择城市";
            }
            if (dotype == "add")
            {
                if (string.IsNullOrWhiteSpace(txtAreaName.Text) || BMSysProvince.IsExistsArea(Utils.GetInt(Utils.GetFormValue("selCity")), txtAreaName.Text.Trim()))
                {
                    strErr = "区县名称已存在，请勿重复录入！";
                }
            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }

            if (dotype == "add")
            {
                tbl_SysArea model = new tbl_SysArea();
                model.CityId = Utils.GetInt(Utils.GetFormValue("selCity"));
                model.Name = txtAreaName.Text.Trim();
                model.JP = txtJP.Text.Trim();
                model.QP = txtQP.Text.Trim();
                model.EnName = txtEnName.Text.Trim();
                model.ThName = "";
                BMSysProvince.AddArea(model);
                MessageBox.ShowAndParentReload("新增成功！");
                return;
            }
            else if (dotype == "update")
            {
                tbl_SysArea model = new tbl_SysArea();
                if (Id > 0)
                {
                    model.CountyId = Id;
                    model.CityId = Utils.GetInt(Utils.GetFormValue("selCity"));
                    model.Name = txtAreaName.Text.Trim();
                    model.JP = txtJP.Text.Trim();
                    model.QP = txtQP.Text.Trim();
                    model.EnName = txtEnName.Text.Trim();
                    model.ThName = "";
                    bool IsSucess = BMSysProvince.UpdateArea(model);
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