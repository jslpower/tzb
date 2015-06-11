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
    public partial class Default : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            BWebMemberAuth.LoginCheck();
           
         
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            this.Master.Page.Title = "会员中心";
            var model = BWebMemberAuth.GetUserModel();
            string memberId = model.Id;

            var Umodel = BMember.GetModel(memberId);
           // <img src="/images/user-tx.gif">

            if (!string.IsNullOrWhiteSpace(Umodel.MemberPhoto))
            {
                this.ltrPhoto.Text = "<img src=\"" + Umodel.MemberPhoto + "\" style=\"float:left; padding-right:10px; width:120px; height:120px;\">";
            }
            else
            {
                this.ltrPhoto.Text = "<img src=\"/images/user-tx.gif\">";
            }
            lblUserName.Text = Umodel.NickName;
            lblIntNo.Text = Umodel.IntegrationNumber.ToString();
           lblHonorNumber.Text = Umodel.HonorNumber.ToString();
         //   ltrhonorName.Text = GetTitle(Umodel.HonorNumber);
            lblCurrNo.Text = Umodel.CurrencyNumber.ToString("f2");
            lblTitle.Text = Umodel.Title;

            var Tmodel = BTeamMember.GetModel(memberId);
            if (Tmodel!=null)
            {
                lblTeam.Text = Tmodel.TeamName;
                lblPostion.Text = Tmodel.SQWZ;
            }

        }

        /// <summary>
        /// 根据荣誉取得会员头衔
        /// </summary>
        /// <param name="HonorNumber"></param>
        /// <returns></returns>
        //public static string GetTitle(int HonorNumber)
        //{
        //    string title = "<span>铁矿石</span><span><img src=\""  + "/WX/images/touxian/tx_01.png\"/></span>";
        //    if (HonorNumber >= 50 && HonorNumber < 200)
        //    {
        //        title = "<span>生铁</span><span><img src=\""  + "/WX/images/touxian/tx_02.png\"/></span>";
        //    }
        //    if (HonorNumber >= 200 && HonorNumber < 500)
        //    {
        //        title = "<span>熟铁</span><span><img src=\""  + "/WX/images/touxian/tx_03.png\"/></span>";
        //    }
        //    if (HonorNumber >= 500 && HonorNumber < 1000)
        //    {
        //        title = "<span>钢铁</span><span><img src=\""  + "/WX/images/touxian/tx_04.png\"/></span>";
        //    }
        //    if (HonorNumber >= 1000 && HonorNumber < 2000)
        //    {
        //        title = "<span>钨铁</span><span><img src=\""  + "/WX/images/touxian/tx_05.png\"/></span>";
        //    }
        //    if (HonorNumber >= 2000 && HonorNumber < 5000)
        //    {
        //        title = "<span>合金铁</span><span><imgsrc=\""  + "/WX/images/touxian/tx_06.png\"/></span>";
        //    }
        //    if (HonorNumber >= 5000)
        //    {
        //        title = "<span>超合金铁</span><span><img src=\""  + "/WX/images/touxian/tx_07.png\"/></span>";
        //    }
        //    //区域中月增加个人荣誉值最者取代前获取头衔人 = 外星铁
        //    return title;
        //}
    }
}