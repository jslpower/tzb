using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Article
{
    public partial class DZArticle : System.Web.UI.Page
    {
        // <summary>
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
            if (doType == "delete")
            {
                DeleteArticle();
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                phadd.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.舵主日志新增);
                phupdate.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.舵主日志修改);
                phdelete.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.舵主日志删除);
            }

            InitList();
        }
        /// <summary>
        /// 删除日志
        /// </summary>
        private void DeleteArticle()
        {
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            string ContactName = UserModel.ContactName;
            UserModel = null;
            string s = Utils.GetQueryStringValue("id");
            string[] ids = s.Split(',');
            foreach (var IdStr in ids)
            {
                if (string.IsNullOrEmpty(IdStr))
                {
                    //操作失败：未选择任何要操作的信息！
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                    return;
                }

                bllRetCode = BArticle.Delete(IdStr);
            }
            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "日志删除成功!"));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "日志删除失败!"));
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        private void InitList()
        {
            var model = ManageUserAuth.GetManageUserModel();
            Model.MMemberArticleSearch SearchModel = new Model.MMemberArticleSearch();
            if (model!=null)
            {
                SearchModel.MemberId = model.Id.ToString();
            }
           
            SearchModel.IsEnable = true;
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
           
            SearchModel.TypeId = 1;//舵主日志
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }

            string KeyWord = Utils.GetQueryStringValue("Name");
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.KeyWords = Server.UrlDecode(KeyWord.Trim());
            }
            var list = BArticle.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();

                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = intPageSize;
                ExportPageInfo1.intRecordCount = rowCounts;
                ExportPageInfo1.CurrencyPage = CurrencyPage;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
           
        
        }
    }
}