using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using System.Text;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.Manage.Court
{
    /// <summary>
    /// 球场添加
    /// </summary>
    public partial class CourtAdd : System.Web.UI.Page
    {
        protected string CId = "", PId = "", CSId = "", AId = "";
        protected string TypeIdV = "";
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("dotype");
            if (doType == "save")
            {
                SaveData();
                return;
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();                
                string act = Utils.GetQueryStringValue("act");
                switch (act)
                {
                    case "update":
                        InitModel();
                        break;
                    default:
                        InitLis(0);
                        break;
                }
            }       
        }
        #region 绑定数据 
        private void InitLis(int SelectId) {
            var list = BBallField.GetFieldTypeList();
            if (list != null) {
                this.ddlFieldTypeId.DataSource = list;
                this.ddlFieldTypeId.DataTextField = "FieldType";
                this.ddlFieldTypeId.DataValueField = "Id";
                this.ddlFieldTypeId.DataBind();
                if (SelectId > 0) {
                    this.ddlFieldTypeId.Items.FindByValue(SelectId.ToString()).Selected = true;
                }
            }
        }
        private void InitModel()
        {
            string Id = Utils.GetQueryStringValue("id");
            if (!String.IsNullOrWhiteSpace(Id))
            {
                var model = BBallField.GetModel(Id);
                if (model != null)
                {
                    InitLis(model.FieldType);
                    TypeIdV = model.TypeId.ToString();
                    CId = model.CountryId.ToString();
                    PId = model.ProvinceId.ToString();
                    CSId = model.CityId.ToString();
                    AId = model.CountyId.ToString();
                    txtBallFieldName.Text = model.FieldName;
                    txtShortName.Text = model.ShortName;
                    #region 附件1
                    IList<Model.MFileInfo> files = new List<Model.MFileInfo>();
                    files.Add(new Model.MFileInfo() { FilePath = model.FieldPhoto });
                    UploadControl1.YuanFiles = files;
                    #endregion
                    txtFieldNumber.Text = model.FieldNumber.ToString();
                    txtAddress.Text = model.Address;
                    txtLongitude.Text = model.Longitude;
                    txtLatitude.Text = model.Latitude;
                    txtContactTel.Text = model.ContactTel;
                    txtMarketPrice.Text = model.MarketPrice.ToString("F2");
                    txtPrice.Text = model.Price.ToString("F2");
                    txtHour.Text = model.Hours;
                    txtFieldSize.Text = model.FieldSize;
                    txtRemark.Text = model.Remark;
                    #region 附件2 多图
                    if (!String.IsNullOrWhiteSpace(model.OtherPhotoXml))
                    {
                        string[] f = model.OtherPhotoXml.Split(',');
                        IList<Model.MFileInfo> filelist = new List<Model.MFileInfo>();
                        foreach (string s in f)
                        {
                            filelist.Add(new Model.MFileInfo() { FilePath = s });
                        }
                        UploadControl2.YuanFiles = filelist;
                    }
                    #endregion
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(43));
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(43));
                return;
            }
        }
        #endregion
        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            bool IsResult = false;
            #region 取值
            string strErr = "";
            string act = Utils.GetQueryStringValue("act");
            string Id = Utils.GetQueryStringValue("id");
            ManagerList ManageModel = ManageUserAuth.GetManageUserModel();
            int UserId = ManageModel.Id;
            string ContactName = ManageModel.ContactName;
            ManageModel = null;
            int FieldTypeId = Utils.GetInt(Utils.GetFormValue(ddlFieldTypeId.UniqueID));
            string FieldName = Utils.GetFormValue(txtBallFieldName.UniqueID);
            string FieldShortName = Utils.GetFormValue(txtShortName.UniqueID);
            string FieldPhoto = GetAttachFile();
            int FieldNumber = Utils.GetInt(Utils.GetFormValue(txtFieldNumber.UniqueID));
            int TypeId = Utils.GetInt(Utils.GetFormValue("ddlTypeId"));
            string CountryName = "", ProvinceName = "", CityName = "", AreaName = "";
            int CountryId = Utils.GetInt(Utils.GetFormValue("ddlCountry"), 0);
            int ProvinceId = Utils.GetInt(Utils.GetFormValue("ddlProvince"), 0);
            int CityId = Utils.GetInt(Utils.GetFormValue("ddlCity"), 0);
            int AreaId = Utils.GetInt(Utils.GetFormValue("ddlArea"), 0);
            string Address = Utils.GetFormValue(txtAddress.UniqueID);
            string Longitude = Utils.GetFormValue(txtLongitude.UniqueID);
            string Latitude = Utils.GetFormValue(txtLatitude.UniqueID);
            string ContactTel = Utils.GetFormValue(txtContactTel.UniqueID);
            decimal MarketPrice = Utils.GetDecimal(Utils.GetFormValue(txtMarketPrice.UniqueID));
            decimal Price = Utils.GetDecimal(Utils.GetFormValue(txtPrice.UniqueID));
            string Hour = Utils.GetFormValue(txtHour.UniqueID);
            string FieldSize = Utils.GetFormValue(txtFieldSize.UniqueID);
            string Remark = Utils.GetFormEditorValue(txtRemark.UniqueID);
            string OtherPhoto = GetAttachFileList();
            #region 判断
            if (String.IsNullOrWhiteSpace(FieldName))
            {
                strErr += "请填写场地名称！\n";
            }
            if (String.IsNullOrWhiteSpace(FieldPhoto))
            {
                strErr += "请上传场地形象图！\n";
            }
            var CountryModel = BMSysProvince.GetCountryModel(CountryId);
            if (CountryModel != null)
            {
                CountryName = CountryModel.Name;
            }
            else
            {
                strErr += "请选择国家！\n";
            }
            var ProModel = BMSysProvince.GetProvinceModel(ProvinceId);
            if (ProModel != null)
            {
                ProvinceName = ProModel.Name;
            }
            else
            {
                strErr += "请选择省份！\n";
            }
            var CityModel = BMSysProvince.GetCityModel(CityId);
            if (CityModel != null)
            {
                CityName = CityModel.Name;
            }
            else
            {
                strErr += "请选择城市！\n";
            }
            var AreaModel = BMSysProvince.GetAreaModel(AreaId);
            if (AreaModel != null)
            {
                AreaName = AreaModel.Name;
            }
            else
            {
                strErr += "请选择区县！\n";
            }
            if (!String.IsNullOrWhiteSpace(strErr))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", strErr));
                return;
            }
            #endregion            
            #endregion
            switch (act)
            {
                case "add":
                    IsResult = BBallField.Add(new tbl_BallField
                    { 
                        Id = System.Guid.NewGuid().ToString(),
                        FieldType = FieldTypeId,
                        UserId = UserId,
                        ContactName = ContactName,
                        TypeId = TypeId,
                        CountryId = CountryId,
                        CountryName = CountryName,
                        ProvinceId = ProvinceId,
                        ProvinceName =ProvinceName,
                        CityId = CityId,
                        CityName = CityName,
                        CountyId = AreaId,
                        CountyName = AreaName,
                        FieldName = FieldName,
                        ShortName = FieldShortName,
                        FieldPhoto = FieldPhoto,
                        Longitude = Longitude,
                        Latitude = Latitude,
                        FieldNumber = FieldNumber,
                        Address = Address,
                        ContactTel = ContactTel,
                        MarketPrice = MarketPrice,
                        Price = Price,
                        Hours = Hour,
                        FieldSize = FieldSize,
                        Remark = Remark,
                        OtherPhotoXml = OtherPhoto,
                        State = (int)Model.EnumType.球场审核状态.已审核,
                        IssueTime = DateTime.Now
                    });
                    break;
                case "update":
                    IsResult = BBallField.Update(new tbl_BallField
                    {
                        Id = Id,
                        FieldType = FieldTypeId,
                        TypeId = TypeId,
                        CountryId = CountryId,
                        CountryName = CountryName,
                        ProvinceId = ProvinceId,
                        ProvinceName = ProvinceName,
                        CityId = CityId,
                        CityName = CityName,
                        CountyId = AreaId,
                        CountyName = AreaName,
                        FieldName = FieldName,
                        ShortName = FieldShortName,
                        FieldPhoto = FieldPhoto,
                        Longitude = Longitude,
                        Latitude = Latitude,
                        FieldNumber = FieldNumber,
                        Address = Address,
                        ContactTel = ContactTel,
                        MarketPrice = MarketPrice,
                        Price = Price,
                        Hours = Hour,
                        FieldSize = FieldSize,
                        Remark = Remark,
                        OtherPhotoXml = OtherPhoto
                    });
                    break;
            }
            if (IsResult)
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
        /// <summary>
        /// 上传的附件
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
        /// <summary>
        /// 上传的附件
        /// </summary>
        /// <returns></returns>
        protected string GetAttachFileList()
        {
            string fileList ="" ;
            var files1 = UploadControl2.Files;
            var files2 = UploadControl2.YuanFiles;
            IList<Model.MFileInfo> files = new List<Model.MFileInfo>();
            if (files1 != null && files1.Count > 0)
            {
                foreach (var item in files1)
                {
                    files.Add(item);
                }
            }
            if (files2 != null && files2.Count > 0)
            {
                foreach (var item in files2)
                {
                    files.Add(item);
                }
            }
            if (files != null && files.Count > 0)
            {                
                foreach (var file in files)
                {
                    fileList += file.FilePath + ",";
                }
            }
            return fileList.TrimEnd(',');
        }
        #endregion
    }
}