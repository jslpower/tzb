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

namespace Enow.TZB.Web.WX.Member
{
    public partial class GatheringSignUp : System.Web.UI.Page
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
        /// <summary>
        /// 标题
        /// </summary>
        public string ApTitle = "";
        /// <summary>
        /// 分类
        /// </summary>
        public string types = "";
        /// <summary>
        /// 编号
        /// </summary>
        public string Acid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = Utils.GetInt(Utils.GetQueryStringValue("ActId"), 0);
                InitListModel(id);
            }
        }
        private void InitListModel(int id)
        {
            Acid = id.ToString();
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            int typeid = Utils.GetInt(Utils.GetQueryStringValue("types"), 1);
            types = typeid.ToString();
            if (typeid <= 4)
            {
                ApTitle = ((Enow.TZB.Model.EnumType.ActivityEnum)(typeid)).ToString();
                UserHome1.Userhometitle = ApTitle;
            }
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            Model.MApplicants SearchModel = new Model.MApplicants();
            SearchModel.ActivityId = id;
            SearchModel.fbmb = (int)Enow.TZB.Model.EnumType.ReleaseEnum.微信;
            var list =BApplicants.GetApplUserList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            if (list.Count <= rowCounts)
                this.PlaceHolder1.Visible = false;
        }
    }
}