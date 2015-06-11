using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Team
{
    /// <summary>
    /// 组队
    /// </summary>
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
            Getgzlist();
            var ation = Utils.GetQueryStringValue("ation").ToLower();
            switch (ation)
            {
                case "inter":
                    Intermodel();
                    break;
            }
            if (!IsPostBack)
            {
                InitList();
            }
        }

       
        #region 会员关注列表信息
        /// <summary>
        /// 获取用户关注列表
        /// </summary>
        private void Getgzlist()
        {
            var model = BMemberApp.GetUserModel();
            if (model!=null)
            {
                strlist = BOfferpat.GetStrlist(model.Id, 3);
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
                return "取消关注";
            }
            return "关注";

        }
        #endregion
        #region 关注
        private void Intermodel()
        {
            var JobId = Utils.GetQueryStringValue("JobId");//球队ID
            var qiudui= BTeam.GetModel(JobId);
            if (qiudui == null || qiudui.State != ((int)Model.EnumType.球队审核状态.终审通过))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "关注对象错误！"));
            }
            var AuthModel = BMemberApp.GetUserModel();
            if (AuthModel != null)
            {
                tbl_Offerpat ofmodel = new tbl_Offerpat();
                ofmodel.Id = Guid.NewGuid().ToString();
                ofmodel.Inserttime = DateTime.Now;//添加时间
                ofmodel.MemberId = AuthModel.Id;//当前登陆的用户ID
                ofmodel.PatId = JobId;//关注对象
                ofmodel.Pattype = (int)Enow.TZB.Model.EnumType.GuanZhuenum.球队关注;
                var retmodel = BOfferpat.Getmodelbool(ofmodel);
                if (retmodel != null)
                {
                    BOfferpat.Delete(retmodel.Id);
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "取消成功！"));
                }
                else
                {
                    BOfferpat.Add(ofmodel);
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "关注成功！"));
                }

            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请先登录！"));
            }


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
            if (cityid==0)
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
            Model.MBallTeamSearch SearchModel = new Model.MBallTeamSearch();
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                txtKeyWord.Value = Server.UrlDecode(KeyWord.Trim());
                SearchModel.KeyWord = Server.UrlDecode(KeyWord.Trim());
            }
            SearchModel.State = Model.EnumType.球队审核状态.终审通过;
            SearchModel.CityId = cityid;
            var list = BTeam.GetTeamTimeList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            else
            {
                this.PlaceHolder1.Visible = false;
            }
        }
        /// <summary>
        /// 查询会员所在城市
        /// </summary>
        /// <returns></returns>
        private int Getuscityid()
        {

            var model = BMemberApp.GetUserModel();
                if (model!=null)
                {
                   return model.CityId;
                }
            
            return 0;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string KeyWords = Utils.GetFormValue("txtKeyWord");
            int cityid=Utils.GetInt(Utils.GetQueryStringValue("CityId"),0);
            Response.Redirect("Default.aspx?KeyWord=" + Server.UrlEncode(KeyWords.Trim()) + "&CityId=" + cityid, true);
        }        
    }
}