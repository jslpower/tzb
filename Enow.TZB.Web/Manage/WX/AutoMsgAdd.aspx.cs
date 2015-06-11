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
    public partial class AutoMsgAdd : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
            }
            string doType = Utils.GetQueryStringValue("doType");
            if (doType == "save")
            {
                SaveMsg();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void SaveMsg()
        {
            int TypeId = Utils.GetInt(Utils.GetFormValue("rblType"));
            string Question = Utils.GetFormValue(txtQuestion.UniqueID);
            string Answer = Utils.GetFormValue(txtAnswer.UniqueID);
            if (String.IsNullOrWhiteSpace(Question)) {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请填写留言内容!"));
            }
            if (TypeId == 1)
            { //图文描述
                string[] Titles = Utils.GetFormValues("txtTitle");
                string[] Description = Utils.GetFormValues("txtDescription");
                string[] Photo = Request.Form.GetValues("hide_Journey_file");
                string[] Url = Utils.GetFormValues("txtUrl");
                List<NewsCustomMessageItem> list = new List<NewsCustomMessageItem>();
                for (int i = 0; i < Titles.Length; i++)
                {
                    list.Add(new NewsCustomMessageItem
                    {
                        Title = Titles[i],
                        Description = Description[i],
                        PicUrl = Photo[i],
                        Url = Url[i]
                    });
                }
                BWeixinAutoMsg.AddNews(Question, list);                
            }
            else
            {
                BWeixinAutoMsg.Add(new tbl_WxAutoMsg
                {
                    TypeId = (int)Model.EnumType.微信回复类型.文本,
                    Question = Question,
                    Answer = Answer,
                    IssueTime = DateTime.Now
                });
            }
            Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "添加成功!"));
        }
    }
}