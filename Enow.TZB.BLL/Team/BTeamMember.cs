using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    public class BTeamMember2
    {
        

       /// <summary>
        /// 根据参赛人员信息编号和球队编号取得参赛信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static dt_TeamMember GetTeamMemberModel(string teamid, string memberid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_TeamMember.FirstOrDefault(n => n.TeamId == teamid && n.MemberId == memberid);
                return model;
            }
        }
    }
}
