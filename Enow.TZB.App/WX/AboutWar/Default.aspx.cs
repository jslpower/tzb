using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.AboutWar
{
    public partial class Default : System.Web.UI.Page
    {
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
            litcityname.Text = cityid == 0 ? "全部" : BCity.Getcityname(cityid);
            dt_AboutWarBallTeam SearchModel = new dt_AboutWarBallTeam();
            SearchModel.title = Utils.InputText(txtGoodsName.Text);
            SearchModel.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.约战中;
            SearchModel.Wtypes = 1;
            SearchModel.Wstates = -1;
            SearchModel.Atypes = 1;
            SearchModel.AwcityId = cityid;
            var list =BAboutWar.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
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
        /// <summary>
        /// 查询会员所在城市
        /// </summary>
        /// <returns></returns>
        private int Getuscityid()
        {
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                return model.CityId;
            }

            return 0;
        }
    }
}