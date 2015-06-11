using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class MBallTeamSearch
    {

        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 区县编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 球队名称关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 球队队长
        /// </summary>
        public string TeamOwner { get; set; }
        /// <summary>
        /// 队长电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EnumType.球队审核状态? State { get; set; }
        /// <summary>
        /// 是否参加比赛
        /// </summary>
        public bool? JoinMatch { get; set; }
        /// <summary>
        /// 是否有队员
        /// </summary>
        public bool? IsTeamer { get; set; }

        /// <summary>
        /// 注册开始日期
        /// </summary>
        public DateTime? IssueBeginTime { get; set; }
        /// <summary>
        /// 注册结束日期
        /// </summary>
        public DateTime? IssueEndTime { get; set; }
        /// <summary>
        /// 审核开始日期
        /// </summary>
        public DateTime? CheckBeginTime{get;set;}
        /// <summary>
        /// 审核结束日期
        /// 
        /// </summary>
        public DateTime? CheckEndTime { get; set; }
        /// <summary>
        /// 是否所有城市
        /// </summary>
        public bool? IsAllCity { get; set; }
        /// <summary>
        /// 城市限制
        /// </summary>
        public string CityLimitList { get; set; }
        /// <summary>
        /// 关注人ID
        /// </summary>
        public string OfferpatMemberID { get; set; }

    }
}
