using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Member
{
    public partial class ArticleLeaveWord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitModel(OpenId);

                string flag = Request.QueryString["flag"].ToString();
                if (flag != "leave")
                {
                    litIntroduce.Text = "回复";
                }
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitModel(string OpenId)
        {
            var MemberModel = BMember.GetModelByOpenId(OpenId);
            if (MemberModel != null)
            {
                
                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
               
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        protected void btnAddArticle_Click1(object sender, EventArgs e)
        {

            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            string ArticleId =Utils.GetQueryStringValue("articleId");
            string LeaveId = Utils.GetQueryStringValue("leaveid");
            string flag = Utils.GetQueryStringValue("flag");
            string ReplyId = Utils.GetQueryStringValue("Id");
            string dzty = Utils.GetQueryStringValue("dzty");
            string strErr = "";
            if (flag == "leave")
            {
                #region
                if (string.IsNullOrEmpty(txtLeaveWord.Text))
                {
                    strErr += "留言内容不能为空!/n";
                }
            
                if (!String.IsNullOrEmpty(strErr))
                {
                    MessageBox.ShowAndReturnBack(strErr);
                    return;
                }

                string MemberId = "";
                string NickName = "", MemberName = "";
                var model = BMember.GetModelByOpenId(OpenId);
                if (model != null)
                {
                    MemberId = model.Id;
                    NickName = model.NickName;
                    MemberName = model.ContactName;

              
                    BArticleLeaveWord.Add(new tbl_ArticleLeaveWord
                    {
                        Id = Guid.NewGuid().ToString(),
                        ArticleId = ArticleId,
                        MemberId = MemberId,
                        NickName = NickName,
                        MemberName = MemberName,
                        LeaveWord = txtLeaveWord.Text ,
                        IsEnable = true,
                        IsReply = false,
                        ReplyId = string.IsNullOrEmpty(LeaveId)?"":LeaveId,
                  
                        IssueTime = DateTime.Now

                    });
                    if (dzty=="jobly")
                    {
                        MessageBox.ShowAndRedirect("留言成功！", "/WX/Rudder/RudderView.aspx?typ=dzsx&JobId=" + ArticleId + "");
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect("留言成功！", "TeamArticles.aspx");
                    }
                    
                }
                else { MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8)); }
                #endregion
            }
            else
            {
                #region
                if (string.IsNullOrEmpty(txtLeaveWord.Text))
                {
                    strErr += "回复内容不能为空!/n";
                }

                if (!String.IsNullOrEmpty(strErr))
                {
                    MessageBox.ShowAndReturnBack(strErr);
                    return;
                }

                string MemberId = "";
                string NickName = "", MemberName = "";
                var model = BMember.GetModelByOpenId(OpenId);
                if (model != null)
                {
                    MemberId = model.Id;
                    NickName = model.NickName;
                    MemberName = model.ContactName;


                    BArticleLeaveWord.Add(new tbl_ArticleLeaveWord
                    {
                        Id = Guid.NewGuid().ToString(),
                        ArticleId = ArticleId,
                        MemberId = MemberId,
                        NickName = NickName,
                        MemberName = MemberName,
                        LeaveWord = txtLeaveWord.Text,
                        IsEnable = true,
                        IsReply = true,
                        ReplyId = string.IsNullOrEmpty(ReplyId) ? "" : ReplyId,

                        IssueTime = DateTime.Now

                    });
                    MessageBox.ShowAndRedirect("回复成功！", "ArticleView.aspx?Id="+ArticleId);
                }
                else { MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8)); }
                #endregion 
            }
           
        }
    }
}