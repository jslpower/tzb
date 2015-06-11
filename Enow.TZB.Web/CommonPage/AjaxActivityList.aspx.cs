using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.CommonPage
{
    public partial class AjaxActivityList : System.Web.UI.Page
    {
        public string Aptitle = "";
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
                Getusermodel();
                InitList();
            }
        }
        private void Getusermodel()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel==null)
            {
                Response.End();
            }
        }
        /// <summary>
        /// 加载商品列表
        /// </summary>
        private void InitList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            int typeid = Utils.GetInt(Utils.GetQueryStringValue("types"), 1);
            string title = Utils.GetQueryStringValue("title");
            if (typeid <= 4)
            {
                Aptitle = ((Enow.TZB.Model.EnumType.ActivityEnum)(typeid)).ToString();
            }
            int cityid = Utils.GetInt(Utils.GetQueryStringValue("CityId"), 0);
            if (cityid == 0)
            {
                int uscityid = Getuscityid();
                cityid = uscityid != 0 ? uscityid : cityid;

            }
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            Model.MActivity SearchModel = new Model.MActivity();
            SearchModel.types = typeid;
            SearchModel.title = Utils.InputText(title);
            SearchModel.fbmb = (int)Enow.TZB.Model.EnumType.ReleaseEnum.微信;
            SearchModel.CityId = cityid;
            var list = BActivity.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            //else
            //{
            //    this.PlaceHolder1.Visible = false;
            //}


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
    }
}