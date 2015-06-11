using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;


namespace Enow.TZB.Web.WX.AboutWar
{
    public partial class GathersGoing : System.Web.UI.Page
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
        /// 球队编号
        /// </summary>
        protected string TeamId = "";
        /// <summary>
        /// 判断是否队长
        /// </summary>
        protected bool dzqx = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Getusermodel();
            if (!IsPostBack)
            {
                
                InitList();
            }
        }
        #region 应战相关

        #endregion


        /// <summary>
        /// 登录验证
        /// </summary>
        private void Getusermodel()
        {
            BMemberAuth.LoginCheck();
            var AuthModel = BMemberAuth.GetUserModel();
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
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
                var btmodel = BTeamMember.GetModel(MemberId);
                if (btmodel!=null&&btmodel.RoleType == (int)Enow.TZB.Model.EnumType.球员角色.队长)
                {
                    dzqx = true;
                    TeamId = btmodel.TeamId;
                }
                else
                {
                    TeamId = BAboutWar.GetUserTeam(MemberId);
                }
                if (string.IsNullOrEmpty(TeamId))
                {
                     MessageBox.ShowAndRedirect("请先参加球队", "/WX/Team/");
                }
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 加载商品列表
        /// </summary>
        private void InitList()
        {
            if (!string.IsNullOrEmpty(TeamId))
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

                tbl_AboutWar SearchModel = new tbl_AboutWar();
                SearchModel.title = Utils.InputText(txtGoodsName.Text);
                SearchModel.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.进行中;
                SearchModel.MainID = TeamId;
                var list = BAboutWar.GetAboutWarList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
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
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            InitList();
        }
    }
}