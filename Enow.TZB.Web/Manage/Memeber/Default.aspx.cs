using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using System.Data;

namespace Enow.TZB.Web.Manage.Memeber
{
    /// <summary>
    /// 会员管理
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;

        /// <summary>
        /// 省市联动下拉框初始化
        /// </summary>
        protected string CId = "", PId = "", CSId = "", AId = "";
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");

            switch (doType)
            {
                case "Disabled": DisabledData(); break;
                case "Disabled2": DisabledDataReturn(); break;
                case "Enable": EnableData(); break;
                case "Craw": CrawData(); break;
                default: break;
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                if (ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.铁丝审核) == true)
                {
                    this.phCheck.Visible = true;
                }
                else
                {
                    this.phCheck.Visible = false;
                }
                if (ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.会员积分管理) == true)
                {
                    this.phIntegration.Visible = true;
                }
                else
                {
                    this.phIntegration.Visible = false;
                }
                if (ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.会员荣誉管理) == true)
                {
                    this.phHonor.Visible = true;
                }
                else
                {
                    this.phHonor.Visible = false;
                }
                if (ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.铁丝导出) == true)
                {
                    this.phToExcel.Visible = true;
                }
                else
                {
                    this.phToExcel.Visible = false;
                }
                InitList();
            }
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitList()
        {
            int rowsCount = 0;

            #region 查询条件

            int State = Utils.GetInt(Utils.GetQueryStringValue("State"), -1);
            string ContractName = Utils.GetQueryStringValue("ContractName");
            string MobileNo = Utils.GetQueryStringValue("MobilePhone");
            string NickName = Utils.GetQueryStringValue("NickName");
            int IsJoinTeam = Utils.GetInt(Utils.GetQueryStringValue("IsJoinTeam"), -1);
            int IsJoinMatch = Utils.GetInt(Utils.GetQueryStringValue("IsJoinMatch"), -1);
            int CountryId = Utils.GetInt(Utils.GetQueryStringValue("CountryId"), 0);
            int ProvinceId = Utils.GetInt(Utils.GetQueryStringValue("ProvinceId"), 0);
            int CityId = Utils.GetInt(Utils.GetQueryStringValue("CityId"), 0);
            int AreaId = Utils.GetInt(Utils.GetQueryStringValue("AreaId"), 0);
            DateTime? IssueBeginTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("IssueBeginTime"));
            DateTime? IssueEndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("IssueEndTime"));
            DateTime? CheckBeginTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("CheckBeginTime"));
            DateTime? CheckEndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("CheckEndTime"));
            Model.MMemberSearch SearchModel = new Model.MMemberSearch();
            #region 赋值
            if (CountryId > 0)
                CId = CountryId.ToString();
            if (ProvinceId > 0)
                PId = CountryId.ToString();
            if (CityId > 0)
                CSId = CityId.ToString();
            if (AreaId > 0)
                AId = AreaId.ToString();
            this.txtContractName.Text = ContractName;
            this.txtMobile.Text = MobileNo;
            this.txtNickName.Text = NickName;
            if (IsJoinTeam != -1) {
                this.ddlJoinTeam.Items.FindByValue(IsJoinTeam.ToString()).Selected=true;
            }
            if (IsJoinMatch != -1)
            {
                this.ddlJoinMatch.Items.FindByValue(IsJoinMatch.ToString()).Selected = true;
            }
            if (IssueBeginTime.HasValue)
                this.txtIBeginDate.Text = IssueBeginTime.Value.ToString("yyyy-MM-dd");
            if (IssueEndTime.HasValue)
                this.txtIEndDate.Text = IssueEndTime.Value.ToString("yyyy-MM-dd");
            if (CheckBeginTime.HasValue)
                this.txtCBeginDate.Text = CheckBeginTime.Value.ToString("yyyy-MM-dd");
            if (CheckEndTime.HasValue)
                this.txtCEndDate.Text = CheckEndTime.Value.ToString("yyyy-MM-dd");
            #endregion
            if (State > -1)
            {
                SearchModel.State = (Model.EnumType.会员状态)State;
            }
            if (!string.IsNullOrWhiteSpace(ContractName))
            {
                SearchModel.ContractName = ContractName;
               
            }
            if (!string.IsNullOrWhiteSpace(MobileNo))
            {
                SearchModel.MobileNo = MobileNo;
               
            }
            if (!string.IsNullOrWhiteSpace(NickName))
            {
                SearchModel.NickName = NickName;
                
            }
            if (IsJoinTeam > 0)
            {
                if (IsJoinTeam == 1)
                {
                    SearchModel.IsJoinTeam = false;
                }
                else
                {
                    SearchModel.IsJoinTeam = true;
                }
            }
            if (IsJoinMatch > 0)
            {
                if (IsJoinMatch == 1)
                {
                    SearchModel.IsJoinMatch = false;
                }
                else
                {
                    SearchModel.IsJoinMatch = true;
                }
            }
            if (CountryId > 0)
            {
                SearchModel.CountryId = CountryId;
            }
            if (ProvinceId > 0)
            {
                SearchModel.ProvinceId = ProvinceId;
            }
            if (CityId > 0)
            {
                SearchModel.CityId = CityId;
            }
            if (AreaId > 0)
            {
                SearchModel.AreaId = AreaId;
            }
