using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BAboutWarReport
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">实体</param>
        public static bool Add(tbl_AboutWarReport model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_AboutWarReport.InsertOnSubmit(model);
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
        /// 删除
        /// </summary>
        /// <param name="listid">ID列表</param>
        /// <returns></returns>
        public static bool Delete(List<string> listid)
        {
            using (FWDC rdc = new FWDC())
            {
                var liid = listid;
                var m = (from tmp in rdc.tbl_AboutWarReport where liid.Contains(tmp.Wid.ToString()) select tmp).ToList();
                if (m.Count > 0)
                {
                    for (int i = 0; i < m.Count; i++)
                    {
                        m[i].RIsDelete = 1;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        public static tbl_AboutWarReport GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_AboutWarReport.FirstOrDefault(n => n.Wid == Id);
                return model;
            }
        }
        /// <summary>
        /// 根据约战编号和球队编号查询是否已报名
        /// </summary>
        /// <param name="AId">约战编号</param>
        /// <param name="QdId">球队编号</param>
        /// <returns></returns>
        public static bool Getboolyuezhan(string AId,string QdId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_AboutWarReport.FirstOrDefault(n => n.TeamId == QdId && n.AboutWarID == AId && n.Wtypes==2);
                if (model!=null)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 确认是否已经报名约战
        /// </summary>
        /// <param name="tmp">实体</param>
        /// <returns></returns>
        public static bool GetTeamYZ(tbl_AboutWarReport tmp)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_AboutWarReport.FirstOrDefault(n => n.Wid == tmp.Wid&&n.Wtypes==2&&n.Wstates==(int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.待确认&&n.AboutWarID==tmp.AboutWarID);
                if (model != null)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 根据ID修改对战状态
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        public static bool UpdateModel(tbl_AboutWarReport tmp)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_AboutWarReport.FirstOrDefault(n => n.Wid == tmp.Wid);
                if (model!=null)
                {
                    model.Wstates = tmp.Wstates;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 根据约战ID把客队状态为待确认的修改为应战被拒
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        public static bool UpdateKdstate(tbl_AboutWarReport tmp)
        {
            using (FWDC rdc = new FWDC())
            {
                var model =(from m in rdc.tbl_AboutWarReport where m.AboutWarID==tmp.AboutWarID && m.Wtypes==2&&m.Wstates==(int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.待确认 select m).ToList();
                if (model != null)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        model[i].Wstates = (int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.应战被拒;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 根据约战ID把客队状态为待确认的修改为进行中
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        public static bool Updatejxzall(string AboutWarID)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from m in rdc.tbl_AboutWarReport where m.AboutWarID == AboutWarID select m).ToList();
                if (model != null)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        model[i].Wstates = (int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.进行中;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 根据约战ID修改主队状态
        /// </summary>
        /// <param name="Aid">约战ID</param>
        /// <param name="states">状态</param>
        /// <returns></returns>
        public static bool Updatezdstate(string Aid,int states)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_AboutWarReport.FirstOrDefault(n => n.AboutWarID == Aid&&n.Wtypes==1&&n.Wstates==(int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.约战中);
                if (model != null)
                {
                    model.Wstates = states;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 根据约战ID同时修改主客队状态
        /// </summary>
        /// <param name="Id">ID</param>
        /// <param name="ztnum">状态</param>
        /// <returns></returns>
        public static bool UpdateKdstate(string Id,int ztnum)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from m in rdc.tbl_AboutWarReport where m.AboutWarID == Id select m).ToList(); rdc.tbl_AboutWarReport.FirstOrDefault(n => n.AboutWarID == Id);
                if (model.Count>0)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        model[i].Wstates = ztnum;
                    }
                    
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
