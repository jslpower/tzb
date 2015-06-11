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
    public partial class MatchPaying : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BWebMemberAuth.LoginCheck();
                string Id = Utils.GetQueryStringValue("Id");
                if (!String.IsNullOrWhiteSpace(Id))
                {
                    InitMatchInfo(Id);
                    this.Master.Page.Title = "赛事报名";
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
        }
        #region 赛事初始化
        /// <summary>
        /// 加载赛事信息
        /// </summary>
        /// <param name="Id"></param>
        private void InitMatchInfo(string Id)
        {
            var MemberMatchModel = BMatchTeamMember.GetMemberMatchModel(Id);
            if (MemberMatchModel != null)
            {
                if (MemberMatchModel.State > (int)Model.EnumType.参赛审核状态.资格审核通过)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(56), "./");
                    return;
                }
                var model = BMatch.GetModel(MemberMatchModel.MatchId);
                if (model != null)
                {
                    ltrMatchName.Text = model.MatchName;
                    ltrSignDate.Text = model.SignBeginDate.ToString("yyyy年MM月dd日") + " 至 " + model.SignEndDate.ToString("yyyy年MM月dd日");
                    ltrMatchDate.Text = model.BeginDate.ToString("yyyy年MM月dd日") + " 至 " + model.EndDate.ToString("yyyy年MM月dd日");
                    ltrMatchArea.Text = model.CountryName + "-" + model.ProvinceName + "-" + model.CityName + "-" + model.AreaName;
                    ltrSignTeamCount.Text = model.SignUpNumber.ToString() + "/" + model.TeamNumber.ToString() + "支";
                    ltrDepositMoney.Text = model.RegistrationFee.ToString("C2") + "元";
                    ltrEarnestMoney.Text = model.EarnestMoney.ToString("C2") + "元";
                    ltrMasterOrganizer.Text = model.MasterOrganizer.ToString();
                    ltrCoOrganizers.Text = model.CoOrganizers.ToString();
                    ltrOrganizer.Text = model.Organizer.ToString();
                    ltrSponsors.Text = model.Sponsors.ToString();
                    ltrSignUpNumber.Text = model.PlayersMin.ToString() + "-" + model.PlayersMax.ToString() + "人";
                    ltrBayNumber.Text = model.BayMin.ToString() + "-" + model.BayMax.ToString() + "人";
                    ltrTotalTime.Text = model.TotalTime.ToString() + "分钟";
                    ltrBreakTime.Text = model.BreakTime.ToString() + "分钟";

                    this.ltrTeamNumber.Text = "<span id=\"spanSignUpNumber\">" + MemberMatchModel.JoinNumber.ToString() + "</span>" + "/" + model.PlayersMax.ToString();
                    var JoinList = BMatchTeamMember.GetListByMatchId(MemberMatchModel.MatchId, MemberMatchModel.MatchTeamId);
                    if (JoinList != null)
                    {
                        this.rptList.DataSource = JoinList;
                        this.rptList.DataBind();
                    }
                    this.ltrFieldName.Text = MemberMatchModel.FieldName;
                    this.ltrFieldAddress.Text = MemberMatchModel.FieldAddress;
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
        #endregion
        #region 加载成员列表
        /// <summary>
        /// 绑定球员列表操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitOperation(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = (dt_MatchTeamMember)e.Item.DataItem;
                Literal ltrOperation = (Literal)e.Item.FindControl("ltrOperation");
                Literal ltrPhoto = (Literal)e.Item.FindControl("ltrPhoto");
                if (!string.IsNullOrWhiteSpace(row.HeadPhoto))
                {
                    ltrPhoto.Text = "<img src=\"" + row.HeadPhoto + "\" width=\"120\" height=\"120\">";
                }
                else
                {
                    ltrPhoto.Text = "<img src=\"/images/user-tx.gif\">";
                }
            }
        }
        #endregion 
        #region 支付
        /// <summary>
        /// 支付宝支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAliPay_Click(object sender, EventArgs e)
        {
            string Id = Utils.GetQueryStringValue("Id");
            var MemberMatchModel = BMatchTeamMember.GetMemberMatchModel(Id);
            if (MemberMatchModel != null)
            {
                var model = BMatch.GetModel(MemberMatchModel.MatchId);
                if (model != null)
                {
                    //取得用户信息
                    var AuthModel = BMemberAuth.GetUserModel();
                    var MemberModel = BMember.GetModelByOpenId(AuthModel.OpenId);
                    if (MemberModel != null)
                    {
                        //判断是否设置过支付密码
                        bool IsPayPassword = BMemeberPayPassword.IsExist(MemberModel.Id);
                        if (IsPayPassword == false)
                        {
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(58), "/WX/Fund/PayPasswordSet.aspx");
                            return;
                        }
                        if (String.IsNullOrWhiteSpace(MemberModel.ContactName))
                        {
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                            return;
                        }
                        BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                        //写入待支付
                        string PayId = System.Guid.NewGuid().ToString();
                        BMemberWallet.Add(new tbl_MemberWallet
                        {
                            Id = PayId,
                            TradeNumber = PayId,
                            UserId = 0,
                            UserContactName = "",
                            TypeId = (int)Model.EnumType.财务流水类型.比赛保证金,
                            MemberId = MemberModel.Id,
                            ContactName = MemberModel.ContactName,
                            TradeMoney = model.RegistrationFee + model.EarnestMoney,
                            IsPayed = '0',
                            PayMemberId = MemberModel.Id,
                            PayContactName = MemberModel.ContactName,
                            Remark = MemberModel.ContactName + "于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "支付" + model.MatchName + "赛事保证金:" + model.RegistrationFee.ToString("F2") + "元，报名费:" + model.EarnestMoney.ToString("F2") + "元！",
                            BindId = MemberMatchModel.MatchTeamId,
                            IssueTime = DateTime.Now
                        });
                        BMatchTeam.UpdatePayInfo(MemberMatchModel.MatchTeamId, Model.EnumType.支付方式.微信支付, false);
                        //跳转到支付页
                        //string Sign = BMemberWallet.Sign(PayId);
                        string Sign = PayId;
                        Response.Redirect("../AliPay/Pay.aspx?OrderId=" + Sign, true);
                        return;
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/Default.aspx");
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
        /// <summary>
        /// 线下支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOffLinePay_Click(object sender, EventArgs e)
        {
            string Id = Utils.GetQueryStringValue("Id");
            var MemberMatchModel = BMatchTeamMember.GetMemberMatchModel(Id);
            if (MemberMatchModel != null)
            {
                var model = BMatch.GetModel(MemberMatchModel.MatchId);
                if (model != null)
                {
                    //取得用户信息
                    var AuthModel = BMemberAuth.GetUserModel();
                    var MemberModel = BMember.GetModelByOpenId(AuthModel.OpenId);
                    if (MemberModel != null)
                    {
                        //判断是否设置过支付密码
                        bool IsPayPassword = BMemeberPayPassword.IsExist(MemberModel.Id);
                        if (IsPayPassword == false)
                        {
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(58), "/WX/Fund/PayPasswordSet.aspx");
                            return;
                        }
                        if (String.IsNullOrWhiteSpace(MemberModel.ContactName))
                        {
                            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                            return;
                        }
                        BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                        //写入待支付
                        string PayId = System.Guid.NewGuid().ToString();
                        BMemberWallet.Add(new tbl_MemberWallet
                        {
                            Id = PayId,
                            TradeNumber = PayId,
                            UserId = 0,
                            UserContactName = "",
                            TypeId = (int)Model.EnumType.财务流水类型.比赛保证金,
                            MemberId = MemberModel.Id,
                            ContactName = MemberModel.ContactName,
                            TradeMoney = model.RegistrationFee + model.EarnestMoney,
                            IsPayed = '0',
                            PayMemberId = MemberModel.Id,
                            PayContactName = MemberModel.ContactName,
                            Remark = MemberModel.ContactName + "于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "支付" + model.MatchName + "赛事保证金:" + model.RegistrationFee.ToString("F2") + "元，报名费:" + model.EarnestMoney.ToString("F2") + "元！",
                            BindId = MemberMatchModel.MatchTeamId,
                            IssueTime = DateTime.Now
                        });
                    BMatchTeam.UpdatePayInfo(MemberMatchModel.MatchTeamId, Model.EnumType.支付方式.线下支付, false);
                    bool bllRetCode = BMatchTeam.UpdateValid(MemberMatchModel.MatchTeamId, Model.EnumType.参赛审核状态.报名费确认中, 0, "", 0);
                    if (bllRetCode)
                        MessageBox.ShowAndRedirect(string.Format(CacheSysMsg.GetMsg(127), model.MatchName, (model.RegistrationFee + model.EarnestMoney).ToString("F2")), "/My/MyMatch.aspx");
                    else
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(19));
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/Default.aspx");
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
        #endregion
    }
}