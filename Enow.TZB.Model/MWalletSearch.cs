using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class MWalletSearch
    {
        /// <summary>
        /// 财务流水类型
        /// </summary>
        public Model.EnumType.财务流水类型? TypeId { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 流水号码
        /// </summary>
        public string TradeNumber { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        public Model.EnumType.付款状态? IsPay { get; set; }
        /// <summary>
        /// 充值开始日期
        /// </summary>
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// 充值结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 是否所有城市
        /// </summary>
        public bool? IsAllCity { get; set; }
        /// <summary>
        /// 城市限制
        /// </summary>
        public string CityLimitList { get; set; }
    }
    public class MWalletViewSearch : MWalletSearch
    {
        /// <summary>
        /// 统计权限类别
        /// </summary>
        public Model.EnumType.财务流水类型? PerClass { get; set; }
        /// <summary>
        /// 会员名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }
    }
    public class MTeamWalletViewSearch
    {
        /// <summary>
        /// 会员名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// 赛事名称
        /// </summary>
        public string MatchName { get; set; }
        /// <summary>
        /// 是否所有城市
        /// </summary>
        public bool? IsAllCity { get; set; }
        /// <summary>
        /// 城市限制
        /// </summary>
        public string CityLimitList { get; set; }
    }
}
