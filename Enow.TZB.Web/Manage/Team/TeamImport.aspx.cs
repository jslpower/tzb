using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using Enow.TZB.Model;

namespace Enow.TZB.Web.Manage.Team
{
    /// <summary>
    /// 球队导入
    /// </summary>
    public partial class TeamImport : System.Web.UI.Page
    {
        /// <summary>
        /// 文件上传路径
        /// </summary>
        private string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
            }
        }
        /// <summary>
        /// 客户数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkSave_Click(object sender, EventArgs e)
        {
            string ifrmaeId = Request.QueryString["iframeId"];
            int CountryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID));
            int ProvinceId = Utils.GetInt(Utils.GetFormValue(ddlProvince.UniqueID));
            int CityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID));
            int AreaId = Utils.GetInt(Utils.GetFormValue(ddlArea.UniqueID));
            string CountryName = "";
            var CountryModel = BMSysProvince.GetCountryModel(CountryId);
            if (CountryId > 0 && CountryModel != null)
            {
                CountryName = CountryModel.Name;
            }else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(50));
                return;
            }
            string ProvinceName = "";
            var ProvinceModel = BMSysProvince.GetProvinceModel(ProvinceId);
            if (ProvinceId > 0 && ProvinceModel != null)
            {
                ProvinceName = ProvinceModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(51));
                return;
            }
            string CityName = "";
            var CityModel = BMSysProvince.GetCityModel(CityId);
            if (CityId > 0 && CityModel != null)
            {
                CityName = CityModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(52));
                return;
            }
            string AreaName = "";
            var AreaModel = BMSysProvince.GetAreaModel(AreaId);
            if (AreaId > 0 && AreaModel != null)
            {
                AreaName = AreaModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(53));
                return;
            }
            HttpPostedFile image_upload = Request.Files[0];
            if (image_upload == null || image_upload.ContentLength <= 0)
            {
                MessageBox.ShowAndReturnBack("请选择上传的文件!");
                return;
            }
            else
            {
                string fileExt = System.IO.Path.GetExtension(image_upload.FileName);
                if (fileExt.ToLower() != ".xls")
                {
                    MessageBox.ShowAndReturnBack("文件格式不正确，只能上传.xls格式文件!");
                    return;
                }
                else
                {
                    string fileName = Utils.GenerateFileName(fileExt);
                    string relativePath = UploadPath + fileName;
                    string desDirPath = Server.MapPath(UploadPath);
                    if (!System.IO.Directory.Exists(desDirPath))
                    {
                        System.IO.Directory.CreateDirectory(desDirPath);
                    }
                    string desFilePath = Server.MapPath(relativePath);
                    image_upload.SaveAs(desFilePath);//保存文件
                    //解析EXCEL文件并保存到客户信息库中
                    System.Data.DataTable dt = NPOIHelper.ExcelToTableForXLS(desFilePath);
                    int InserCount = 0;
                    int ExistsCount = 0;
                    int RowsCount = dt.Rows.Count;
                    IList<tbl_TeamImportTemplate> list = new List<tbl_TeamImportTemplate>();
                    for (int i = 0; i < RowsCount; i++)
                    {
                        list.Add(new tbl_TeamImportTemplate { 
                            Id = System.Guid.NewGuid().ToString(),
                           球队名=Utils.InputText(dt.Rows[i]["球队名"]),
                           球队介绍 = Utils.InputText(dt.Rows[i]["球队介绍"]),
                           姓名 = Utils.InputText(dt.Rows[i]["姓名"]),
                           性别 = Utils.InputText(dt.Rows[i]["性别"]),
                           手机号 = Utils.InputText(dt.Rows[i]["手机号"]),
                           角色 = Utils.InputText(dt.Rows[i]["角色"]),
                           身份证号 = Utils.InputText(dt.Rows[i]["身份证号"]),
                           邮箱 = Utils.InputText(dt.Rows[i]["邮箱"]),
                           住址 = Utils.InputText(dt.Rows[i]["住址"]),
                           场上位置 = Utils.InputText(dt.Rows[i]["场上位置"]),
                           球衣号 = Convert.ToDouble(Utils.GetInt(Convert.ToString(dt.Rows[i]["球衣号"]),0))
                        });
                    }
                    string rv = BTeam.Import(list, CountryId, CountryName, ProvinceId, ProvinceName, CityId, CityName, AreaId, AreaName);
                    #region 短信发送
                    for (int i = 0; i < RowsCount; i++)
                    {
                        string MobilePhone = Utils.GetString(Convert.ToString(dt.Rows[i]["手机号"]),"");
                        if (!String.IsNullOrEmpty(MobilePhone))
                        {
                            //发送短信
                            BSMS.Send(MobilePhone.Trim(), CacheSysMsg.GetMsg(165));
                        }
                    }
                    #endregion
                    MessageBox.ShowAndParentReload(rv);
                    return;
                }
            }
        }
    }
}