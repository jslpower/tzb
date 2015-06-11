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
    public partial class CountryEdit : System.Web.UI.Page
    {
        protected string doType = "";
        protected int Id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            doType = Utils.GetQueryStringValue("doType");
            Id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                if (doType.ToLower() == "update" && Id > 0)
                {
                    InitPage(Id);
                }
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="id"></param>
        protected void InitPage(int id)
        {
            var model = BMSysProvince.GetCountryModel(id);
            if (model!=null)
            {
                txtCountryName.Text = model.Name;
                txtJP.Text = model.JP;
                txtQP.Text = model.QP;
                txtEnName.Text = model.EnName;
            }

        }

        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (doType.ToLower()=="add")
            {
                if (string.IsNullOrWhiteSpace(txtCountryName.Text) || BMSysProvince.IsExistsCountry(txtCountryName.Text.Trim()))
                {
                    strErr = "国家名称已存在，请勿重复录入！";
                }
            }
            
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }
            if (doType.ToLower()=="add")
            {
                tbl_SysCountry model = new tbl_SysCountry();
                model.Name = txtCountryName.Text.Trim();
                model.JP = txtJP.Text.Trim();
                model.QP = txtQP.Text.Trim();
                model.EnName = txtEnName.Text.Trim();
                model.CompanyId = Guid.NewGuid().ToString();
                model.IsDefault =(char)1;
                BMSysProvince.AddCountry(model);
                MessageBox.ShowAndParentReload("新增成功！");
                return;
            }
            else if (doType.ToLower()=="update")
            {
                tbl_SysCountry model = new tbl_SysCountry();
                if (Id>0)
                {
                    model.CountryId = Id;
                    model.Name = txtCountryName.Text.Trim();
                    model.JP = txtJP.Text.Trim();
                    model.QP = txtQP.Text.Trim();
                    model.EnName = txtEnName.Text.Trim();
                    model.CompanyId = Guid.NewGuid().ToString();
                    model.IsDefault = (char)1;
                    bool IsSucess = BMSysProvince.UpdateCountry(model);
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