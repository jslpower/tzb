using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Fund
{
    /// <summary>
    /// 队员转账
    /// </summary>
    public partial class Transfers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitModel(OpenId);
            }
        }
        #region 加载球队成员基本信息
        /// <summary>
        /// 加载球队基本信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitModel(string OpenId)
        {
            var MemberModel = BMember.GetModelByOpenId(OpenId);
            if (MemberModel != null)
            {
                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                //判断是否设置过支付密码
                bool IsPayPassword = BMemeberPayPassword.IsExist(MemberModel.Id);
                if (IsPayPassword == false)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(58), "/WX/Fund/PayPasswordSet.aspx");
                    return;
                }
                string MemberId = MemberModel.Id;
                //查询自己所在的球队
                var TeamModel = BTeamMember.GetModel(MemberId);
                if (TeamModel != null)
                {
                    var model = BTeam.GetModel(TeamModel.TeamId);
                    if (model != null)
                    {
                        InitList(TeamModel.TeamId, MemberModel.Id);
                    }
                    else
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(6));
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(7), "/WX/Team/Default.aspx");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "/WX/Register/Default.aspx");
                return;
            }
        }
        /// <summary>
        /// 加载成员列表
        /// </summary>
        /// <param name="TeamId"></param>
        private void InitList(string TeamId,string MemberId)
        {
            var list = BTeamMember.GetList(TeamId);
            if (list != null)
            {
                var TeamMeberList = list.Where(n => n.MemberId != MemberId);
                this.ddlTeamMember.DataSource = TeamMeberList;
                this.ddlTeamMember.DataTextField = "ContactName";
                this.ddlTeamMember.DataValueField = "MemberId";
                this.ddlTeamMember.DataBind();
            }
        }
        #endregion
        /// <summary>
        /// 转账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                string ContactName = "";
                string MemberId = Utils.GetFormValue(this.ddlTeamMember.UniqueID);
                decimal PayMoney = Utils.GetDecimal(Utils.GetFormValue(this.txtMoney.UniqueID));
                string Password = Utils.GetFormValue(this.txtPayPassword.UniqueID);
                string CuPassword = "";
                if (String.IsNullOrWhiteSpace(MemberId))
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(156));
                    return;
                }
                else
                {
                    MemberId = MemberId.Trim();
                }
                if (MemberId == model.Id)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(157));
                    return;
                }
                var MemberModel = BMember.GetModel(MemberId);
                if (MemberModel != null)
                {
                    ContactName = MemberModel.ContactName;
                    MemberModel = null;
                }
                else
                {
                    MemberModel = null;
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(154));
                    return;
                }                
                if (String.IsNullOrWhiteSpace(Password))
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(59));
                    return;
                }
                else
                {
                    HashCrypto CrypTo = new HashCrypto();
                    Password = CrypTo.SHAEncrypt(Password.Trim());
                    CrypTo.Dispose();
                }
                //取得原始支付密码
                var PassModel = BMemeberPayPassword.GetModel(model.Id);
                if (PassModel != null)
                {
                    CuPassword = PassModel.PayPassword;
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(58), "/WX/Fund/PayPasswordSet.aspx");
                    return;
                }
                if (CuPassword != Password)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(61));
                    return;
                }
                //检查金额
                if (model.CurrencyNumber < PayMoney)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(149));
                    return;
                }
                //写入转账支付
                DateTime PayTime = DateTime.Now;
                string Id = System.Guid.NewGuid().ToString();
                BMemberWallet.Add(new tbl_MemberWallet
                {
                    Id = Id,
                    TradeNumber = Id,
                    UserId = 0,
                    UserContactName = "",
                    TypeId = (int)Model.EnumType.财务流水类型.转账支出,
                    MemberId = model.Id,
                    ContactName = model.ContactName,
                    TradeMoney = PayMoney,
                    IsPayed = '0',
                    PayMemberId = MemberId,
                    PayContactName = ContactName,
                    Remark = string.Format(CacheSysMsg.GetMsg(151), PayTime.ToString("yyyy-MM-dd HH:mm:ss"), ContactName, PayMoney.ToString("F2")),
                    IssueTime = PayTime
                });
                //更新支付状态
                bool rv = BMemberWallet.UpdatePayState(Id);
                //写入转账收入
                Id = System.Guid.NewGuid().ToString();
                BMemberWallet.Add(new tbl_MemberWallet
                {
                    Id = Id,
                    TradeNumber = Id,
                    UserId = 0,
                    UserContactName = "",
                    TypeId = (int)Model.EnumType.财务流水类型.转账收入,
                    MemberId = MemberId,
                    ContactName = ContactName,
                    TradeMoney = PayMoney,
                    IsPayed = '0',
                    PayMemberId = model.Id,
                    PayContactName = model.ContactName,
                    Remark = string.Format(CacheSysMsg.GetMsg(151), model.ContactName,PayTime.ToString("yyyy-MM-dd HH:mm:ss"), PayMoney.ToString("F2")),
                    IssueTime = PayTime
                });
                //更新收入状态
                rv = BMemberWallet.UpdatePayState(Id);
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(155), "/WX/Member/Default.aspx");
                return;
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "/WX/Register/Default.aspx");
                return;
            }
            //转账金额
            //判断转账金额不大于当前用户余额
            //转账
        }
    }
}