using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 个人进球数据
    /// </summary>
    public class BMatchMemberTechnical
    {
        /// <summary>
        /// 返回比赛个人进球数据
        /// </summary>
        /// <param name="MatchId"></param>
        /// <returns></returns>
        public static List<tbl_MatchMemberTechnical> GetList(string ScheduleId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.tbl_MatchMemberTechnical
                            where q.ScheduleId == ScheduleId
                            orderby q.TypeId ascending,q.MemberId ascending
                            select q;
                return query.ToList();
            }
        }
        /// <summary>
        /// 返回比赛个人进球数据
        /// </summary>
        /// <param name="MatchId"></param>
        /// <returns></returns>
        public static List<tbl_MatchMemberTechnical> GetList(string ScheduleId,string MatchTeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.tbl_MatchMemberTechnical
                            where q.ScheduleId == ScheduleId && q.MatchTeamId == MatchTeamId
                            orderby q.IssueTime ascending
                            select q;
                return query.ToList();
            }
        }
        /// <summary>
        /// 批量删除排赛的个人战绩
        /// </summary>
        /// <param name="ScheduleId"></param>
        public static void DelSchedule(string ScheduleId)
        {
            using (FWDC rdc = new FWDC())
            {
                var list = rdc.tbl_MatchMemberTechnical.Where(n => n.ScheduleId == ScheduleId);
                if (list != null && list.Count() > 0)
                {
                    rdc.tbl_MatchMemberTechnical.DeleteAllOnSubmit(list);
                    rdc.SubmitChanges();
                }
            }
        }
        /// <summary>
        /// 批量添加个人战绩
        /// </summary>
        /// <param name="list"></param>
        public static void Add(List<tbl_MatchMemberTechnical> list)
        {
            using (FWDC rdc = new FWDC())
            {
                if (list != null && list.Count() > 0)
                {
                    rdc.tbl_MatchMemberTechnical.InsertAllOnSubmit(list);
                    rdc.SubmitChanges();
                }
            }
        }
    }
}
