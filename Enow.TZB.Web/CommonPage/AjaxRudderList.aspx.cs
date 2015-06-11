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
    public partial class AjaxRudderList : System.Web.UI.Page
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
        /// <summary>
        /// 会员关注列表
        /// </summary>
        private List<string> strlist = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Getgzlist();
            if (!IsPostBack)
            {
                Getusermodel();
                InitList();
            }
        }
        #region 关注信息
        /// <summary>
        /// 获取用户关注列表
        /// </summary>
        private void Getgzlist()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            var model = BMember.GetModelByOpenId(AuthModel.OpenId);
            strlist = BOfferpat.GetStrlist(model.Id, 1);
        }
        /// <summary>
        /// 查询是否已关注
        /// </summary>
        /// <param name="Usid"></param>
        /// <returns></returns>
        protected string Selgzyf(string Usid)
        {
            if (strlist != null && strlist.Contains(Usid))
            {
                return "取消关注";
            }
            return "关注";

        }
        #endregion

        
        #region 登陆相关
        /// <summary>
        /// 验证登陆
        /// </summary>
        private void Getusermodel()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel==null)
            {
                Response.End();
            }
            string OpenId = AuthModel.OpenId;
            InitMember(OpenId);
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember(string OpenId)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    Response.End();
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
            }
            else
            {
                Response.End();
            }
        }
        #endregion
        /// <summary>
        /// 加载商品列表
        /// </summary>
        private void InitList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            int types = Utils.GetInt(Utils.GetQueryStringValue("types"), 1);//一级分类
            string keyword = Utils.GetQueryStringValue("keyword");
            types = types > 3 ? 1 : types;
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
            dt_OfferMemberView SearchModel = new dt_OfferMemberView();
            SearchModel.Jobtyoe = types;
            SearchModel.CityId = cityid;
            SearchModel.ContactName = keyword;
            var list = BJob.GetdtzList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
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
    }
}