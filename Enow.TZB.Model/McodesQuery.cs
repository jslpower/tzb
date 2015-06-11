using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class McodesQuery
    {
        /// <summary>
        /// 码类型 (Enow.TZB.Model.EnumType.CodeEnum-码类型枚举)
        /// </summary>
        public int Codetype { get; set; }
        /// <summary>
        /// 码编号
        /// </summary>
        public string Codenum { get; set; }
        /// <summary>
        /// 码使用状态 0:未使用 1:已使用 -1:全部
        /// </summary>
        public int Codestate { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Usnc { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Usname { get; set; }
        /// <summary>
        /// 用户用户电话
        /// </summary>
        public string Ustel { get; set; }
    }
}
