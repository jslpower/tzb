//铁丝充值 2014-11-28
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Enow.TZB.Web.ShouYin
{
    /// <summary>
    /// 铁丝充值
    /// </summary>
    public partial class ChongZhi : ShouYinYeMian
    {
        #region attributes
        /// <summary>
        /// 充值折扣
        /// </summary>
        protected decimal CZ_ZheKou = 10.0M;
        /// <summary>
        /// 广发卡折扣
        /// </summary>
        protected decimal GFK_ZheKou = Utility.Utils.GetDecimal(System.Configuration.ConfigurationManager.AppSettings["GFKSXFZheKou"]);
        /// <summary>
        /// 球场名称
        /// </summary>
        protected string QiuChangName = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.Utils.GetQueryStringValue("dotype") == "chongzhi") BaoCunChongZhi();
            InitPayType();
            InitInfo();
        }

        #region private members
        /// <summary>
        /// 加载收银类型
        /// </summary>
        void InitPayType()
        {
            string rv = "";
            var list = Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.收款类型), new string[] { Convert.ToString((int)Enow.TZB.Model.收款类型.铁丝卡)});
            int k = 0;
            foreach (var s in list)
            {
                if (s.Value == "0")
                    rv += "<input type=\"radio\" name=\"rbPayType\" id=\"rbPayType_" + k + "\" value=\"" + s.Value + "\" checked=\"checked\"><label for=\"rbPayType_" + k + "\">" + s.Text + "&nbsp;&nbsp;</label>";
                else
                    rv += "<input type=\"radio\" name=\"rbPayType\" id=\"rbPayType_" + k + "\" value=\"" + s.Value + "\"><label for=\"rbPayType_" + k + "\">" + s.Text + "&nbsp;&nbsp;</label>";
                k++;
            }
            this.ltrPayType.Text = rv;
        }
        /// <summary>
        /// 保存充值
        /// </summary>
        void BaoCunChongZhi()
        {
            Enow.TZB.Model.收款类型 PayType = (Enow.TZB.Model.收款类型)Utility.Utils.GetInt(Utility.Utils.GetFormValue("rbPayType"));
            //获取表单
            string txtShouJiHao = Utility.Utils.GetFormValue("txtShouJiHao");
            string txtShuLiang = Utility.Utils.GetFormValue("txtShuLiang");
            string txtYingShouJinE = Utility.Utils.GetFormValue("txtYingShouJinE");

            int shuLiang = Utility.Utils.GetInt(txtShuLiang);
            decimal yingShouJinE = Utility.Utils.GetDecimal(txtYingShouJinE);

            var shangPinInfo = Enow.TZB.BLL.BGoods.GetChongzhiCardModel(YongHuInfo.FieldId);

            //验证表单
            if (string.IsNullOrEmpty(txtShouJiHao) || string.IsNullOrEmpty(txtShuLiang) || string.IsNullOrEmpty(txtYingShouJinE))
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "表单异常"));
            }

            if (shuLiang <= 0 || yingShouJinE <= 0)
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "表单异常"));
            }
            switch (PayType) { 
                case Model.收款类型.工商银行卡:
                    if (Utility.Utils.GetDecimal((shuLiang * (GFK_ZheKou / 10)).ToString("F2")) != yingShouJinE)
                    {
                        RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "表单异常"));
                    }
                    break;
                default:
                    if (Utility.Utils.GetDecimal((shuLiang * (CZ_ZheKou / 10)).ToString("F2")) != yingShouJinE)
                    {
                        RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "表单异常"));
                    }
                    break;
            }
            var huiYuanInfo =new Enow.TZB.BLL.BMember().GetModelByPhone(txtShouJiHao);
            if (huiYuanInfo == null)
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "手机号码对应的会员信息不存在"));
            }

            if (shangPinInfo == null)
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "充值卡（商品）信息错误"));
            }

            //构造实体
            var dingDanInfo = new Enow.TZB.Model.MOrderInfo();
            dingDanInfo.ActualMoney = yingShouJinE;
            dingDanInfo.CurrencyNumber = shuLiang;
            dingDanInfo.CustomerName = huiYuanInfo.ContactName;
            dingDanInfo.FieldId = YongHuInfo.FieldId;
            dingDanInfo.JiaoYiHao = string.Empty;
            dingDanInfo.JinE = yingShouJinE;
            dingDanInfo.MemberId = huiYuanInfo.Id;
            dingDanInfo.Mingxi = new List<Enow.TZB.Model.MorderMingxi>();
            dingDanInfo.Mobile = txtShouJiHao;
            dingDanInfo.OperatorId = YongHuInfo.Id;
            dingDanInfo.OperatorName = YongHuInfo.ContactName;
            dingDanInfo.OperatorTime = DateTime.Now;
            dingDanInfo.OrderId = Guid.NewGuid().ToString();
            dingDanInfo.PayStatus = Model.收款状态.已支付;            
            dingDanInfo.PayType = PayType;
            dingDanInfo.Point = 0;
            dingDanInfo.TotalCurrencyNumber = 0;
            dingDanInfo.TotalPoint = 0;
            switch (PayType)
            {
                case Model.收款类型.工商银行卡:
                    dingDanInfo.ZheKou = GFK_ZheKou;
                    break;
                default:
                    dingDanInfo.ZheKou = CZ_ZheKou;
                    break;
            }

            var dingDanMingXiItem = new Enow.TZB.Model.MorderMingxi();
            dingDanMingXiItem.Amount = shuLiang;
            dingDanMingXiItem.Cprice = shangPinInfo.CurrencyPrice;
            dingDanMingXiItem.GoodsId = shangPinInfo.ID;
            dingDanMingXiItem.Id = 0;
            dingDanMingXiItem.IssueTime = DateTime.Now;
            dingDanMingXiItem.JinE = shuLiang * dingDanMingXiItem.Cprice;
            dingDanMingXiItem.OrderId = dingDanInfo.OrderId;
            dingDanMingXiItem.Price = shangPinInfo.Price;
            dingDanMingXiItem.State = Model.订单状态.已付款;

            dingDanInfo.Mingxi.Add(dingDanMingXiItem);

            int bllRetCode = Enow.TZB.BLL.Order.BOrder.MemberChongzhi(dingDanInfo);

            if (bllRetCode == 1)
            {
                var responseInfo = new MChongZhiAjaxResponseInfo();
                responseInfo.liushuihao = dingDanInfo.JiaoYiHao;
                responseInfo.shoukuanrenid = dingDanInfo.OperatorId.ToString().PadLeft(4, '0');
                responseInfo.shoukuanshijian = dingDanInfo.OperatorTime;
                responseInfo.shuliang = shuLiang;
                responseInfo.yingshoujine = yingShouJinE;
                responseInfo.zhekou = dingDanInfo.ZheKou;
                responseInfo.zongshuliang = dingDanInfo.TotalCurrencyNumber;

                RCWE(Utility.UtilsCommons.AjaxReturnJson("1", "", responseInfo));
            }
            else if (bllRetCode == -1)
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "手机号码对应的会员信息不存在"));
            }
            else
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "充值失败"));
            }
            
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

    #region 充值异步提交返回信息业务实体
    /// <summary>
    /// 充值异步提交返回信息业务实体
    /// </summary>
    public class MChongZhiAjaxResponseInfo
    {
        /// <summary>
        /// 充值数量
        /// </summary>
        public int shuliang { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal yingshoujine { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string liushuihao { get; set; }
        /// <summary>
        /// 总数量（会员铁丝币余额）
        /// </summary>
        public decimal zongshuliang { get; set; }
        /// <summary>
        /// 充值折扣
        /// </summary>
        public decimal zhekou { get; set; }
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
    }
    #endregion
}