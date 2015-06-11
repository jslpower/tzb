using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using Newtonsoft.Json;

namespace Enow.TZB.Web.Manage.Vote
{
    public partial class VoteAdd : System.Web.UI.Page
    {
        public string endtime = "";
        public string voteuslist = "null";
        public string matchtype = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            var auto = Utils.GetQueryStringValue("dotype").ToLower();
            switch (auto)
            {
                case "save":
                    Bsave();
                    break;
            }
            if (!IsPostBack)
            {
                Gettypes();
                InitLoad();
            }
        }
        private void InitLoad()
        {
            var id = Utils.GetQueryStringValue("id");
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("id")))
            {
               var model=BVote.GetModel(id);
               if (model != null)
               {
                   droptypes.SelectedValue = model.Vtype.ToString();//分类
                   dropfbmb.SelectedValue = model.VRelease.ToString();//发布目标
                   txtName.Text = model.Vtitle;//标题
                   txtendtime.Text = model.ExpireTime.ToString("yyyy-MM-dd");
                   txtContent.Text = model.Remarks;
                   matchtype = model.SubjectType.ToString();
                   Gettpxx(model.Vid);
                   if (model.SubjectType==1)
                   {
                       drophd.SelectedValue = model.MatchId;
                   }
                   else
                   {
                       dropss.SelectedValue = model.MatchId;
                   }
               }
            }
        }
        /// <summary>
        /// 投票选项
        /// </summary>
        private void Gettpxx(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var Optionlist = BVoteOption.GettypList(id);
                if (Optionlist.Count > 0)
                {
                    voteuslist = JsonConvert.SerializeObject(Optionlist);
                }
            }
            
        }
        private void Gettypes()
        {
            ManageUserAuth.ManageLoginCheck();
            //发布至
            var fblist = EnumObj.GetList(typeof(Model.EnumType.ReleaseEnum));
            dropfbmb.DataSource = fblist;
            dropfbmb.DataBind();
            //活动类型
            var hdlist = EnumObj.GetList(typeof(Model.EnumType.VoteEnum));
            droptypes.DataSource = hdlist;
            droptypes.DataBind();
            //赛事信息
            var ssxxlist =BMatch.GetwfyList();
            if (ssxxlist.Count<=0)
            {
                ssxxlist.Insert(0, new tbl_Match() { Id = "", MatchName = "请选择" });
            }
            dropss.DataSource = ssxxlist;
            dropss.DataBind();
            //活动信息
            var hdxxlist =BActivity.GettypeList();
            if (hdxxlist.Count <= 0)
            {
                hdxxlist.Insert(0, new tbl_Activity() { Id = 0, Title = "请选择" });
            }
            drophd.DataSource = hdxxlist;
            drophd.DataBind();
        }
        private void Bsave()
        {
            ManagerList model = ManageUserAuth.GetManageUserModel();
            var id = Utils.GetQueryStringValue("id");
            tbl_Vote vote = new tbl_Vote()
            {
                Vtitle = Utils.GetFormValue(txtName.UniqueID),//标题
                VRelease = Utils.GetInt(Utils.GetFormValue(dropfbmb.UniqueID)),//发布目标
                Vtype = Utils.GetInt(Utils.GetFormValue(droptypes.UniqueID)),//分类
                ExpireTime = Utils.GetDateTime(Utils.GetFormValue(txtendtime.UniqueID),DateTime.Now),//截止时间
                Remarks = Utils.EditInputText(Request.Form[txtContent.UniqueID]),//备注
                SponsorId = model != null ? model.Id : 0,
                SponsorName = model != null ? model.ContactName : "",
                LaunchTime = DateTime.Now,
                 ColumnID=1,
                IsDelete = 0,
            };
            if (vote.Vtype==3)
            {
                var matctype =Utils.GetInt(Utils.GetFormValue("zttypes"),1);//主体投票分类
                vote.SubjectType = matctype;
                if (matctype==1)
                {
                    vote.MatchId = Utils.GetFormValue(drophd.UniqueID);
                }
                else
                {
                    vote.MatchId = Utils.GetFormValue(dropss.UniqueID);
                }
               //vote.MatchId= 
            }
            bool retbol = false;
            if (!string.IsNullOrEmpty(id))
            {
                vote.Vid = id;
                retbol = BVote.Update(vote);
            }
            else
            {
                vote.Vid = Guid.NewGuid().ToString();
                retbol = BVote.Add(vote);
            }
            if (retbol)
            {
                InsertOption(vote.Vid);
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("1", "操作成功"));
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", "操作失败"));
                Response.End();
            }
        }
        private void InsertOption(string VoteId)
        {
            var strids = Utils.GetFormValue(hdfdeleteOpId.UniqueID);
            var listid = Utils.GetStringArray(strids, ",");
            if (listid!=null&&listid.Count > 0)
            {
                BVoteOption.Delete(listid);
            }
            var TrIndex = Utils.GetFormValues("OptionTrIndex");
            var OptionIDs = Utils.GetFormValues("hidOldVoteOptionID");
            var OptionName = Utils.GetFormValues("OptionName");
            var OptionNum = Utils.GetFormValues("OptionNum");
            for (int i = 0; i < TrIndex.Length; i++)
            {
                var OptionModel = new tbl_VoteOption()
                {
                    ONumber = Utils.GetInt(OptionNum[i],0),//数量
                    Otitle = OptionName[i],//名称
                    VoteId = VoteId,//所属投票编号
                    OptIsDelete = 0,
                    SortNum = (i + 1)
                };
                if (!string.IsNullOrEmpty(OptionIDs[i]))
                {
                    OptionModel.Oid = OptionIDs[i];
                    BVoteOption.Update(OptionModel);
                }
                else
                {
                    OptionModel.Oid = Guid.NewGuid().ToString();
                    BVoteOption.Add(OptionModel);
                }
            }
        }
    }
}