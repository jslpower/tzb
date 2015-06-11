using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Weixin.Mp.Sdk;
using Weixin.Mp.Sdk.Request;
using Weixin.Mp.Sdk.Response;
using Weixin.Mp.Sdk.Domain;
using System.Web.Script.Serialization;
using Weixin.Mp.Sdk.Util;
using Enow.TZB.Utility;
using System.IO;

namespace Enow.TZB.BLL
{
    #region 结果
    /// <summary>
    /// 微信结果
    /// </summary>
    public class WeiXinResult
    {
        public WeiXinResult() { }
        /// <summary>
        /// 结果 true:成功, false:失败
        /// </summary>
        public bool IsResult { get; set; }
        /// <summary>
        /// 结果消息
        /// </summary>
        public string ResultMsg { get; set; }
    }
    #endregion
    /// <summary>
    /// 微信方法类
    /// </summary>
    public class BWeiXin
    {
        private const string Token = "Aa123456";
        private static string appId = "wx33bbf1c5c1d5a10b";
        private static string appSecret = "4700b1876dbe932b1f57047b312343c5";
        private static string WXDomain = "t.tie-zi.com";//System.Configuration.ConfigurationManager.AppSettings["WXDomain"];        
        #region 菜单
        // <summary>
        /// 返回OAUTH 2.0网页直连
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string OAuth2UrlRewrite(string url)
        {
            return "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appId + "&redirect_uri=" + HttpContext.Current.Server.UrlDecode(url) + "&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect";
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        public static WeiXinResult CreateMenu()
        {
            WeiXinResult rv = new WeiXinResult { IsResult = false, ResultMsg = "系统错误！" };
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                rv.ResultMsg = "获取令牌环失败";
                return rv;
            }
            else
            {
                string Url = "";
                Menu menu = new Menu();
                List<Button> button = new List<Weixin.Mp.Sdk.Domain.Button>();
                #region 菜单一 
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Member/Default.aspx";
                Button Menu1SubBtn1 = new Button()
                {
                    name = "铁丝中心",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Article/TieOriginal.aspx";
                Button Menu1SubBtn2 = new Button()
                {
                    name = "铁原创",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Register/RegisterChoose.aspx";
                Button Menu1SubBtn3 = new Button()
                {
                    name = "成为铁丝",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Team/Default.aspx";
                Button Menu1SubBtn4 = new Button()
                {
                    name = "铁丝球队",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Team/Default.aspx";
                Button Menu1SubBtn5 = new Button()
                {
                    name = "铁丝约战",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                List<Button> subBtnAll1 = new List<Button>();
                subBtnAll1.Add(Menu1SubBtn1);
                subBtnAll1.Add(Menu1SubBtn2);
                subBtnAll1.Add(Menu1SubBtn3);
                subBtnAll1.Add(Menu1SubBtn4);
                subBtnAll1.Add(Menu1SubBtn5);
                Button btn = new Button()
                {
                    key = "smartWeb",
                    name = "铁微站",
                    url = "httpbig",
                    type = "click",
                    sub_button = subBtnAll1
                };
                button.Add(btn);
                #endregion
                #region 菜单二
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Mall/TrainMall.aspx";
                Button Menu2SubBtn1 = new Button()
                {
                    name = "培训报名",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Match/List.aspx";
                Button Menu2SubBtn2 = new Button()
                {
                    name = "赛事报名",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Job/DZJob.aspx";
                Button Menu2SubBtn3 = new Button()
                {
                    name = "舵主报名",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Job/TZJob.aspx";
                Button Menu2SubBtn4 = new Button()
                {
                    name = "堂主报名",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Job/ZZJob.aspx";
                Button Menu2SubBtn5 = new Button()
                {
                    name = "站长报名",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                List<Button> subBtnAll2 = new List<Button>();
                subBtnAll2.Add(Menu2SubBtn1);
                subBtnAll2.Add(Menu2SubBtn2);
                subBtnAll2.Add(Menu2SubBtn3);
                subBtnAll2.Add(Menu2SubBtn4);
                subBtnAll2.Add(Menu2SubBtn5);
                btn = new Button()
                {
                    key = "Member",
                    name = "铁报名",
                    url = "httpbig",
                    type = "click",
                    sub_button = subBtnAll2
                };
                button.Add(btn);

                #endregion
                #region 菜单三
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/SmartWeb/PlayTogether.aspx";
                Button Menu3SubBtn1 = new Button()
                {
                    name = "一起玩吧",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Team/BallField.aspx";
                Button Menu3SubBtn2 = new Button()
                {
                    name = "球场大联盟",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Article/TieShare.aspx";
                Button Menu3SubBtn3 = new Button()
                {
                    name = "李铁培训堂",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                    
                };
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Article/BigLove.aspx";
                Button Menu3SubBtn4 = new Button()
                {
                    name = "铁公益",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                //Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Notice/Default.aspx";
                Url = "http://" + WXDomain + "/WX/OAuthRedirectUri.aspx?BaseUrl=/WX/Mall/TiesiMall.aspx";
                Button Menu3SubBtn7 = new Button()
                {
                    name = "铁丝购",
                    sub_button = null,
                    type = "view",
                    url = OAuth2UrlRewrite(Url)
                };
                List<Button> subBtnAll3 = new List<Button>();
                subBtnAll3.Add(Menu3SubBtn1);
                subBtnAll3.Add(Menu3SubBtn2);
                subBtnAll3.Add(Menu3SubBtn3);
                subBtnAll3.Add(Menu3SubBtn4);
                //subBtnAll3.Add(Menu3SubBtn5);
                //subBtnAll3.Add(Menu3SubBtn6);
                subBtnAll3.Add(Menu3SubBtn7);
                btn = new Button()
                {
                    key = "Card",
                    name = "铁制造",
                    sub_button = subBtnAll3,
                    type = "click",
                    url = "httpbig"
                };
                button.Add(btn);
                #endregion
                menu.button = button;
                MenuGroup mg = new MenuGroup()
                {
                    menu = menu
                };
                string postData = mg.ToJsonString();
                CreateMenuRequest createRequest = new CreateMenuRequest()
                {
                    AccessToken = response.AccessToken.AccessToken,
                    SendData = postData
                };
                CreateMenuResponse createResponse = mpClient.Execute(createRequest);
                if (createResponse.IsError)
                {
                    rv.ResultMsg = "创建菜单失败，错误信息为：" + createResponse.ErrInfo.ErrCode + "-" + createResponse.ErrInfo.ErrMsg;
                    return rv;
                }
                else
                {
                    rv.IsResult = true;
                    rv.ResultMsg = "创建成功";
                    return rv;
                }
            }
        }
        /// <summary>
        /// 取得菜单
        /// </summary>
        /// <returns></returns>
        public static List<Button> GetMenu()
        {
            //取得菜单信息
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                return null;
            }
            else
            {
                GetMenuRequest getRequest = new GetMenuRequest()
                {
                    AccessToken = response.AccessToken.AccessToken
                };
                var getResponse = mpClient.Execute(getRequest);
                if (getResponse.IsError)
                {
                    return null;
                }
                else
                {
                    var m = getResponse.Menu.menu.button;
                    return m;
                }
            }
        }
        #endregion
        #region 事件响应
        
        public static string MsgHandler()
        {
            string OpenId = "";
            var recMsg = MessageHandler.ConvertMsgToObject(Token);  //将消息转换成对象
            //WLog(recMsg.MessageBody,"Err.txt");
            if (recMsg == null)
            {
                return OpenId;
            }
            else
            {
                IMessageProcessor msgProcessor = new MessageProcessor();  //处理消息
                if (msgProcessor.ProcessMessage(recMsg)) //处理消息
                {
                    return recMsg.FromUserName;
                }
                else {
                    return OpenId;
                }
            }

        }        
        #endregion
        #region 验证
        /// <summary>
        /// 启用开发模式时验证URL方法
        /// </summary>
        /// <returns></returns>
        public static string Auth()
        {
            if (MessageHandler.CheckSignature(Token))
            {
                string rv = HttpContext.Current.Request.QueryString["echostr"];
                return rv;
            }
            else { return ""; }
        }
        #endregion
        #region 获取用户信息
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="OpenId">用户OpenId</param>
        /// <returns></returns>
        public static User GetUserInfo(string OpenId)
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                return null;
            }

            GetUserInfoRequest request2 = new GetUserInfoRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                OpenId = OpenId,
            };

            var response2 = mpClient.Execute(request2);
            if (response2.IsError)
            {
                return null;
            }
            else
            {
                return response2.UserInfo;
            }
        }
        /// <summary>
        /// 获取关注者列表
        /// </summary>
        /// <param name="AttentionsList">返回关注的列表</param>
        /// <param name="NextOpenId">超过一万时最后一个关注OPENID</param>
        /// <returns></returns>
        public static WeiXinResult GetAttentions(ref List<string> AttentionsList, string NextOpenId)
        {
            WeiXinResult AttentionsResult = new WeiXinResult();
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                AttentionsResult.IsResult = false;
                AttentionsResult.ResultMsg = "获取令牌环失败";
                return AttentionsResult;

            }
            string AccessToken = response.AccessToken.AccessToken;
            GetAttentionsRequest request2 = new GetAttentionsRequest()
            {
                AccessToken = AccessToken,
                NextOpenId = NextOpenId
            };

            var response2 = mpClient.Execute(request2);
            if (response2.IsError)
            {
                AttentionsResult.IsResult = false;
                AttentionsResult.ResultMsg = "获取关注者列表失败，错误信息为：" + response2.ErrInfo.ErrCode + "-" + response2.ErrInfo.ErrMsg;
                return AttentionsResult;
            }
            else
            {
                var list = response2.Attentions;
                if (list.total > 0)
                {
                    if (list.data.openid.Count() > 0)
                    {
                        AttentionsList.AddRange(list.data.openid);
                        if (list.total > 10000 && !String.IsNullOrEmpty(list.next_openid))//超过一万
                        {
                            return GetAttentions(ref AttentionsList, list.next_openid);
                        }
                        else
                        {
                            ConfigClass.SetConfigKeyValue("next_openid", list.next_openid);
                            AttentionsResult.IsResult = true;
                            AttentionsResult.ResultMsg = "获取关注者列表成功";
                            return AttentionsResult;
                        }
                    }
                    else
                    {
                        AttentionsResult.IsResult = false;
                        AttentionsResult.ResultMsg = "无关注者数据";
                        return AttentionsResult;
                    }
                }
                else
                {
                    AttentionsResult.IsResult = false;
                    AttentionsResult.ResultMsg = "无关注者数据";
                    return AttentionsResult;
                }
            }
        }
        #endregion          
        #region 微信消息媒体下载
        /// <summary>
        /// 发送图文客服消息
        /// </summary>
        /// <param name="msg">图文消息</param>
        /// <returns></returns>
        public static bool SendNewsCustomMessage(NewsCustomMessage msg)
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                return false;
            }
            else
            {
                msg.AccessToken = response.AccessToken.AccessToken;
                var response2 = MessageHandler.SendCustomMessage(response.AccessToken.AccessToken, msg);
                if (response2.IsError)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 发送客服消息 
        /// </summary>
        /// <param name="FromUserName">接收对象</param>
        /// <param name="TextMsg">消息内容</param>
        /// <returns></returns>
        public static bool SendTextCustomMessage(string ToUser, string TextMsg) {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                return false;
            }
            else
            {
                var response2 = MessageHandler.SendTextCustomMessage(response.AccessToken.AccessToken, ToUser, TextMsg);
                if (response2.IsError)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 多媒体文件下载接口测试
        /// </summary>
        public static string MediaDownloadTest(string SavePath,string MediaId)
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                return "";
            }
            else
            {
                DownloadMediaRequest DownRequest = new DownloadMediaRequest()
                {
                    AccessToken = response.AccessToken.AccessToken, 
                    MediaId = MediaId,
                    SaveDir = SavePath
                };
                DownloadMediaResponse DownResponse = mpClient.Execute(DownRequest);
                if (DownResponse.IsError)
                {
                    return "";
                }
                else
                {
                    return DownResponse.SaveFileName;
                }
            }
        }
        #endregion
    }
    #region 事件处理
    /// <summary>
    /// 消息处理类
    /// </summary>
    public class MessageProcessor : IMessageProcessor
    {
        private const string Token = "Aa123456";
        private static string appId = "wx33bbf1c5c1d5a10b";
        private static string appSecret = "4700b1876dbe932b1f57047b312343c5";
        /// <summary>
        /// 关注欢迎词
        /// </summary>
        private static string WXSubscribeMsg = System.Configuration.ConfigurationManager.AppSettings["WXSubscribeMsg"];
        private static string WXAutoReply = System.Configuration.ConfigurationManager.AppSettings["WXAutoReply"];
        #region 菜单业务处理
        /// <summary>
        /// 菜单点击业务
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <param name="FromUserName"></param>
        /// <param name="EventKey"></param>
        public bool MenuClick(string ToUserName, string FromUserName, string EventKey)
        {
            string msg = "";
            switch (EventKey)
            {
                case "MemberRegister"://铁丝注册
                    msg = "紧急开发中!";
                    //这里回应1条文本消息，当然您也可以回应其他消息
                    MessageHandler.SendTextReplyMessage(ToUserName, FromUserName, msg);
                    break;
                case "BallTeam"://铁丝组队
                    msg = "紧急开发中!";
                    //这里回应1条文本消息，当然您也可以回应其他消息
                    MessageHandler.SendTextReplyMessage(ToUserName, FromUserName, msg);
                    break;
                case "Card"://铁丝卡
                    msg = "紧急开发中!";
                    //这里回应1条文本消息，当然您也可以回应其他消息
                    MessageHandler.SendTextReplyMessage(ToUserName, FromUserName, msg);
                    break;
                default:
                    msg = EventKey+"紧急开发中!" + FromUserName;
                    //这里回应1条文本消息，当然您也可以回应其他消息
                    MessageHandler.SendTextReplyMessage(ToUserName, FromUserName, msg);
                    break;
            }            
            return true;
        }
        
        #endregion
        #region 菜单业务处理
        
        #endregion
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="msg">消息对象</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        public bool ProcessMessage(ReceiveMessageBase msg,params object[] args)
        {            
            switch (msg.MsgType)
            {
                case MsgType.UnKnown:
                //return ProcessNotHandlerMsg(msg, args);
                case MsgType.Text:
                    BWeixinMsg.Add(msg);
                    var txtMsg = msg as TextReceiveMessage;
                    var model = BWeixinAutoMsg.GetModelByQuestion(txtMsg.Content);
                    if (model == null)
                    {
                        return ProcessTextMessage(txtMsg, args);
                    }
                    else
                    {
                        switch ((Model.EnumType.微信回复类型)model.TypeId)
                        {
                            case Model.EnumType.微信回复类型.图文消息:
                                List<NewsCustomMessageItem> list = BWeixinAutoMsg.GetNewsList(model.Answer);
                                if (list != null)
                                {
                                    BWeiXin.SendNewsCustomMessage(new NewsCustomMessage
                                    {
                                        ToUser = txtMsg.FromUserName,
                                        Articles = list
                                    });
                                }
                                else
                                {
                                    return ProcessTextMessage(txtMsg, args);
                                }
                                break;
                            default:
                                MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, model.Answer);
                                break;
                        }
                        return true;
                    }
                case MsgType.Image:
                    BWeixinMsg.Add(msg);
                    return ProcessNotHandlerMsg(msg, args);
                //return ProcessImageMessage(msg as ImageReceiveMessage, args);
                case MsgType.Link:
                    BWeixinMsg.Add(msg);
                    return ProcessNotHandlerMsg(msg, args);
                //return ProcessLinkMessage(msg as LinkReceiveMessage, args);
                case MsgType.Location:
                //return ProcessLocationMessage(msg as LocationReceiveMessage, args);              
                case MsgType.Video:
                    BWeixinMsg.Add(msg);
                    return ProcessNotHandlerMsg(msg, args);
                //return ProcessVideoMessage(msg as VideoReceiveMessage, args);
                case MsgType.Voice:
                case MsgType.VoiceResult:
                    BWeixinMsg.Add(msg);
                    return ProcessNotHandlerMsg(msg, args);
                //return ProcessVoiceMessage(msg as VoiceReceiveMessage, args);
                
                //return ProcessVoiceMessage(msg as VoiceReceiveMessage, args);
                default:                    
                    return ProcessNotHandlerMsg(msg, args);
                case MsgType.Event:
                    EventMessage evtMsg = msg as EventMessage;
                    switch (evtMsg.EventType)
                    {
                        case EventType.Subscribe://关注
                            return ProcessSubscribeEvent(msg as SubscribeEventMessage, args);
                        case EventType.Click:
                            TextReplyMessage replyMsg = new TextReplyMessage()
                                {
                                    Content = "您好,您发送的消息类型为：" + msg.GetType().ToString(),
                                    CreateTime = Tools.ConvertDateTimeInt(DateTime.Now),
                                    FromUserName = msg.ToUserName,
                                    ToUserName = msg.FromUserName
                                };
                                //MessageHandler.SendReplyMessage(replyMsg);
                                return true;
                        case EventType.View:
                            TextReplyMessage replyMsg1 = new TextReplyMessage()
                                {
                                    Content = "您好,您发送的消息类型为：View",
                                    CreateTime = Tools.ConvertDateTimeInt(DateTime.Now),
                                    FromUserName = msg.ToUserName,
                                    ToUserName = msg.FromUserName
                                };
                                //MessageHandler.SendReplyMessage(replyMsg1);
                                return true;
                            //return ProcessMenuEvent(msg as MenuEventMessage, args);
                        case EventType.Location:
                        //return ProcessUploadLocationEvent(msg as UploadLocationEventMessage, args);
                        case EventType.Scan:
                        //return ProcessScanEvent(msg as ScanEventMessage, args);                       
                        case EventType.UnKnown:
                        //return ProcessNotHandlerMsg(msg, args);                        
                        case EventType.UnSubscribe:
                        //return ProcessUnSubscribeEvent(msg as UnSubscribeEventMessage, args);
                        default:
                            return ProcessNotHandlerMsg(msg, args);
                    }
            }
        }


        /// <summary>
        /// 处理文本消息
        /// </summary>
        /// <param name="msg">消息对象</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        public bool ProcessTextMessage(TextReceiveMessage msg, params object[] args)
        {            
            //自动回复
            MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, WXAutoReply);
            return true;
        }

        /// <summary>
        /// 处理图片消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessImageMessage(ImageReceiveMessage msg, params object[] args)
        {
            try
            {
                // string token = args[0].ToString();
                string token = "";
                IMpClient mpClient = new MpClient();
                AccessTokenGetRequest request = new AccessTokenGetRequest()
                {
                    AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
                };
                AccessTokenGetResponse response = mpClient.Execute(request);
                if (response.IsError)
                {
                    //这里回应1条文本消息，当然您也可以回应其他消息
                    MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "您发送了图片消息");
                    return true;
                }
                else
                {
                    token = response.AccessToken.AccessToken;
                    //这里回复一个图片，当然您也可以回复其他类型的消息
                    return MessageHandler.SendImageReplyMessage(token, msg.ToUserName, msg.FromUserName, AppDomain.CurrentDomain.BaseDirectory + "11.jpg");
                }
            }
            catch (Exception ex)
            {
                //这里回应1条文本消息，当然您也可以回应其他消息
                MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "出错了：" + ex.ToString());
                return true;

            }
        }

        /// <summary>
        /// 处理语音消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessVoiceMessage(VoiceReceiveMessage msg, params object[] args)
        {
            string token = "";
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                //这里回应1条文本消息，当然您也可以回应其他消息
                MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "您发送了语音消息");
                return true;
            }
            else
            {
                token = response.AccessToken.AccessToken;
                //这里回复1条语音消息，当然您也可以回复其他类型的信息
                return MessageHandler.SendVoiceReplyMessage(token, msg.ToUserName, msg.FromUserName, AppDomain.CurrentDomain.BaseDirectory + "11.mp3");
            }

        }

        /// <summary>
        /// 处理视频消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessVideoMessage(VideoReceiveMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "您发送的视频" + msg.MediaId.ToString() + "-" + msg.ThumbMediaId.ToString());
            return true;
        }

        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessLocationMessage(LocationReceiveMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "您的地里位置为：" + msg.Label + "(" + msg.Location_X + "," + msg.Location_Y + ")");
            return true;
        }

