using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.Model;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Team
{
    /// <summary>
    /// 球场地图显示
    /// </summary>
    public partial class BallMapView : System.Web.UI.Page
    {
        protected string Latitude = "", Longitude = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                string Id = Utils.GetQueryStringValue("Id");
                if (!String.IsNullOrWhiteSpace(Id))
                {
                    InitDetail(Id);
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
        }
        /// <summary>
        /// 加载信息
        /// </summary>
        /// <param name="Id"></param>
        private void InitDetail(string Id)
        {
            var model = BBallField.GetModel(Id);
            if (model != null)
            {
                Latitude = model.Latitude;
                Longitude = model.Longitude;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
    }
}