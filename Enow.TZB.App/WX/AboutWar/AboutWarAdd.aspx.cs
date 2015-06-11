using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using Newtonsoft.Json;

namespace Enow.TZB.Web.WX.AboutWar
{
    public partial class AboutWarAdd : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected string changdilist = "null";
        protected void Page_Load(object sender, EventArgs e)
        {
            Getusermodel();
            if (true)
            {
                Getchangdi();
                Getqiudui();
            }
          
        }
        #region 球队约战
        private void Getqiudui()
        {
            string Teamid = Utils.GetQueryStringValue("Teamid");
            tbl_BallTeam kteam = BAboutWar.GetQdid(Teamid);
            if (kteam != null)
            {
                litqd.Text = "<li><span class=\"label_name\">约战球队:</span>";
                litqd.Text +=kteam.TeamName;
                litqd.Text += "</li>";
            }
        }
        #endregion
        /// <summary>
        /// 验证会员登录
        /// </summary>
        private void Getusermodel()
        {
            BMemberApp.LoginCheck();
            InitMember();
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
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RedirectUrl);
                return;
            }
        }
        /// <summary>
        /// 初始化球场信息
        /// </summary>
        private void Getchangdi()
        {
            var qclist = BBallField.GetFieldList();
            if (qclist.Count > 0)
            {
                List<QiuChang> list = new List<QiuChang>();
                for (int i = 0; i < qclist.Count; i++)
                {
                    list.Add(new QiuChang()
                    {
                        Id = qclist[i].Id,
                        FieldName = qclist[i].FieldName,
                        Address = qclist[i].CountryName + qclist[i].ProvinceName + qclist[i].CityName + qclist[i].CountyName + qclist[i].Address
                    });
                }

                changdilist = JsonConvert.SerializeObject(list);
                list.Insert(0, new QiuChang() { Id = "-1", FieldName = "请选择" });
                dropqiuchang.DataSource = list;
                dropqiuchang.DataBind();
                var Qid = Utils.GetQueryStringValue("QID");
                if (!string.IsNullOrEmpty(Qid))
                {
                    if (dropqiuchang.Items.FindByValue(Qid) != null)
                    {
                        dropqiuchang.SelectedValue = Qid;
                        dropqiuchang.Enabled = false;
                    }
                }
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            var Loadmodel = BMemberApp.GetUserModel();
            if (Loadmodel==null)
            {
                MessageBox.Show("请先登录！");
                return;
            }
            var ballmodel = BTeamMember.GetModel(Loadmodel.Id);
            if (ballmodel == null)
            {
                MessageBox.Show("请先参加球队！");
                return;
            }
            if (ballmodel.RoleType != (int)Enow.TZB.Model.EnumType.球员角色.队长)
            {
                MessageBox.Show("只有队长才能发布约战信息！");
                return;
            }
            tbl_BallField qiuchang = null;
            var Qid = Utils.GetQueryStringValue("QID");
            if (!string.IsNullOrEmpty(Qid))
            {
                qiuchang = BBallField.GetModel(Qid);
            }
            else
            {
                qiuchang = BBallField.GetModel(Utils.GetFormValue(dropqiuchang.UniqueID));
            }
            if (qiuchang == null)
            {
                MessageBox.Show("球场信息不正确！");
                return;
            }
            tbl_AboutWar model = new tbl_AboutWar()
            {
                Aid = Guid.NewGuid().ToString(),
                title = Utils.GetFormValue(txtkouhao.UniqueID),//标题
                AboutTime = Utils.GetDateTime(Utils.GetFormValue(txttime.UniqueID)),//约战时间
                AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.约战中,//约战状态
                CourtId = qiuchang.Id,//球场编号
                CourtName = qiuchang.FieldName,//球场名称
                Address = qiuchang.CountryName + qiuchang.ProvinceName + qiuchang.CityName + qiuchang.CountyName + qiuchang.Address,//地址
                AwcityId=qiuchang.CityId,
                Afternoon = Utils.GetFormValue(txtWarBook.UniqueID),//战书
                Format = Utils.GetInt(Utils.GetFormValue(dropsaizhi.UniqueID)),//赛制
                Costnum = Utils.GetInt(Utils.GetFormValue(dropfeiyong.UniqueID)),//费用
                MainID = ballmodel.TeamId,//主队
                MainName = ballmodel.TeamName,
                GuestID = "",//客队
                GuestName = "",
                Atypes = 1,//约战分类 1(一对多 客队报名等待主队选择)   2(一对一  主队挑战客队  等待客队确定)
                AIsDelete = 0,
                Zbstate = 1,//审核状态 默认1未审核
                Releasetime = DateTime.Now,
            };
            string Teamid = Utils.GetQueryStringValue("Teamid");
            tbl_BallTeam kteam = null;
            if (!string.IsNullOrEmpty(Teamid))
            {
                kteam = BAboutWar.GetQdid(Teamid);
                if (kteam != null)
                {
                    model.GuestID = kteam.Id;
                    model.GuestName = kteam.TeamName;
                    model.Atypes = 2;
                }
            }
            bool retbol = BAboutWar.Add(model);
            string tzurl = "Default.aspx";
            if (retbol)
            {
                
                var tmp = new tbl_AboutWarReport()
                {
                    Wid = Guid.NewGuid().ToString(),
                    Wstates = (int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.约战中,
                    Wtypes = 1,//主队1  客队2
                    RIsDelete = 0,
                    TeamId = ballmodel.TeamId,
                    AboutWarID = model.Aid,
                    Inserttime = DateTime.Now

                };
                BAboutWarReport.Add(tmp);
                if (kteam != null)
                {
                    var kdtmp = new tbl_AboutWarReport()
                    {
                        Wid = Guid.NewGuid().ToString(),
                        Wstates = (int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.待确认,
                        Wtypes = 2,//主队1  客队2
                        RIsDelete = 0,
                        TeamId = kteam.Id,
                        AboutWarID = model.Aid,
                        Inserttime = DateTime.Now
                    };
                    BAboutWarReport.Add(kdtmp);
                    tzurl = "UserAwList.aspx";
                }
            }

            MessageBox.ShowAndRedirect("约战发布成功！", tzurl);
        }
        #region 球场信息
        /// <summary>
        /// 球场信息
        /// </summary>
        class QiuChang
        {
            /// <summary>
            /// 球场ID
            /// </summary>
            public string Id
            { get; set; }
            /// <summary>
            /// 球场名称
            /// </summary>
            public string FieldName
            { get; set; }
            /// <summary>
            /// 球场地址
            /// </summary>
            public string Address
            { get; set; }
        }
        #endregion

    }
}