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
    public partial class HonorManage : System.Web.UI.Page
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
                ltrContactName.Text = model.ContactName;
                ltrMobile.Text = model.MobilePhone;
                ltrTotalInter.Text = model.IntegrationNumber.ToString();
                ltrHonorNumber.Text = model.HonorNumber.ToString();
                #region 取得球队名称
                var TeamModel = BTeamMember.GetModel(Id);
                if (TeamModel != null)
                {
                    ltrTeamName.Text = TeamModel.TeamName;
                }
                else
                {
                    ltrTeamName.Text = "暂未加入任何球队";
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
            string TypeName = "";
            #region 赋值
            string Id = Utils.GetQueryStringValue("id");
            int Type = Utils.GetInt(Utils.GetFormValue(ddlTypeId.UniqueID));
            int HonorNumber = Utils.GetInt(Utils.GetFormValue(txtHonorNumber.UniqueID));
            string Remark = Utils.GetFormValue(txtRemark.UniqueID);
            #endregion
            var model = BMember.GetModel(Id);
            if (model != null)
            {
                if (Type == (int)Model.EnumType.积分类型.惩罚)
                {
                    TypeName = "扣除";
                    if (HonorNumber > model.HonorNumber)
                    {
                        MessageBox.ShowAndReload("扣除荣誉值大于现有荣誉值，操作失败!");
                        return;
                    }
                }
                else
                {
                    TypeName = "奖励";
                }
                #region 写入荣誉值流水
                BMemberHonor.Add(new tbl_MemberHonor
                {
                    Id = Guid.NewGuid().ToString(),
                    TypeId = Type,
                    HonorType = 1,
                    HostId = model.Id,
                    HostName = model.ContactName,
                    Title = string.Format(CacheSysMsg.GetMsg(147),TypeName,HonorNumber,TypeName),
                    Remark =string.Format(CacheSysMsg.GetMsg(148),TypeName,HonorNumber,TypeName,Remark),
                    HonorNumber = HonorNumber,
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
                    MsgTitle = string.Format(CacheSysMsg.GetMsg(147), TypeName, HonorNumber, TypeName),
                    MsgInfo = string.Format(CacheSysMsg.GetMsg(148), TypeName, HonorNumber, TypeName, Remark),
                    IsRead = false,
                    IssueTime = DateTime.Now
                });
                #endregion
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }

            #region 更新荣誉值

            bool Result = false;
            switch (Type)
            {
                case (int)Model.EnumType.积分类型.奖励:
                    Result = BMemberHonor.UpdateHonorNumber(model.Id, Model.EnumType.操作符号.加, HonorNumber);
                    break;
                case (int)Model.EnumType.积分类型.惩罚:
                    Result = BMember.UpdateIntegrationNumber(model.Id, Model.EnumType.操作符号.减,HonorNumber);
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