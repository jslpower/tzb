using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 赛程编号
    /// </summary>
    public class BMatchTeamCode
    {
        /// <summary>
        /// 更新参赛球队抽签状态
        /// </summary>
        /// <param name="MatchTeamId"></param>
        /// <param name="MatchCode"></param>
        /// <returns></returns>
        public static bool UpdateBallot(string MatchTeamId, ref string MatchCode)
        {
            using (FWDC rdc = new FWDC())
            {
                var MatchCodeModel = GetFirtstModel(MatchTeamId);
                if (MatchCodeModel != null)
                {
                    MatchCode = MatchCodeModel.MatchCode;
                    MatchCodeModel = null;
                    var m = rdc.tbl_MatchTeam.FirstOrDefault(n => n.Id == MatchTeamId);
                    if (m != null)
                    {
                        m.IsBallot = true;
                        m.BallotTime = DateTime.Now;
                        rdc.SubmitChanges();
                        #region 写入消息中心
                        var TeamModel = BTeam.GetModel(m.TeamId);
                        if (TeamModel != null)
                        {
                            //队长消息
                            BMessage.Add(new tbl_Message
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                TypeId = (int)Model.EnumType.消息类型.系统消息,
                                SendId = "0",
                                SendName = "铁子帮",
                                SendTime = DateTime.Now,
                                ReceiveId = TeamModel.MemberId,
                                ReceiveName = TeamModel.MemberName,
                                MasterMsgId = "0",
                                MsgTitle = CacheSysMsg.GetMsg(132),
                                MsgInfo = string.Format(CacheSysMsg.GetMsg(133), MatchCode),
                                IsRead = false,
                                IssueTime = DateTime.Now
                            });
                            //所有成员发消息
                            var list2 = BTeamMember.GetList(m.TeamId);
                            if (list2 != null)
                            {
                                foreach (var TMModel in list2)
                                {
                                    if (TMModel.RoleType != (int)Model.EnumType.球员角色.队长)
                                    {
                                        BMessage.Add(new tbl_Message
                                        {
                                            Id = System.Guid.NewGuid().ToString(),
                                            TypeId = (int)Model.EnumType.消息类型.系统消息,
                                            SendId = "0",
                                            SendName = "铁子帮",
                                            SendTime = DateTime.Now,
                                            ReceiveId = TMModel.MemberId,
                                            ReceiveName = TMModel.ContactName,
                                            MasterMsgId = "0",
                                            MsgTitle = CacheSysMsg.GetMsg(132),
                                            MsgInfo = string.Format(CacheSysMsg.GetMsg(133), MatchCode),
                                            IsRead = false,
                                            IssueTime = DateTime.Now
                                        });
                                    }
                                }
                            }
                        }
                        #endregion
                        BMatchSchedule.UpdateBallot(MatchTeamId);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 参赛球队是否抽签
        /// </summary>
        /// <param name="MatchTeamId"></param>
        /// <returns></returns>
        public static bool IsBallot(string MatchTeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MatchTeam.FirstOrDefault(n => n.Id == MatchTeamId);
                if (m != null)
                {
                    if (m.IsBallot)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(tbl_MatchTeam model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_MatchTeam.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 判断参赛球队是否分配赛程编号
        /// </summary>
        /// <param name="GameId">赛程</param>
        /// <param name="MatchTeamId">球队参赛编号</param>
        /// <returns></returns>
        public static tbl_MatchTeamCode GetFirtstModel(string MatchTeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.tbl_MatchTeamCode
                            where q.MatchTeamId == MatchTeamId
                            orderby q.IssueTime ascending
                            select q;
                var m = query.FirstOrDefault();
                return m;
            }
        }
        /// <summary>
        /// 判断参赛球队是否分配赛程编号
        /// </summary>
        /// <param name="GameId">赛程</param>
        /// <param name="MatchTeamId">球队参赛编号</param>
        /// <returns></returns>
        public static tbl_MatchTeamCode GetModel(string GameId, string MatchTeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MatchTeamCode.FirstOrDefault(n => n.GameId == GameId && n.MatchTeamId == MatchTeamId);
                return m;
            }
        }
    }
}
