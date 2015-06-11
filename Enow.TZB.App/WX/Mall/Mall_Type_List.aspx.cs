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
    public partial class Mall_Type_List : System.Web.UI.Page
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
            int typeid =Utils.GetInt(Utils.GetQueryStringValue("RoleType"),-1);//二级分类
            int GoodsClassId = Utils.GetInt(Utils.GetQueryStringValue("type"), -1);//一级分类
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
           
                Model.MMallGoodsSearch SearchModel = new Model.MMallGoodsSearch();
                SearchModel.GoodsName = Utils.InputText(txtGoodsName.Text);
                SearchModel.Status =Enow.TZB.Model.商品销售状态.在售;
                if (GoodsClassId>=1&&GoodsClassId <= 4)
                {
                    SearchModel.TypeId = GoodsClassId;
                    Typetitle = ((Enow.TZB.Model.商品分类)GoodsClassId).ToString();
                    UserHome1.Userhometitle = ((Enow.TZB.Model.商品分类)GoodsClassId).ToString();
                }
                else
                {
                    if (typeid > 0)
                    {
                        var ejty=BRoleClass.GetModel(typeid);
                        if (ejty != null)
                        {
                            SearchModel.Roleype = typeid;
                            Typetitle = ((Enow.TZB.Model.商品分类)ejty.Type).ToString() + "-" + ejty.Rolename;
                            UserHome1.Userhometitle = ((Enow.TZB.Model.商品分类)ejty.Type).ToString() + "-" + ejty.Rolename;
                        }
                        else
                        {
                            SearchModel.TypeId = 3;
                        }
                    }
                    else
                    {
                        SearchModel.TypeId = 3;
                    }
                }
                var list = BMallGoods.GetGoodsmodelList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
                if (list.Count() > 0)
                {
                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                }
                else
                {
                    this.PlaceHolder1.Visible = false;
                }
        }
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            InitList();
        }
    }
}