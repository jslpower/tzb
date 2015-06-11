using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.TZB.SMS;
using Enow.TZB.Utility;
using Enow.TZB.BLL;


namespace Enow.TZB.Web.Ashx
{
    /// <summary>
    /// ValidCode 的摘要说明
    /// </summary>
    public class ValidCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string dotype = Utils.GetQueryStringValue("dotype");

            switch (dotype)
            {

                case "getCode": GetZhuCeYanZhengMa(); break;
                case "Register": ZhuCe(); break;

                default: break;
            }
        }

        /// <summary>
        /// 获取注册验证码
        /// </summary>
        void GetZhuCeYanZhengMa()
        {
            BLL.BMember bll=new BLL.BMember();
            BLL.BMemberVerCode bllcode = new BLL.BMemberVerCode();
            string mobile = Utils.GetFormValue("txt_phone");
            ///防止手机号码多次注册
            if (bll.IsExistsPhone(mobile))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", CacheSysMsg.GetMsg(24)));
            }
            ///获取6位随机验证码
            string ValidCode = MakeValidCode();
            BLL.BMemberVerCode.Add(new BLL.tbl_MemberVerCode {
            Id=Guid.NewGuid().ToString(),
            MobilePhone=mobile,
            VerCode=ValidCode,
            IsUsed=false,
            IssueTime=DateTime.Now
            });
            //发送验证码至手机
            string v = BSMS.VerCodeSend(mobile, ValidCode);
              Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "",ValidCode));
        }

        /// <summary>
        /// 注册
        /// </summary>
        void ZhuCe()
        {
            BLL.BMember bll = new BLL.BMember();
            string phone = Utils.GetFormValue("txt_phone");

            string valCode = Utils.GetFormValue("txtcode");
            string OpenId = Utils.GetQueryStringValue("OpenId");
            int CountryId = Utils.GetInt(Utils.GetFormValue("ddlCountry"));
            int ProvinceId = Utils.GetInt(Utils.GetFormValue("ddlProvince"));
            int CityId = Utils.GetInt(Utils.GetFormValue("ddlCity"));
            string strpwd = Utils.GetFormValue("txtpwd");
            string email = Utils.GetFormValue("txtemail");
            string StrGender = Utils.GetFormValue("hGender");
            var model = BLL.BMemberVerCode.GetInfo(phone, valCode, 0);
            if (model == null || model.VerCode != valCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", CacheSysMsg.GetMsg(22)));

            if (model.IsUsed)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-2", CacheSysMsg.GetMsg(23)));
            }



            if (bll.IsExistsPhone(phone))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-3", CacheSysMsg.GetMsg(24)));
            }
            HashCrypto has = new HashCrypto();
            string MemberId = Guid.NewGuid().ToString();
            BMember.Add(new BLL.tbl_Member
            {
                Id = MemberId,
                TypeId = (int)Model.EnumType.会员类型.微信,
                CountryId = CountryId,
                CountryName = BLL.BMSysProvince.GetCountryModel(CountryId).Name,
                ProvinceId = ProvinceId,
                ProvinceName = BLL.BMSysProvince.GetProvinceModel(ProvinceId).Name,
                CityId = CityId,
                CityName = BLL.BMSysProvince.GetCityModel(CityId).Name,
                UserName = phone,
                Gender = StrGender == "男" ? 1 : 0,
                Password = has.MD5Encrypt(strpwd),
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
                IssueTime = DateTime.Now
            });
            //添加微信信息
            //取得微信用户信息
            var WeiXinModel = BWeiXin.GetUserInfo(OpenId);
            if (WeiXinModel != null)
            {
                BMemberWeiXin.Add(new tbl_MemberWeiXin
                {
                    Id = System.Guid.NewGuid().ToString(),
                    MemberId = MemberId,
                    OpenId = OpenId,
                    NickName = WeiXinModel.NickName,
                    HeadPhoto = WeiXinModel.HeadImgUrl,
                    IssueTime = DateTime.Now
                });
            }
            else {
                BMemberWeiXin.Add(new tbl_MemberWeiXin
                {
                    Id = System.Guid.NewGuid().ToString(),
                    MemberId = MemberId,
                    OpenId = OpenId,
                    NickName = "",
                    HeadPhoto = "",
                    IssueTime = DateTime.Now
                });
            }
        }


        /// <summary>
        /// 生成6位数字随机验证码
        /// </summary>
        /// <returns></returns>
        protected static string MakeValidCode()
        {
            char[] str = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string num = "";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                num += str[r.Next(0, str.Length)].ToString();
            }

            return num;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}