#region 城市权限控制
            var ManageModel = ManageUserAuth.GetManageUserModel();
            if (ManageModel != null)
            {
                SearchModel.IsAllCity = ManageModel.IsAllCity;
                SearchModel.CityLimitList = ManageModel.CityList;
            }
#endregion
            if (IssueBeginTime.HasValue)
            {
                SearchModel.IssueBeginTime = IssueBeginTime;
            }
            if (IssueEndTime.HasValue)
            {
                SearchModel.IssueEndTime = IssueEndTime;
            }
            if (CheckBeginTime.HasValue)
            {
                SearchModel.CheckBeginTime = CheckBeginTime;
            }
            if (CheckEndTime.HasValue)
            {
                SearchModel.CheckEndTime = CheckEndTime;
            }
            #endregion

            string Page = Request.QueryString["Page"];

            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                CurrencyPage = int.Parse(Page);
                if (CurrencyPage < 1)
                    CurrencyPage = 1;
            }


            var list = BMember.GetjobList(ref rowsCount, intPageSize, CurrencyPage, SearchModel);
            if (rowsCount > 0)
            {
                this.rptList.DataSource = list;
             
                this.rptList.DataBind();
                this.ExportPageInfo1.LinkType = 3;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = rowsCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;

            }
          CId = CountryId.ToString();
          PId = ProvinceId.ToString();
          CSId = CityId.ToString();
          AId = AreaId.ToString();

        }
        #region 禁用/启用
        /// <summary>
        /// 启用操作
        /// </summary>
        private void EnableData()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            bool bllRetCode = BMember.UpdateState(s,Model.EnumType.会员状态.通过);

            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        /// <summary>
        /// 禁用操作
        /// </summary>
        private void DisabledData()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            bool bllRetCode = BMember.UpdateState(s, Model.EnumType.会员状态.拒绝);

            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        /// <summary>
        /// 退回待审操作
        /// </summary>
        private void DisabledDataReturn()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            bool bllRetCode = BMember.UpdateState(s, Model.EnumType.会员状态.审核中);

            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        /// <summary>
        /// 重抓微信信息
        /// </summary>
        private void CrawData()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            bool bllRetCode = BMember.CrawWxData(s);

            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        #endregion
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            #region 查询条件

      
            int State = Utils.GetInt(Utils.GetFormValue("state"), -1);
            string ContractName = Utils.GetFormValue(txtContractName.UniqueID);
            string MobileNo = Utils.GetFormValue(txtMobile.UniqueID);
            string NickName = Utils.GetFormValue(txtNickName.UniqueID);
            int IsJoinTeam = Utils.GetInt(Utils.GetFormValue(ddlJoinTeam.UniqueID), -1);
            int IsJoinMatch = Utils.GetInt(Utils.GetFormValue(ddlJoinMatch.UniqueID), -1);
            int CountryId = Utils.GetInt(Utils.GetFormValue("ddlCountry"), 0);
            int ProvinceId = Utils.GetInt(Utils.GetFormValue("ddlProvince"), 0);
            int CityId = Utils.GetInt(Utils.GetFormValue("ddlCity"), 0);
            int AreaId = Utils.GetInt(Utils.GetFormValue("ddlArea"), 0);
            DateTime? IssueBeginTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtIBeginDate.UniqueID));
            DateTime? IssueEndTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtIEndDate.UniqueID));
            DateTime? CheckBeginTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtCBeginDate.UniqueID));
            DateTime? CheckEndTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtCEndDate.UniqueID));
            Model.MMemberSearch SearchModel = new Model.MMemberSearch();
         

            if (State > -1)
            {
                SearchModel.State = (Model.EnumType.会员状态)State;
            }
            if (!string.IsNullOrWhiteSpace(ContractName))
            {
                SearchModel.ContractName = ContractName;
            }
            if (!string.IsNullOrWhiteSpace(MobileNo))
            {
                SearchModel.MobileNo = MobileNo;
            }
            if (!string.IsNullOrWhiteSpace(NickName))
            {
                SearchModel.NickName = NickName;
            }
            if (IsJoinTeam > 0)
            {
                if (IsJoinTeam == 1)
                {
                    SearchModel.IsJoinTeam = false;
                }
                else
                {
                    SearchModel.IsJoinTeam = true;
                }
            }
            if (IsJoinMatch > 0)
            {
                if (IsJoinMatch == 1)
                {
                    SearchModel.IsJoinMatch = false;
                }
                else
                {
                    SearchModel.IsJoinMatch = true;
                }
            }
            if (CountryId > 0)
            {
                SearchModel.CountryId = CountryId;
            }
            if (ProvinceId > 0)
            {
                SearchModel.ProvinceId = ProvinceId;
            }
            if (CityId > 0)
            {
                SearchModel.CityId = CityId;
            }
            if (AreaId > 0)
            {
                SearchModel.AreaId = AreaId;
            }
            if (IssueBeginTime.HasValue)
            {
                SearchModel.IssueBeginTime = IssueBeginTime;
            }
            if (IssueEndTime.HasValue)
            {
                SearchModel.IssueEndTime = IssueEndTime;
            }
            if (CheckBeginTime.HasValue)
            {
                SearchModel.CheckBeginTime = CheckBeginTime;
            }
            if (CheckEndTime.HasValue)
            {
                SearchModel.CheckEndTime = CheckEndTime;
            }
            #endregion
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&State=" + State + "&MobilePhone=" + MobileNo.Trim() + "&ContractName=" + Server.UrlEncode(ContractName) + "&NickName=" + NickName + "&IsJoinTeam=" + IsJoinTeam + "&IsJoinMatch=" + IsJoinMatch + "&CountryId=" + CountryId + "&ProvinceId=" + ProvinceId + "&CityId=" + CityId + "&AreaId=" + AreaId + "&IssueBeginTime=" + IssueBeginTime + "&IssueEndTime=" + IssueEndTime + "&CheckBeginTime=" + CheckBeginTime + "&CheckEndTime=" + CheckEndTime, true);
        }
        /// <summary>
        /// 导出EXCEL
        /// </summary>        
        protected void btnExport_Click(object sender, EventArgs e)
        {
            int rowsCount = 0;

            #region 查询条件
            int State = Utils.GetInt(Utils.GetFormValue("state"), -1);
            string ContractName = Utils.GetFormValue(txtContractName.UniqueID);
            string MobileNo = Utils.GetFormValue(txtMobile.UniqueID);
            string NickName = Utils.GetFormValue(txtNickName.UniqueID);
            int IsJoinTeam = Utils.GetInt(Utils.GetFormValue(ddlJoinTeam.UniqueID), -1);
            int IsJoinMatch = Utils.GetInt(Utils.GetFormValue(ddlJoinMatch.UniqueID), -1);
            int CountryId = Utils.GetInt(Utils.GetFormValue("ddlCountry"), 0);
            int ProvinceId = Utils.GetInt(Utils.GetFormValue("ddlProvince"), 0);
            int CityId = Utils.GetInt(Utils.GetFormValue("ddlCity"), 0);
            int AreaId = Utils.GetInt(Utils.GetFormValue("ddlArea"), 0);
            DateTime? IssueBeginTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtIBeginDate.UniqueID));
            DateTime? IssueEndTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtIEndDate.UniqueID));
            DateTime? CheckBeginTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtCBeginDate.UniqueID));
            DateTime? CheckEndTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtCEndDate.UniqueID));
            Model.MMemberSearch SearchModel = new Model.MMemberSearch();

            if (State > -1)
            {
                SearchModel.State = (Model.EnumType.会员状态)State;
            }
            if (!string.IsNullOrWhiteSpace(ContractName))
            {
                SearchModel.ContractName = ContractName;
            }
            if (!string.IsNullOrWhiteSpace(MobileNo))
            {
                SearchModel.MobileNo = MobileNo;
            }
            if (!string.IsNullOrWhiteSpace(NickName))
            {
                SearchModel.NickName = NickName;
            }
            if (IsJoinTeam > 0)
            {
                if (IsJoinTeam == 1)
                {
                    SearchModel.IsJoinTeam = false;
                }
                else
                {
                    SearchModel.IsJoinTeam = true;
                }
            }
            if (IsJoinMatch > 0)
            {
                if (IsJoinMatch == 1)
                {
                    SearchModel.IsJoinMatch = false;
                }
                else
                {
                    SearchModel.IsJoinMatch = true;
                }
            }
            if (CountryId > 0)
            {
                SearchModel.CountryId = CountryId;
            }
            if (ProvinceId > 0)
            {
                SearchModel.ProvinceId = ProvinceId;
            }
            if (CityId > 0)
            {
                SearchModel.CityId = CityId;
            }
            if (AreaId > 0)
            {
                SearchModel.AreaId = AreaId;
            }
            if (IssueBeginTime.HasValue)
            {
                SearchModel.IssueBeginTime = IssueBeginTime;
            }
            if (IssueEndTime.HasValue)
            {
                SearchModel.IssueEndTime = IssueEndTime;
            }
            if (CheckBeginTime.HasValue)
            {
                SearchModel.CheckBeginTime = CheckBeginTime;
            }
            if (CheckEndTime.HasValue)
            {
                SearchModel.CheckEndTime = CheckEndTime;
            }
            #endregion

            var list = BMember.GetjobList(ref rowsCount, 99999, 1, SearchModel);
            if (rowsCount > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("地域", typeof(System.String));
                dt.Columns.Add("手机号码", typeof(System.String));
                dt.Columns.Add("姓名", typeof(System.String));
                dt.Columns.Add("身份证号", typeof(System.String));
                dt.Columns.Add("昵称", typeof(System.String));
                dt.Columns.Add("状态", typeof(System.String));
                dt.Columns.Add("所属球队", typeof(System.String));
                dt.Columns.Add("注册时间", typeof(System.String));
                dt.Columns.Add("审核时间", typeof(System.String));
                dt.Columns.Add("铁丝币", typeof(System.Decimal));
                dt.Columns.Add("积分", typeof(System.Int32));
                dt.Columns.Add("荣誉值", typeof(System.Int32));

                foreach (var lst in list)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = lst.CountryName + "-" + lst.ProvinceName + "-" + lst.CityName + "-" + lst.AreaName;
                    dr[1] = lst.MobilePhone;
                    dr[2] = lst.ContactName;
                    dr[3] = lst.PersonalId;
                    dr[4] = lst.NickName;
                    dr[5] = (Model.EnumType.会员状态)lst.State;
                    dr[6] = lst.TeamName;
                    dr[7] = lst.IssueTime.ToString("yyyy-MM-dd");
                    dr[8] = DateTime.Parse(lst.CheckTime.ToString()).ToString("yyyy-MM-dd");
                    dr[9] = lst.CurrencyNumber;
                    dr[10] = lst.IntegrationNumber;
                    dr[11] = lst.HonorNumber;

                    dt.Rows.Add(dr);
                }
                NPOIHelper.TableToExcelForXLSAny(dt, "铁丝信息报表");
                //NPOIHelper.TableToExcelForXLSAny(UtilsCommons.ListToDataTable<dt_MemberView>(list), "铁丝信息报表");
            }
            else
            {
                Utils.ShowMsg("暂无满足条件的数据");
            }
        }

    }
}