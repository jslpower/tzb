using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.Model;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Member
{
    /// <summary>
    /// 赛事支付
    /// </summary>
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
                Response.Expires = -1;
                Response.CacheControl = "no-cache";
                Response.Cache.SetNoStore();
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitMember(OpenId);
                string Id = Utils.GetQueryStringValue("Id");
                if (!String.IsNullOrWhiteSpace(Id))
                {
                    InitMatchInfo(Id);
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
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
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 加载赛事信息
        /// </summary>
        /// <param name="Id"></param>
        private void InitMatchInfo(string Id)
        {
            var MemberMatchModel = BMatchTeamMember.GetMemberMatchModel(Id);
            if (MemberMatchModel != null)
            {
                var model = BMatch.GetModel(MemberMatchModel.MatchId);
                if (model != null)
                {
                    this.ltrMatchName.Text = model.MatchName;
                    this.ltrMoney.Text = model.RegistrationFee.ToString("F2");
                    this.ltrEarnestMoney.Text = model.EarnestMoney.ToString("F2");
                        //取得参赛球队信息
                        var MM = BMatchTeam.GetModel(MemberMatchModel.MatchTeamId);
                        if (MM != null)
                        {
                            switch (MemberMatchModel.State)
                            {
                                case (int)Model.EnumType.参赛审核状态.资格审核通过:
                                    switch (MM.PayType)
                                    {
                                        case (int)Model.EnumType.支付方式.微信支付:
                                            //判断是否存在支付
                                            var m = BMemberWallet.GetExistsModel(MemberMatchModel.MatchTeamId);
                                            if (m != null)
                                            {
                                                //存在支付行为
                                                //跳转到支付页
                                                //string Sign = BMemberWallet.Sign(m.Id);
                                                string Sign = m.Id;
                                                Response.Redirect("/TenPay/P.aspx?s=" + Sign, true);
                                            }
                                            break;
                                    }
                                    break;
                                case (int)Model.EnumType.参赛审核状态.报名费确认中:
                                    switch (MM.PayType)
                                    {
                                        case (int)Model.EnumType.支付方式.线下支付:
                                            MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(129));
                                            break;
                                        case (int)Model.EnumType.支付方式.微信支付:
                                            //判断是否存在支付
                                            var m = BMemberWallet.GetExistsModel(MemberMatchModel.MatchTeamId);
                                            if (m != null)
                                            {
                                                //存在支付行为
                                                //跳转到支付页
                                                //string Sign = BMemberWallet.Sign(m.Id);
                                                string Sign = m.Id;
                                                Response.Redirect("/TenPay/P.aspx?s=" + Sign, true);
                                            }
                                            break;
                                    }
                                    break;
                                case (int)Model.EnumType.参赛审核状态.已获参赛权:
                                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(128));
                                    break;
                            }
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
        protected void btnLinePay_Click(object sender, EventArgs e)
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
                        TradeMoney = model.RegistrationFee + model.EarnestMoney,//比赛保证金+报名费
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
                        MessageBox.ShowAndRedirect(string.Format(CacheSysMsg.GetMsg(127), model.MatchName, (model.RegistrationFee + model.EarnestMoney).ToString("F2")), "/WX/Member/");
                    else
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(19));
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
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
        /// 微信支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTenPay_Click(object sender, EventArgs e)
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
                            TradeMoney = model.RegistrationFee + model.EarnestMoney,//比赛保证金+报名费
                            IsPayed = '0',
                            PayMemberId = MemberModel.Id,
                            PayContactName = MemberModel.ContactName,
                            Remark = MemberModel.ContactName + "于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "支付" + model.MatchName + "赛事费用:" + model.RegistrationFee.ToString("F2") + "元，报名费:" + model.EarnestMoney.ToString("F2") + "元！",
                            BindId = MemberMatchModel.MatchTeamId,
                            IssueTime = DateTime.Now
                        });
                        BMatchTeam.UpdatePayInfo(MemberMatchModel.MatchTeamId, Model.EnumType.支付方式.微信支付, false);
                        //跳转到支付页
                        //string Sign = BMemberWallet.Sign(PayId);
                        string Sign = PayId;
                        Response.Redirect("/TenPay/P.aspx?s=" + Sign, true);
                        return;
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
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
    }
}