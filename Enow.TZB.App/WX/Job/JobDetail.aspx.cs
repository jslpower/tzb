using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.Model;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Job
{
    public partial class JobDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberApp.LoginCheck();
                string Id = Utils.GetQueryStringValue("Id");
                if (!String.IsNullOrWhiteSpace(Id))
                {
                    var model = BJob.GetViewModel(Id);
                    if (model != null)
                    {
                        this.ltrJobName.Text = model.JobName;
                        this.ltrCity.Text = model.ProvinceName + "-" + model.CityName;
                        this.ltrDate.Text = model.StartDate.ToString("yyyy-MM-dd") + " - " + model.EndDate.ToString("yyyy-MM-dd");
                        this.ltrJobNumber.Text = model.JobNumber;
                        this.ltrJobRule.Text = model.JobRule;
                        this.ltrJobInfo.Text = model.JobInfo;
                        this.ltrJobReward.Text = model.JobReward;
                        if (model.TypeId == (int)Model.EnumType.招聘类型.招聘)
                            this.ltrRewardTitle.Text = "岗位支持";
                        else
                            this.ltrRewardTitle.Text = "岗位奖励";
                    }
                    else
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                    }
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
        }
    }
}