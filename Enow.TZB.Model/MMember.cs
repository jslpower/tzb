using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 会员查询实体
    /// </summary>
    public class MMemberSearch
    {
        /// <summary>
        /// 国家ID
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// 省份ID
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 区县ID
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNo { get; set; }
        /// <summary>
        /// 注册开始时间
        /// </summary>
        public DateTime? IssueBeginTime { get; set; }
        /// <summary>
        /// 注册结束时间
        /// </summary>
        public DateTime? IssueEndTime { get; set; }
        /// <summary>
        /// 审核开始时间
        /// </summary>
        public DateTime? CheckBeginTime { get; set; }
        /// <summary>
        /// 审核结束时间
        /// </summary>
        public DateTime? CheckEndTime { get; set; }
        /// <summary>
        /// 是否参加球队
        /// </summary>
        public bool? IsJoinTeam { get; set; }
        /// <summary>
        /// 是否参加比赛
        /// </summary>
        public bool? IsJoinMatch { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EnumType.会员状态? State { get; set; }
        /// <summary>
        /// 是否有上传图片
        /// </summary>
        public bool? IsHasPhoto { get; set; }
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
