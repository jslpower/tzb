using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Enow.TZB.Web.ShouYin
{
    /// <summary>
    /// 退款
    /// </summary>
    public partial class Refund : ShouYinYeMian
    {
        #region attributes
        /// <summary>
        /// 退款方式 Cash:现金退款 Card:铁丝退款
        /// </summary>
        protected string SKFS = "Cash";
        /// <summary>
        /// 广发卡折扣
        /// </summary>
        protected decimal GFK_ZheKou = Utility.Utils.GetDecimal(System.Configuration.ConfigurationManager.AppSettings["GFKSXFZheKou"]);
        /// <summary>
        /// 球场名称
        /// </summary>
        protected string QiuChangName = string.Empty;        
        #endregion
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SKFS = Utility.Utils.GetQueryStringValue("Type");
            var _skfs = new string[] { "Cash", "Card"};
            if (!_skfs.Contains(SKFS)) RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "异常请求"));
            InitInfo();

            if (Utility.Utils.GetQueryStringValue("dotype") == "Refund") { DoRefund(); return; }
        }

        #region private members
        /// <summary>
        /// 退款操作
        /// </summary>
        void DoRefund()
        {
            List<Model.MRefundResponseInfo> strJosonList = new List<Model.MRefundResponseInfo>();
            string Ids = Utility.Utils.GetFormValue("hidId");
            string tieSiErWeiMa = Utility.Utils.GetFormValue("txtTieSiErWeiMa");
            SKFS = Utility.Utils.GetQueryStringValue("Type");
            var _skfs = new string[] { "Cash", "Card" };
            if (!_skfs.Contains(SKFS))
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "异常请求"));
                return;
            }
            int rv = Enow.TZB.BLL.Order.BOrder.OrderDetailRefund(Ids, YongHuInfo.Id, YongHuInfo.ContactName, ref strJosonList);
            string msg = "";
            switch (rv)
            { //1:处理成功 0:订单明细编号空值 -1:订单明细编号不正确 -2：找不到相关订单明细内容 -3:未找到相应订单内容 -4:未找到退款用户

                case 1:
                    msg = "处理成功";
                    RCWE(Utility.UtilsCommons.AjaxReturnJson("1", msg, strJosonList));
                    break;
                case 0:
                    msg = "订单明细编号空值";
                    RCWE(Utility.UtilsCommons.AjaxReturnJson("0", msg));
                    break;
                case -1:
                    msg = "订单明细编号不正确";
                    RCWE(Utility.UtilsCommons.AjaxReturnJson("0", msg));
                    break;
                case -2:
                    msg = "找不到相关订单明细内容";
                    RCWE(Utility.UtilsCommons.AjaxReturnJson("0", msg));
                    break;
                case -3:
                    msg = "未找到相应订单内容";
                    RCWE(Utility.UtilsCommons.AjaxReturnJson("0", msg));
                    break;
                case -4:
                    msg = "未找到退款用户";
                    RCWE(Utility.UtilsCommons.AjaxReturnJson("0", msg));
                    break;
            }
        }
        /// <summary>
        /// get tiesi erweima info
        /// </summary>
        /// <param name="erWeiMa">erWeiMa</param>
        /// <returns></returns>
        MTieSiErWeiMaInfo GetTieSiErWeiMaInfo(string erWeiMa)
        {
            var info = new MTieSiErWeiMaInfo();
            info.HuiYuanId = string.Empty;
            info.TimeStamp = new DateTime(2000, 1, 1);

            if (string.IsNullOrEmpty(erWeiMa)) return info;

            string s = Enow.TZB.BLL.BMemberWallet.UnSign(erWeiMa);
            if (string.IsNullOrEmpty(s)) return info;


            var items = s.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            if (items == null || items.Length != 2) return info;

            info.HuiYuanId = items[0];
            info.TimeStamp = Utility.Utils.GetDateTime(items[1]);

            return info;
        }

        /// <summary>
        /// 球场名称
        /// </summary>
        void InitInfo()
        {
            QiuChangName = YongHuInfo.FieldName;
        }
        #endregion
    }
}