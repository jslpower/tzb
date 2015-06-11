using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Vote
{
    public partial class LotteryInfo : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            var ation = Utils.GetQueryStringValue("ation").ToLower();
            switch (ation)
            {
                case "saveadd":
                    Addtoupiao();
                    break;
            }
            if (!IsPostBack)
            {
                Getusermodel();
                InitLoad();
            }
        }
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
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        private void InitLoad()
        {
            var id = Utils.GetQueryStringValue("Vid");
            string strmsg = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                var model = BVote.GetModel(id);
                if (model != null && model.VRelease==((int)Model.发布对象.微信))
                {
                    ltrGoodsName.Text = model.Vtitle;
                    litltxt.Text = model.Remarks;
                    rptList.DataSource = BVoteOption.GettypList(model.Vid);
                    rptList.DataBind();
                }
                else
                {
                    strmsg = "抽奖信息编号不正确！请重新选择！";
                }
            }
            else
            {
                //未找到您的用户信息!
                strmsg = "该抽奖信息不存在！请重新选择！";
            }
            if (!string.IsNullOrEmpty(strmsg))
            {
                MessageBox.ShowAndRedirect(strmsg, "Defaut.aspx");
                return;
            }
        }
        /// <summary>
        /// 添加投票信息
        /// </summary>
        private void Addtoupiao()
        {
            #region 抽奖限制
            bool Userbol = false;
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel != null)
            {
                var model = BMember.GetModelByOpenId(AuthModel.OpenId);
                if (model != null)
                {
                    MemberId = model.Id;
                    Userbol = true;
                }

            }
            if (!Userbol)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "只支持会员抽奖！"));
            var VoteId = Utils.GetQueryStringValue("VoteID");
            if (string.IsNullOrEmpty(VoteId))
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "抽奖信息不正确！"));
            //抽奖信息实体
            var Votemodel = BVote.GetModel(VoteId);
            if (Votemodel == null && Votemodel.VRelease!=((int)Model.发布对象.微信))
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "该抽奖信息不存在！"));
            if (Votemodel.ExpireTime < DateTime.Now)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "该抽奖已过期！"));
            #endregion
            #region 抽奖资格判断
            bool retbol = false;//成功与否返回值
            //判断当前抽奖类型
            //主体抽奖
            if (Votemodel.Vtype == 3 && !string.IsNullOrEmpty(MemberId))
            {
                bool cansaibool = false;
                if (Votemodel.SubjectType == 1)
                {
                    var appuser = BApplicants.GetUsbool(Votemodel.MatchId, MemberId);
                    if (appuser != null && appuser.IsState == ((int)Enow.TZB.Model.EnumType.ApplicantsStartEnum.已通过))
                    {
                        cansaibool = true;
                    }
                }
                else
                {
                    cansaibool = BMatchTeamMember.GetMemberMatchModelBool(MemberId, Votemodel.MatchId);
                }
                if (cansaibool == false)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未参与比赛或活动无法参与抽奖！"));
                }

            }
            //判断是否已经参与抽奖
            retbol = BVoteUser.GetVoteBool(VoteId, MemberId);
            #endregion
            #region 抽奖
            if (retbol)
            {
                
                string zjtxtname="";
                bool zjmesg = Getzjmodel(Votemodel, ref zjtxtname);//抽奖
                if (zjmesg)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "恭喜抽中了" + zjtxtname + "！"));
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未中奖！"));
                }
                
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "你已参与过抽奖！"));
            }
            #endregion
        }
        /// <summary>
        /// 添加用户抽奖
        /// </summary>
        /// <param name="VoteId">抽奖信息ID</param>
        /// <returns></returns>
        private bool Getzjmodel(tbl_Vote VoteModel,ref string zjtxtname)
        {

            #region 奖项基本信息
            //抽奖状态
            bool zjmesg = false;
            //奖项数
            var jxmodel = BVoteOption.GettypList(VoteModel.Vid);
            #endregion    
            #region 中奖信息实体初始化
            //用户中奖实体
            var uszjxx = new tbl_VoteUser()
            {
                Uid = Guid.NewGuid().ToString(),//默认值 用户抽奖信息编号
                VoteInfoId = VoteModel.Vid,//默认值  抽奖编号
                UserId = MemberId,//默认值  用户编号
                UsVoteIsDelete = 0,//默认值 是否可用
                VoteOptionId = jxmodel.Count > 0 ? jxmodel[0].Oid : "",//默认值 奖项编号
                AwardsNum = 0,//默认值  未中奖
                UIndatetime = DateTime.Now//默认值 抽奖时间
            };
            #endregion
            #region 抽奖信息生成
            //奖项总数
            int? jxcount = (from tmp in jxmodel select tmp.SortNum).Sum(w => w != null ? w : 0);
            List<int> zhongzi = new List<int>();
            //随即种子
            for (int i = 0; i < (jxcount != null ? jxcount : 0); i++)
            {
                zhongzi.Add(i + 1);
            }
            //已抽奖集合
            List<int> Userlottery = BVoteUser.GetVotelistCJ(VoteModel.Vid);
            if (Userlottery != null)
            {
                //奖项已全部抽完则未中奖
                if ((jxcount != null ? jxcount : 0) == Userlottery.Count)
                {
                    BVoteUser.Add(uszjxx);
                    return zjmesg;
                }
                //排除以抽奖的种子
                zhongzi = (from tmp in zhongzi where !Userlottery.Contains(tmp) select tmp).ToList();
            }
            Random ran = new Random();
            //中奖索引
            int ranjx = ran.Next(0, VoteModel.EstimateNum);
            int Awards = 0;//中奖范围
            string bhAwards = "";//中奖编号
            string titleAwards = "";//中奖名称
            //随机数大于数组长度则未中奖
            if (ranjx >= zhongzi.Count)
            {
                BVoteUser.Add(uszjxx);
                return zjmesg;
            }
            for (int i = 0; i < jxmodel.Count; i++)
            {
                if (jxmodel[i].ONumber != null)
                {
                    Awards += Utils.GetInt(jxmodel[i].ONumber.ToString(), 0);
                    if (zhongzi[ranjx] <= Awards)
                    {
                        bhAwards = jxmodel[i].Oid;
                        titleAwards = jxmodel[i].Otitle;
                        break;
                    }
                }
            }
            #endregion
            #region 添加抽奖信息
            if (!string.IsNullOrEmpty(bhAwards))
            {
                uszjxx.VoteOptionId = bhAwards;
                uszjxx.AwardsNum = zhongzi[ranjx];
                zjtxtname = titleAwards;
                zjmesg = true;
            }
            BVoteUser.Add(uszjxx);
            #endregion
            return zjmesg;
        }
    }
}