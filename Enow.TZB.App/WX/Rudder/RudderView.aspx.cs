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
    public partial class RudderView : System.Web.UI.Page
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
            InitMember();
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
                Getjob();
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
        #region 关注
        private void Intermodel()
        {
            var JobId = Utils.GetQueryStringValue("RzId");//会员ID
            var rzmodel= BArticle.GetModel(JobId);
            if (rzmodel == null || rzmodel.IsEnable==false)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "日志错误！"));
            }
            var AuthModel = BMemberApp.GetUserModel();
            if (AuthModel != null)
            {
                tbl_Offerpat ofmodel = new tbl_Offerpat();
                ofmodel.Id = Guid.NewGuid().ToString();
                ofmodel.Inserttime = DateTime.Now;//添加时间
                ofmodel.MemberId = AuthModel.Id;//当前登陆的用户ID
                ofmodel.PatId = JobId;//关注对象
                ofmodel.Pattype = (int)Enow.TZB.Model.EnumType.GuanZhuenum.点赞;
                var retmodel = BOfferpat.Getmodelbool(ofmodel);
                if (retmodel != null)
                {
                    BOfferpat.Delete(retmodel.Id);
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "取消成功！"));
                }
                else
                {
                    BOfferpat.Add(ofmodel);
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "点赞成功！"));
                }

            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请先登录！"));
            }


        }
        #endregion
        #region 验证登录信息
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember()
        {
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
                string JobId = Utils.GetQueryStringValue("JobId");
                if (BOfferpat.GetMPidbool(JobId, MemberId))
                {
                    litguanzhu.Text = "取消关注";

                }
                else
                {
                    litguanzhu.Text = "关注";
                }
            }
        }
        #endregion
        #region 舵/堂/站主基本信息
        /// <summary>
        /// 查询基本信息
        /// </summary>
        private void Getjob()
        {
            string JobId = Utils.GetQueryStringValue("JobId");
            if (!string.IsNullOrEmpty(JobId))
            {

               var Jmodel=BJob.GetJobdtzModel(JobId);
               if (Jmodel!=null&&Jmodel.Jobtyoe!=null&&Jmodel.Jobtyoe!=0)
               {
                   litimage.Text = "<img class=\"floatR\" src=\""+Jmodel.MemberPhoto+"\">";//头像
                   litname.Text = Jmodel.ContactName;//姓名
                   litgzjy.Text = Jmodel.WorkYear;//年龄
                   litjuzhudi.Text = Jmodel.CityName;//居住地
                   litsex.Text = Jmodel.Gender != null ? Jmodel.Gender ==1?"男":"女": "男";
                   litshouji.Text = (Jmodel.MobilePhone.Length > 3 ? Jmodel.MobilePhone.Substring(1, 3) : Jmodel.MobilePhone)+"********";//电话
                   litemail.Text = Jmodel.Email;
                   litContent.Text = Jmodel.ApplyInfo;
                   Typetitle = ((Enow.TZB.Model.EnumType.JobType)(Jmodel.Jobtyoe)).ToString();
                   UserHome1.Userhometitle = Typetitle;
                   Getrzlist(Jmodel.MemberId, Utils.GetInt(Jmodel.Jobtyoe.ToString()));
               }
               else
               {
                   Response.Redirect("Default.aspx");
               }
            }
        }
        /// <summary>
        /// 舵主/堂主日志
        /// </summary>
        /// <param name="MemberID">舵主/堂主编号</param>
        private void Getrzlist(string JobID,int jobtypes)
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
            Model.MMemberArticleSearch SearchModel = new Model.MMemberArticleSearch();
            SearchModel.MemberId = JobID;
            SearchModel.IsEnable = true;
            SearchModel.TypeId = jobtypes;
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            var list = BArticle.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
        #endregion

        /// <summary>
        /// 加载商品列表
        /// </summary>
        private void InitList()
        {
 
        }
    }
}