using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Match
{
    /// <summary>
    /// 新增/修改赛事信息
    /// </summary>
    public partial class MatchAdd : System.Web.UI.Page
    {
        protected string CId = "", PId = "", CSId = "", AId = "";
        protected string TypeIdV = "",MatchAreaId = "";
        protected string FiledSelectStr = "<optgroup label=\"请选择球场\"></optgroup>";
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
                }
            }

        }

        #region 绑定数据
        private void InitModel()
        {
            string id = Utils.GetQueryStringValue("Id");
            if (!string.IsNullOrWhiteSpace(id))
            {
                var model = BMatch.GetModel(id);
                if (model != null)
                {
                    TypeIdV = model.TypeId.ToString();
                    MatchAreaId = model.MatchAreaType.ToString();
                    CId = model.CountryId.ToString();
                    PId = model.ProvinceId.ToString();
                    CSId = model.CityId.ToString();
                    AId = model.AreaId.ToString();
                    txtMatchName.Text = model.MatchName;
                    #region 附件1
                    IList<Model.MFileInfo> files = new List<Model.MFileInfo>();
                    files.Add(new Model.MFileInfo() { FilePath = model.MatchPhoto });
                    UploadControl1.YuanFiles = files;
                    #endregion
                    radLimit.SelectedValue = model.IsCityLimit == true ? "1" : "0";
                    txtSignBegin.Text = model.SignBeginDate.ToString("yyyy-MM-dd");
                    txtSignEnd.Text = model.SignEndDate.ToString("yyyy-MM-dd");
                    txtStartDate.Text = model.BeginDate.ToString("yyyy-MM-dd");
                    txtEndDate.Text = model.EndDate.ToString("yyyy-MM-dd");
                    txtRegistrationFee.Text = model.RegistrationFee.ToString("F2");
                    txtEarnestMoney.Text = model.EarnestMoney.ToString("F2");
                    txtMasterOrganizer.Text = model.MasterOrganizer;
                    txtCoOrganizers.Text = model.CoOrganizers;
                    txtOrganizer.Text = model.Organizer;
                    txtSponsors.Text = model.Sponsors;
                    txtTeamNumber.Text = model.TeamNumber.ToString();
                    txtPlayersMin.Text = model.PlayersMin.ToString();
                    txtPlayersMax.Text = model.PlayersMax.ToString();
                    txtBayMin.Text = model.BayMin.ToString();
                    txtBayMax.Text = model.BayMax.ToString();
                    txtTotalTime.Text = model.TotalTime.ToString();
                    txtBreakTime.Text = model.BreakTime.ToString();
                    txtMinAge.Text = model.MinAge.ToString();
                    txtMaxAge.Text = model.MaxAge.ToString();
                    txtRemark.Text = model.Remark.ToString();
                    //球场数据
                    this.txtMaxTeamNumber.Text = BMatchField.GetTeamNumber(id).ToString();
                    var FieldList = BMatchField.GetList(id);
                    var list = BBallField.GetList(model.CityId);
                    FiledSelectStr = "<optgroup label=\"请选择球场\">";
                    if (list.Count() > 0)
                    {
                        foreach (var m in list)
                        {
                            if (IsFieldExist(FieldList, m.Id))
                                FiledSelectStr += "<option value=\"" + m.Id + "\" selected>" + m.FieldName + "</option>";
                            else
                                FiledSelectStr += "<option value=\"" + m.Id + "\">" + m.FieldName + "</option>";
                        }
                    }
                    FiledSelectStr += "</optgroup>";
                }
            }
        }
        /// <summary>
        /// 判断球场是否存在
        /// </summary>
        /// <param name="list"></param>
        /// <param name="FieldId"></param>
        /// <returns></returns>
        private bool IsFieldExist(List<tbl_MatchField> list, string FieldId) {
            var model = list.FirstOrDefault(n => n.FieldId == FieldId);
            if (model != null)
                return true;
            else
                return false;
        }
        #endregion

        #region 保存数据
        private void SaveData()
        {
            bool isResult=false;
            #region 取值
            string strErr = "";
            string act = Utils.GetQueryStringValue("act");
            string id = Utils.GetQueryStringValue("id");
            ManagerList ManageModel = ManageUserAuth.GetManageUserModel();
            int UserId = ManageModel.Id;
            string ContactName = ManageModel.ContactName;
            ManageModel = null;
            string MatchName = Utils.GetFormValue(txtMatchName.UniqueID);
            string MatchPhoto = GetAttachFile();
            int MatchAreaType = Utils.GetInt(Utils.GetFormValue("ddlMatchAreaType"));
            int TypeId = Utils.GetInt(Utils.GetFormValue("ddlTypeId"));
            int CountryId = Utils.GetInt(Utils.GetFormValue("ddlCountry"), 0);
            int ProvinceId = Utils.GetInt(Utils.GetFormValue("ddlProvince"), 0);
            int CityId = Utils.GetInt(Utils.GetFormValue("ddlCity"), 0);
            int AreaId = Utils.GetInt(Utils.GetFormValue("ddlArea"), 0);
            //球场
            string[] Fieldlist = Utils.GetFormValues("ddlField");
            int FieldMaxNumber = Utils.GetInt(Utils.GetFormValue(txtMaxTeamNumber.UniqueID));
            string CountryName = "", ProvinceName = "", CityName = "", AreaName = "";
            bool IsCityLimit = Utils.GetInt(Utils.GetFormValue(radLimit.UniqueID), 0) == 0 ? false : true;
            string Signbegin = Utils.GetFormValue(txtSignBegin.UniqueID);
            string Signend = Utils.GetFormValue(txtSignEnd.UniqueID);
            string beginDate = Utils.GetFormValue(txtStartDate.UniqueID);
            string EndDate = Utils.GetFormValue(txtEndDate.UniqueID);
            decimal RegistrationFee = Utils.GetDecimal(Utils.GetFormValue(txtRegistrationFee.UniqueID), 0);
            decimal EarnestMoney = Utils.GetDecimal(Utils.GetFormValue(txtEarnestMoney.UniqueID), 0);
            string MasterOrganizer = Utils.GetFormValue(txtMasterOrganizer.UniqueID);
            string CoOrganizers = Utils.GetFormValue(txtCoOrganizers.UniqueID);
            string Organizer = Utils.GetFormValue(txtOrganizer.UniqueID);
            string Sponsors = Utils.GetFormValue(txtSponsors.UniqueID);
            int TeamNumber = Utils.GetInt(Utils.GetFormValue(txtTeamNumber.UniqueID), 0);
            int PlaysMin = Utils.GetInt(Utils.GetFormValue(txtPlayersMin.UniqueID), 0);
            int PlayMax = Utils.GetInt(Utils.GetFormValue(txtPlayersMax.UniqueID), 0);
            int BayMax = Utils.GetInt(Utils.GetFormValue(txtBayMax.UniqueID), 0);
            int BayMin = Utils.GetInt(Utils.GetFormValue(txtBayMin.UniqueID), 0);
            int TotalTime = Utils.GetInt(Utils.GetFormValue(txtTotalTime.UniqueID), 0);
            int BreakTime = Utils.GetInt(Utils.GetFormValue(txtBreakTime.UniqueID), 0);
            int MinAge = Utils.GetInt(Utils.GetFormValue(txtMinAge.UniqueID), 0);
            int MaxAge = Utils.GetInt(Utils.GetFormValue(txtMaxAge.UniqueID), 0);
            //string Remark = Utils.GetFormValue(txtRemark.UniqueID);
            string Remark = txtRemark.Text;

            #region  判断
            if (string.IsNullOrWhiteSpace(MatchName))
            {
                strErr += "请填写赛事名称！\n";
            }
            if (Fieldlist.Length < 1) {
                strErr += "请至少选择一个比赛球场！\n";
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
            var ProvinceModel = BMSysProvince.GetProvinceModel(ProvinceId);
            if (ProvinceModel != null)
            {
                ProvinceName = ProvinceModel.Name;
            }
            else
            {
                strErr += "请选择省份!\n";
            }
            var CityModel = BMSysProvince.GetCityModel(CityId);
            if (CityModel != null)
            {
                CityName = CityModel.Name;
            }
            else
            {
                strErr += "请选择城市!\n";
            }
            var AreaModel = BMSysProvince.GetAreaModel(AreaId);
            if (AreaModel != null)
            {
                AreaName = AreaModel.Name;
            }
            else
            {
                strErr += "请选择区县\n";
            }
            if (!StringValidate.IsDateTime(Signbegin))
            {
                strErr += "请选择正确的报名开始日期!/n";
            }
            if (!StringValidate.IsDateTime(Signend))
            {
                strErr += "请选择正确的报名截止日期!/n";
            }
            if (!StringValidate.IsDateTime(beginDate))
            {
                strErr += "请填写正确的开始日期!\n";
            }

            if (!StringValidate.IsDateTime(EndDate))
            {
                strErr += "请填写正确的结束日期!\n";
            }
            if (TeamNumber == 0)
            {
                strErr += "参赛球队必须大于0!\n";
            }
            if (PlaysMin == 0)
            {
                strErr += "报名最低名额不能小于0!\n";
            }
            if (PlayMax == 0)
            {
                strErr += "报名最高名额不能小于0!\n";
            }
            if (TotalTime == 0)
            {
                strErr += "比赛时间不能小于0！\n";
            }
            if (BreakTime == 0)
            {
                strErr += " 中场休息时间不能小于0\n";
            }
            if (!String.IsNullOrWhiteSpace(strErr))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", strErr));
                return;
            }
            #endregion

            switch (act)
            {
                case "add":
                    string NewId = Guid.NewGuid().ToString();
                    isResult = BMatch.Add(new tbl_Match
                    {
                        Id = NewId,
                        MatchAreaType = MatchAreaType,
                        TypeId = TypeId,
                        UserId = UserId,
                        ContactName = ContactName,
                        CountryId = CountryId,
                        CountryName = CountryName,
                        ProvinceId = ProvinceId,
                        ProvinceName = ProvinceName,
                        CityId = CityId,
                        CityName = CityName,
                        AreaId = AreaId,
                        AreaName = AreaName,
                        MatchName = MatchName,
                        MatchPhoto=MatchPhoto,
                        IsCityLimit = IsCityLimit,
                        SignBeginDate=DateTime.Parse(Convert.ToDateTime(Signbegin).ToString("yyyy-MM-dd")),
                        SignEndDate=DateTime.Parse(Convert.ToDateTime(Signend).ToString("yyyy-MM-dd")),
                        BeginDate = DateTime.Parse(Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd")),
                        EndDate = DateTime.Parse(Convert.ToDateTime(EndDate).ToString("yyyy-MM-dd")),
                        RegistrationFee = RegistrationFee,
                        EarnestMoney = EarnestMoney,
                        MasterOrganizer = MasterOrganizer,
                        CoOrganizers = CoOrganizers,
                        Organizer = Organizer,
                        Sponsors = Sponsors,
                        TeamNumber = TeamNumber,
                        PlayersMin = PlaysMin,
                        PlayersMax = PlayMax,
                        BayMin = BayMin,
                        BayMax = BayMax,
                        TotalTime = TotalTime,
                        BreakTime = BreakTime,
                        MinAge = MinAge,
                        MaxAge = MaxAge,
                        Remark = Remark,
                        IssueTime = DateTime.Now

                    });
                    //写入参赛球场信息
                    foreach (string str in Fieldlist)
                    {
                        BMatchField.Add(new tbl_MatchField
                        {
                            Id = System.Guid.NewGuid().ToString(),
                            MatchId = NewId,
                            FieldId = str,
                            TeamNumber = FieldMaxNumber
                        });
                    }
                    break;
                case "update":
                    isResult = BMatch.Update(new tbl_Match
                    {

                        Id = id,
                        MatchAreaType = MatchAreaType,
                        TypeId = TypeId,
                        UserId = UserId,
                        ContactName = ContactName,
                        CountryId = CountryId,
                        CountryName = CountryName,
                        ProvinceId = ProvinceId,
                        ProvinceName = ProvinceName,
                        CityId = CityId,
                        CityName = CityName,
                        AreaId = AreaId,
                        AreaName = AreaName,
                        MatchName = MatchName,
                        MatchPhoto=MatchPhoto,
                        IsCityLimit = IsCityLimit,
                        SignBeginDate = DateTime.Parse(Signbegin),
                        SignEndDate = DateTime.Parse(Signend),
                        BeginDate = DateTime.Parse(beginDate.ToString()),
                        EndDate = DateTime.Parse(EndDate.ToString()),
                        RegistrationFee = RegistrationFee,
                        EarnestMoney = EarnestMoney,
                        MasterOrganizer = MasterOrganizer,
                        CoOrganizers = CoOrganizers,
                        Organizer = Organizer,
                        Sponsors = Sponsors,
                        TeamNumber = TeamNumber,
                        PlayersMin = PlaysMin,
                        PlayersMax = PlayMax,
                        BayMin = BayMin,
                        BayMax = BayMax,
                        TotalTime = TotalTime,
                        BreakTime = BreakTime,
                        MinAge = MinAge,
                        MaxAge = MaxAge,
                        Remark = Remark
                    });
                    BMatchField.Delete(id);
                    //写入参赛球场信息
                    foreach (string str in Fieldlist)
                    {
                        BMatchField.Add(new tbl_MatchField
                        {
                            Id = System.Guid.NewGuid().ToString(),
                            MatchId = id,
                            FieldId = str,
                            TeamNumber = FieldMaxNumber
                        });
                    }
                    break;
            }
            if (isResult)
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
        

            #endregion

      
        }
        #endregion


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
    }
}