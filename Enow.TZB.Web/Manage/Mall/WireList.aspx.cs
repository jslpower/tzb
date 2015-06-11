using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;


namespace Enow.TZB.Web.Manage.Mall
{
    public partial class WireList : System.Web.UI.Page
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
                DeleteGoods();
            }
            if (doType == "stop")
            {
                StopSellGoods();
            }
            if (doType == "start")
            {
                EnableSellGoods();
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                PhAdd.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.铁丝购商品管理);
                phUpdate.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.铁丝购商品修改);
                phdelete.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.铁丝购商品删除);
                phstop.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.铁丝购商品停售);
                phstart.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.铁丝购商品恢复销售);
                GetRoleclass();
                InitList();

            }
        }
        /// <summary>
        /// 查询分类
        /// </summary>
        private void GetRoleclass()
        {
            var list = BRoleClass.GetTypeList(4);
            if (list.Count > 0)
            {
                list.Insert(0, new tbl_GoodsRoleClass() { Id = 0, Rolename = "全部" });
            }
            dropyjclass.DataSource = list;
            dropyjclass.DataBind();
        }
        /// <summary>
        /// 停售商品
        /// </summary>
        protected void StopSellGoods()
        {
            string id = Utils.GetQueryStringValue("id");

            int bllRetCode = BMallGoods.StopSellGoods(id) == true ? 1 : -99;

            if (bllRetCode == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功！"));
            else if (bllRetCode == -99) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));
        }
        /// <summary>
        /// 恢复销售商品
        /// </summary>
        protected void EnableSellGoods()
        {
            string id = Utils.GetQueryStringValue("id");

            int bllRetCode = BMallGoods.EnableSellGoods(id) == true ? 1 : -99;

            if (bllRetCode == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功！"));
            else if (bllRetCode == -99) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        private void InitList()
        {
            MMallGoodsSearch searchModel = new MMallGoodsSearch();

            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            string name = Utils.GetQueryStringValue("Name");
            int Status = Utils.GetInt(Utils.GetQueryStringValue("Status"), 0);
            int types = Utils.GetInt(Utils.GetQueryStringValue("types"), 0);
            if (!string.IsNullOrWhiteSpace(name))
            {
                searchModel.GoodsName = name;
            }
            if (Status > 0)
            {
                searchModel.Status = (商品销售状态)Status;
            }
            if (types != 0)
            {
                searchModel.Roleype = types;
                dropyjclass.SelectedValue = types.ToString();
            }
            else
            {
                searchModel.Roleype = null;
            }
            searchModel.TypeId = (int)Enow.TZB.Model.商品分类.铁丝购;
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            var list = BMallGoods.GetGoodsmodelList(ref rowCounts, intPageSize, CurrencyPage, searchModel);
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
        /// 删除商品
        /// </summary>
        protected void DeleteGoods()
        {
            string id = Utils.GetQueryStringValue("id");

            int bllRetCode = BMallGoods.Delete(id) == true ? 1 : -99;

            if (bllRetCode == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：已成功删除！"));
            else if (bllRetCode == -99) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));
        }

        /// <summary>
        /// 绑定商品状态
        /// </summary>
        protected string InitStatus(string selectItem)
        {
            StringBuilder Strddl = new StringBuilder();
            Array array = Enum.GetValues(typeof(Model.商品销售状态));
            Strddl.Append("<option value='0'>-请选择-</option>");
            foreach (var arr in array)
            {

                int value = (int)Enum.Parse(typeof(Model.商品销售状态), arr.ToString());
                string text = Enum.GetName(typeof(Model.商品销售状态), arr);

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
    }
}