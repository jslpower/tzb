using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using System.Data;

namespace Enow.TZB.Web.Manage.Mall
{
    public partial class Orders : System.Web.UI.Page
    {
        // <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();

                //phpass.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.订单审核通过);
                phnopass.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.订单退款); 
                phsend.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.发货);
            }
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");

            switch (doType)
            {
                case "CheckPass": UpdateState(Model.商城订单状态.未支付); break;
                case "NoPass": UpdateState(Model.商城订单状态.审核无效); break;
                case "tuikuan": UpdateState(Model.商城订单状态.退款); break;
                case "Send": WriteInfo(); break;
                default: break;
            }
            #endregion
            InitList();

        }
        #region
        /// <summary>
        /// 填写物流信息
        /// </summary>
        private void WriteInfo()
        {

            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;

            string ContactName = UserModel.ContactName;
            UserModel = null;

            string addId = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(addId))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                return;
            }
           
            bllRetCode = BSendAddress.Update(new tbl_SendAddress
            {
                Id = addId,
                LogisticsNo = DateTime.Now.ToString("yyyyMMddHHmmssffff")
            });
            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        /// <summary>
        /// 修改订单状态
        /// </summary>
        private void UpdateState(Model.商城订单状态 state)
        {
           
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            string ContactName = UserModel.ContactName;
            UserModel = null;
            string s = Utils.GetQueryStringValue("id");
            string[] ids = s.Split(',');
            foreach (var IdStr in ids)
            {
                if (string.IsNullOrEmpty(IdStr))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                    return;
                }

                bllRetCode = BMallOrders.UpdateState(IdStr, UserId, ContactName,state );
            }
            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "审核成功!"));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "审核失败!"));
        }
        /// <summary>
        ///  审核无效
        /// </summary>
        private void NoPass()
        {
          
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            string ContactName = UserModel.ContactName;
            UserModel = null;
            string s = Utils.GetQueryStringValue("id");
             string[] ids = s.Split(',');
             foreach (var IdStr in ids)
             {
                 if (string.IsNullOrEmpty(IdStr))
                 {
                     Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                     return;
                 }
                 bllRetCode = BMallOrders.UpdateState(IdStr, UserId, ContactName, Model.商城订单状态.审核无效);
             }
          
            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败!"));
        }
        /// <summary>
        #endregion
        /// <summary>
        /// 绑定列表
        /// </summary>
        private void InitList()
        {
            Model.MallorderQuery searchModel = new Model.MallorderQuery();

            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            //string name = Utils.GetQueryStringValue("Name");
            if (Utils.GetInt(Utils.GetQueryStringValue("ddlState")) > 0)
            {
                searchModel.PayStatus = Utils.GetInt(Utils.GetQueryStringValue("ddlState"));

            }
            //else
            //{ 
            //    //未支付
            //    searchModel.PayStatus = 1; 
            //}
            string orderNo = Utils.GetQueryStringValue("orderNo");
            if (!string.IsNullOrEmpty(orderNo)&&orderNo!="undefined")
            {
                searchModel.OrderNo = orderNo;
            }
            string sDate = Utils.GetQueryStringValue("startDate");
            if (!string.IsNullOrEmpty(sDate) && sDate != "undefined")
            {
                searchModel.StartDate = sDate;
            }
            string eDate = Utils.GetQueryStringValue("endDate");
            if (!string.IsNullOrEmpty(eDate) && eDate != "undefined")
            {
                searchModel.EndDate = eDate;
            }

            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            var list = BMallOrders.GetList(ref rowCounts, intPageSize, CurrencyPage, searchModel);
            if (rowCounts > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = intPageSize;
                ExportPageInfo1.intRecordCount = rowCounts;
                ExportPageInfo1.CurrencyPage = CurrencyPage;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }


        }
      


      
    }
}