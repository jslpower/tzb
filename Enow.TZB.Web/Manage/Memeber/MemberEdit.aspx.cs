using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Memeber
{
    public partial class MemberEdit : System.Web.UI.Page
    {
        protected string CId = "", PId = "", CSId = "", AId = "";

        private string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            if (Utils.GetQueryStringValue("doType") == "save")
            {
                SaveData();
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitPage();
            }
        }

        /// <summary>
        /// 加载用户信息
        /// </summary>
        private void InitPage()
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var model = BMember.GetModel(Id);
                if (model != null)
                {
                    ltrUserName.Text = model.UserName;
                    this.ltrNickName.Text = model.NickName;
                    CId = model.CountryId.ToString();
                    PId = model.ProvinceId.ToString();
                    CSId = model.CityId.ToString();
                    AId = model.AreaId.ToString();
                    #region 用户头像
                    IList<Model.MFileInfo> files = new List<Model.MFileInfo>();
                    files.Add(new Model.MFileInfo() { FilePath = model.MemberPhoto });
                    UploadControl1.YuanFiles = files;
                    #endregion
                    txtMobile.Text = model.MobilePhone;
                    txtEmail.Text = model.Email;
                    txtContactName.Text = model.ContactName;
                    txtPersonalId.Text = model.PersonalId;
                    txtAddress.Text = model.Address;
                    if (model.Gender == 0)
                    {
                        radWoman.Checked = true;
                    }
                    else
                    {
                        radMan.Checked = true;
                    }
                }
                else
                {
                  
                   showMsg("0", "未找到您要修改的球队信息");
                    
                }
            }
            else
            {
               showMsg("0", "未找到您要修改的球队信息");
             
            }

        }

        #region 修改用户信息
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (!string.IsNullOrWhiteSpace(Id))
            {
                #region 取值
                int countryId = Utils.GetInt(Utils.GetFormValue("ddlCountry"));
                int provinceId = Utils.GetInt(Utils.GetFormValue("ddlProvince"));
                int cityId = Utils.GetInt(Utils.GetFormValue("ddlCity"));
                int areaId = Utils.GetInt(Utils.GetFormValue("ddlArea"));
                int Gender = 0;
                if (radMan.Checked == true)
                {
                    Gender = 1;
                }
                string email = Utils.GetFormValue(txtEmail.UniqueID);
                string contractName = Utils.GetFormValue(txtContactName.UniqueID);
                string personalId = Utils.GetFormValue(txtPersonalId.UniqueID);
                string address = Utils.GetFormValue(txtAddress.UniqueID);

                string CountryName = "";
                var CountryModel = BMSysProvince.GetCountryModel(countryId);
                if (countryId > 0 && CountryModel != null)
                {
                    CountryName = CountryModel.Name;
                }
                else
                {
                    showMsg("0", CacheSysMsg.GetMsg(50));

                }
                string ProvinceName = "";
                var ProvinceModel = BMSysProvince.GetProvinceModel(provinceId);
                if (provinceId > 0 && ProvinceModel != null)
                {
                    ProvinceName = ProvinceModel.Name;
                }
                else
                {
                    showMsg("0", CacheSysMsg.GetMsg(51));

                }
                string CityName = "";
                var CityModel = BMSysProvince.GetCityModel(cityId);
                if (cityId > 0 && CityModel != null)
                {
                    CityName = CityModel.Name;
                }
                else
                {
                    showMsg("0", CacheSysMsg.GetMsg(52));

                }
                string AreaName = "";
                var AreaModel = BMSysProvince.GetAreaModel(areaId);
                if (areaId > 0 && AreaModel != null)
                {
                    AreaName = AreaModel.Name;
                }
                else
                {
                    showMsg("0", CacheSysMsg.GetMsg(53));

                }
                if (countryId == 1 && provinceId != 190 && provinceId != 191 && provinceId != 988)
                {
                    if (!StringValidate.IsIDcard(personalId))
                    {
                        showMsg("0", "身份证号码不正确!");

                    }
                }
                if (BMember.GetPesonalModel(Id, personalId) == true)
                {
                    MessageBox.ShowAndReturnBack("该身份证号码已被注册，请使用其他身份证号码！");
                    return;
                }
                if (!StringValidate.IsEmail(email))
                {
                    showMsg("0", "邮箱格式不正确!");

                }
                if (string.IsNullOrWhiteSpace(contractName))
                {
                    showMsg("0", "请输入真实姓名!");

                }
                if (string.IsNullOrWhiteSpace(personalId))
                {
                    showMsg("0", "请填写身份证号!");

                }
                if (string.IsNullOrWhiteSpace(address))
                {
                    showMsg("0", "请填写详细地址");
                }
                string MemberPhoto = GetAttachFile();
                #endregion

                #region 保存数据
                var model = BMember.GetModel(Id);
                if (model != null)
                {
                    model.CountryId = countryId;
                          model.CountryName = CountryName;
                          model.ProvinceId = provinceId;
                          model.ProvinceName = ProvinceName;
                          model.CityId = cityId;
                          model.CityName = CityName;
                          model.AreaId = areaId;
                          model.AreaName = AreaName;
                          model.Gender = Gender;
                          model.Email = email;
                          model.ContactName = contractName;
                          model.PersonalId = personalId;
                          model.Address = address;
                          model.MemberPhoto = MemberPhoto;
                          model.LastModifyTime = DateTime.Now;
                    bool ret = BMember.Update(model);
                    if (ret)
                    {
                        showMsg("1", "操作成功");
                    }
                    else
                    {
                        showMsg("0", "操作失败");
                    }
                }
                else { showMsg("0", "未找到您要修改的会员信息"); }
                #endregion

            }
            else
            {
                showMsg("0", "未找到您要修改的会员信息");
            }
        }
        #endregion


        protected string showMsg(string typeId, string Msg)
        {
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(typeId,Msg));
            Response.End();
            return "";
        }

        /// <summary>
        /// 附件上传
        /// </summary>
        /// <returns></returns>
        protected string GetAttachFile()
        {
            var files1 = UploadControl1.Files;
            var files2 = UploadControl1.YuanFiles;
            if (files1 != null && files1.Count > 0)
            {
                return files1[0].FilePath;
            }
            if (files2 != null && files2.Count > 0)
            {
                return files2[0].FilePath;
            }
            return string.Empty;
        }
    }
}