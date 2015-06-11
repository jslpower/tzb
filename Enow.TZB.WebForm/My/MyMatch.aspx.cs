using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.My
{
    public partial class MyMatch : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = 50;
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BWebMemberAuth.LoginCheck();
                InitList();
            }
        }

        private void InitList()
        {
            this.Master.Page.Title = "我的比赛";
            int rowCounts = 0;
            var model = BWebMemberAuth.GetUserModel();
            string MemberId=model.Id;
            string Page = Request.QueryString["Page"];
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            List<dt_MemberMatch> list = BMatchTeamMember.GetList(ref rowCounts, intPageSize, CurrencyPage,MemberId);
            if (rowCounts>0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
        }
        /// <summary>
        /// 显示操作信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        protected string UpdateOpt(string Id, Model.EnumType.参赛审核状态 State, Model.EnumType.球员角色 RoleType)
        {
            string strRv = "";
            switch (State)
            {
                case Model.EnumType.参赛审核状态.资格审核中:
                case Model.EnumType.参赛审核状态.资格审核拒绝:
                    if (RoleType == Model.EnumType.球员角色.队长)
                    {
                        strRv = "<a href=\"MatchUpdate.aspx?Id=" + Id + "\" class=\"yellowbg\">修改信息</a>";
                    }
                    break;
                case Model.EnumType.参赛审核状态.资格审核通过:
                    if (RoleType == Model.EnumType.球员角色.队长)
                    {
                        strRv = "<a href=\"MatchPaying.aspx?Id=" + Id + "\" class=\"yellowbg\">交纳保证金</a>";
                    }
                    break;
                case Model.EnumType.参赛审核状态.报名费确认中:
                    if (RoleType == Model.EnumType.球员角色.队长)
                    {
                        strRv = "<a href=\"javascript:void(0)\" class=\"yellowbg\">财务确认中</a>";
                    }
                    break;
                case Model.EnumType.参赛审核状态.已获参赛权:
                    if (RoleType == Model.EnumType.球员角色.队长)
                    {
                        strRv = "<a href=\"MatchTeamBallotResult.aspx?Id=" + Id + "\" class=\"yellowbg\">查看赛程</a>";
                    }
                    break;
            }
            return strRv;
        }
    }
}