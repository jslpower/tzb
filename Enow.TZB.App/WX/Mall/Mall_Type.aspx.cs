using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Mall
{
    public partial class Mall_Type : System.Web.UI.Page
    {
        public string Typetitle = "";
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                InitList();
            }
        }
        /// <summary>
        /// 加载商品列表
        /// </summary>
        private void InitList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            int types =Utils.GetInt(Utils.GetQueryStringValue("type"),5);
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            if (types<=5)
            {
                Typetitle = ((Enow.TZB.Model.商品分类)types).ToString();
                UserHome1.Userhometitle = ((Enow.TZB.Model.商品分类)types).ToString();
            }
            else
            {
                types = 3;
            }
            //if (!string.IsNullOrEmpty(txtGoodsName.Text))
            //{
                Model.MGoodsClassSearch SearchModel = new Model.MGoodsClassSearch();
                SearchModel.IsDelete = false;
                SearchModel.GoodsType = types;
                SearchModel.ClassName = Utils.GetFormValue(txtGoodsName.UniqueID);
                //SearchModel.ClassName = Utils.InputText(txtGoodsName.Text);
                var list =BRoleClass.GetList(ref rowCounts, 100, CurrencyPage, SearchModel);
                if (list.Count() > 0)
                {
                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                }
            //}
            
        }
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            InitList();
        }
    }
}