using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.CommonPage
{
    public partial class AjaxArticles : System.Web.UI.Page
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

                Getuser();
                InitList();
            }
        }
        private void Getuser()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel!=null)
            {
                InitMember(AuthModel.OpenId);
            }
            
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
        /// <summary>
        /// 加载我的日志列表
        /// </summary>
        private void InitList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            int types = Utils.GetInt(Utils.GetQueryStringValue("types"), 0);
            types = types > 2 ? 0 : types;
            if (types != 0 && types != Getusjob())
            {
                types = 0;
            }
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            Model.MMemberArticleSearch SearchModel = new Model.MMemberArticleSearch();
            SearchModel.MemberId = MemberId;
            SearchModel.IsEnable = true;
            SearchModel.TypeId = types;
          
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.KeyWords = Server.UrlDecode(KeyWord.Trim());
            }
            var list = BArticle.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
        public int Getusjob()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel != null)
            {
                var model = BMember.GetModelByOpenId(AuthModel.OpenId);
                if (model != null && model.Jobtyoe != null && model.Jobtyoe != 0)
                {
                    return Utils.GetInt(model.Jobtyoe.ToString());
                }
            }
            return 0;

        }
    }
}