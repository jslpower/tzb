using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Collections;

namespace Enow.TZB.Utility
{
    public class UtilsCommons
    {

        #region 获取分页页索引
        /// <summary>
        /// 获取分页页索引，url页索引查询参数为Page
        /// </summary>
        /// <returns></returns>
        public static int GetPadingIndex()
        {
            return GetPadingIndex("Page");
        }

        /// <summary>
        /// 获取分页页索引
        /// </summary>
        /// <param name="s">url页索引查询参数</param>
        /// <returns></returns>
        public static int GetPadingIndex(string s)
        {
            int index = Utils.GetInt(Utils.GetQueryStringValue(s), 1);

            return index < 1 ? 1 : index;
        }
        #endregion

        #region 获取URI，拼接sl查询参数
        /// <summary>
        /// 获取URI，拼接 菜单参数
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetMenuUri(string uri)
        {
            string MId = Utils.GetQueryStringValue("MId");
            string SMId = Utils.GetQueryStringValue("SMId");
            string CId = Utils.GetQueryStringValue("CId");
            if (uri.IndexOf('?') > -1) return uri + "&MId=" + MId + "&SMId=" + SMId + "&CId=" + CId;
            return uri + "?MId=" + MId + "&SMId=" + SMId + "&CId=" + CId;
        }
        /// <summary>
        /// 获取URI，拼接sl查询参数
        /// </summary>
        /// <param name="uri">uri</param>
        /// <returns></returns>
        public static string GetSLUri(string uri)
        {
            return GetSLUri(uri, Utils.GetQueryStringValue("sl"));
        }

        /// <summary>
        /// 获取URI，拼接sl查询参数
        /// </summary>
        /// <param name="uri">uri</param>
        /// <param name="sl">sl查询参数</param>
        /// <returns></returns>
        public static string GetSLUri(string uri, string sl)
        {
            if (string.IsNullOrEmpty(sl)) sl = Utils.GetQueryStringValue("sl");
            if (string.IsNullOrEmpty(uri)) return "?sl=" + sl;

            if (uri.IndexOf('?') > -1) return uri + "&sl=" + sl;

            return uri + "?sl=" + sl;
        }
        #endregion

        #region ajax request,response josn data.  create by cyn
        /// <summary>
        /// ajax request,response josn data
        /// </summary>
        /// <param name="retCode">return code</param>
        /// <returns></returns>
        public static string AjaxReturnJson(string retCode)
        {
            return AjaxReturnJson(retCode, string.Empty);
        }
        /// <summary>
        /// ajax request,response josn data
        /// </summary>
        /// <param name="retCode">return code</param>
        /// <param name="retMsg">return msg</param>
        /// <returns></returns>
        public static string AjaxReturnJson(string retCode, string retMsg)
        {
            return AjaxReturnJson(retCode, retMsg, null);
        }

        /// <summary>
        /// ajax request,response josn data
        /// </summary>
        /// <param name="retCode">return code</param>
        /// <param name="retMsg">return msg</param>
        /// <param name="retObj">return object</param>
        /// <returns></returns>
        public static string AjaxReturnJson(string retCode, string retMsg, object retObj)
        {
            string output = "{}";

            if (retObj != null)
            {
                output = Newtonsoft.Json.JsonConvert.SerializeObject(retObj);
            }

            return string.Format("{{\"result\":\"{0}\",\"msg\":\"{1}\",\"obj\":{2}}}", retCode, retMsg, output);
        }
        #endregion

        /// <summary>
        /// 是否导出列表
        /// </summary>
        /// <returns></returns>
        public static bool IsToXls()
        {
            return Utils.GetQueryStringValue("toxls") == "1";
        }

        /// <summary>
        /// 获取导出列表导出的记录数
        /// </summary>
        /// <returns></returns>
        public static int GetToXlsRecordCount()
        {
            return Utils.GetInt(Utils.GetQueryStringValue("toxlsrecordcount"));
        }

        /// <summary>
        /// 处理隐藏密码字符串
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public static string GetHiddeString(string Phone)
        {
            if (!string.IsNullOrEmpty(Phone) && Phone.Length > 8)
            {
                string FString = Phone.Substring(0, 3);
                string LString = Phone.Substring(Phone.Length - 4, 4);
                return FString + "****" + LString;
            }
            return string.Empty;
        }
        /// <summary>
        /// 取得指定长度的内容
        /// </summary>
        /// <param name="v"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public static string GetStringSubstring(string v, int lenght,bool IsPoint)
        {
            if (!string.IsNullOrEmpty(v))
            {
                if (v.Length > lenght)
                {
                    if (IsPoint)
                        return v.Substring(0, lenght) + "...";
                    else
                    return v.Substring(0, lenght);
                }
                else
                {
                    return v;
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 分页参数处理
        /// </summary>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        public static void Paging(int pageSize, ref int pageIndex, int recordCount)
        {
            if (pageSize < 1) pageSize = 1;
            int pageCount = recordCount / pageSize;
            if (recordCount % pageSize > 0) pageCount++;
            if (pageIndex > pageCount) pageIndex = pageCount;
            if (pageIndex < 1) pageIndex = 1;
        }

        #region 泛型转Datatable

        /// <summary>
        /// 泛型转换DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                if (info.PropertyType.IsGenericType && info.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    dt.Columns.Add(new DataColumn(info.Name, info.PropertyType.GetGenericArguments()[0]));
                }
                else
                {
                    dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
                }
               
            }
            if (list != null && list.Count > 0)
            {
                foreach (T t in list)
                {
                    DataRow row = dt.NewRow();
                    foreach (PropertyInfo info in typeof(T).GetProperties())
                    {
                        row[info.Name] = info.GetValue(t, null);
                    }
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        #endregion
    }
}
