using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.SMS;
using Enow.TZB.Utility;
using Enow.TZB.BLL;


namespace Enow.TZB.Web.WX.Register
{
    /// <summary>
    /// 会员注册
    /// </summary>
    public partial class Step1 : System.Web.UI.Page
    {
        #region 省市联动下拉框初始化
        protected int ProvinceId = -1;
        protected int CityId = -1;
        protected string SmsCode = "";
        #endregion
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var model = BMemberApp.GetUserModel();
                if (model != null)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(20), "/WX/Member/Default.aspx");
                    return;
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
            this.btnSave.Enabled = false;
            string phone = Utils.GetFormValue("txt_phone");
            string valCode = Utils.GetFormValue("txtcode");
            int CountryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID));
            int ProvinceId = Utils.GetInt(Utils.GetFormValue(ddlProvince.UniqueID));
            int CityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID));
            int AreaId = Utils.GetInt(Utils.GetFormValue(ddlArea.UniqueID));
            string CountryName = "";
            var CountryModel = BMSysProvince.GetCountryModel(CountryId);
            BMember bllMember = new BMember();
            if (string.IsNullOrWhiteSpace(phone) || !StringValidate.IsNumber(phone))
            {
                MessageBox.ShowAndReturnBack("手机号码不能为空，并且只能输入数字");
                return;
            }
            if (bllMember.IsExistsPhone(phone))
            {
                MessageBox.ShowAndReturnBack("手机号码已存在!");
                return;
            }
            if (CountryId > 0 && CountryModel != null)
            {
                CountryName = CountryModel.Name;
            }
               
            else {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(50));
                return;
            }
            string ProvinceName = "";
            var ProvinceModel = BMSysProvince.GetProvinceModel(ProvinceId);
            if (ProvinceId>0 && ProvinceModel != null)
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
            if (CityId>0 && CityModel != null)
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
            if (AreaId>0 && AreaModel != null)
            {
                AreaName = AreaModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(53));
                return;
            }

            string strpwd = Utils.GetFormValue("txtpwd");
            string email = Utils.GetFormValue("txtemail");
            string StrGender = Utils.GetFormValue("hidSex");
            BLL.BMember bll = new BLL.BMember();
            if (bll.IsExistsPhone(phone))
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(21));
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
            else
            {
                /*
                //发送验证邮件
                string MailSendUserName = System.Configuration.ConfigurationManager.AppSettings["MailSendUserName"];
                string MailSendPassword = System.Configuration.ConfigurationManager.AppSettings["MailSendPassword"];
                string MailTemplate = System.Configuration.ConfigurationManager.AppSettings["MailTemplate"];

                string VerUrl = "http://" + Request.Url.Host + "/WX/Register/EmailVerCheck.aspx?VerCode=";
                 * */
            }
            HashCrypto has = new HashCrypto();
            string MemberId = Guid.NewGuid().ToString();
            string NickName="",MemberPhoto = "";
            //添加微信信息
            //取得微信用户信息
            //tbl_MemberWeiXin MemberWeiXinModel = new tbl_MemberWeiXin();
            //var WeiXinModel = BWeiXin.GetUserInfo(OpenId);
            //if (WeiXinModel != null)
            //{
            //    NickName = WeiXinModel.NickName;
            //    MemberPhoto = WeiXinModel.HeadImgUrl.Replace("\\/", "/");
            //    MemberWeiXinModel.Id = System.Guid.NewGuid().ToString();
            //    MemberWeiXinModel.MemberId = MemberId;
            //    MemberWeiXinModel.OpenId = OpenId;
            //    MemberWeiXinModel.NickName = WeiXinModel.NickName;
            //    MemberWeiXinModel.HeadPhoto = WeiXinModel.HeadImgUrl.Replace("\\/", "/");
            //    MemberWeiXinModel.IssueTime = DateTime.Now;
            //}
            //else
            //{
            //    MemberWeiXinModel.Id = System.Guid.NewGuid().ToString();
            //    MemberWeiXinModel.MemberId = MemberId;
            //    MemberWeiXinModel.OpenId = OpenId;
            //    MemberWeiXinModel.NickName = "";
            //    MemberWeiXinModel.HeadPhoto = "";
            //    MemberWeiXinModel.IssueTime = DateTime.Now;
            //}
            //添加会员信息
            BMember.Add(new BLL.tbl_Member
            {
                Id = MemberId,
                TypeId = (int)Model.EnumType.会员类型.微信,
                CountryId = CountryId,
                CountryName = CountryName,
                ProvinceId = ProvinceId,
                ProvinceName = ProvinceName,
                CityId = CityId,
                CityName = CityName,
                AreaId = AreaId,
                AreaName = AreaName,
                NickName = NickName,
                MemberPhoto = MemberPhoto,
                UserName = phone,
                Gender = StrGender == "男" ? 1 : 0,
                Password = String.IsNullOrWhiteSpace(strpwd) ? "" : has.MD5Encrypt(strpwd),
                Title = "",
                ContactName = "",
                MobilePhone = phone,
                Email = email,
                PersonalId = "",
                Address = "",
                State = (int)Model.EnumType.会员状态.审核中,
                LastModifyTime = DateTime.Now,
                CheckTime = DateTime.Now,
                CurrencyNumber = 0,
                IntegrationNumber = 0,
                HonorNumber = 0,
                OpenId = "",
                IssueTime = DateTime.Now
            });
            //添加微信信息
            //BMemberWeiXin.Add(MemberWeiXinModel);
            BMemberApp.UserLogin(phone, has.MD5Encrypt(strpwd));
            MessageBox.ShowAndRedirect("注册成功！请填写实名信息！", "/WX/Member/Step2.aspx");
            return;
        }
    }
}