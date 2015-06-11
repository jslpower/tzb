//收银端-收款
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Enow.TZB.Web.ShouYin
{
    /// <summary>
    /// 收银端-收款
    /// </summary>
    public partial class ShouKuan : ShouYinYeMian
    {
        #region attributes
        /// <summary>
        /// 收款方式 xianjinshoukuan:现金收款 tiesishoukuan:铁丝收款 guangfashoukuan:guangfashoukuan
        /// </summary>
        protected string SKFS = "xianjinshoukuan";
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
            SKFS = Utility.Utils.GetQueryStringValue("skfs");
            var _skfs =new string[] { "xianjinshoukuan", "tiesishoukuan", "guangfashoukuan" };
            if (!_skfs.Contains(SKFS)) RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "异常请求"));
            InitPayType(SKFS);
            InitInfo();

            if (Utility.Utils.GetQueryStringValue("dotype") == "shoukuan") BaoCunShouKuan();            
        }

        #region private members
        /// <summary>
        /// 加载收银类型
        /// </summary>
        /// <param name="SKFS"></param>
        void InitPayType(string SKFS)
        {
            string rv = "";

            switch (SKFS) {
                case "xianjinshoukuan":
                    var list = Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.收款类型), new string[] { Convert.ToString((int)Enow.TZB.Model.收款类型.铁丝卡), Convert.ToString((int)Enow.TZB.Model.收款类型.工商银行卡) });
                    int k = 0;
                    foreach (var s in list)
                    {
                        if (s.Value == "0")
                            rv += "<input type=\"radio\" name=\"rbPayType\" id=\"rbPayType_" + k + "\" value=\"" + s.Value + "\" checked=\"checked\"><label for=\"rbPayType_" + k + "\">" + s.Text + "&nbsp;&nbsp;</label>  ";
                        else
                            rv += "<input type=\"radio\" name=\"rbPayType\" id=\"rbPayType_" + k + "\" value=\"" + s.Value + "\"><label for=\"rbPayType_" + k + "\">" + s.Text + "&nbsp;&nbsp;</label>";
                        k++;
                    }
                    break;
                case "tiesishoukuan":
                    rv += "<input type=\"radio\" name=\"rbPayType\" id=\"rbPayType\" value=\"" + (int)Enow.TZB.Model.收款类型.铁丝卡 + "\" checked=\"checked\"><label for=\"rbPayType\">" + Enow.TZB.Model.收款类型.铁丝卡.ToString() + "&nbsp;&nbsp;</label>  ";
                    break;
                case "guangfashoukuan":
                    rv += "<input type=\"radio\" name=\"rbPayType\" id=\"rbPayType\" value=\"" + (int)Enow.TZB.Model.收款类型.工商银行卡 + "\" checked=\"checked\"><label for=\"rbPayType\">" + Enow.TZB.Model.收款类型.工商银行卡.ToString() + "&nbsp;&nbsp;</label>  ";
                    break;
            }
            this.ltrPayType.Text = rv;
        }
        /// <summary>
        /// 保存收款信息
        /// </summary>
        void BaoCunShouKuan()
        {
            Enow.TZB.Model.收款类型 PayType = (Enow.TZB.Model.收款类型)Utility.Utils.GetInt(Utility.Utils.GetFormValue("rbPayType"));
            //获取表单
            decimal shangPinJinE = Utility.Utils.GetDecimal(Utility.Utils.GetFormValue("txtShangPinJinE"));
            decimal shiShouJinE = Utility.Utils.GetDecimal(Utility.Utils.GetFormValue("txtShiShouJinE"));
            string tieSiErWeiMa = Utility.Utils.GetFormValue("txtTieSiErWeiMa");
            decimal yingShouJinE = Utility.Utils.GetDecimal(Utility.Utils.GetFormValue("txtYingShouJinE"));
            decimal zheKou = Utility.Utils.GetDecimal(Utility.Utils.GetFormValue("txtZheKou"));
            string shangPinJson = Utility.Utils.GetFormValue("txtShangPinJson");
            var shangPinItems = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<MFormShangPinInfo>>(shangPinJson);
            string HuiYuanId = "";
            //验证表单-商品
            if (shangPinItems == null || shangPinItems.Count == 0)
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "表单异常"));
            }

            //验证表单-金额
            if (zheKou != GFK_ZheKou)
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "表单异常"));
            }

            //验证表单-其它
            if (SKFS == "tiesishoukuan")
            {
                if (string.IsNullOrEmpty(tieSiErWeiMa))
                {
                    RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "请扫描铁丝二维码"));
                }
                var tieSiErWeiMaInfo = GetTieSiErWeiMaInfo(tieSiErWeiMa.Trim());
                HuiYuanId = tieSiErWeiMaInfo.HuiYuanId;
                if (!tieSiErWeiMaInfo.IsYouXiao)
                {
                    RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "二维码已失效，请重新扫描"));
                }
                if (!Enow.TZB.BLL.BMember.isExistsId(HuiYuanId))
                {
                    RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "无效二维码：无该会员信息"));
                }
                var huiYuanInfo = Enow.TZB.BLL.BMember.GetModel(HuiYuanId);
                if (huiYuanInfo == null)
                {
                    RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "无效二维码：无该会员信息"));
                }
                if (huiYuanInfo.CurrencyNumber < yingShouJinE)
                {
                    RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "支付失败：铁丝账户余额不足"));
                }
            }

            //构造实体
            var dingDanInfo = new Model.MOrderInfo();
            dingDanInfo.ActualMoney = yingShouJinE;
            dingDanInfo.CustomerName = string.Empty;
            dingDanInfo.FieldId = shangPinItems[0].qiuchangid;
            dingDanInfo.JiaoYiHao = string.Empty;
            dingDanInfo.JinE = shangPinJinE;
            dingDanInfo.MemberId = string.Empty;
            dingDanInfo.Mingxi = new List<Enow.TZB.Model.MorderMingxi>();
            dingDanInfo.OperatorId = YongHuInfo.Id;
            dingDanInfo.OperatorName = YongHuInfo.ContactName;
            dingDanInfo.OperatorTime = DateTime.Now;
            dingDanInfo.OrderId = Guid.NewGuid().ToString();
            dingDanInfo.PayStatus = Model.收款状态.已支付;
            dingDanInfo.ZheKou = GFK_ZheKou;

            switch (SKFS)
            {
                case "xianjinshoukuan":
                    dingDanInfo.ZheKou = 10;
                    dingDanInfo.PayType = PayType; 
                    break;
                case "tiesishoukuan":
                    dingDanInfo.ZheKou = 10;
                    dingDanInfo.PayType = PayType;
                    dingDanInfo.MemberId = HuiYuanId;
                    break;
                case "guangfashoukuan":
                    dingDanInfo.ZheKou = GFK_ZheKou;
                    dingDanInfo.PayType = PayType; 
                    break;
            }

            foreach (var shangPinItem in shangPinItems)
            {
                var item = new Enow.TZB.Model.MorderMingxi();
                item.Amount = shangPinItem.shuliang;
                item.Cprice = shangPinItem.tiesijiage;
                item.GoodsId = shangPinItem.shangpinid;
                item.IssueTime = DateTime.Now;
                if (SKFS == "tiesishoukuan")
                {
                    item.JinE = shangPinItem.tiesijine;
                }
                else
                {
                    item.JinE = shangPinItem.shichangjine;
                }
                item.OrderId = dingDanInfo.OrderId;
                item.Price = shangPinItem.shichangjiage;
                item.State = Model.订单状态.已付款;
                dingDanInfo.Mingxi.Add(item);
            }

            int bllRetCode = Enow.TZB.BLL.Order.BOrder.AddOrder(dingDanInfo);

            if (bllRetCode == 1)
            {
                var responseInfo = new MShouKuanAjaxResponseInfo();
                responseInfo.shoukuanrenid = dingDanInfo.OperatorId.ToString().PadLeft(4, '0');
                responseInfo.jinfen = dingDanInfo.Point;
                responseInfo.liushuihao = dingDanInfo.JiaoYiHao;
                if (SKFS == "tiesishoukuan")
                {
                    responseInfo.shishoutiesijine = dingDanInfo.ActualMoney;
                }
                else
                {
                    responseInfo.shishoujine = dingDanInfo.ActualMoney;
                }
                responseInfo.shoukuanshijian = dingDanInfo.OperatorTime;
                responseInfo.yingshoujine = dingDanInfo.ActualMoney;                
                responseInfo.zhekou = dingDanInfo.ZheKou;
                responseInfo.zongjifen = dingDanInfo.TotalPoint;
                responseInfo.shangpinjine = shangPinJinE;

                RCWE(Utility.UtilsCommons.AjaxReturnJson("1", "", responseInfo));
            }
            else if (bllRetCode == -1)
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "支付失败：铁丝账户余额不足"));
            }
            else
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "操作失败"));
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
            if (string.IsNullOrEmpty(s) || s=="-1") return info;


            var items = s.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            if (items == null || items.Length != 2) return info;

            info.HuiYuanId = items[0];
            info.TimeStamp = Utility.Utils.GetDateTime(items[1]);

            return info;
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            QiuChangName = YongHuInfo.FieldName;
        }
        #endregion
    }

    #region 表单提交过来的商品信息
    /// <summary>
    /// 表单提交过来的商品信息
    /// </summary>
    public class MFormShangPinInfo
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public int shangpinid { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string shangpinname { get; set; }
        /// <summary>
        /// 商品单位
        /// </summary>
        public string shangpindanwei { get; set; }
        /// <summary>
        /// 球场编号
        /// </summary>
        public string qiuchangid { get; set; }
        /// <summary>
        /// 市场价格
        /// </summary>
        public decimal shichangjiage { get; set; }
        /// <summary>
        /// 铁丝价格
        /// </summary>
        public decimal tiesijiage { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int shuliang { get; set; }
        /// <summary>
        /// 市场金额
        /// </summary>
        public decimal shichangjine { get; set; }
        /// <summary>
        /// 铁丝金额
        /// </summary>
        public decimal tiesijine { get; set; }
    }
    #endregion

    #region 收款异步提交返回信息业务实体
    /// <summary>
    /// 收款异步提交返回信息业务实体
    /// </summary>
    public class MShouKuanAjaxResponseInfo
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string liushuihao { get; set; }
        /// <summary>
        /// 收款人编号
        /// </summary>
        public string shoukuanrenid { get; set; }
        /// <summary>
        /// 收款时间
        /// </summary>
        public DateTime shoukuanshijian { get; set; }
        /// <summary>
        /// 收款时间
        /// </summary>
        public string shoukuanshijian1 { get { return shoukuanshijian.ToString("yyyy-MM-dd HH:mm"); } }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal yingshoujine { get; set; }
        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal shishoujine { get; set; }
        /// <summary>
        /// 实收铁丝金额
        /// </summary>
        public decimal shishoutiesijine { get; set; }
        /// <summary>
        /// 本次积分
        /// </summary>
        public int jinfen { get; set; }
        /// <summary>
        /// 总积分
        /// </summary>
        public int zongjifen { get; set; }
        /// <summary>
        /// 收款折扣
        /// </summary>
        public decimal zhekou { get; set; }
        /// <summary>
        /// 商品金额
        /// </summary>
        public decimal shangpinjine { get; set; }
    }
    #endregion

    #region 铁丝二维码信息业务实体
    /// <summary>
    /// 铁丝二维码信息业务实体
    /// </summary>
    public class MTieSiErWeiMaInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// 二维码是否有效
        /// </summary>
        public bool IsYouXiao
        {
            get
            {
                if (string.IsNullOrEmpty(HuiYuanId)) return false;
                if (TimeStamp.AddMinutes(15) < DateTime.Now) return false;
                return true;
            }
        }
    }
    #endregion
}