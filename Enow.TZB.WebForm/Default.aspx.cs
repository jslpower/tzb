using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;
using Enow.TZB.Model;

namespace Enow.TZB.WebForm
{
    public partial class Default : System.Web.UI.Page
    {
        //图片裁剪后保存的文件夹
        protected const string DIRPATH = "/ufiles/";
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 登录
            string login = Utils.GetQueryStringValue("login");
            if (!string.IsNullOrEmpty(login))
            {
                Response.Clear();
                Response.Write(UserLogin());
                Response.End();
            }

            #region 退出
            string logout = Utils.GetQueryStringValue("type");
            if (!string.IsNullOrWhiteSpace(logout))
            {
                if (logout == "logout")
                {
                    BWebMemberAuth.Logout("Default.aspx");
                    return;
                }
            }
            #endregion




            #endregion

            if (!IsPostBack)
            {
                var model = BWebMemberAuth.GetUserModel();
                if (model != null)
                {
                    plnLogin.Visible = false;
                    plnDetail.Visible = true;
                    lblUserName.Text = model.UserName;

                    var userModel = BMember.GetModel(model.Id);
                    if (!string.IsNullOrWhiteSpace(userModel.MemberPhoto))
                    {
                        this.ltrPhoto.Text = "<img src=\"" + userModel.MemberPhoto + "\"  class=\"user-pic\">";

                    }
                    else
                    {
                        this.ltrPhoto.Text = "<img src=\"/images/user-pic.gif\"\" class=\"user-pic\">";
                    }
                    this.ltrCurrencyNumber.Text = userModel.CurrencyNumber.ToString("F2");
                }
                //铁文集
                BindArticleList(15, rpt_tie, 9);
                //铁文集图片轮播
                BindArticlePhoto(15, rpt_tiePhoto);
                //铁资讯
                BindArticleList(16, rpt_Lball, 9);
                //铁资讯图片轮播
                BindArticlePhoto(16, rpt_LballPhoto);
                //铁漫画
                BindArticleList(17, rptMHArticle, 6);
                //铁漫画图片轮播
                BindArticlePhoto(17, rptMHArticlePhoto);

                //铁众享
                BindArticleList(13, rpt_Share, 2);
                //铁丝球队
                BindTeam();
                //新入铁丝
                BindNewMember();
                //联赛杯赛
                BindMatch();
                //铁丝网
                BindBallField();


            }
        }


        private string UserLogin()
        {
            tbl_Member userInfo = null;
            string userName = Utils.GetFormValue("txtUid");
            string pwd = Utils.GetFormValue("txtPassword");
            if (string.IsNullOrEmpty(userName))
            {
                return UtilsCommons.AjaxReturnJson("0", "请填写用户名");
            }
            if (string.IsNullOrEmpty(pwd))
            {
                return UtilsCommons.AjaxReturnJson("0", "请填写密码");
            }

            int loginState = BWebMemberAuth.UserLogin(userName, pwd, out userInfo);

            switch (loginState)
            {
                case 1:
                    //  lblUserName.Text = userInfo.UserName;

                    return UtilsCommons.AjaxReturnJson("1", "登录成功");
                    break;
                case 2:
                    return UtilsCommons.AjaxReturnJson("2", "登录失败，您的账号尚未通过审核");
                    break;
                case 3:
                    return UtilsCommons.AjaxReturnJson("3", "登录失败，您的账号已禁用");
                    break;
                case -1:
                    return UtilsCommons.AjaxReturnJson("-1", "登录失败，用户名或者密码不正确");
                    break;
                default:
                    return UtilsCommons.AjaxReturnJson("-7", "登录错误，请联系管理员！");
                    break;
            }


        }

        /// <summary>
        /// 根据类型ID绑定相应的文章
        /// </summary>
        /// <param name="ClassId">类型ID</param>
        /// <param name="rpt">控件变量</param>
        private void BindArticleList(int ClassId, Repeater rpt, int PageSize)
        {
            MArticleSearch searchModel = new MArticleSearch();

            searchModel.ClassId = ClassId;
            searchModel.IsValid = true;
            // searchModel.PublicTarget = Model.发布对象.网站;
            int recordCount = 0;
            var list = BLL.Common.sys.Article.GetList(ref recordCount, PageSize, 1, searchModel);

            if (recordCount > 0)
            {
                rpt.DataSource = list;
                rpt.DataBind();
            }
        }

        /// <summary>
        /// 绑定铁丝球队
        /// </summary>
        private void BindTeam()
        {
            MBallTeamSearch searchModel = new MBallTeamSearch();
            searchModel.State = Model.EnumType.球队审核状态.终审通过;
            int recordCount = 0;
            var list = BTeam.GetList(ref recordCount, 12, 1, searchModel);
            if (recordCount > 0)
            {
                rptTteam.DataSource = list;
                rptTteam.DataBind();
            }
        }


        /// <summary>
        /// 新入铁丝
        /// </summary>
        private void BindNewMember()
        {
            MMemberSearch searchModel = new MMemberSearch();
            searchModel.State = Model.EnumType.会员状态.通过;
            int recordCount = 0;
            searchModel.IsHasPhoto = true;
            var list = BMember.GetList(ref recordCount, 6, 1, searchModel);
            if (recordCount > 0)
            {
                rptMemberList.DataSource = list;
                rptMemberList.DataBind();
            }
        }

        /// <summary>
        /// 联赛杯赛
        /// </summary>
        private void BindMatch()
        {
            int recordCount = 0;
            MMatch searchModel = new MMatch();
            var list = BMatch.GetList(ref recordCount, 2, 1, searchModel);
            if (recordCount > 0)
            {
                rptMatchList.DataSource = list;
                rptMatchList.DataBind();
            }
        }

        /// <summary>
        /// 文章图片轮播
        /// </summary>
        private void BindArticlePhoto(int classId, Repeater rpt)
        {
            int recordCount = 0;
            MArticleSearch searchModel = new MArticleSearch();
            searchModel.ShowPhoto = true;
            searchModel.ClassId = classId;
            var list = BLL.Common.sys.Article.GetList(ref recordCount, 5, 1, searchModel);
            if (recordCount > 0)
            {
                rpt.DataSource = list;
                rpt.DataBind();
            }
        }


        /// <summary>
        /// 铁丝网图片切换
        /// </summary>
        protected void BindBallField()
        {
            int rowCounts = 0;

            Model.MBallFieldSearch SearchModel = new Model.MBallFieldSearch();

            var list = BBallField.GetList(ref rowCounts, 2, 1, SearchModel);
            System.Text.StringBuilder sbSlider = new System.Text.StringBuilder();


            if (rowCounts > 0)
            {
                rptBallField.DataSource = list;
                rptBallField.DataBind();
            }

         


        }
    }
}