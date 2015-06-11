using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 赛事球场
    /// </summary>
    public class BMatchField
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_MatchField model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.esp_MatchField_ADD(model.Id,model.MatchId,model.FieldId,model.TeamNumber);
                    rdc.SubmitChanges();
                    return true;

                }
                else
                {
                    return false;
                }

            }

        }
        /// <summary>
        /// 修改赛事信息
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_MatchField model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MatchField.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.FieldName = model.FieldName;
                    m.FieldAddress = model.FieldAddress;
                    m.MarketPrice = model.MarketPrice;
                    m.FieldPrice = model.FieldPrice;
                    m.TeamNumber = model.TeamNumber;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 删除赛事球场
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(string MatchId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.tbl_MatchField
                            where q.MatchId == MatchId
                            select q;
                if (query.Count() > 0)
                {
                    rdc.tbl_MatchField.DeleteAllOnSubmit(query);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 赛事列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_MatchField> GetList(string MatchId)
        {

            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.tbl_MatchField
                            where q.MatchId == MatchId
                            select q;
                return query.ToList();
            }
        }

        /// <summary>
        /// 取得赛事信息列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_MatchField GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MatchField.FirstOrDefault(n => n.FieldId == Id);
                return model;
            }
        }
        /// <summary>
        /// 取得赛事球场参赛队伍数
        /// </summary>
        /// <param name="MatchId"></param>
        /// <returns></returns>
        public static int GetTeamNumber(string MatchId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MatchField.FirstOrDefault(n => n.MatchId == MatchId);
                if (model != null)
                {
                    return model.TeamNumber;
                }
                else
                { return 0; }
            }
        }
    }
}
