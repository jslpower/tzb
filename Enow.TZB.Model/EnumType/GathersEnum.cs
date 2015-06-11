using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model.EnumType
{
    public class GathersEnum
    {
        public enum 比赛费用
        {
            AA制 = 1,
            主队,
            客队,
            胜方,
            败方
        }
        public enum 约战状态
        {
            约战中 = 1,
            进行中,
            战报待确认,
            待确认,
            战报重填,
            双方确认,
            终止
        }
        public enum 约战审核状态
        {
            待审核=1,
            已通过,
            未通过
            
        }
        public enum 主客队约战状态
        {
            约战中 = 1,
            进行中,
            应战被拒 ,
            待确认,
            应战,
            战报确认,
            终止
            
        }
    }
}
