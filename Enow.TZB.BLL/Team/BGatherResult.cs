using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    public class BGatherResult
    {
        /// <summary>
        /// 约战战报
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static void Add(tbl_GathersResult model)
        {
            using (FWDC rdc = new FWDC())
            {
                
                rdc.tbl_GathersResult.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
    }
}
