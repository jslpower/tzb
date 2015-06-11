using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Member
{
    /// <summary>
    /// 我的日志列表页
    /// </summary>
    public partial class Articles : System.Web.UI.Page
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
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Request.QueryString["doType"];
             switch (doType)
             {
                 case "delete": DeleteMemberArticle(); break;
             
                
                 default: break;
             }
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitMember(OpenId);
                InitList();
            }
        }

      
        /// <summary>
        /// 删除会员日志
        /// </summary>
        private void DeleteMemberArticle()
        {

            string s = Utils.GetQueryStringValue("id");
           
            int bllRetCode = BArticle.Delete(s) == true ? 1 : -99;

            if (bllRetCode == 1) {
               // MessageBox.ShowAndRedirect("会员日志删除成功！", "Articles.aspx");
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "会员日志删除成功！"));
            }

            else {
              
                //MessageBox.ShowAndRedirect("会员日志删除失败！", "Articles.aspx");
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-99", "会员日志删除失败！"));
            };
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
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        public int Getusjob()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel != null)
            {
                var model = BMember.GetModelByOpenId(AuthModel.OpenId);
                if (model != null && model.Jobtyoe != null && model.Jobtyoe!=0)
                {
                    return Utils.GetInt(model.Jobtyoe.ToString());
                }
            }
            return 0;
            
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
            if (types!=0&&types!=Getusjob())
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
            UserHome1.Userhometitle = types==0?"我的日志":((Enow.TZB.Model.EnumType.JobType)types).ToString()+"日志";
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
            else
            {
                this.PlaceHolder1.Visible = false;
            }
        }
    }
}