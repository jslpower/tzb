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
    /// <summary>
    /// ajax球队列表
    /// </summary>
    public partial class AjaxTeamList : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        //图片裁剪后保存的文件夹
        protected const string DIRPATH = "/ufiles/";
        /// <summary>
        /// 会员关注列表
        /// </summary>
        private List<string> strlist = null;
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Getgzlist();
                InitList();
            }
        }
        #region 会员关注列表信息
        /// <summary>
        /// 获取用户关注列表
        /// </summary>
        private void Getgzlist()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            var model = BMember.GetModelByOpenId(AuthModel.OpenId);
            strlist = BOfferpat.GetStrlist(model.Id, 3);
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
        /// <summary>
        /// 加载球队列表
        /// </summary>
        private void InitList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
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
            Model.MBallTeamSearch SearchModel = new Model.MBallTeamSearch();
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.KeyWord = Server.UrlDecode(KeyWord.Trim());
            }
            SearchModel.State = Model.EnumType.球队审核状态.终审通过;
            SearchModel.CityId = cityid;
            var list = BTeam.GetTeamTimeList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
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