using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;

namespace Enow.TZB.Utility.Function
{
    public class UtilsCommons
    {
        #region 获取枚举下拉菜单 create by cyn
        /// <summary>
        ///  获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls)
        {
            return GetEnumDDL(ls, string.Empty);
        }
        /// <summary>
        ///  获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, object selectedVal)
        {
            if (selectedVal == null)
            {
                return GetEnumDDL(ls);
            }
            else
            {
                return GetEnumDDL(ls, selectedVal.ToString());
            }

        }
        /// <summary>
        /// 获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选择value值</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal)
        {
            return GetEnumDDL(ls, selectedVal ?? "-1", false);

        }
        /// <summary>
        /// 获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选择value值</param>
        /// <param name="isFirst">是否存在默认请选择项</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal, bool isFirst)
        {

            return GetEnumDDL(ls, selectedVal, isFirst, "-1", "-请选择-");
        }
        /// <summary>
        /// 获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选中的值</param>
        /// <param name="defaultKey">默认值Id</param>
        /// <param name="defaultVal">默认值文本</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal, string defaultKey, string defaultVal)
        {

            return GetEnumDDL(ls, selectedVal, true, defaultKey, defaultVal);
        }
        /// <summary>
        /// 获取枚举下拉菜单(该方法isFirst为否则后2个属性无效)
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选中的值</param>
        /// <param name="isFirst">是否有默认值</param>
        /// <param name="defaultKey">默认值Id</param>
        /// <param name="defaultVal">默认值文本</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal, bool isFirst, string defaultKey, string defaultVal)
        {
            StringBuilder sb = new StringBuilder();
            if (isFirst)
            {
                sb.Append("<option value=\"" + defaultKey + "\" selected=\"selected\">" + defaultVal + "</option>");
            }

            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Value != selectedVal.Trim())
                {
                    sb.Append("<option value=\"" + ls[i].Value.Trim() + "\">" + ls[i].Text.Trim() + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + ls[i].Value.Trim() + "\" selected=\"selected\">" + ls[i].Text.Trim() + "</option>");
                }
            }
            return sb.ToString();
        }
        #endregion

        #region ajax request,response josn data.  create by cyn
        /// <summary>
        /// ajax request,response josn data
        /// </summary>
        /// <param name="retCode">return code</param>
        /// <returns></returns>
        /// 
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

        #region 通用获取指定节点值
        /// <summary>
        /// 取得配置文件中的字符串KEY
        /// </summary>
        /// <param name="SectionName">节点名称</param>
        /// <param name="key">KEY名</param>
        /// <returns>返回KEY值</returns>
        public static string GetConfigString(string SectionName, string key)
        {
            string returnVal = "";
            if (SectionName != "")
            {
                try
                {
                    NameValueCollection cfgName = (NameValueCollection)ConfigurationManager.GetSection(SectionName);
                    if (cfgName[key] != null)
                    {
                        returnVal = cfgName[key];
                    }
                    cfgName = null;
                }
                catch
                { }
            }
            return returnVal;
        }
        #endregion
    }
}
