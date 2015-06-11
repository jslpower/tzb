using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class Bcode
    {
        public static List<dt_MemberView> Getuslist()
        {
            using (FWDC rdc = new FWDC())
            {
                var temp = (from tmp in rdc.dt_MemberView where tmp.State==1 select tmp).ToList();
                return temp;
            }
        }
        /// <summary>
        /// 批量添加码
        /// </summary>
        /// <param name="model"></param>
        public static bool Add(string modellist)
        {
            using (FWDC rdc = new FWDC())
            {
                int? rtcode=0;
                rdc.esp_Code_Insert(modellist, ref rtcode);
                rdc.SubmitChanges();
                if (rtcode==1)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 添加码
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_CodelModel model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_CodelModel.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 删除码
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(List<string> listid)
        {
            using (FWDC rdc = new FWDC())
            {
                var liid=listid;
                var m = (from tmp in rdc.tbl_CodelModel where liid.Contains(tmp.id.ToString()) select tmp).ToList();
                if (m.Count>0)
                {
                    for (int i = 0; i < m.Count; i++)
                    {
                        m[i].IsDelete = 0;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 根据ID修改码
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_CodelModel model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_CodelModel.FirstOrDefault(n => n.id == model.id);
                if (m != null)
                {
                    if (!string.IsNullOrEmpty(model.Usid))
                    {
                        m.Usid = model.Usid;
                        m.Usname = model.Usname;
                        m.Usnc = model.Usnc;
                        m.Ustel = model.Ustel;
                        m.Codestate = model.Codestate;
                        rdc.SubmitChanges();
                        return true;
                    }
                    
                }
                return false; 
            }
        }
        /// <summary>
        /// 根据码编号修改码
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdateIDmodel(tbl_CodelModel model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_CodelModel.FirstOrDefault(n => n.IsDelete == 1 && n.Codestate == 0 && n.Codenum == model.Codenum);
                if (m != null)
                {
                    if (!string.IsNullOrEmpty(model.Usid))
                    {
                        m.Usid = model.Usid;
                        m.Usname = model.Usname == null ? "" : model.Usname;
                        m.Usnc = model.Usnc == null ? "" : model.Usnc;
                        m.Ustel = model.Ustel == null ? "" : model.Ustel;
                        m.Codestate = model.Codestate;
                        rdc.SubmitChanges();
                        return true;
                    }

                }
                return false;
            }
        }
        /// <summary>
        /// 验证用户是否绑定码
        /// </summary>
        /// <param name="usid">用户编号</param>
        /// <param name="types">分类</param>
        /// <returns></returns>
        public static bool Getuscode(string usid,int types)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_CodelModel.FirstOrDefault(n => n.Usid == usid && n.Codetype == types && n.IsDelete == 1);
                if (model!=null)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 验证码是否可用
        /// </summary>
        /// <param name="codeid">码编号</param>
        /// <param name="types">分类</param>
        /// <returns></returns>
        public static bool Getcodeky(string codeid, int types)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_CodelModel.FirstOrDefault(n =>n.IsDelete == 1&&n.Codestate==0&&n.Codetype == types&& n.Codenum == codeid);
                if (model != null)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 取得码实体
        /// </summary>
        /// <param name="Id">码ID</param>
        /// <returns></returns>
        public static tbl_CodelModel GetModel(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_CodelModel.FirstOrDefault(n => n.id == Id);
                return model;
            }
        }
        /// <summary>
        /// 码是否存在
        /// </summary>
        /// <param name="Codenum">码编号</param>
        /// <param name="types">分类</param>
        /// <returns></returns>
        public static bool GetCodebool(string Codenum, int types)
        {
            using (FWDC rdc = new FWDC())
            {
                
                var model = rdc.tbl_CodelModel.FirstOrDefault(n => n.Codenum == Codenum && n.Codetype == types);
                if (model!=null)
                {
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 获取码分类列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_CodelModel> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, McodesQuery SearchModel)
        {
            string FieldsList = "*";
            string TableName = " tbl_CodelModel ";
            string OrderString = " ORDER BY id desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            if (SearchModel.Codetype!=0)
            {
                strWhere.Append(" AND ( Codetype=" + SearchModel.Codetype + ")");
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.Codenum))
            {
                strWhere.Append(" AND ( Codenum like '%" + SearchModel.Codenum + "%')");
            }
            if (SearchModel.Codestate != -1)
            {
                strWhere.Append(" AND ( Codestate=" + SearchModel.Codestate + ")");
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.Usnc))
            {
                 strWhere.Append(" AND ( Usnc like '%" + SearchModel.Usnc + "%')");
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.Usname))
            {
                strWhere.Append(" AND ( Usname like '%" + SearchModel.Usname + "%')");
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.Ustel))
            {
                strWhere.Append(" AND ( Ustel like '%" + SearchModel.Ustel + "%')");
            }
            strWhere.Append(" AND (IsDelete=1)");
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<tbl_CodelModel> q = rdc.ExecuteQuery<tbl_CodelModel>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }
}
