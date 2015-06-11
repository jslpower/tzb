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


namespace Enow.TZB.Web.WX.Member
{
    public partial class TZGatheringDetail : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        /// <summary>
        /// 标题
        /// </summary>
        public string ApTitle = "";
        /// <summary>
        /// 分类
        /// </summary>
        public string types = "";
        /// <summary>
        /// 编号
        /// </summary>
        public string Acid = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            var action = Utils.GetQueryStringValue("ation");
            switch (action)
            {
                case "inter":
                    AddActivity();
                    break;
                case "deletc":
                    DeleteActivity();
                    break;
            }
            Getusermodel();
            if (!IsPostBack)
            {
                var id = Utils.GetInt(Utils.GetQueryStringValue("ActId"), 0);
                InitModel(id);
            }
            
        }
        /// <summary>
        /// 跳转
        /// </summary>
        private void Urltz()
        {
            Response.Redirect("TZGathering.aspx?type=1");
        }
        /// <summary>
        /// 获取聚会实体
        /// </summary>
        private void InitModel(int id)
        {
            tbl_Activity actmodel = BActivity.GetModel(id);
            if (actmodel != null && actmodel.IsDelete==0)
            {
                Acid = actmodel.Id.ToString();
                types = actmodel.Activitytypes.ToString();
                ApTitle = ((Enow.TZB.Model.EnumType.ActivityEnum)(actmodel.Activitytypes)).ToString();
                UserHome1.Userhometitle = ApTitle;
                litgl.Text = actmodel.Starname;
                littitle.Text = actmodel.Title;
                littime.Text = actmodel.StartDatetime.ToString("yyyy-MM-dd");
                if (actmodel.Activitytypes==2)
                {
                    litqcname.Text = "<div>球场：" + actmodel.SiteName + "</div>";
                }
                
                litquyu.Text = actmodel.Countryname + actmodel.Provincename + actmodel.Cityname + actmodel.Areaname;
                litdizhi.Text = actmodel.Address;
                litfeiyong.Text = actmodel.CostNum;
                litneirong.Text = actmodel.ActivityTxt;
            }
            else
            {
                Urltz();
            }
        }
        /// <summary>
        /// 判断是否登录
        /// </summary>
        private void Getusermodel()
        {
            BMemberAuth.LoginCheck();
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            InitMember(OpenId);
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember(string OpenId)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        private void DeleteActivity()
        {
            var id = Utils.GetInt(Utils.GetQueryStringValue("ActId"), 0);
            if (id == 0)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "ID错误请刷新后重试！"));
            }
            var actmodel = BActivity.GetModel(id);
            var Muser = GetUser();
            if (Muser == null)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请先登录！"));
            }
            tbl_Applicants tbapp = new tbl_Applicants();
            tbapp.Id = id;
            tbapp.Usid = Muser.Id;
            bool retbol=BApplicants.Deletemodel(tbapp);
            if (retbol)
            {
                 Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "取消成功！"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "取消失败！"));
            }
        }
        /// <summary>
        /// 添加报名信息
        /// </summary>
        private void AddActivity()
        {
            var id = Utils.GetInt(Utils.GetQueryStringValue("ActId"), 0);
            if (id == 0)
            {
               Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "ID错误请刷新后重试！"));
            }
            var actmodel = BActivity.GetModel(id);
            var Muser = GetUser();
            if (Muser == null)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请先登录！"));
            }
            if (actmodel != null && actmodel.IsDelete == 0 && DateTime.Now<=actmodel.Redatetime)
            {
                bool tzjhbol = false;
                if (actmodel.Cityyuzhan == 1)
                {
                    if (Muser.CountryId == actmodel.CountryId && Muser.ProvinceId == actmodel.ProvinceId && Muser.CityId == actmodel.CityId)
                    {
                      tzjhbol = true;
                    }
                }
                else
                {
                    tzjhbol = true;
                }
                if (tzjhbol == false)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "此聚会只支持同城报名！"));
                }
                var applic = new tbl_Applicants();
                applic.IsDelete = 0;
                applic.IsState = 0;
                tbl_Applicants retbol = BApplicants.GetUsbool(actmodel.Id.ToString(), Muser.Id);
                if (retbol != null)
                {
                    if (retbol.IsDelete == 0)
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "您已参加此聚会请勿重复报名！"));
                    }
                    else
                    {
                        applic.Id = retbol.Id;
                        applic.Indatetime = DateTime.Now;
                        bool upbol= BApplicants.Update(applic);
                        
                        Utils.RCWE(UtilsCommons.AjaxReturnJson(upbol?"1":"0",upbol?"报名成功！":"报名失败！"));
                    }
                }
                else
                {

                    applic.ActivityID = actmodel.Id.ToString();
                    applic.Usid = Muser.Id;
                    applic.Indatetime = DateTime.Now;
                    BApplicants.Add(applic);
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "报名成功！"));
                }


            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "报名已终止！"));
            }
        }

        /// <summary>
        /// 获取登录用户实体
        /// </summary>
        /// <returns></returns>
        private tbl_Member GetUser()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel != null)
            {
                string OpenId = AuthModel.OpenId;
                return BMember.GetModelByOpenId(OpenId);
            }
            return null;
        }
    }
}