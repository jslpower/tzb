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
    public partial class AjaxRudderViewList : System.Web.UI.Page
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
        /// 职位分类
        /// </summary>
        protected int jobtype = 1;
        private List<string> strlist = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Getgzlist();
            if (!IsPostBack)
            {
                var jid = Utils.GetQueryStringValue("JobID");
                if (!string.IsNullOrEmpty(jid))
                {
                    Getrzlist(jid);
                }
            }
        }
        #region 关注信息
        /// <summary>
        /// 获取用户关注列表
        /// </summary>
        private void Getgzlist()
        {
            var model = BMemberApp.GetUserModel();
            if (model!=null)
            {
                strlist = BOfferpat.GetStrlist(model.Id, 2);
            }
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
                return "zan_ok";
            }
            return "zan";

        }
        #endregion
        /// <summary>
        /// 舵主/堂主日志
        /// </summary>
        /// <param name="MemberID">舵主/堂主编号</param>
        private void Getrzlist(string JobID)
        {
            var Jmodel = BJob.GetJobdtzModel(JobID);
            if (Jmodel==null||Jmodel.Jobtyoe==null||Jmodel.Jobtyoe==0)
            {
                Response.End();
            }
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
            Model.MMemberArticleSearch SearchModel = new Model.MMemberArticleSearch();
            SearchModel.MemberId = JobID;
            SearchModel.IsEnable = true;
            SearchModel.TypeId =Utils.GetInt(Jmodel.Jobtyoe.ToString());
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            var list = BArticle.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
    }
}