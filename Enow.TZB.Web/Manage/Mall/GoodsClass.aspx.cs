using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using Enow.TZB.Model;

namespace Enow.TZB.Web.Manage.Mall
{
    public partial class GoodsClass : System.Web.UI.Page
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
            string doType = "";
            if (!string.IsNullOrEmpty(Request.QueryString["dotype"]))
            {
                doType = Request.QueryString["dotype"].ToString();
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                Getclass();
                InitData();
            }
            if (doType == "delete")
            {
                DeleteClass();
            }
        }
        /// <summary>
        /// 二级类别
        /// </summary>
        private void Getclass()
        {
            var fblist = new List<EnumObj>();
            fblist.Add(new EnumObj { Value = "0", Text = "全部" });
            fblist.Add(new EnumObj { Value = "1", Text = "爱心义卖" });
            fblist.Add(new EnumObj { Value = "2", Text = "公益拍卖" });
            fblist.Add(new EnumObj { Value = "3", Text = "培训课程" });
            fblist.Add(new EnumObj { Value = "4", Text = "足球旅游" });
            fblist.Add(new EnumObj { Value = "5", Text = "铁丝购" });
            dropyjclass.DataSource = fblist;
            dropyjclass.DataBind();
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitData()
        {
            int rowsCount = 0;
            string page = Request.QueryString["Page"];
            var Title = Utils.GetQueryStringValue("title");
            var types =Utils.GetInt(Utils.GetQueryStringValue("types"),0);
            if (!string.IsNullOrEmpty(page) && StringValidate.IsInteger(page))
            {
                int.TryParse(page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }

            }
            MGoodsClassSearch SearchModel = new MGoodsClassSearch();
            SearchModel.ClassName = Title;
            txtTypeName.Text = Title;
            if (types != 0)
            {
                SearchModel.GoodsType = types;
                dropyjclass.SelectedValue = types.ToString();
            }
            else
            {
                SearchModel.GoodsType = null;
            }
            var list = BRoleClass.GetList(ref rowsCount, intPageSize, CurrencyPage, SearchModel);
            if (rowsCount > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = rowsCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
            }
        }

        /// <summary>
        /// 删除商品类别
        /// </summary>
        private void DeleteClass()
        {
            var id = Utils.GetStringArray(Utils.GetQueryStringValue("id"), ",");
            if (id.Count<=0) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));

            bool bllRetCode = BRoleClass.Delete(id);

            if (bllRetCode) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：已成功删除！"));
            else if (!bllRetCode) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&types=" +Utils.GetFormValue(dropyjclass.UniqueID) + "&title=" + txtTypeName.Text, true);
        }
    }
}