using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.Team
{
    public partial class Default : System.Web.UI.Page
    {
        #region 页面变量
        protected int pageSize = 4, pageIndex = 1, recordCount = 0;
        protected string PageTitle = "";
        //图片裁剪后保存的文件夹
        protected const string DIRPATH = "/ufiles/";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitList();
               
            }
        }

        /// <summary>
        /// 加载球队列表信息
        /// </summary>
        private void InitList()
        {
            this.Master.Page.Title = "一起玩吧-铁丝球队";
            MBallTeamSearch searchModel = new MBallTeamSearch();
            searchModel.State = Model.EnumType.球队审核状态.终审通过;
            pageIndex = UtilsCommons.GetPadingIndex();
            var list = BTeam.GetList(ref recordCount, pageSize, pageIndex, searchModel);
            if (recordCount > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
        }
        /// <summary>
        /// 用户如果已加入或创建球队，则不显示申请加入或者创建球队
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitOperation(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = (tbl_BallTeam)e.Item.DataItem;
                //Literal ltrOperation = (Literal)e.Item.FindControl("ltrOperation");
                PlaceHolder PhCreateJoin = (PlaceHolder)e.Item.FindControl("PhCreateJoin");
                if (BWebMemberAuth.IsLoginCheck())
                {
                    var model = BWebMemberAuth.GetUserModel();
                    var TeamModel = BTeamMember.GetModel(model.Id);
                    if (TeamModel != null)
                    {
                        PhCreateJoin.Visible = false;
                    }
                    else
                    {
                        PhCreateJoin.Visible = true;
                    }
                }
            }
        }

        //protected string TeamJoin(string Id)
        //{
        //    string str = "";
        //    if (BWebMemberAuth.IsLoginCheck())
        //    {
        //        var model = BWebMemberAuth.GetUserModel();
        //        var TeamModel = BTeamMember.GetModel(model.Id);
        //        if (TeamModel != null)
        //        {
        //            str = "";
        //        }
        //        else
        //        {
        //            str = " <a href=\"SignUp.aspx?id=" + Id + "\" class=\"greenbg\">申请加入</a> <a href=\"CreateTeam.aspx\" class=\"yellowbg\">创建球队</a>";
        //        }
        //    }
        //    return str;
        //}

    }
}