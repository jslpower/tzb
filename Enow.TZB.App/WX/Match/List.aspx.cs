using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Match
{
    /// <summary>
    /// 赛事列表
    /// </summary>
    public partial class List : System.Web.UI.Page
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
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitList();
            }
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        void InitList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            int cityid = Utils.GetInt(Utils.GetQueryStringValue("CityId"), 0);
            if (cityid == 0)
            {
                int uscityid = Getuscityid();
                cityid = uscityid != 0 ? uscityid : cityid;

            }
            litcityname.Text = BCity.Getcityname(cityid);
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            Model.MMatch SearchModel = new Model.MMatch();           
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.MatchName = Server.UrlDecode(KeyWord.Trim());
            }
            SearchModel.cityid = cityid;
            var list = BMatch.GetCityMatchList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list != null && list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
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
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                return model.CityId;
            }
            return 0;
        }
        /// <summary>
        /// 获取查询实体
        /// </summary>
        /// <returns></returns>
        MMatch GetQuery()
        {
            return new MMatch() {
                MatchName = Utils.GetQueryStringValue("KeyWord")
            };
        }
        /// <summary>
        /// 报名
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        protected string SignUp(string Id, DateTime StartDate, DateTime EndDate)
        {
            if (StartDate <= DateTime.Now && EndDate >= DateTime.Now) {
                return "<a href=\"SignUp.aspx?Id=" + Id + "\" class=\"basic_btn basic_ybtn\">点击报名</a>";
            } else { return ""; }
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string KeyWords = Utils.GetFormValue(this.txtKeyWords.UniqueID);
            if (!String.IsNullOrEmpty(KeyWords) && KeyWords != "赛事搜索")
            {
                Response.Redirect("List.aspx?KeyWord=" + Server.UrlEncode(KeyWords));
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(57));
            }
        }

    }
}