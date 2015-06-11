using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using Weixin.Mp.Sdk.Domain;

namespace Enow.TZB.Web.Manage.WX
{
    /// <summary>
    /// 留言回复
    /// </summary>
    public partial class MsgReply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitInfo();

            }
        }
        /// <summary>
        /// 初始化留言
        /// </summary>
        private void InitInfo() {
            string Id = Request.QueryString["Id"];
            if (string.IsNullOrEmpty(Id) && StringValidate.IsInteger(Id))
            {
                ErrMsg();
            }
            else
            {

                var model = BWeixinMsg.GetViewModel(Id);
                if (model != null)
                {
                    this.ltrName.Text = model.ContactName + "(" + model.NickName + ")";
                    this.ltrTime.Text = model.MobilePhone;
                    this.ltrInfo.Text = GetMsg(model.MsgType, model.TextMessageInfo, model.MediaPath, model.Format, model.Recognition);
                }
                else { ErrMsg(); }
            }
        }
        /// <summary>
        /// 留言内容
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="TextMsg">文本内容</param>
        /// <param name="MediaPath">非文本路径</param>
        /// <param name="Format">视频格式</param>
        /// <param name="Recognition">视频编码</param>
        /// <returns></returns>
        private string GetMsg(int type, string TextMsg, string MediaPath, string Format, string Recognition)
        {
            string str = "";
            switch ((MsgType)type)
            {
                case MsgType.Text:
                    str = TextMsg;
                    break;
                case MsgType.Image:
                    str = "<a href=\"" + MediaPath + "\" target=\"_blank\"><img src=\"" + MediaPath + "\" width=\"100px\" height=\"100px\" border=\"0\" /></a>";
                    break;
                case MsgType.Voice:
                case MsgType.VoiceResult:
                    str = "<a href=\"" + MediaPath + "\" target=\"_blank\">【语音】下载播放</a>";
                    break;
                case MsgType.Video:
                    str = "<a href=\"" + MediaPath + "\" target=\"_blank\">【视频】下载播放</a>";
                    break;
            }
            return str;
        }
        /// <summary>
        /// 错误提醒
        /// </summary>
        private void ErrMsg()
        {
            MessageBox.ShowAndBoxClose("未找到您要修改的信息!", Request.QueryString["iframeId"]);
            return;
        }
        /// <summary>
        /// 留言回复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            string Id = Request.QueryString["Id"];
            string ReplyInfo = Utils.GetFormValue(txtReplyInfo.UniqueID);
            if (String.IsNullOrEmpty(Id))
            {
                ErrMsg();
            }
            else
            {
                if (string.IsNullOrEmpty(ReplyInfo))
                {
                    strErr = "回复内容不能为空！";
                }
            }

            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }
            var UserModel = ManageUserAuth.GetManageUserModel();
            bool IsSucess = BWeixinMsg.Reply(Id, UserModel.Id, UserModel.ContactName,ReplyInfo);
            if (IsSucess)
            {
                MessageBox.ShowAndParentReload("回复成功！");
            }
            else
            {
                MessageBox.ShowAndReturnBack("回复失败！");
                return;
            }
        }
    }
}