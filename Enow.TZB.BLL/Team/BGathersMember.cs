using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    public class BGathersMember
    {
        /// <summary>
        /// 批量添加约战成员
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static void Add(List<tbl_GathersMember> list)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_GathersMember.InsertAllOnSubmit(list);
                rdc.SubmitChanges();
            }
        }
    }
}
