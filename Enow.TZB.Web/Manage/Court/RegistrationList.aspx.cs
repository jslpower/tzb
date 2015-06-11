using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Court
{
    public partial class RegistrationList : System.Web.UI.Page
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
            var action = Utils.GetQueryStringValue("doType").ToLower();
            switch (action)
            {
                case "shtg":
                    Upstatertg(1);
                    break;
                case "shwtg":
                    Upstatertg(2);
                    break;
            }
            if (!IsPostBack)
            {
                Getquanxian();
                Initload();
                InitPage();
            }
        }
        /// <summary>
        /// 权限验证
        /// </summary>
        private void Getquanxian()
        {
            ManageUserAuth.ManageLoginCheck();
            phadd.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.报名审核);
            
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initload()
        {
            //报名状态
            var bmlist = EnumObj.GetList(typeof(Model.EnumType.ApplicantsStartEnum));
            if (bmlist != null)
            {
                bmlist.Insert(0, new EnumObj { Value = "-1", Text = "全部" });
            }
            dropstater.DataSource = bmlist;
            dropstater.DataBind();
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        private void InitPage()
        {
            MApplicants codequery = new MApplicants();
            #region 查询实体
            var Title = Utils.InputText(Utils.GetQueryStringValue("Title"));
            var staters = Utils.GetInt(Utils.GetQueryStringValue("staters"),-1);
            codequery.Acttitle = Title;
            txtTitle.Value = Title;
            codequery.Appstater = staters;
            dropstater.SelectedValue = staters.ToString();
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
            var list = BApplicants.GetAppBalltUserList(ref rowCounts, intPageSize, CurrencyPage, codequery);
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
        public void Upstatertg(int staters)
        {
            var id = Utils.GetStringArray(Utils.GetQueryStringValue("id"), ",");
            if (id.Count <= 0)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要审核的信息1！"));
            }
            bool bllRetCode = BApplicants.UpdateBallStater(id, staters);
            if (bllRetCode)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：已修改审核状态！"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            }
        }
    }
}