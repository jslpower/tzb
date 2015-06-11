using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.Job
{
    public partial class JobDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                if (BWebMemberAuth.IsLoginCheck() == false) {
                    MessageBox.ShowAndRedirect("请先登陆!", "/");
                }
                else
                {
                    InitPage();
                }
                
            }

        }

        #region 加载招聘信息
        protected void InitPage()
        {
            string id = Utils.GetQueryStringValue("id");
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.ShowAndReturnBack("招聘信息未找到");
                return;
            }

            var model = BJob.GetViewModel(id);
            if (model!=null)
            {
                if (model.StartDate > DateTime.Now)
                {
                    this.phSignUp.Visible = false;
                }
                if (model.EndDate < DateTime.Now)
                {
                    this.phSignUp.Visible = false;
                }
                lblJobName.Text = model.JobName;
                lblArea.Text = model.CoutryName + "-" + model.ProvinceName + "-" + model.CityName;
                lblDate.Text = model.StartDate.ToString("yyyy-MM-dd") + "&nbsp;至&nbsp;" + model.EndDate.ToString("yyyy-MM-dd");
                lblNum.Text = model.JobNumber;
                ltrJobInfo.Text = model.JobInfo;
                ltrJobrule.Text = model.JobRule;
            }
        }
        #endregion


        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string id = Utils.GetQueryStringValue("id");
            #region 取值
            var model = BWebMemberAuth.GetUserModel();
            string MemberId = model.Id;
            string JobYear = Utils.GetFormValue(txtJobYear.UniqueID);
            string BallYear = Utils.GetFormValue(txtBallYear.UniqueID);
            string Specialty = Utils.GetFormValue(txtSpecialty.UniqueID);
            string bmgy = Utils.GetFormValue(txtBMGY.UniqueID);
            string applyInfo = Utils.GetFormValue(txtApplyInfo.UniqueID);
            int CountryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID));
            int ProvinceId = Utils.GetInt(Utils.GetFormValue(ddlProvince.UniqueID));
            int CityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID));
            int AreaId = Utils.GetInt(Utils.GetFormValue(ddlArea.UniqueID));
            if (String.IsNullOrWhiteSpace(bmgy))
            {
                MessageBox.ShowAndReturnBack("报名感言不能为空");
                return;
            }
            if (applyInfo.Length < 50)
            {
                MessageBox.ShowAndReturnBack("个人简历不能为空，并且汉字长度必须超过50汉字");
                return;
            }
            string CountryName = "";
            var CountryModel = BMSysProvince.GetCountryModel(CountryId);
            if (CountryId > 0 && CountryModel != null)
            {
                CountryName = CountryModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(50));
                return;
            }
            string ProvinceName = "";
            var ProvinceModel = BMSysProvince.GetProvinceModel(ProvinceId);
            if (ProvinceId > 0 && ProvinceModel != null)
            {
                ProvinceName = ProvinceModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(51));
                return;
            }
            string CityName = "";
            var CityModel = BMSysProvince.GetCityModel(CityId);
            if (CityId > 0 && CityModel != null)
            {
                CityName = CityModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(52));
                return;
            }
            string AreaName = "";
            var AreaModel = BMSysProvince.GetAreaModel(AreaId);
            if (AreaId > 0 && AreaModel != null)
            {
                AreaName = AreaModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(53));
                return;
            }
            #endregion

            BOffer.Add(new tbl_Offer
            {
                Id = Guid.NewGuid().ToString(),
                JobId = id,
                CountryId = CountryId,
                CountryName = CountryName,
                ProvinceId = ProvinceId,
                ProvinceName = ProvinceName,
                CityId = CityId,
                CityName = CityName,
                AreaId = AreaId,
                AreaName = AreaName,
                MemberId = MemberId,
                WorkYear = JobYear,
                BallYear = BallYear,
                Specialty = Specialty,
                BMGY = bmgy,
                ApplyInfo = applyInfo,
                OfferState = 0,
                IssueTime = DateTime.Now

            });

            Response.Redirect("RegSuccess.aspx", true);
            return;
        }
    }
}