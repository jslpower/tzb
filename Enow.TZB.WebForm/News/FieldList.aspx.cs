using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.News
{
    public partial class FieldList : System.Web.UI.Page
    {
        #region 页面变量
        protected int pageSize = 12, pageIndex = 1, recordCount = 0;
        protected string PageTitle = "";
        protected int ClassId = 0;
        protected int HmId = 0;
        protected int ImgCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
           
           
            HmId = Utils.GetInt(Utils.GetQueryStringValue("HmId"), 0);
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(ClassId.ToString()))
                {
                    InitList();
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
           
                

        }

        /// <summary>
        /// 加载列表
        /// </summary>
        /// <param name="ClassId"></param>
        private void InitList()
        {
            this.Master.Page.Title = "铁丝网";
            Model.MBallFieldSearch SearchModel = new Model.MBallFieldSearch();
            pageIndex = UtilsCommons.GetPadingIndex();
            var list = BBallField.GetList(ref recordCount, pageSize, pageIndex, SearchModel);
            if (recordCount> 0)
            {
                this.rpt_FieldList.DataSource = list;
                rpt_FieldList.DataBind();
            }
        }
    }
}