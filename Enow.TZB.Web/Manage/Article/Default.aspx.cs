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

namespace Enow.TZB.Web.Manage.Article
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
            string doType = Utils.GetQueryStringValue("dotype");
            switch (doType)
            {
                case "delete": DeleteArticle(); break;
                case "Pass": ChangeValid(); break;
                default: break;
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitPage();

            }

        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void InitPage()
        {
            MArticleSearch searchModel = new MArticleSearch();
            #region 查询实体
            searchModel.KeyWords = Utils.GetQueryStringValue("Title");
            if (Utils.GetInt(Utils.GetQueryStringValue("ddlClass")) > 0)
            {
                searchModel.ClassId = Utils.GetInt(Utils.GetQueryStringValue("ddlClass"));

            }
            if (Utils.GetInt(Utils.GetQueryStringValue("ddlPublishTarget")) > 0)
            {
                searchModel.PublicTarget = (发布对象)Utils.GetInt(Utils.GetQueryStringValue("ddlPublishTarget"));
            }
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("ddlIsValid")) && Utils.GetInt(Utils.GetQueryStringValue("ddlIsValid")) > -1)
            {
                searchModel.IsValid = Utils.GetInt(Utils.GetQueryStringValue("ddlIsValid")) == 1 ? true : false;

            }


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
            var list = BLL.Common.sys.Article.GetList(ref rowCounts, intPageSize, CurrencyPage, searchModel);
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
        /// 根据TypeID返回类型名称
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        protected string GetTypeName(int ClassId)
        {
            string ReturnValue = "";
            if (ClassId==-1)
            {
                ReturnValue = "铁子帮爱高";
            }
            MarticleTypeSeach searchModel = new MarticleTypeSeach();
            searchModel.id = ClassId;
            int rowsCount = 0;
            var list = ArticleClass.GetList(ref rowsCount, 1, 1, searchModel);
            if (rowsCount > 0)
            {
                ReturnValue = list[0].TypeName.ToString();
            }

            return ReturnValue;
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        protected void DeleteArticle()
        {

            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            if (StringValidate.IsInteger(s) == false) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            int bllRetCode = BLL.Common.sys.Article.Delete(int.Parse(s)) == true ? 1 : -99;

            if (bllRetCode == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：已成功删除！"));
            else if (bllRetCode == -99) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));
        }
        /// <summary>
        /// 改变文章状态
        /// </summary>
        /// <returns></returns>
        protected void ChangeValid()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要修改的信息"));
            }
            if (!StringValidate.IsInteger(s))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要修改的信息"));
            }
            var model = BLL.Common.sys.Article.GetModel(int.Parse(s));
            if (model.IsValid)
            {
                model.IsValid = false;
            }
            else
            {
                model.IsValid = true;
            }
            int bllRetCode = BLL.Common.sys.Article.Update(model) == true ? 1 : -99;
            if (bllRetCode == 1)
            {
                if (model.IsValid)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：文章状态为已审核！"));
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：文章状态为待审核！"));
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
        /// 绑定文章类型
        /// </summary>
        protected string InitPublishTarget(string selectItem)
        {
            StringBuilder Strddl = new StringBuilder();
            Array array = Enum.GetValues(typeof(Model.发布对象));
            Strddl.Append("<option value='0'>-请选择-</option>");
            foreach (var arr in array)
            {

                int value = (int)Enum.Parse(typeof(Model.发布对象), arr.ToString());
                string text = Enum.GetName(typeof(Model.发布对象), arr);

                if (value.ToString().Equals(selectItem))
                {
                    Strddl.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", value, text);
                }
                else
                {
                    Strddl.AppendFormat("<option value='{0}'>{1}</option>", value, text);
                }
            }
            
            return Strddl.ToString();


        }

        /// <summary>
        /// 绑定资讯类别
        /// </summary>
        protected string InitClass(string selectItem)
        {
            StringBuilder StrClass = new StringBuilder();
            Model.MarticleTypeSeach searchModel = new Model.MarticleTypeSeach();
            int rowsCount = 0;
            var list = ArticleClass.GetList(ref rowsCount, 999, 1, searchModel);
            StrClass.Append("<option value='0'>-请选择-</option>");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id.ToString().Equals(selectItem))
                {
                    StrClass.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", list[i].Id, list[i].TypeName);
                }
                else
                {
                    StrClass.AppendFormat("<option value='{0}'>{1}</option>", list[i].Id, list[i].TypeName);
                }
            }
            StrClass.Append("<option value='100'>铁子帮爱高</option>");
            return StrClass.ToString();
        }
    }
}