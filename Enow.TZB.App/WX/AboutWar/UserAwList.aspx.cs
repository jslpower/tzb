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
    public partial class UserAwList : System.Web.UI.Page
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
        private bool dzqx = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            InitMember();
            if (!IsPostBack)
            {
                
                var action = Utils.GetQueryStringValue("ation").ToLower();
                switch (action)
                {
                    case "jujue":
                        UpdateWstate(1);
                        break;
                        
                    case "yinzhan":
                        UpdateWstate(2);
                        break;
                }
                InitList();
            }
        }
        #region 应战相关
            /// <summary>
        /// 修改客队状态
        /// </summary>
        /// <param name="typ">1拒绝  2应战</param>
        private void UpdateWstate(int typ)
        {
            string AID = Utils.GetQueryStringValue("AID");//约战编号
            string WID = Utils.GetQueryStringValue("WID");//球队约战信息编号
            if (!string.IsNullOrEmpty(AID) && !string.IsNullOrEmpty(WID))
            {
                var ballmodel = BTeamMember.GetModel(MemberId); //球队实体
                if (ballmodel==null)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请先加入球队！"));
                if (ballmodel.RoleType != (int)Enow.TZB.Model.EnumType.球员角色.队长)
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "只有队长才能确认！"));
                var model = BAboutWar.GetModel(AID);//约战信息实体
                if (model == null)
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "约战信息不存在！"));
                if (model.AboutState != (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.约战中)
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "该约战已开始或结束！"));
                tbl_AboutWarReport awrmodel = new tbl_AboutWarReport();
                awrmodel.AboutWarID = AID;
                awrmodel.Wid = WID;
                bool retval= BAboutWarReport.GetTeamYZ(awrmodel);
                if (retval)
                {
                    if (typ==1)
                    {
                        if (model.Atypes == 2)
                        {
                            model.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.终止;
                            BAboutWar.UpdateAboutState(model);//修改主表状态
                            BAboutWarReport.UpdateKdstate(AID,(int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.终止);//修改主客队状态（终止）
                        }
                        else
                        {
                            awrmodel.Wstates = (int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.应战被拒;
                            BAboutWarReport.UpdateModel(awrmodel);
                        }
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "拒绝成功！"));
                    }
                    else
                    {
                        if (model.Atypes == 1)
                        {
                            var Teammodel = Getteam(WID);
                            if (Teammodel != null)
                            {
                                if (BAboutWar.Updatekdid(Teammodel.TeamName, Teammodel.Id, AID))
                                {
                                    awrmodel.Wstates = (int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.进行中;
                                    BAboutWarReport.UpdateModel(awrmodel);//同意应战 修改客队状态为进行中
                                    BAboutWarReport.UpdateKdstate(awrmodel);//把其他队伍状态全部修改为拒绝
                                    BAboutWarReport.Updatezdstate(AID, (int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.进行中);//修改主队约战状态为进行中
                                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "应战成功！"));
                                }
                                else
                                {
                                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "应战失败！"));
                                }

                            }
                            else
                            {
                                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "球队不存在！"));
                            }
                        }
                        else
                        {
                            tbl_AboutWar yzmodel = new tbl_AboutWar();
                            yzmodel.Aid = AID;
                            yzmodel.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.进行中;
                            if (BAboutWar.UpdateAboutState(yzmodel))
                            {
                                BAboutWarReport.Updatejxzall(AID);
                                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "应战成功！"));
                            }
                            else
                            {
                                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "应战失败！"));
                            }
                        }
                        
                    }
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "球队约战信息错误！"));
                }
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "信息不正确！"));
            }
            

        }
        /// <summary>
        /// 是否发起者
        /// </summary>
        /// <param name="qdteamid">球队ID</param>TeamId
        /// <returns></returns>
        protected string Getyztitle(string qdteamid)
        {
            //发起者
            if (qdteamid == TeamId)
            {
                return "<span class=\"green\">发起的约战</span>";
            }
            else
            {
                return "<span class=\"red\">收到的约战</span>";
            }
        }
        /// <summary>
        /// 查询球队信息
        /// </summary>
        /// <param name="WID">报名编号</param>
        /// <returns></returns>
        private tbl_BallTeam Getteam(string WID)
        {
            var Wmodel = BAboutWarReport.GetModel(WID);
            if (Wmodel!=null)
            {
               return BAboutWar.GetQdid(Wmodel.TeamId);
            }
            return null;
        
        }
        #endregion
        #region 约战状态信息
        protected string GetAState(string AboutState, string Wtypes, string Wstates)
        {
            string str = "<p>状态：<span style=\"color: red;\">" + (Enow.TZB.Model.EnumType.GathersEnum.约战状态)(Enow.TZB.Utility.Utils.GetInt(AboutState)) + "</span></p>";

            if (Wtypes == "2")
            {
                    if (Wstates != ((int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.进行中).ToString()&&Wstates != ((int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.终止).ToString())
                    {
                        str = "<p>状态：<span style=\"color: red;\">" + (Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态)(Enow.TZB.Utility.Utils.GetInt(Wstates)) + "</span></p>";
                    }
                
            }
            return str;

        }
        #endregion
        #region 处理约战权限
        /// <summary>
        /// 约战操作
        /// </summary>
        /// <param name="qtWtypes">分类 1 主队  2客队</param>
        /// <param name="qtTeamId">球队编号</param>
        /// <param name="qtAboutState">约战状态</param>
        /// <param name="qtWstates">球队约战状态</param>
        /// <returns></returns>
        protected string Gettzqx(string qtWtypes, string qtTeamId, string qtAboutState, string qtWstates)
        {
            
            string str = "<a href=\"javascript:void(0);\" class=\"basic_btn jjbtn\">拒绝</a><a href=\"javascript:void(0);\"  class=\"basic_ybtn yzbtn\">应战</a>";
            if (!dzqx)
                str = "";
            if (qtAboutState != ((int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.约战中).ToString())
                str = "";
            if (qtWtypes == "1")
                str = "";
            if (TeamId == qtTeamId)
                str = "";
            if (qtWstates != ((int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.待确认).ToString())
                str = "";
            return str;
        }
        #endregion
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
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
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

                dt_AboutWarBallTeam SearchModel = new dt_AboutWarBallTeam();
                SearchModel.title = Utils.InputText(txtGoodsName.Text);
                SearchModel.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.约战中;
                SearchModel.TeamId = TeamId;
                SearchModel.Wtypes = 2;
                SearchModel.Wstates = -1;
                SearchModel.Atypes = -1;
                var list = BAboutWar.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
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