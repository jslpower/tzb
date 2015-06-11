using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Weixin.Mp.Sdk;
using Weixin.Mp.Sdk.Request;
using Weixin.Mp.Sdk.Response;
using Weixin.Mp.Sdk.Domain;
using System.Web.Script.Serialization;
using Weixin.Mp.Sdk.Util;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    #region 微信消息
    /// <summary>
    /// 微信消息
    /// </summary>
    public class BWeixinMsg
    {
        /// <summary>
        /// 微信媒体保存路径
        /// </summary>
        private static string WXMediaDownPath = System.Configuration.ConfigurationManager.AppSettings["WXMediaDownPath"];
        /// <summary>
        /// 获取微信消息实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_WxMsg GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_WxMsg.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 获取微信消息实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static dt_WxMsg GetViewModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_WxMsg.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
       /// 图文消息客服回复
       /// </summary>
       /// <param name="ToUserName">请求人</param>
       /// <returns></returns>
        public static bool NewsReply(string ToUserName, string Answer)
        {
            if (!String.IsNullOrWhiteSpace(Answer))
            {
                NewsCustomMessage replyMsg = new NewsCustomMessage()
                {
                    ToUser = ToUserName,
                    Articles = JsonConvert.DeserializeObject<List<NewsCustomMessageItem>>(Answer)
                };
                BWeiXin.SendNewsCustomMessage(replyMsg);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 文本留言回复
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="OperatorId"></param>
        /// <param name="RelpyName"></param>
        /// <param name="ReplyInfo"></param>
        /// <returns></returns>
        public static bool Reply(string Id,int OperatorId,string RelpyName,string ReplyInfo)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_WxMsg.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    m.IsReply = true;
                    m.ReplyOperatorId = OperatorId;
                    m.ReplyName = RelpyName;
                    m.ReplyInfo = ReplyInfo;
                    m.ReplyTime = DateTime.Now;
                    rdc.SubmitChanges();
                    //微信回复
                    BWeiXin.SendTextCustomMessage(m.FromUserName, ReplyInfo);
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="model"></param>
        public static bool Add(ReceiveMessageBase msg)
        {
            using (FWDC rdc = new FWDC())
            {
                string SavePath = HttpContext.Current.Server.MapPath(WXMediaDownPath);
                if (msg != null)
                {
                    switch (msg.MsgType)
                    {
                        case MsgType.UnKnown:
                             rdc.tbl_WxMsg.InsertOnSubmit(new tbl_WxMsg
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                MsgType = (int)msg.MsgType,
                                FromUserName = msg.FromUserName,
                                ToUserName = msg.ToUserName,
                                MessageBody = msg.MessageBody,
                                CreateTime = msg.CreateTime,
                                IssueTime = DateTime.Now
                            });
                            rdc.SubmitChanges();
                            break;
                        case MsgType.Text:
                            TextReceiveMessage m0 = msg as TextReceiveMessage;
                            rdc.tbl_WxMsg.InsertOnSubmit(new tbl_WxMsg
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                MsgType = (int)msg.MsgType,
                                FromUserName = msg.FromUserName,
                                ToUserName = msg.ToUserName,
                                MessageBody = msg.MessageBody,
                                TextMessageInfo = m0.Content,
                                CreateTime = msg.CreateTime,
                                IssueTime = DateTime.Now
                            });
                            rdc.SubmitChanges();
                            break;
                        case MsgType.Image:                            
                            ImageReceiveMessage m1 = msg as ImageReceiveMessage;
                            string MediaPath = BWeiXin.MediaDownloadTest(SavePath, m1.MediaId);
                            MediaPath = WXMediaDownPath + System.IO.Path.GetFileName(MediaPath);
                            rdc.tbl_WxMsg.InsertOnSubmit(new tbl_WxMsg
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                MsgType = (int)msg.MsgType,
                                FromUserName = msg.FromUserName,
                                ToUserName = msg.ToUserName,
                                MessageBody = msg.MessageBody,
                                CreateTime = msg.CreateTime,
                                MsgId = m1.MsgId,
                                MediaId = m1.MediaId,
                                PicUrl = m1.PicUrl,
                                MediaPath = MediaPath,
                                IssueTime = DateTime.Now
                            });
                            rdc.SubmitChanges();
                            break;
                        case MsgType.Link:
                            var m2 = msg as LinkReceiveMessage;
                            rdc.tbl_WxMsg.InsertOnSubmit(new tbl_WxMsg
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                MsgType = (int)msg.MsgType,
                                FromUserName = msg.FromUserName,
                                ToUserName = msg.ToUserName,
                                MessageBody = msg.MessageBody,
                                CreateTime = msg.CreateTime,
                                MsgId = m2.MsgId,
                                Description = m2.Description,
                                Title = m2.Title,
                                Url = m2.Url,
                                IssueTime = DateTime.Now
                            });
                            rdc.SubmitChanges();
                            break;
                        case MsgType.Location:
                            var m3 = msg as LocationReceiveMessage;
                            rdc.tbl_WxMsg.InsertOnSubmit(new tbl_WxMsg
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                MsgType = (int)msg.MsgType,
                                FromUserName = msg.FromUserName,
                                ToUserName = msg.ToUserName,
                                MessageBody = msg.MessageBody,
                                CreateTime = msg.CreateTime,
                                MsgId = m3.MsgId,
                                Label = m3.Label,
                                Location_X = m3.Location_X,
                                Location_Y = m3.Location_Y,
                                Scale = m3.Scale,
                                IssueTime = DateTime.Now
                            });
                            rdc.SubmitChanges();
                            break;
                        case MsgType.Video:
                            var m4 = msg as VideoReceiveMessage;
                            MediaPath = BWeiXin.MediaDownloadTest(SavePath, m4.MediaId);
                            MediaPath = WXMediaDownPath + System.IO.Path.GetFileName(MediaPath);
                            rdc.tbl_WxMsg.InsertOnSubmit(new tbl_WxMsg
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                MsgType = (int)msg.MsgType,
                                FromUserName = msg.FromUserName,
                                ToUserName = msg.ToUserName,
                                MessageBody = msg.MessageBody,
                                CreateTime = msg.CreateTime,
                                MsgId = m4.MsgId,
                                MediaId = m4.MediaId,
                                ThumbMediaId = m4.ThumbMediaId,
                                MediaPath = MediaPath,
                                IssueTime = DateTime.Now
                            });
                            rdc.SubmitChanges();
                            break;
                        case MsgType.Voice:
                        case MsgType.VoiceResult:
                            var m5 = msg as VoiceReceiveMessage;
                            MediaPath = BWeiXin.MediaDownloadTest(SavePath, m5.MediaId);
                            MediaPath = WXMediaDownPath + System.IO.Path.GetFileName(MediaPath);
                            rdc.tbl_WxMsg.InsertOnSubmit(new tbl_WxMsg
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                MsgType = (int)msg.MsgType,
                                FromUserName = msg.FromUserName,
                                ToUserName = msg.ToUserName,
                                MessageBody = msg.MessageBody,
                                CreateTime = msg.CreateTime,
                                MsgId = m5.MsgId,
                                MediaId = m5.MediaId,
                                Format = m5.Format,
                                Recognition = m5.Format,
                                MediaPath = MediaPath,
                                IssueTime = DateTime.Now
                            });
                            rdc.SubmitChanges();
                            break;
                        default:
                            break;
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="model"></param>
        public static bool Add(tbl_WxMsg model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_WxMsg.InsertOnSubmit(model);
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
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_WxMsg> GetList(ref int rowsCount, int intPageSize, int CurrencyPage)
        {
            string FieldsList = "*";
            string TableName = "dt_WxMsg";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(MsgType>0) AND (MsgType<6)";
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_WxMsg> q = rdc.ExecuteQuery<dt_WxMsg>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }
    #endregion
    #region 微信自动回复
    /// <summary>
    /// 微信自动回复
    /// </summary>
    public class BWeixinAutoMsg {
        /// <summary>
        /// 文件上传域名
        /// </summary>
        private static string UploadFileDomain = System.Configuration.ConfigurationManager.AppSettings["UploadFileDomain"];
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        public static bool Add(tbl_WxAutoMsg model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_WxAutoMsg.InsertOnSubmit(model);
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
        /// 新增图文消息
        /// </summary>
        /// <param name="Question">问题</param>
        /// <param name="NewsList">图文消息列表</param>
        /// <returns></returns>
        public static bool AddNews(string Question, List<NewsCustomMessageItem> NewsList)
        {
            using (FWDC rdc = new FWDC())
            {
                if (NewsList != null && NewsList.Count() > 0)
                {
                    tbl_WxAutoMsg model = new tbl_WxAutoMsg
                    {
                        TypeId = (int)Model.EnumType.微信回复类型.图文消息,
                        Question = Question,
                        Answer = JsonConvert.SerializeObject(NewsList),
                        IssueTime = DateTime.Now
                    };
                    rdc.tbl_WxAutoMsg.InsertOnSubmit(model);
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
        /// 根据问题返回结果
        /// </summary>
        /// <param name="Answer"></param>
        /// <returns></returns>
        public static tbl_WxAutoMsg GetModelByQuestion(string Question)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_WxAutoMsg.FirstOrDefault(n => n.Question == Question);
                return model;
            }
        }
        /// <summary>
        /// 删除自动回复消息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool Delete(string Id) {
            using (FWDC rdc = new FWDC())
            {
                if (!String.IsNullOrWhiteSpace(Id))
                {
                    var query = rdc.ExecuteQuery<int>(@"DELETE FROM tbl_WxAutoMsg WHERE Id IN (" + StringValidate.CheckSql(Id) + ")");
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 图片分割
        /// </summary>
        /// <param name="PhotoPath"></param>
        /// <param name="ArrayNumber"></param>
        /// <returns></returns>
        public static string PhotoSplit(string PhotoPath, int ArrayNumber)
        {
            if (!String.IsNullOrWhiteSpace(PhotoPath))
            {
                string[] pl = PhotoPath.Split('|');
                if (pl.Length < ArrayNumber + 1)
                    return "";
                else
                    return pl[ArrayNumber];
            }
            else { return ""; }
        }
        /// <summary>
        /// 返回回复信息实体列表
        /// </summary>
        /// <param name="ReplyInfo"></param>
        /// <returns></returns>
        public static List<NewsCustomMessageItem> GetNewsList(string ReplyInfo)
        {
            if (!String.IsNullOrWhiteSpace(ReplyInfo))
            {
                List<NewsCustomMessageItem> NewsList = new List<NewsCustomMessageItem>();
                var list = JsonConvert.DeserializeObject<List<NewsCustomMessageItem>>(ReplyInfo);
                foreach (var model in list)
                {
                    model.PicUrl = UploadFileDomain + PhotoSplit(model.PicUrl, 1);
                    NewsList.Add(model);
                }
                return NewsList;
            }
            else { return null; }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_WxAutoMsg> GetList(ref int rowsCount, int intPageSize, int CurrencyPage)
        {
            string FieldsList = "*";
            string TableName = "tbl_WxAutoMsg";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(1=1)";
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_WxAutoMsg> q = rdc.ExecuteQuery<tbl_WxAutoMsg>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }
    #endregion
}
