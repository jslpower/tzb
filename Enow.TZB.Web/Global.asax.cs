using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Timers;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;

namespace Enow.TZB.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                //记录系统错误日志
                Exception Err = Server.GetLastError().GetBaseException();
                string ErrMsg = "<strong>错误发生地址:</strong>" + Request.Url.ToString() + "<br>" + "<strong>错误标题:</strong>" + Err.Message.ToString().Trim() + "<br><strong>发生错误对象名:</strong>" + Err.Source.ToString().Trim() + "<br><strong>错误堆栈内容:</strong><br>" + Err.StackTrace.ToString() +"实例:"+ Err.InnerException;
                WLog("发生IP:" + Enow.TZB.Utility.StringValidate.GetRemoteIP() + ",错误信息" + Err.Message.ToString() + ",错误发生地址：" + Request.Url.ToString() + "\n\n", "/Log/Error.txt");
            }
            catch
            {
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="s">内容</param>
        /// <param name="path">相对路径，确保目录实际存在</param>
        private void WLog(string s, string path)
        {
            string fPath = Server.MapPath(path);
            string extension = System.IO.Path.GetExtension(fPath);
            if (!string.IsNullOrEmpty(extension))
            {
                fPath = fPath.Substring(0, fPath.LastIndexOf(extension)) + "." + DateTime.Today.ToString("yyyyMMdd") + extension;
            }
            if (!File.Exists(fPath))
            {
                FileStream fs = File.Create(fPath);
                fs.Close();
            }
            try
            {
                var sw = new StreamWriter(fPath, true, System.Text.Encoding.UTF8);
                sw.Write(DateTime.Now.ToString("o") + "\t" + s + "\r\n");
                sw.Close();
            }
            catch { }
        }
    }
}