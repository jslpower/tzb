using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Mall
{
    public partial class LogisticsNo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

       /// <summary>
       /// 填写物流单号
       /// </summary>
       /// <param name="addId"></param>
       /// <returns></returns>
        private bool WriteData(string orderid)
        {

            if (string.IsNullOrEmpty(orderid))
            {
                MessageBox.ShowAndParentReload("请先选择订单！");
            }
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            string ContactName = UserModel.ContactName;
            string addNo = Utils.GetFormValue(txtNo.UniqueID);

            if (bllRetCode)
                return true;
            else
                return false;
        }

     

        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype");
            string addId = Utils.GetQueryStringValue("id");
            string orderid = Utils.GetQueryStringValue("orderid");
            if (!string.IsNullOrEmpty(orderid))
            {
                bool retval=BMallOrders.UpdateLogisticsState(orderid,Utils.GetText(Utils.GetFormEditorValue(txtNo.UniqueID),50));
                if (retval)
                {
                    var UserModel = ManageUserAuth.GetManageUserModel();
                    int UserId = UserModel.Id;
                    string ContactName = UserModel.ContactName;
                    UserModel = null;
                    BMallOrders.UpdateState(orderid, UserId, ContactName, Model.商城订单状态.已发货);
                    //BMallOrders.UpdateGoodsStockNum(orderid);
                    MessageBox.ShowAndParentReload("操作成功!");
                }
                else
                {
                    MessageBox.ShowAndParentReload("订单状态不正确!");
                }
                
            }
            //bool ret = false;
            //switch (dotype)
            //{
            //    case "Send":

            //        ret = WriteData(orderid);
            //        break;
                
            //    default:
            //        break;
            //}
            //if (ret)
            //{
            //      var UserModel = ManageUserAuth.GetManageUserModel();
            //      int UserId = UserModel.Id;
            //      string ContactName = UserModel.ContactName;
            //      UserModel = null;
            //      BMallOrders.UpdateState(orderid, UserId, ContactName, Model.商城订单状态.已发货);
            //    MessageBox.ShowAndParentReload("操作成功!");
            //}
            //else
            //{
            //    MessageBox.ShowAndReturnBack("操作失败!");
            //}
        }
    }
}