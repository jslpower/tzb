using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 战报文章管理
    /// </summary>
    public class BMatchSchedultReport
    {
        /// <summary>
        /// 添加战报文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_MatchSchedultReport model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    var m = rdc.tbl_MatchSchedultReport.FirstOrDefault(n => n.ScheduleId == model.ScheduleId);
                    if (m != null) {
                        m.PhotoUrl = model.PhotoUrl;
                        m.titleSulg = model.titleSulg;
                        m.Title = model.Title;
                        m.ContentInfo = model.ContentInfo;
                        rdc.SubmitChanges();
                    }
                    else
                    {
                        rdc.tbl_MatchSchedultReport.InsertOnSubmit(model);
                        rdc.SubmitChanges();
                    }
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 取得战报
        /// </summary>
        /// <param name="ScheduleId"></param>
        /// <returns></returns>
        public static tbl_MatchSchedultReport GetModel(string ScheduleId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MatchSchedultReport.FirstOrDefault(n => n.ScheduleId == ScheduleId);
                return m;
            }
        }
    }
}
