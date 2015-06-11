using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.Match
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
        /// 加载赛事列表
        /// </summary>
        private void InitList()
        {
            pageIndex = UtilsCommons.GetPadingIndex();
            this.Master.Page.Title = "联赛杯赛";
            MMatch searchModel = new MMatch();
            var list = BMatch.GetList(ref recordCount, pageSize, pageIndex, searchModel);
            if (list.Count>0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
        }

        /// <summary>
        /// 判断是否拥有赛事报名的权限
        /// </summary>
        /// <param name="MatchId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        protected string SignUp(string Id, DateTime StartDate, DateTime EndDate)
        {
            if (StartDate <= DateTime.Now && EndDate >= DateTime.Now)
            {
                return "<a href=\"SignUp.aspx?Id=" + Id + "\" class=\"yellowbg\">点击报名</a>";
            }
            else { return ""; }
        }
    }
}