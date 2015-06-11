using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Enow.TZB.Utility
{
    /// <summary>
    /// WebConfig Appsetting 节点操作
    /// </summary>
    public class AppsettingConfigUtils
    {
        ///<summary>  
        ///在*.config文件中appSettings配置节增加一对键、值对  
        ///</summary>  
        ///<param name="newKey"></param>  
        ///<param name="newValue"></param>  
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                }
            }

            string XPath = System.Web.HttpContext.Current.Server.MapPath("~/Web.config");
            Configuration config =ConfigurationManager.OpenExeConfiguration(XPath);
            // 如果操作的Key 已存在，则删除
            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            // 在配置文件中添加节点
            config.AppSettings.Settings.Add(newKey, newValue);
            // 修改配置文件后还需要保存
            config.Save(ConfigurationSaveMode.Modified);
            // 重置配置文件，如果不执行这行语句，已更新的配置文件不会得到应用
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
