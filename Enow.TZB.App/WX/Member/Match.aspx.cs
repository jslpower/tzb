using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Member
{
    /// <summary>
    /// 我的赛事
    /// </summary>
    public partial class Match : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = 50;
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
                InitMember();
                InitList();
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void InitMember()
        {
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        void InitList()
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
            var model = BMemberApp.GetUserModel();
             if (model != null)
             {
                 List<dt_MemberMatch> list = BMatchTeamMember.GetList(ref rowCounts, intPageSize, CurrencyPage, model.Id);
                 if (list != null && list.Count > 0)
                 {
                     rptList.DataSource = list;
                     rptList.DataBind();
                 }
                 else {
                     MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(55), "/WX/Match/List.aspx");
                     return;
                 }
             }
             else
             {
                 MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                 return;
             }
        }
        /// <summary>
        /// 显示操作信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        protected string UpdateOpt(string Id, Model.EnumType.参赛审核状态 State,Model.EnumType.球员角色 RoleType) {
            string strRv = "";
            switch (State) { 
                case Model.EnumType.参赛审核状态.资格审核中:
                case Model.EnumType.参赛审核状态.资格审核拒绝:
                    if (RoleType == Model.EnumType.球员角色.队长) {
                        strRv = "<a href=\"MatchUpdate.aspx?Id=" + Id + "\" class=\"basic_btn basic_ybtn\" style=\"float:right\">修改信息</a>";
                    }
                    break;
                case Model.EnumType.参赛审核状态.资格审核通过:
                    if (RoleType == Model.EnumType.球员角色.队长)
                    {
                        strRv = "<a href=\"MatchPaying.aspx?Id=" + Id + "\" class=\"basic_btn basic_ybtn\" style=\"float:right\">支付赛事费用</a>";
                    }
                    break;
                case Model.EnumType.参赛审核状态.报名费确认中:
                    if (RoleType == Model.EnumType.球员角色.队长)
                    {
                        strRv = "<a href=\"javascript:void(0)\" class=\"basic_btn basic_ybtn\" style=\"float:right\">财务确认中</a>";
                    }
                    break;
                case Model.EnumType.参赛审核状态.已获参赛权:
                    if (RoleType == Model.EnumType.球员角色.队长)
                    {
                        strRv = "<a href=\"MatchTeamBallotResult.aspx?Id=" + Id + "\" class=\"basic_btn basic_ybtn\" style=\"float:right\">查看赛程</a>";
                    }
                    break;
            }
            return strRv;
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
                Response.Redirect("Match.aspx?KeyWord=" + Server.UrlEncode(KeyWords));
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(33));
            }
        }
    }
}