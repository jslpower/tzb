using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using System.Text;

namespace Enow.TZB.Web.Manage.Job
{
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
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType").ToLower();
            switch (doType)
            {
                case "delete": DelJobInfo(); break;
                case "pass": ChangeState(); break;
                default: break;
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitPage();
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        private void InitPage()
        {
            #region 查询实体
            MjobSearch SearchModel = new MjobSearch();
            SearchModel.JobName = Utils.GetQueryStringValue("name");
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("jobtype"))&&Utils.GetInt(Utils.GetQueryStringValue("jobtype"))>-1)
            {
                SearchModel.jobType = Utils.GetInt(Utils.GetQueryStringValue("jobtype"));
            }
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("Isvalid"))&&Utils.GetInt(Utils.GetQueryStringValue("Isvalid")) > -1)
            {
                SearchModel.IsValid = Utils.GetInt(Utils.GetQueryStringValue("Isvalid"));
            }
            SearchModel.startDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startDate"));
            SearchModel.endDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endDate"));

            #endregion
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];

            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            var list = BJob.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (rowCounts > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = intPageSize;
                ExportPageInfo1.intRecordCount = rowCounts;
                ExportPageInfo1.CurrencyPage = CurrencyPage;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }

        }

        /// <summary>
        /// 根据ID获取城市名称
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        protected string GetCityName(int CityId)
        {
            string ReturnValue = "";

            var model = BLL.BMSysProvince.GetCityModel(CityId);
            if (model != null)
            {
                ReturnValue = model.Name;
            }
            return ReturnValue;
        }

        /// <summary>
        /// 删除招聘信息
        /// </summary>
        protected void DelJobInfo()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));

            int bllRetCode = BJob.Delete(s) == true ? 1 : -99;

            if (bllRetCode == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：已成功删除！"));
            else if (bllRetCode == -99) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));
        }

        /// <summary>
        /// 改变前台发布状态
        /// </summary>
        protected void ChangeState()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要修改的信息"));
            }

            var model = BJob.GetModel(s);
            if (model.IsValid)
            {
                model.IsValid = false;
            }
            else
            {
                model.IsValid = true;
            }
            int bllRetCode = BJob.UpdateValid(s, model.IsValid) == true ? 1 : -99;
            if (bllRetCode == 1)
            {
                if (model.IsValid)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：前台发布状态为隐藏！"));
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：前台发布状态为显示！"));
                }

            }
            else if (bllRetCode == -99)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));
            }
        }

        /// <summary>
        /// 招聘类别下拉框绑定
        /// </summary>
        /// <returns></returns>
        protected string InitJobType(string str)
        {
            StringBuilder strJobType = new StringBuilder();
            Array array = Enum.GetValues(typeof(Model.EnumType.JobType));
            strJobType.Append("<option value='-1' selected='selected'>-请选择-</option>");
            foreach (var arr in array)
            {
                int value = (int)Enum.Parse(typeof(Model.EnumType.JobType), arr.ToString());
                string text = Enum.GetName(typeof(Model.EnumType.JobType), arr);

                if (value.ToString().Equals(str))
                {
                    strJobType.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", value, text);
                }
                else
                {
                    strJobType.AppendFormat("<option value='{0}'>{1}</option>", value, text);
                }

            }
            return strJobType.ToString();
        }
    }
}