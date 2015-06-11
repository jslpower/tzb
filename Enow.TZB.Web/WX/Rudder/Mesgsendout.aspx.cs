using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Rudder
{
    public partial class Mesgsendout : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        /// <summary>
        /// 收件人编号
        /// </summary>
        protected string SjrId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Getusermodel();
                Getjob();
            }
        }
        #region 验证登录信息
        /// <summary>
        /// 微信会员登录验证
        /// </summary>
        private void Getusermodel()
        {
            BMemberAuth.LoginCheck();
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            InitMember(OpenId);
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember(string OpenId)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        #endregion
        #region 舵/堂/站主基本信息
        /// <summary>
        /// 查询基本信息
        /// </summary>
        private void Getjob()
        {
            string JobId = Utils.GetQueryStringValue("JobId");
            var Jmodel = BJob.GetJobdtzModel(JobId);
            if (Jmodel != null && Jmodel.Jobtyoe != 0)
            {
                litsjr.Text = Jmodel.ContactName;
            }
            else
            {
                Response.Redirect("Detail.aspx");
            }
        }
        #endregion
        
        protected void btnEditArticle_Click(object sender, EventArgs e)
        {
            var AuthModel = BMemberAuth.GetUserModel();
            var model = BMember.GetModelByOpenId(AuthModel.OpenId);
            if (model != null)
            {
                string JobId = Utils.GetQueryStringValue("JobId");
                var Jmodel = BJob.GetJobdtzModel(JobId);
                if (Jmodel != null && Jmodel.Jobtyoe == 0)
                {
                    MessageBox.ShowAndRedirect("舵主信息错误！请重新选择", "Detail.aspx");
                    return;
                }
                if (Jmodel.MemberId == model.Id)
                {
                    MessageBox.ShowAndRedirect("不能给自己发送信息!", "Detail.aspx");
                    return;
                }
                //写入会员消息
                BMessage.Add(new tbl_Message
                {
                    Id = System.Guid.NewGuid().ToString(),
                    TypeId = (int)Model.EnumType.消息类型.会员发送,
                    SendId = model.Id,
                    SendName = model.ContactName,
                    SendTime = DateTime.Now,
                    ReceiveId =Jmodel.MemberId ,
                    ReceiveName =Jmodel.ContactName ,
                    MasterMsgId = "0",
                    MsgTitle = string.Format("{0}发送了消息:{1}", model.ContactName,Utils.InputText(Utils.GetFormValue(txtTitle.UniqueID),25)),
                    MsgInfo = string.Format("内容:{0}", Utils.InputText(Utils.GetFormValue(txtArticleInfo.UniqueID),50)),
                    IsRead = false,
                    IssueTime = DateTime.Now
                });
                MessageBox.ShowAndRedirect("发送成功！", "RudderView.aspx?JobId=" + Utils.GetQueryStringValue("JobId"));

            }
                
        }
    }
}