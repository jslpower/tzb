using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.register
{
    public partial class Register : System.Web.UI.Page
    {
        #region 省市联动下拉框初始化
        protected int CountryId = 0, ProvinceID = 0, CityId = 0, AreaId = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetNoStore();
            if (!IsPostBack)
            {
              bool isLogin=  BWebMemberAuth.IsLoginCheck();
            // MessageBox.ShowAndRedirect("请搜索关注铁子帮微信公众号(TIEZIBANG)，并完成铁丝注册!", "/Default.aspx");
                if (isLogin)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(21), "/Default.aspx");
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            BMember bll = new BMember();
            string phone = Utils.GetFormValue(txt_phone.UniqueID);
            string valCode = Utils.GetFormValue(txtcode.UniqueID);
            //用户名只能使用手机号，注册后用户名不可修改
           // string UserName = Utils.GetFormValue("txt_name");
            string UserName = phone;
            string pwd = Utils.GetFormValue(txtpwd.UniqueID);
         //   string txtVc = Utils.GetFormValue("txtVC");
            int CountryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID));
            int ProvinceId = Utils.GetInt(Utils.GetFormValue(ddlProvince.UniqueID));
            int CityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID));
            int AreaId = Utils.GetInt(Utils.GetFormValue(ddlArea.UniqueID));
            string StrGender = Utils.GetFormValue("hidSex");
            string CountryName = "";
            BMember bllMember = new BMember();
            if (string.IsNullOrWhiteSpace(phone)||!StringValidate.IsNumber(phone))
            {
                MessageBox.ShowAndReturnBack("手机号码不能为空，并且只能输入数字");
                return;
            }
            if (bllMember.IsExistsPhone(phone))
            {
                MessageBox.ShowAndReturnBack("手机号码已存在!");
                return;
            }
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
            if (CountryId == 1 && ProvinceId != 190 && ProvinceId != 191 && ProvinceId != 988)
            {
                var model = BLL.BMemberVerCode.GetInfo(phone, valCode, 0);
                if (model == null || model.VerCode != valCode)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(22));
                    return;
                }

                if (model.IsUsed)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(23));
                    return;
                }
            }
            if (bll.IsExistsPhone(phone))
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(21));
                return;
            }
            if (bll.IsExistsName(UserName))
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(21));
                return;
            }
            HashCrypto has = new HashCrypto();
            string memberId = System.Guid.NewGuid().ToString();
            BMember.Add(new tbl_Member {
                Id = memberId,
                TypeId = (int)Model.EnumType.会员类型.网站,
                CountryId = CountryId,
                CountryName = CountryName,
                ProvinceId = ProvinceId,
                ProvinceName = ProvinceName,
                CityId = CityId,
                CityName = CityName,
                AreaId = AreaId,
                AreaName = AreaName,
                UserName = UserName,
                Gender = StrGender == "男" ? 1 : 0,
                Password = String.IsNullOrWhiteSpace(pwd) ? "" : has.MD5Encrypt(pwd),
                Title = "",
                ContactName = "",
                MobilePhone = phone,
                PersonalId = "",
                Address = "",
                Email="",
                State = (int)Model.EnumType.会员状态.审核中,
                LastModifyTime = DateTime.Now,
                CheckTime = DateTime.Now,
                CurrencyNumber = 0,
                IntegrationNumber = 0,
                HonorNumber = 0,
                IssueTime = DateTime.Now
            });
            #region 用户Id 写入Cookie
            HttpCookie cookie=new HttpCookie("MemberId");
            cookie.Values.Add("MemberId", memberId);
            #endregion
          //  Response.Redirect("RegSuccess.aspx", true);
            Response.Redirect("Step2.aspx?Id=" + memberId + "", true);
            return;
        }
    }
}