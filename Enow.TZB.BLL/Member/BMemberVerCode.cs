using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    public class BMemberVerCode
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(tbl_MemberVerCode model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_MemberVerCode.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 统计号码一天注册验证码发送次数
        /// </summary>
        /// <param name="MobilePhone"></param>
        /// <returns></returns>
        public static int TodayVerCodeSendCount(string MobilePhone)
        {
            using (FWDC rdc = new FWDC())
            {
                int Count = 0;
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM tbl_MemberVerCode WHERE MobilePhone='" + MobilePhone + "' AND IssueTime>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00'");
                Count = query.First<int>();
                return Count;
            }
        }
        /// <summary>
        /// 统计号码一天发送次数
        /// </summary>
        /// <param name="MobilePhone"></param>
        /// <returns></returns>
        public static int TodaySendCount(string MobilePhone)
        {
            using (FWDC rdc = new FWDC())
            {
                int Count = 0;
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM tbl_SMSLog WHERE MobilePhone='" + MobilePhone + "' AND SendTime>'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                Count = query.First<int>();
                return Count;
            }
        }
        /// <summary>
        /// 获取验证码相关信息
        /// </summary>
        /// <param name="Phone">手机号码</param>
        /// <param name="verCode">验证码</param>
        /// <param name="IsUsed">是否使用</param>
        /// <returns></returns>
        public static tbl_MemberVerCode GetInfo(string Phone, string verCode, int IsUsed)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from q in rdc.tbl_MemberVerCode
                             where q.MobilePhone == Phone && q.VerCode == verCode && q.IsUsed == false
                             orderby q.IssueTime descending
                             select q).FirstOrDefault();
                return model;
            }
        }
    }
}
