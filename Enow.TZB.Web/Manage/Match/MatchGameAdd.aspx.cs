using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;
using Enow.TZB.Model;

namespace Enow.TZB.Web.Manage.Match
{
    public partial class MatchGameAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string MatchId = Utils.GetQueryStringValue("MatchId");
            string GameName = Utils.GetQueryStringValue("gname");
            string id = Utils.GetQueryStringValue("id");
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                this.txt_GameName.Text = GameName;
                BindMatch(MatchId);
                BindField(MatchId);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    InitPage(id);
                }
            }

        }

        /// <summary>
        /// 绑定赛程下拉框
        /// </summary>
        private void BindMatch(string MatchId)
        {
            var list = BMatch.GetMatchList();
            if (list.Count > 0)
            {
                ddlMatchName.DataSource = list;
                ddlMatchName.DataTextField = "MatchName";
                ddlMatchName.DataValueField = "Id";
                ddlMatchName.DataBind();
                this.ddlMatchName.Items.Insert(0, new ListItem("请选择赛事名称", "0"));
                foreach (var lst in list)
                {
                    if (lst.Id == MatchId)
                    {
                        this.ddlMatchName.Items.FindByValue(MatchId).Selected = true;
                    }
                }
               
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void InitPage(string Id)
        {
            var model = BMatchGame.GetModel(Id);
            if (model != null)
            {
                ddlMatchName.SelectedValue = model.MatchId.ToString();
                ddlFieldName.SelectedValue = model.MatchFieldId.ToString();
                this.txt_GameName.Text = model.GameName;
                BindField(model.MatchId);
            }
        }

        /// <summary>
        /// 绑定球场下拉框
        /// </summary>
        private void BindField(string MatchId)
        {
            if (!string.IsNullOrWhiteSpace(MatchId))
            {
                var list = BMatchField.GetList(MatchId);
                if (list.Count > 0)
                {
                    ddlFieldName.DataSource = list;
                    ddlFieldName.DataTextField = "FieldName";
                    ddlFieldName.DataValueField = "FieldId";
                    ddlFieldName.DataBind();
                }
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string act = Utils.GetQueryStringValue("act").ToLower();
            string id = Utils.GetQueryStringValue("Id");
            string MatchId = Utils.GetFormValue(ddlMatchName.UniqueID);
            string FieldId = Utils.GetFormValue(ddlFieldName.UniqueID);
            string GameName = Utils.GetFormValue(txt_GameName.UniqueID);
            string MatchName = "";
            string FieldName = "";
            var matchModel = BMatch.GetModel(MatchId);
            if (matchModel != null)
            {
                MatchName = matchModel.MatchName;
            }
            

            var FieldModel = BMatchField.GetModel(FieldId);
            if (FieldModel != null)
            {
                FieldName = FieldModel.FieldName;
            }
            bool result = false;
            switch (act)
            {
                case "add":
                    result = BMatchGame.Add(new tbl_MatchGame
                    {
                        Id = Guid.NewGuid().ToString(),
                        MatchId = MatchId,
                        MatchName = MatchName,
                        MatchFieldId = FieldId,
                        FieldName = FieldName,
                        GameName = GameName,
                        IssueTime = DateTime.Now
                    });
                    break;

                case "update":
                    result = BMatchGame.Update(new tbl_MatchGame
                    {
                        Id = id,
                        MatchFieldId = FieldId,
                        FieldName = FieldName,
                        GameName = GameName
                    });
                    break;
                default:
                    result = false;
                    break;
            }
            if (result)
            {
                MessageBox.ShowAndParentReload("操作成功");
            }
            else
            {
                MessageBox.ShowAndReload("操作失败");
                return;

            }
        }

        protected void ddlMatchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MatchId = ddlMatchName.SelectedValue;
            BindField(MatchId);
        }
    }
}