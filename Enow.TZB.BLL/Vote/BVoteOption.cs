using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BVoteOption
    {
        /// <summary>
        /// 添加投票选项信息
        /// </summary>
        /// <param name="model">添加实体</param>
        public static bool Add(tbl_VoteOption model)
        {
            using (FWDC rdc = new FWDC())
            {

                rdc.tbl_VoteOption.InsertOnSubmit(model);
                rdc.SubmitChanges();
                return true;

            }
        }
        /// <summary>
        /// 删除投票信息
        /// </summary>
        /// <param name="DelId">要删除的ID</param>
        /// <returns></returns>
        public static bool Delete(List<string> DelIds)
        {
            using (FWDC rdc = new FWDC())
            {
                var listid = DelIds;
                var m = (from tmp in rdc.tbl_VoteOption where listid.Contains(tmp.Oid.ToString()) select tmp).ToList();
                if (m.Count>0)
                {
                    for (int i = 0; i < m.Count; i++)
                    {
                        m[i].OptIsDelete = 1;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 修改投票选项投票数
        /// </summary>
        /// <param name="OpId">编号</param>
        /// <returns></returns>
        public static bool UpdateNum(string OpId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_VoteOption.FirstOrDefault(w => w.Oid == OpId&&w.OptIsDelete==0);
                if (m != null)
                {
                    m.ONumber +=1;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 修改投票选项信息
        /// </summary>
        /// <param name="model">修改的实体</param>
        /// <returns></returns>
        public static bool Update(tbl_VoteOption model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_VoteOption.FirstOrDefault(w => w.Oid == model.Oid);
                if (m != null)
                {
                    m.Otitle = model.Otitle;
                    m.ONumber = model.ONumber;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 查询投票选项信息
        /// </summary>
        /// <param name="SelId">投票表ID</param>
        /// <returns></returns>
        public static List<tbl_VoteOption> GettypList(string SelId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = (from tmp in rdc.tbl_VoteOption where tmp.OptIsDelete==0&& tmp.VoteId == SelId orderby tmp.SortNum ascending  select tmp).ToList();
                return m;
            }
        }
    }
}
