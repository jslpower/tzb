using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Enow.TZB.Web.ShouYin
{
    public partial class DingDan :ShouYinYeMian
    {
        #region attributes
        /// <summary>
        /// 球场名称
        /// </summary>
        protected string QiuChangName = string.Empty;
        /// <summary>
        /// 每页记录数
        /// </summary>
        protected int PageSize = 5;
        /// <summary>
        /// 页序号
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        protected int RecordCount = 0;
        /// <summary>
        /// 订单编号
        /// </summary>
        protected string DingDanId = string.Empty;
        /// <summary>
        /// 方式 
        /// </summary>
        protected string FS = string.Empty;
        /// <summary>
        /// 订单明细标题
        /// </summary>
        protected string DingDanMingXiBiaoTi = string.Empty;
        /// <summary>
        /// 订单收款方式
        /// </summary>
        protected Enow.TZB.Model.收款类型 ShouKuanFangShi = Model.收款类型.现金;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            DingDanId = Utility.Utils.GetQueryStringValue("dingdanid");
            FS = Utility.Utils.GetQueryStringValue("fs");
            InitInfo();

            if (FS == "dingdanmingxi")
            {
                InitDingDanMingXi();
            }
            else
            {
                InitDingDanRpt();
            }

        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            QiuChangName = YongHuInfo.FieldName;
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        Enow.TZB.Model.MOrderSearch GetChaXunInfo()
        {
            var info = new Enow.TZB.Model.MOrderSearch();

            info.BallFieldId = YongHuInfo.FieldId;
            info.OperatorBeginTime = Utility.Utils.GetDateTimeNullable(Utility.Utils.GetQueryStringValue("txtShiJian1"));
            info.OperatorEndTime = Utility.Utils.GetDateTimeNullable(Utility.Utils.GetQueryStringValue("txtShiJian2"));
            info.JiaoYiHao = Utility.Utils.GetQueryStringValue("txtJiaoYiHao");

            return info;
        }

        /// <summary>
        /// init dingdan repeater
        /// </summary>
        void InitDingDanRpt()
        {
            phDingDan.Visible = true;
            var chaXun = GetChaXunInfo();
            PageIndex = Utility.UtilsCommons.GetPadingIndex();

            var items = Enow.TZB.BLL.Order.BOrder.GetList(ref RecordCount, PageSize, PageIndex, chaXun);

            if (items != null && items.Count > 0)
            {
                rptDingDan.DataSource = items;
                rptDingDan.DataBind();
            }
            else
            {
                phEmptyDingDan.Visible = true;
            }
        }

        /// <summary>
        /// init dingdan mingxi
        /// </summary>
        void InitDingDanMingXi()
        {
            phDingDanMingXi.Visible = true;
            var dingDanInfo = Enow.TZB.BLL.Order.BOrder.GetOrderModel(DingDanId);
            if (dingDanInfo == null) { RCWE("异常请求"); }

            ShouKuanFangShi = (Enow.TZB.Model.收款类型)dingDanInfo.PayType;
            string shouKuanLeiXing = "未知";
            switch (ShouKuanFangShi)
            {
                case Model.收款类型.铁丝卡: shouKuanLeiXing = "铁丝二维码"; break;
                case Model.收款类型.工商银行卡: shouKuanLeiXing = "广发银行"; break;
                case Model.收款类型.现金: shouKuanLeiXing = "现金"; break;
                default: break;
            }

            DingDanMingXiBiaoTi = dingDanInfo.OperatorTime.ToString("yyyy-MM-dd HH:mm") + "收款项(" + shouKuanLeiXing + "实收<span class=\"fontred price\">" + dingDanInfo.ActualMoney.ToString("F2") + "</span>元)";


            var items = Enow.TZB.BLL.Order.BOrder.GetOrderDetail(DingDanId);
            if (items != null || items.Count > 0)
            {
                rptDingDanMingXi.DataSource = items;
                rptDingDanMingXi.DataBind();
            }
            else
            {
                phEmptyDingDanMingXi.Visible = true;
            }
        }
        #endregion

        #region protected members
        /// <summary>
        /// get yingshou jine
        /// </summary>
        /// <param name="shuLiang">shuliang</param>
        /// <param name="shiChangJiaGe">shichang jiage</param>
        /// <param name="tieSiJiaGe">tiesi jiage</param>
        /// <returns></returns>
        protected string GetYingShouJinE(object shuLiang,object shiChangJiaGe,object tieSiJiaGe)
        {
            int _shuLiang = (int)shuLiang;
            decimal _shiChangJiaGe = (decimal)shiChangJiaGe;
            decimal _tieSiJiaGe = (decimal)tieSiJiaGe;
            string s = string.Empty;
            switch (ShouKuanFangShi)
            {
                case Model.收款类型.工商银行卡:
                    s = (_shuLiang * _shiChangJiaGe * 0.9M).ToString("F2");
                    break;
                case Model.收款类型.现金:
                    s = (_shuLiang * _shiChangJiaGe).ToString("F2");
                    break;
                case Model.收款类型.铁丝卡:
                    s = (_shuLiang * _tieSiJiaGe).ToString("F2");
                    break;
            }

            return s;
        }

        /// <summary>
        /// get tiesi jine
        /// </summary>
        /// <param name="shuLiang">shuliang</param>
        /// <param name="tieSiJiaGe">tiesi jiage</param>
        /// <returns></returns>
        protected string GetTieSiJinE(object shuLiang,object tieSiJiaGe)
        {
            int _shuLiang = (int)shuLiang;
            decimal _tieSiJiaGe = (decimal)tieSiJiaGe;
            string s = string.Empty;

            s = (_shuLiang * _tieSiJiaGe).ToString("F2");

            return s;
        }

        /// <summary>
        /// get tuikuan tubiao
        /// </summary>
        /// <param name="zhuangTai">zhuangtai</param>
        /// <returns></returns>
        protected string GetTuiKuanTuBiao(object zhuangTai,object typeId)
        {
            var _zhuangTai = (Enow.TZB.Model.订单状态)((int)zhuangTai);
            int TypeId = (int)typeId;

            if (_zhuangTai == Model.订单状态.已退款)
            {
                return "<a data-class=\"tuikuan\" class=\"tuikuan_on\" href=\"javascript:void(0)\" data-status=\"yitui\" ></a>";
                
            }
            if (TypeId == 1)
            {
                return "";
            }
            return "<a data-class=\"tuikuan\" class=\"tuikuan\" href=\"javascript:void(0)\" data-status=\"weitui\" data-tui=\"0\"></a>";
        }
        #endregion
    }
}