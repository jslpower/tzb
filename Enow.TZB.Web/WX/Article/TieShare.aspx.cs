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

namespace Enow.TZB.Web.WX.Article
{
    /// <summary>
    /// 铁众享
    /// </summary>
    public partial class TieShare : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        protected int TypeId = 13;
        protected string MemberId = "";
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitTypeList();
            }
        }
        /// <summary>
        /// 加载资讯列表
        /// </summary>
        /// <param name="TypeId"></param>
        private void InitTypeList()
        {
                int rowCounts = 0;
                string Page = Request.QueryString["Page"];
                int typeid = 2;
                if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
                {
                    int.TryParse(Page, out CurrencyPage);
                    if (CurrencyPage < 1)
                    {
                        CurrencyPage = 1;
                    }
                }
                int cityid = Utils.GetInt(Utils.GetQueryStringValue("CityId"), 0);
                if (cityid == 0)
                {
                    int uscityid = Getuscityid();
                    cityid = uscityid != 0 ? uscityid : cityid;

                }
                litcityname.Text = BCity.Getcityname(cityid);
                Model.MActivity SearchModel = new Model.MActivity();
                SearchModel.types = typeid;
                SearchModel.fbmb = (int)Enow.TZB.Model.EnumType.ReleaseEnum.微信;
                SearchModel.title = Utils.InputText(txtGoodsName.Text);
                var list = BActivity.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
                if (list.Count() > 0)
                {

                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                }
                else
                {
                    PlaceHolder1.Visible = false;
                }
 
        }
        /// <summary>
        /// 查询会员所在城市
        /// </summary>
        /// <returns></returns>
        private int Getuscityid()
        {

            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel != null)
            {
                var model = BMember.GetModelByOpenId(AuthModel.OpenId);
                if (model != null)
                {
                    return model.CityId;
                }
            }
            return 0;
        }
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            InitTypeList();
        }
    }
}