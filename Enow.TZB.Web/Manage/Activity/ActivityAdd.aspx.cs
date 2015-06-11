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

namespace Enow.TZB.Web.Manage.Activity
{
    public partial class ActivityAdd : System.Web.UI.Page
    {
        public string changdilist = "null";
        public string stime = "";
        public string endtime = "";
        public string context = "";
        protected string CId = "", PId = "", CSId = "", AId = "",SiteID="";
        protected void Page_Load(object sender, EventArgs e)
        {
            Getchangdi();
            if (!IsPostBack)
            {
                Getfabuzhi();
                GetModel();
                
            }
        }
        private void Getchangdi()
        {
            var qclist = BBallField.GetFieldList();
            if (qclist.Count>0)
            {
                changdilist = JsonConvert.SerializeObject(qclist);
            }
        }
        private void Getfabuzhi()
        {
            ManageUserAuth.ManageLoginCheck();
            //发布至
            var fblist = EnumObj.GetList(typeof(Model.EnumType.ReleaseEnum));
            dropfbmb.DataSource = fblist;
            dropfbmb.DataBind();
            //活动类型
            var hdlist = EnumObj.GetList(typeof(Model.EnumType.ActivityEnum));
            hdlist.RemoveAt(0);
            droptypes.DataSource = hdlist;
            droptypes.DataBind();
        }
        /// <summary>
        /// 修改
        /// </summary>
        private void GetModel()
        {
            
            var id = Utils.GetInt(Utils.GetQueryStringValue("id"),0);
            if (id!=0)
            {
               tbl_Activity actmodel= BActivity.GetModel(id);
               if (actmodel!=null)
               {
                   droptypes.SelectedValue = actmodel.Activitytypes.ToString();
                   dropfbmb.SelectedValue = actmodel.Release.ToString();
                   txtqx.Text = actmodel.Starname;//高亮
                   txtfeiyong.Text = actmodel.CostNum;//费用
                   txtMatchName.Text = actmodel.Title;//标题
                   txtAddress.Text = actmodel.Address;//聚会地址
                   stime = actmodel.StartDatetime.ToString("yyyy-MM-dd");//聚会日期
                   endtime = actmodel.Redatetime.ToString("yyyy-MM-dd");//报名截止日期
                   txtContent.Text = actmodel.ActivityTxt;//聚会内容
                   CId = actmodel.CountryId.ToString();
                   PId = actmodel.ProvinceId.ToString();
                   CSId = actmodel.CityId.ToString();
                   AId = actmodel.AreaId.ToString();
                   SiteID = actmodel.SiteID.ToString();
                   RadioButtonList1.SelectedValue = actmodel.Cityyuzhan.ToString();
               }
            }
        }
       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ManagerList model= ManageUserAuth.GetManageUserModel();
            var id = Utils.GetInt(Utils.GetQueryStringValue("id"), 0);
            tbl_Activity actmodel = new tbl_Activity();
            var didianid = GetStringArray(Utils.GetFormValue(hdfgssq.UniqueID), ",");
            string strErr = "";
            if (didianid.Count==4)
            {
                actmodel.CountryId = Utils.GetInt(didianid[0],0);
                actmodel.ProvinceId = Utils.GetInt(didianid[1], 0);
                actmodel.CityId = Utils.GetInt(didianid[2], 0);
                actmodel.AreaId = Utils.GetInt(didianid[3], 0);
                var CountryModel = BMSysProvince.GetCountryModel(actmodel.CountryId);
                if (CountryModel != null)
                {
                    actmodel.Countryname = CountryModel.Name;
                }
                else
                {
                    strErr += "请选择国家！\n";
                }
                var ProModel = BMSysProvince.GetProvinceModel(actmodel.ProvinceId);
                if (ProModel != null)
                {
                    actmodel.Provincename = ProModel.Name;
                }
                else
                {
                    strErr += "请选择省份！\n";
                }
                var CityModel = BMSysProvince.GetCityModel(actmodel.CityId);
                if (CityModel != null)
                {
                    actmodel.Cityname = CityModel.Name;
                }
                else
                {
                    strErr += "请选择城市！\n";
                }
                var AreaModel = BMSysProvince.GetAreaModel(actmodel.AreaId);
                if (AreaModel != null)
                {
                    actmodel.Areaname = AreaModel.Name;
                }
                else
                {
                    strErr += "请选择区县！\n";
                }  
            }
            else
            {
                strErr += "请选择国家地区！";
            }
            if (!string.IsNullOrEmpty(strErr))
            {
                MessageBox.Show(strErr);
                return;
            }
            actmodel.Activitytypes = Utils.GetInt(Utils.GetFormEditorValue(droptypes.UniqueID),1);
            if (actmodel.Activitytypes<=2)
            {
                actmodel.SiteID = Utils.GetFormEditorValue(hdfcdID.UniqueID);
                var changdimodel = BBallField.GetModel(actmodel.SiteID);
                if (changdimodel != null)
                    actmodel.SiteName = changdimodel.FieldName;
            }
            actmodel.Cityyuzhan = Utils.GetInt(Utils.GetFormValue(RadioButtonList1.UniqueID), 0);
            actmodel.Starname =Utils.GetText(Utils.GetFormValue(txtqx.UniqueID),250);
            actmodel.Title =Utils.GetText(Utils.GetFormValue(txtMatchName.UniqueID),250);
            actmodel.Address = Utils.GetText(Utils.GetFormValue(txtAddress.UniqueID), 250);
            actmodel.CostNum = Utils.GetText(Utils.GetFormValue(txtfeiyong.UniqueID), 250);
            actmodel.Release = Utils.GetInt(Utils.GetFormValue(dropfbmb.UniqueID), 1);
            actmodel.Rsdatetime =DateTime.Now;
            actmodel.Redatetime = Utils.GetDateTime(Utils.GetFormValue(hdfjztime.UniqueID));
            actmodel.StartDatetime = Utils.GetDateTime(Utils.GetFormValue(hdfsttime.UniqueID));
            actmodel.EndDatetime = DateTime.Now;
            actmodel.ActivityTxt = Utils.EditInputText(Request.Form[txtContent.UniqueID]);
            actmodel.Condition = "";
            actmodel.Innumber = "0";
            actmodel.IsDelete = 0;
            actmodel.UpDatetime = DateTime.Now;
            actmodel.UpUserId = model!=null?model.Id.ToString():"";
            actmodel.UpUserName = model!=null?model.ContactName:"";
            bool IsReturn = false;
            if (id!=0)
            {
                actmodel.Id = id;
               IsReturn= BActivity.Update(actmodel);
            }
            else
            {
               IsReturn= BActivity.Add(actmodel);
            }
            if (IsReturn)
            {
                MessageBox.ShowAndRedirect("发布成功！", "Default.aspx"+ UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"])); 
            }
            else
            {
                MessageBox.ShowAndRedirect("只有发布活动的管理员才能修改！", "Default.aspx" + UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]));
            }
        }
        /// <summary>
        /// 将用特殊符号分割的guid字符串转换为字符串数组
        /// </summary>
        /// <param name="strValue">字符串</param>
        /// <param name="space">分割符</param>
        /// <returns></returns>
        public static List<string> GetStringArray(string strValue, string space)
        {
            if (string.IsNullOrEmpty(strValue) || string.IsNullOrEmpty(space))
                return null;

            var strArray = strValue.TrimEnd(space.ToCharArray()).Split(space.ToCharArray());
            if (strArray.Length <= 0) return null;
            return strArray.Where(c => !string.IsNullOrEmpty(c)).ToList();
        }
    }
}