        /// <summary>
        /// 处理链接消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessLinkMessage(LinkReceiveMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "您发送的是连接信息");
            return true;
        }

        /// <summary>
        /// 处理关注事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessSubscribeEvent(SubscribeEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, WXSubscribeMsg);            
            return true;
        }

        /// <summary>
        /// 处理取消关注事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessUnSubscribeEvent(UnSubscribeEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "您触发了取消关注事件，欢迎您下次再光临哦");
            return true;
        }

        /// <summary>
        /// 处理扫描二维码关注事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessScanSubscribeEvent(ScanSubscribeEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "您扫描了我们的二维码关注我们");
            return true;
        }

        /// <summary>
        /// 处理扫描二维码事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessScanEvent(ScanEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "您扫描了我们的二维码");
            return true;
        }

        /// <summary>
        /// 处理上报地理位置事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessUploadLocationEvent(UploadLocationEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "您的地里位置为：" + msg.Latitude + "-" + msg.Longitude);
            return true;
        }

        /// <summary>
        /// 处理自定义菜单事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessMenuEvent(MenuEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            //MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName, "您触发了自定义事件" + msg.EventKey.ToString());
            MenuClick(msg.ToUserName, msg.FromUserName, msg.EventKey.ToString());
            return true;
        }
        /// <summary>
        /// 处理未定义处理方法消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ProcessNotHandlerMsg(ReceiveMessageBase msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            /*
             回复空值
          1、直接回复空串（指字节长度为0的空字符串，而不是XML结构体中content字段的内容为空）
          2、直接回复success
             */
            HttpContext.Current.Response.Write("success");
            //MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName,"");
            //MessageHandler.SendTextReplyMessage(msg.ToUserName, msg.FromUserName,  msg.MsgType.ToString() + " 类型的消息");
            return true;
        }
    }
    #endregion
}
