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

namespace Enow.TZB.Web.Manage.Vote
{
    public partial class VoteView : System.Web.UI.Page
    {
        public string Types = "投票";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var strtype=Utils.GetQueryStringValue("doType").ToLower();
                if (!string.IsNullOrEmpty(strtype))
                {
                    if (strtype == "getlottery")
                    {
                        Types="抽奖";
                    }
                }
                var id = Utils.GetQueryStringValue("id");
                Gettpxx(id);
            }
        }
        /// <summary>
        /// 投票选项
        /// </summary>
        private void Gettpxx(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var Optionlist = BVoteOption.GettypList(id);
                if (Optionlist.Count > 0)
                {
                    rptList.DataSource = Optionlist;
                    rptList.DataBind();
                }
            }

        }
    }
}