using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;


namespace Enow.TZB.Web.Manage.Match
{
    /// <summary>
    /// 保证金分配
    /// </summary>
    public partial class Deposit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("dotype");
            switch (doType) {
                case "save":
                    Save();
                    return;
                    break;
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitList();
            }

        }
        #region 保存分配
        /// <summary>
        /// 保存分配
        /// </summary>
        private void Save()
        {
            string msg = ""; 
            bool result = false;
            string Id = Utils.GetQueryStringValue("id");//MatchTeamId
            if (!string.IsNullOrEmpty(Id))
            {
                string[] Ids = Utils.GetFormValues("hidId");
                string[] TeamIds = Utils.GetFormValues("hidTeamId");
                string[] ReFees = Utils.GetFormValues("hidReFee");
                string[] DepositMoneys = Utils.GetFormValues("txtDepositMoney");
                if (Ids.Length<1)
                {
                    msg += "没有需要保存的内容<br/>";
                }
                if (!string.IsNullOrEmpty(msg))
                {
                    Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                    Response.End();
                    return;
                }
                else
                {
                    //保存分配
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        //更新参赛球队
                        BMatchTeam.UpdateDepositMoney(Ids[i], Model.EnumType.操作符号.减, Utils.GetDecimal(DepositMoneys[i]));
                    }
                    msg = "资金分配成功！";
                    result = true; 
                    Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                    Response.End();
                }
            }
            else
            {
                msg = "数据操作有误，请重新操作";
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
            } 
        }
        #endregion
        #region 加载保证金列表
        /// <summary>
        /// 加载保证金列表
        /// </summary>
        private void InitList()
        {
            string Id = Utils.GetQueryStringValue("id");//MatchTeamId
            if (string.IsNullOrWhiteSpace(Id))
            {
                MessageBox.ShowAndParentReload(CacheSysMsg.GetMsg(31));
                return;
            }
            else
            {
                string MTID = "";
                string[] IdList = Id.Split(',');
                foreach (string s in IdList) {
                    MTID = MTID+"'"+ s + "',";
                }
                MTID = MTID.TrimEnd(',');
                var list = BMatchTeam.GetList(MTID);
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
        #endregion
    }
}