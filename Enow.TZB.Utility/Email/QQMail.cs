using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Enow.TZB.Utility.Email
{
    public class QQMail
    {
        /// <summary>
        /// 向用户发送邮件
        /// </summary>
        /// <param name="receiveUser">接收邮件的用户</param>
        /// <param name="sendUser">发送者</param>
        /// <param name="sendusername">发送者的邮箱登陆名</param>
        /// <param name="userpassword">发送者的登陆密码</param>
        /// <param name="sendTitle">发送标题</param>
        /// <param name="sendContent">发送的内容</param>
        public static void sendMail(string receiveUser, string sendUser, string sendusername, string userpassword, string sendTitle, string sendContent)
        {

            MailAddress to = new MailAddress(receiveUser);//接收者邮箱
            MailAddress from = new MailAddress(sendUser);//发送者邮箱
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(from, to);
            mail.Subject = sendTitle;
            mail.IsBodyHtml = true;
            mail.Body = sendContent;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.exmail.qq.com";//设置发送者邮箱对应的smtpserver
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(sendusername, userpassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
        }
    }
}
