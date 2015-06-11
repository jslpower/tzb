using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Rudder
{
    public partial class Default : System.Web.UI.Page
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
        #region 关注信息
        /// <summary>
        /// 获取用户关注列表
        /// </summary>
        private void Getgzlist()
        {

            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                strlist = BOfferpat.GetStrlist(model.Id, 1);
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
            var JobId = Utils.GetQueryStringValue("JobId");//会员ID
            if (!BJob.Getdzbool(JobId))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "关注对象错误！"));
            }
            var AuthModel = BMemberApp.GetUserModel();
            if (AuthModel!=null)
            {
                tbl_Offerpat ofmodel = new tbl_Offerpat();
                ofmodel.Id = Guid.NewGuid().ToString();
                ofmodel.Inserttime = DateTime.Now;//添加时间
                ofmodel.MemberId = AuthModel.Id;//当前登陆的用户ID
                ofmodel.PatId = JobId;//关注对象
                ofmodel.Pattype = (int)Enow.TZB.Model.EnumType.GuanZhuenum.人员关注;
                var retmodel = BOfferpat.Getmodelbool(ofmodel);
                if (retmodel!=null)
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
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请先登陆！"));
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
            types = types > 3 ? 1 : types;
            Typetitle = ((Enow.TZB.Model.EnumType.JobType)types).ToString();
            UserHome1.Userhometitle = Typetitle + "风采";
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
            dt_OfferMemberView SearchModel = new dt_OfferMemberView();
            SearchModel.Jobtyoe = types;
            SearchModel.CityId = cityid;
            SearchModel.ContactName = Utils.GetFormValue(txtKeyWord.UniqueID);
            var list =BJob.GetdtzList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
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