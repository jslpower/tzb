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
    public partial class JobSignUp : System.Web.UI.Page
    {
        #region 省市联动下拉框初始化
        protected int ProvinceId = -1;
        protected int CityId = -1;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                var MemeberModel = BMember.GetModelByOpenId(OpenId);
                if (MemeberModel != null)
                {
                    if (String.IsNullOrWhiteSpace(MemeberModel.ContactName))
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Default.aspx");
                        return;
                    }
                    BMember.StateCheck((Model.EnumType.会员状态)MemeberModel.State);
                    string Id = Utils.GetQueryStringValue("Id");
                    if (!String.IsNullOrWhiteSpace(Id))
                    {
                        if (BOffer.IsSignUp(Id, MemeberModel.Id))
                        {
                            MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(37), 2);
                        }
                        else
                        {
                            var model = BJob.GetModel(Id);
                            if (model != null)
                            {
                                if (model.StartDate > DateTime.Now)
                                {
                                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(38));
                                    return;
                                }
                                if (model.EndDate < DateTime.Now)
                                {
                                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(39));
                                    return;
                                }
                                this.ltrJobName.Text = model.JobName;
                            }
                            else
                            {
                                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(40));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(40));
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect("未找到您的用户请先注册", "/WX/Register/Default.aspx");
                    return;
                }
            }
        }
        /// <summary>
        /// 报名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkSave_Click(object sender, EventArgs e)
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (!String.IsNullOrWhiteSpace(Id))
            {
                var model = BJob.GetModel(Id);
                if (model != null)
                {
                    var AuthModel = BMemberAuth.GetUserModel();
                    string OpenId = AuthModel.OpenId;
                    string WorkYear = Utils.GetFormValue(txtWorkYear.UniqueID);
                    string BallYear = Utils.GetFormValue(txtBallYear.UniqueID);
                    string Specialty = Utils.GetFormValue(txtSpecialty.UniqueID);
                    string BMGY = Utils.GetFormValue(txtBMGY.UniqueID);
                    string ApplyInfo = Utils.GetFormValue(txtApplyInfo.UniqueID);
                    int CountryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID));
                    int ProvinceId = Utils.GetInt(Utils.GetFormValue(ddlProvince.UniqueID));
                    int CityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID));
                    int AreaId = Utils.GetInt(Utils.GetFormValue(ddlArea.UniqueID));
                    if (BMGY.Length<100)
                    {
                         MessageBox.ShowAndReturnBack("报名感言不能为空，并且汉字长度必须超过100汉字");
                        return;
                    }
                    if (ApplyInfo.Length<50)
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
                    var MemeberModel = BMember.GetModelByOpenId(OpenId);
                    if (MemeberModel != null)
                    {
                        if (!String.IsNullOrWhiteSpace(MemeberModel.ContactName))
                        {
                            BOffer.Add(new tbl_Offer
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                JobId = Id,
                                CountryId=CountryId,
                                CountryName=CountryName,
                                ProvinceId=ProvinceId,
                                ProvinceName=ProvinceName,
                                CityId=CityId,
                                CityName=CityName,
                                AreaId=AreaId,
                                AreaName=AreaName,
                                MemberId = MemeberModel.Id,
                                WorkYear = WorkYear,
                                BallYear = BallYear,
                                Specialty = Specialty,
                                BMGY = BMGY,
                                ApplyInfo = ApplyInfo,
                                OfferState = 0,
                                IssueTime = DateTime.Now
                            });
                            MessageBox.ShowAndRedirect("报名成功，您的报名信息已成功提交!", "/WX/Member/Default.aspx");
                            return;
                        }
                        else {
                            MessageBox.ShowAndRedirect("请先填写实名信息!", "/WX/Register/Step2.aspx");
                            return;
                        }
                    }
                    else {
                        MessageBox.ShowAndRedirect("未找到您的用户请先注册", "/WX/Register/Default.aspx");
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(40));
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(40));
            }
        }
    }
}