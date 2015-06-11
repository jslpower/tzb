using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Enow.TZB.Utility
{
    /// <summary>
    /// 创建Email实体对象
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// 发件人名称
        /// </summary>
        protected string _sendername = "";
        /// <summary>
        /// 邮件主题
        /// </summary>
        protected string _subject = string.Empty;
        /// <summary>
        /// 邮件内容
        /// </summary>
        protected string _body = string.Empty;
        /// <summary>
        /// 邮件接收地址
        /// </summary>
        protected string _receiveaddress = string.Empty;

        /// <summary>
        /// 发件人名称
        /// </summary>
        public string SenderName
        {
            get { return this._sendername; }
            set { this._sendername = value; }
        }
        /// <summary>
        /// 是否使用安全套接字层 (SSL) 加密连接
        /// </summary>
        protected bool IsEnableSSL
        {
            get
            {
                bool isenablessl = false;
                Boolean.TryParse(Enow.TZB.Utility.Function.UtilsCommons.GetConfigString("MailSettings", "IsEnableSSL"), out isenablessl);
                return isenablessl;
            }
        }
        /// <summary>
        /// (SSL) 端口
        /// </summary>
        protected int SSLPort
        {
            get
            {
                int port = 0;
                int.TryParse(Enow.TZB.Utility.Function.UtilsCommons.GetConfigString("MailSettings", "SSLPort"), out port);
                return port;
            }
        }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject
        {
            get { return this._subject; }
            set { this._subject = value; }
        }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body
        {
            get { return this._body; }
            set { this._body = value; }
        }
        /// <summary>
        /// 邮件接收地址
        /// </summary>
        public string ReceiveAddress
        {
            get { return this._receiveaddress; }
            set { this._receiveaddress = value; }
        }
        /// <summary>
        /// 邮件发送地址
        /// </summary>
        protected string SendAddress
        {
            get
            {
                return Enow.TZB.Utility.Function.UtilsCommons.GetConfigString("MailSettings", "SendAddress");
            }
        }
        /// <summary>
        /// 可以使用 System.Net.Mail.SmtpClient 类发送的电子邮件
        /// </summary>
        protected MailMessage _mail = new MailMessage();

        /// <summary>
        /// 初始化系统信息
        /// </summary>
        protected virtual void InitSystem()
        {
            this._mail.SubjectEncoding = Encoding.GetEncoding("gb2312");
            this._mail.From = new MailAddress(this.SendAddress, this.SenderName, Encoding.GetEncoding("UTF-8"));
            this._mail.IsBodyHtml = true;
            this._mail.BodyEncoding = Encoding.UTF8;

        }
        /// <summary>
        /// 创建发送的消息
        /// </summary>
        /// <returns></returns>
        protected virtual MailMessage CreateMessage()
        {
            this.InitSystem();

            this._mail.Subject = this.Subject;
            this._mail.Body = this.Body;
            this._mail.To.Clear();
            this._mail.To.Add(this.ReceiveAddress);
            //this._mail.ReplyTo = new MailAddress("zhangzy@enowinfo.com");  
            return this._mail;
        }
        /// <summary>
        /// 添加电子邮件附件
        /// </summary>
        /// <param name="item">电子邮件附件</param>
        public void AddAttachment(Attachment item)
        {
            if (item != null)
                this._mail.Attachments.Add(item);
        }
        /// <summary>
        /// 将字符按照某种附件类型添加到邮件附件中
        /// </summary>
        /// <param name="content">字符串内容</param>
        /// <param name="attachmentName">附件名称[请不要包含非法字符]</param>
        /// <param name="type">附件类型</param>
        public void AddAttachmentByString(string content, string attachmentName, AttachmentType type)
        {
            if (string.IsNullOrEmpty(content))
                return;
            System.IO.Stream stream = new System.IO.MemoryStream(System.Text.Encoding.Default.GetBytes(content));
            string mediaType = "";
            switch (type)
            {
                case AttachmentType.Doc:
                    attachmentName += ".doc";
                    mediaType = "application/msword";
                    break;
                case AttachmentType.Html:
                    attachmentName += ".html";
                    mediaType = "text/html";
                    break;
            }
            this._mail.Attachments.Add(new System.Net.Mail.Attachment(stream, attachmentName, mediaType));
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        public bool Send()
        {
            if (string.IsNullOrEmpty(this.ReceiveAddress) || string.IsNullOrEmpty(this.SendAddress))
            {
                return false;
            }
            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = this.IsEnableSSL;
            int port = this.SSLPort;
            if(port>0)
                smtp.Port = port;
            try
            {
                smtp.Send(this.CreateMessage());
                return true;
            }
            catch (System.Exception ex)
            {
                //log ex，可对发送失败的邮件进行记录，以便对邮件发送程序进行跟踪记录
                //System.IO.StreamWriter wrlog = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath("maillog.txt"));
                //wrlog.WriteLine(ex.Message);
                //wrlog.WriteLine(ex.InnerException.ToString());
                //wrlog.WriteLine("--------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "----");
                //wrlog.Flush();
                //wrlog.Close();
                //wrlog.Dispose();
                //wrlog = null;
                return false;
            }
        }
    }

    /// <summary>
    /// 附件类型
    /// </summary>
    public enum AttachmentType
    {
        /// <summary>
        /// doc文档
        /// </summary>
        Doc,
        /// <summary>
        /// html文件
        /// </summary>
        Html
    }
}
