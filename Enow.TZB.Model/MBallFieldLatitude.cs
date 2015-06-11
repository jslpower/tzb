using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 球场经纬度
    /// </summary>
    public class MBallFieldLatitude
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 球场名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }
    }
}
