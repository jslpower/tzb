using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using System.Text;

namespace Enow.TZB.Web.Manage.Job
{
    public partial class JobEdit : System.Web.UI.Page
    {
        protected int CountryId = 0;
        protected int ShengFenId = 0;
        protected int ChengShiId = 0;
        protected int AreaId = 0;
        protected string doType = "";
        protected string Id = "";
        protected string JobType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            doType = Utils.GetQueryStringValue("doType");
            Id = Utils.GetQueryStringValue("id");
            if (doType == "update" && !string.IsNullOrEmpty(Id))
            {
                if (!IsPostBack)
                {
                    ManageUserAuth.ManageLoginCheck();
                    InitPage(Id);
                }
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="id"></param>
        private void InitPage(string id)
        {
            var model = BJob.GetModel(id);
            if (model != null)
            {
                txtJobName.Text = model.JobName;
                ShengFenId = model.ProvinceId;
                ChengShiId = model.CityId;
                txtStartDate.Text = model.StartDate.ToString("yyyy-MM-dd");
                txtEndDate.Text = model.EndDate.ToString("yyyy-MM-dd");
                txtNumber.Text = model.JobNumber;
                txtJobRule.Text = model.JobRule;
                txtJobInfo.Text = model.JobInfo;
                txtJobReward.Text = model.JobReward;
                JobType = model.JobType.ToString();
                CountryId = model.CoutryId;
                AreaId = model.AreaId;
            }
            else { 
                
            }
        }

       

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            int JobType = Utils.GetInt(Utils.GetFormValue("ddlJobType"));
            if (!StringValidate.IsDateTime(txtStartDate.Text))
            {
                strErr = "开始时间格式不正确!";
            }
            if (!StringValidate.IsDateTime(txtEndDate.Text))
            {
                strErr = "结束时间不正确!";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("selCountry")))
            {
                strErr = "请选择国家!";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("txtShengFenId")))
            {
                strErr = "请选择省份!";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("txtChengShiId")))
            {
                strErr = "请选择城市!";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("selArea")))
            {
                strErr = "请选择区县!";
            }
            if (!StringValidate.IsInteger(txtNumber.Text))
            {
                strErr = "请填写正确的招聘人数!";
            }
            if (JobType < 1)
            {
                strErr = "请选择招聘类别!";
            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }
            //获取当前登录用户的信息
            var userModel = ManageUserAuth.GetManageUserModel();
            //新增招聘信息
            if (doType.ToLower() == "add")
            {
                tbl_Job model = new tbl_Job();
                model.Id = Guid.NewGuid().ToString();
                model.OperatorId = userModel.Id;
                model.ContactName = userModel.ContactName;
                model.TypeId = Utils.GetInt(Utils.GetFormValue(ddlTypeId.UniqueID));
                model.JobName = txtJobName.Text.Trim();
                model.CoutryId =Utils.GetInt(Utils.GetFormValue("selCountry"));
                model.ProvinceId = Utils.GetInt(Utils.GetFormValue("txtShengFenId"));
                model.CityId = Utils.GetInt(Utils.GetFormValue("txtChengShiId"));
                model.AreaId = Utils.GetInt(Utils.GetFormValue("selArea"));
                model.StartDate = Convert.ToDateTime(Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"));
                model.EndDate = Convert.ToDateTime(Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd"));
                model.JobNumber = txtNumber.Text;
                model.JobRule = txtJobRule.Text;
                model.JobInfo = txtJobInfo.Text;
                model.JobReward =this.txtJobReward.Text;
                model.IsValid = true;
                model.IssueTime = DateTime.Now;
                model.JobType = JobType;
                model.IsDelete = 0;

                BJob.Add(model);

                MessageBox.ShowAndParentReload("新增成功！");
                return;

            }
            //更新招聘信息
            else if (doType.ToLower() == "update")
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    tbl_Job model = new tbl_Job();
                    model.Id = Id;
                    model.OperatorId = userModel.Id;
                    model.ContactName = userModel.ContactName;
                    model.TypeId = Utils.GetInt(Utils.GetFormValue(ddlTypeId.UniqueID));
                    model.JobName = txtJobName.Text.Trim();
                    model.CoutryId = Utils.GetInt(Utils.GetFormValue("selCountry"));
                    model.ProvinceId = Utils.GetInt(Utils.GetFormValue("txtShengFenId"));
                    model.CityId = Utils.GetInt(Utils.GetFormValue("txtChengShiId"));
                    model.AreaId = Utils.GetInt(Utils.GetFormValue("selArea"));
                    model.StartDate = Convert.ToDateTime(Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"));
                    model.EndDate = Convert.ToDateTime(Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd"));
                    model.JobNumber = txtNumber.Text;
                    model.JobRule = txtJobRule.Text;
                    model.JobInfo = txtJobInfo.Text;
                    model.JobReward = this.txtJobReward.Text;
                    model.IsValid = true;
                    model.IssueTime = DateTime.Now;
                    model.JobType = JobType;
                    model.IsDelete = 0;
                    bool IsSucess = BJob.Update(model);
                    if (IsSucess)
                    {
                        MessageBox.ShowAndParentReload("修改成功！");
                    }
                    else
                    {
                        MessageBox.ShowAndReturnBack("修改失败！");
                        return;
                    }
                }
            }
        }
    }
}