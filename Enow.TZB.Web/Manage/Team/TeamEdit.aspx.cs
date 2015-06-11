using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Team
{
    public partial class TeamEdit : System.Web.UI.Page
    {
        protected string CId = "", PId = "", CSId = "", AId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype");
            string teamId = Utils.GetQueryStringValue("id");
            if (dotype == "save")
            {
                SaveData(teamId);
                return;
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitPage(teamId);
            }
        }

        /// <summary>
        /// 加载球队信息
        /// </summary>
        /// <param name="teamId">球队ID</param>
        private void InitPage(string teamId)
        {
            if (!string.IsNullOrWhiteSpace(teamId))
            {
                var model = BTeam.GetViewModel(teamId);
                if (model != null)
                {
                    txtTeamName.Text = model.TeamName;
                    CId = model.CountryId.ToString();
                    PId = model.ProvinceId.ToString();
                    CSId = model.CityId.ToString();
                    AId = model.AreaId.ToString();
                    lblMemberName.Text = model.ContactName;
                    lblMobile.Text = model.MobilePhone;
                    lblIssueDate.Text = model.IssueTime.ToString("yyyy-MM-dd");
                    lblState.Text = ((Model.EnumType.球队审核状态)model.State).ToString();
                    #region 球队照片
                    IList<Model.MFileInfo> files = new List<Model.MFileInfo>();
                    files.Add(new Model.MFileInfo() { FilePath = model.TeamPhoto });
                    UploadControl2.YuanFiles = files;
                    #endregion
                    txtTeamInfo.Text = model.TeamInfo;
                }
                else
                {
                    MessageBox.ShowAndReturnBack("未找到您要修改的球队信息！");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack("未找到您要修改的球队信息！");
                return;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void SaveData(string teamId)
        {
            if (!string.IsNullOrWhiteSpace(teamId))
            {
                #region 取值
                string teamName = Utils.GetFormValue(txtTeamName.UniqueID);
                int countryId = Utils.GetInt(Utils.GetFormValue("ddlCountry"), 0);
                int provinceId = Utils.GetInt(Utils.GetFormValue("ddlProvince"), 0);
                int cityId = Utils.GetInt(Utils.GetFormValue("ddlCity"), 0);
                int areaId = Utils.GetInt(Utils.GetFormValue("ddlArea"), 0);
                string teamPhoto = GetAttachFile();
                string teamInfo = Utils.GetFormEditorValue(txtTeamInfo.UniqueID);
                string countryName = "";
                string provinceName = "";
                string cityName = "";
                string areaName = "";
                #endregion

                #region 判断
                if (string.IsNullOrWhiteSpace(teamName))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请填写球队名称！"));
                    return;
                }
                if (string.IsNullOrWhiteSpace(teamPhoto))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请上传球队照片！"));
                    return;
                }
                var countryModel = BMSysProvince.GetCountryModel(countryId);
                if (countryModel != null)
                {
                    countryName = countryModel.Name;
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请选择国家！"));
                    return;
                }
                var provinceModel = BMSysProvince.GetProvinceModel(provinceId);
                if (provinceModel != null)
                {
                    provinceName = provinceModel.Name;
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请填写省份！"));
                    return;
                }
                var cityModel = BMSysProvince.GetCityModel(cityId);
                if (cityModel != null)
                {
                    cityName = cityModel.Name;
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请填写城市!"));
                    return;
                }
                var areaModel = BMSysProvince.GetAreaModel(areaId);
                if (areaModel != null)
                {
                    areaName = areaModel.Name;
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请填写区县!"));
                    return;
                }
                if (string.IsNullOrWhiteSpace(teamInfo))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请填写球队介绍!"));
                    return;
                }
                #endregion

                #region 保存

                var model = BTeam.GetModel(teamId);
                bool ret = BTeam.Update(new tbl_BallTeam
                  {

                      Id = teamId,
                      CountryId = countryId,
                      CountryName = countryName,
                      ProvinceId = provinceId,
                      ProvinceName = provinceName,
                      CityId = cityId,
                      CityName = cityName,
                      AreaId = areaId,
                      AreaName = areaName,
                      TeamName = teamName,
                      TeamPhoto = teamPhoto,
                      TeamInfo = teamInfo,
                      State = model.State

                  });
                #endregion
                if (ret)
                {
                    Response.Clear();
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "操作成功"));
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "操作失败"));
                    Response.End();
                }
            }
            else
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", "未找到您要修改的球队信息"));
                Response.End();
            }
        }
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <returns></returns>
        protected string GetAttachFile()
        {
            var files1 = UploadControl2.Files;
            var files2 = UploadControl2.YuanFiles;
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