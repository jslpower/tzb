using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Job
{
    public partial class jobView : System.Web.UI.Page
    {
        protected string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utils.GetQueryStringValue("id");
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitPage(id);
            }

        }

        protected void InitPage(string Id)
        {
          
            if (!string.IsNullOrEmpty(id))
            {
                var model = BJob.GetViewModel(id);
                if (model!=null)
                {
                    lblJobName.Text = model.JobName;
                    lblArea.Text = model.CoutryName + "-" + model.ProvinceName + "-" + model.CityName + "-" + model.AreaName;
                    lblStartDate.Text = model.StartDate.ToString("yyyy-MM-dd");
                    lblEndDate.Text = model.EndDate.ToString("yyyy-MM-dd");
                    lblNum.Text = model.JobNumber.ToString();
                    lblJobType.Text=((Model.EnumType.JobType)model.JobType).ToString();
                    ltrJobRule.Text = model.JobRule;
                    ltrJonInfo.Text = model.JobInfo;
                    ltrJobReward.Text = model.JobReward;
                    lblIsValid.Text = (bool)model.IsValid ? "是" : "否";
                }
            }
        }
    }
}