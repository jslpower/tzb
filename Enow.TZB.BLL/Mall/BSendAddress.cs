using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BSendAddress
    {
        /// <summary>
        /// 新增配送信息
        /// </summary>
        /// <param name="model"></param>
        public static bool Add(tbl_SendAddress  model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_SendAddress.InsertOnSubmit(model);
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
        /// 修改配送信息
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_SendAddress model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SendAddress.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    if (!string.IsNullOrEmpty(model.LogisticsNo))
                    {
                        m.LogisticsNo = model.LogisticsNo;
                        
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改配送信息
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdateModel(tbl_SendAddress model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SendAddress.FirstOrDefault(n => n.Id == model.Id&&n.MemberId==model.MemberId);
                if (m != null)
                {
                    m.Recipient = model.Recipient;
                    m.MobilePhone = model.MobilePhone;
                    m.CountyId = model.CountyId;
                    m.CountyName = model.CountyName;
                    m.ProvinceId = model.ProvinceId;
                    m.ProvinceName = model.ProvinceName;
                    m.CityId = model.CityId;
                    m.CityName = model.CityName;
                    m.AreaId = model.AreaId;
                    m.AreaName = model.AreaName;
                    m.Address = model.Address;
                    m.IsDefaultAddress = model.IsDefaultAddress;
                    m.MemberId = model.MemberId;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 获取配送信息实体
        /// </summary>
        /// <param name="Id">配送地址ID</param>
        /// <returns></returns>
        public static tbl_SendAddress  GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SendAddress.FirstOrDefault(n => n.Id == Id );
                return model;
            }
        }
        /// <summary>
        /// 根据配送地址ID 用户ID查询实体
        /// </summary>
        /// <param name="Id">配送地址ID</param>
        /// <param name="Usid">用户编号</param>
        /// <returns></returns>
        public static tbl_SendAddress GetSendIdUserIDModel(string Id, string Usid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SendAddress.FirstOrDefault(n => n.Id == Id && n.MemberId == Usid);
                return model;
            }
        }
        /// <summary>
        /// 取消指定用户所有默认地址
        /// </summary>
        /// <param name="Usid">用户编号</param>
        /// <returns></returns>
        public static void UpUserAddressDefault(string Usid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from tmp in rdc.tbl_SendAddress where tmp.MemberId==Usid select tmp).ToList();
                if (model.Count>0)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        model[i].IsDefaultAddress = false;
                    }
                    rdc.SubmitChanges();
                }
            }
        }
        /// <summary>
        /// 获取配送信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static List<tbl_SendAddress> GetDefaultAddress(string memberid,bool isDefault)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from temp in rdc.tbl_SendAddress where temp.MemberId == memberid && temp.IsDefaultAddress == isDefault select temp);
                return model.ToList();
            }
           
        }
        /// <summary>
        /// 根据获取会员默认地址实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_SendAddress GetDefaultAddress(string memberid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SendAddress.FirstOrDefault(w => w.MemberId == memberid && w.IsDefaultAddress == true);
                return model;
            }

        }
        /// <summary>
        /// 根据会员ID重置默认地址
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static void UpdateDefaultAddress(string memberid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from temp in rdc.tbl_SendAddress where temp.MemberId == memberid select temp).ToList();
                if (model.Count > 0)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        model[i].IsDefaultAddress = false;
                    }
                    rdc.SubmitChanges();
                }
                
            }

        }
        /// <summary>
        /// 根据编号改变默认地址
        /// </summary>
        /// <param name="SjdzId">配送地址ID</param>
        /// <param name="memberid">会员ID</param>
        /// <returns></returns>
        public static bool UpdateMrdz(string SjdzId, string memberid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SendAddress.FirstOrDefault(w => w.MemberId == memberid && w.Id == SjdzId);
                if (model!=null)
                {
                    model.IsDefaultAddress = true;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 根据联系人等信息获取配送地址
        /// </summary>
        /// <param name="SjdzId">配送地址ID</param>
        /// <param name="charid">会员ID</param>
        /// <returns></returns>
        public static tbl_SendAddress GetSendmodel(tbl_SendAddress Model)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SendAddress.FirstOrDefault(w => w.MemberId == Model.MemberId && w.MobilePhone == Model.MobilePhone && w.Recipient == Model.Recipient && w.Address == Model.Address && w.CountyId == Model.CountyId && w.ProvinceId == Model.ProvinceId && w.CityId == Model.CityId && w.AreaId == Model.AreaId);
                if (model != null)
                {
                    return model;
                }
                return null;
            }
        }
        /// <summary>
        /// 新增配送信息
        /// </summary>
        /// <param name="adderid"></param>
        public static bool Deladdress(string adderid)
        {
            using (FWDC rdc = new FWDC())
            {
                var temp = rdc.tbl_SendAddress.Where(w => w.Id == adderid).SingleOrDefault();
                if (temp != null)
                {
                    rdc.tbl_SendAddress.DeleteOnSubmit(temp);
                    rdc.SubmitChanges();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 删除配送信息(逻辑删除)
        /// </summary>
        /// <param name="adderid">配送信息ID</param>
        /// <param name="usid">会员ID</param>
        public static bool Deletebol(string adderid,string usid)
        {
            using (FWDC rdc = new FWDC())
            {
                var temp = rdc.tbl_SendAddress.Where(w => w.Id == adderid && w.MemberId == usid).SingleOrDefault();
                if (temp != null)
                {
                    temp.IsDelete = 1;
                    rdc.SubmitChanges();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 配送信息列表(无分页)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static List<tbl_SendAddress> GetMemberIdList(string memberid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from temp in rdc.tbl_SendAddress where temp.MemberId == memberid && temp.IsDelete==0 select temp).ToList();
                if (model.Count > 0)
                {
                    return model;
                }
                return null;

            }

        }
        /// <summary>
        /// 配送信息列表（分页）
        /// </summary>
        /// <returns></returns>
        public static List<tbl_SendAddress> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, tbl_SendAddress SearchModel)
        {
            string FieldsList = "*";
            string TableName = " tbl_SendAddress ";
            string OrderString = " ORDER BY IsDefaultAddress desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            strWhere.Append(" AND (IsDelete=0)");
            #region 查询条件
            if (!string.IsNullOrEmpty(SearchModel.MemberId))
                strWhere.Append(" AND ( MemberId='" + SearchModel.MemberId + "')");
            #endregion

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<tbl_SendAddress> q = rdc.ExecuteQuery<tbl_SendAddress>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }
}
