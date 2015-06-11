using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Memeber
{
    public partial class IntegrationManage : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
               string Id = Utils.GetQueryStringValue("id");
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    InitPage(Id);
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }

            }
        }

        #region 初始化数据
        private void InitPage(string Id)
        {
            var model = BMember.GetModel(Id);
            if (model != null)
            {
                lblContactName.Text = model.ContactName;
                lblMobile.Text = model.MobilePhone;
                lblIntegrationNumber.Text = model.IntegrationNumber.ToString();
                lblHonorNumber.Text = model.HonorNumber.ToString();
                #region 取得球队名称
                var TeamModel = BTeamMember.GetModel(Id);
                if (TeamModel != null)
                {
                    lblTeamName.Text = TeamModel.TeamName;
                }
                else
                {
                    lblTeamName.Text = "暂未加入任何球队";
                }
                #endregion

            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
        #endregion

        #region 保存数据
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            
            string TypeName="";
            #region 赋值
            string Id = Utils.GetQueryStringValue("id");
            int Type = Utils.GetInt(Utils.GetFormValue(ddlTypeId.UniqueID));
            int Intergration = Utils.GetInt(Utils.GetFormValue(txtIntergrationNumber.UniqueID));
            string Remark = Utils.GetFormValue(txtRemark.UniqueID);
            #endregion
            var model = BMember.GetModel(Id);
            if (model != null)
            {
                if (Type == (int)Model.EnumType.积分类型.惩罚)
                {
                    TypeName = "扣除";
                    if (Intergration > model.IntegrationNumber)
                    {
                        MessageBox.ShowAndReload("惩罚积分大于现有积分，操作失败!");
                        return;
                    }
                }
                else
                {
                    TypeName = "奖励";
                }
                #region 写入积分流水
                BMemberIntegration.Add(new tbl_MemberIntegration
                {
                    Id = Guid.NewGuid().ToString(),
                    TypeId = Type,
                    MemberId = model.Id,
                    UserName = model.UserName,
                    ContactName = model.ContactName,
                    OrderId = Id,
                    IntegrationNumber = Intergration,
                    Remark = string.Format(CacheSysMsg.GetMsg(145), TypeName, Intergration, TypeName, Remark),
                    IssueTime = DateTime.Now

                }); ;
                #endregion

                #region 写入消息中心
                BMessage.Add(new tbl_Message
                {
                    Id = System.Guid.NewGuid().ToString(),
                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                    SendId = "0",
                    SendName = "铁子帮",
                    SendTime = DateTime.Now,
                    ReceiveId = model.Id,
                    ReceiveName = model.ContactName,
                    MasterMsgId = "0",
                    MsgTitle = string.Format(CacheSysMsg.GetMsg(146), TypeName, Intergration),
                    MsgInfo = string.Format(CacheSysMsg.GetMsg(145), TypeName, Intergration, TypeName, Remark),
                    IsRead = false,
                    IssueTime = DateTime.Now
                });
                #endregion
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }

            #region 更新积分

            bool Result = false;
            switch (Type)
            {
                case (int)Model.EnumType.积分类型.奖励:
                   Result= BMember.UpdateIntegrationNumber(model.Id, Model.EnumType.操作符号.加, Intergration);
                   break;
                case (int)Model.EnumType.积分类型.惩罚:
                   Result = BMember.UpdateIntegrationNumber(model.Id, Model.EnumType.操作符号.减, Intergration);
                   break;
                default:
                   Result = false;
                   break;
            }
            if (Result)
            {
                MessageBox.ShowAndParentReload("操作成功");
            }
            else
            {
                MessageBox.ShowAndReload("操作失败");
                return;
            }
            #endregion 

        }
        #endregion 
    }
}