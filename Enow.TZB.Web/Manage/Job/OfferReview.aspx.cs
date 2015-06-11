using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Job
{
    public partial class OfferReview : System.Web.UI.Page
    {

        protected string OfferState = "";
        protected string dotype = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            dotype = Utils.GetQueryStringValue("dotype").ToLower();
            if (dotype=="view")
            {
                linkBtnSave.Visible = false;
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                string id = Utils.GetQueryStringValue("Id");
                if (!string.IsNullOrEmpty(id))
                {
                    InitPage(id);
                }
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="id"></param>
        protected void InitPage(string id)
        {
            #region 查询实体
            MOfferSearch searchModel = new MOfferSearch();
            searchModel.OfferId = id;
            
            #endregion
            int rowsCount=0;
            var list = BOffer.GetList(ref rowsCount, 1, 1, searchModel);
            if (rowsCount>0)
            {
                lblJobName.Text = list[0].JobName;
                lblUserName.Text = list[0].UserName;
                lblPhone.Text = list[0].MobilePhone;
                lblContactName.Text = list[0].ContactName;
                lblPersonalId.Text = list[0].PersonalId;
                lblAddress.Text = list[0].Address;
                lblEmail.Text = list[0].Email;
                lblWorkYear.Text = list[0].WorkYear;
                lblBallYear.Text = list[0].BallYear;
                lblSpecialty.Text = list[0].Specialty;
                ltrBMGY.Text = list[0].BMGY;
                ltrApplyInfo.Text = list[0].ApplyInfo;
                OfferState = list[0].OfferState.ToString();
            }
        }

        /// <summary>
        /// 绑定招聘状态下拉框
        /// </summary>
        protected string InitOfferState(string str)
        {
            System.Text.StringBuilder strState = new System.Text.StringBuilder();
            Array array = Enum.GetValues(typeof(Model.EnumType.应聘状态));
            strState.Append("<option value='-1' selected='selected'>-请选择-</option>");
            foreach (var arr in array)
            {
                int value = (int)Enum.Parse(typeof(Model.EnumType.应聘状态), arr.ToString());
                string text = Enum.GetName(typeof(Model.EnumType.应聘状态), arr);

                if (value.ToString().Equals(str))
                {
                    strState.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", value, text);
                }
                else
                {
                    strState.AppendFormat("<option value='{0}'>{1}</option>", value, text);
                }

            }
            return strState.ToString();
        }

        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (Utils.GetInt(Utils.GetFormValue("selState"))<1)
            {
                strErr = "请选择应聘状态!";
            }

            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }

            //保存审核状态
            bool IsSucess = BOffer.UpdateState(Utils.GetQueryStringValue("Id"), (Model.EnumType.应聘状态)Utils.GetInt(Utils.GetFormValue("selState")));
            if (IsSucess)
            {
                Updateus(Utils.GetInt(Utils.GetFormValue("selState")));
                MessageBox.ShowAndParentReload("审核成功");
            }
            else
            {
                MessageBox.ShowAndReturnBack("审核失败！");
                return;
            }
        }
        /// <summary>
        /// 修改应聘者 职位
        /// </summary>
        /// <param name="jobstate">应聘状态</param>
        private void Updateus(int jobstate)
        {
            string id = Utils.GetQueryStringValue("Id");
            if (string.IsNullOrEmpty(id))
                return;   
            var list = BOffer.GetModel(id);
            if (list !=null)
            {
              var jobmodel=BJob.GetModel(list.JobId);
              if (jobmodel == null)
                  return;
              string bt = "应聘回复";
              string nr = "";
              Getjobzt(jobstate, ref nr, ((Enow.TZB.Model.EnumType.JobType)(jobmodel.JobType)).ToString());
              BMember.UpdateZw(list.MemberId, jobstate, (Enow.TZB.Model.EnumType.JobType)(jobmodel.JobType), bt, nr);
            }
        }
        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="jobstate">状态</param>
        /// <param name="nr">回复内容</param>
        /// <param name="zw">应聘职位</param>
        private void Getjobzt(int jobstate,ref string nr,string zw)
        {

           
            switch (jobstate)
            {
                case 1://婉拒
                    nr = "您的条件不符合(" + zw+")的条件！谢谢您的参加！";
                    break;
                case 2://面试
                    nr = "经初步审核您的简历符合(" + zw + ")的面试要求，请等待管理员通知！";
                    break;
                case 3://试用
                    nr = "经初步面试您符合(" + zw + ")的要求，现在开始进入试用期！";
                    break;
                case 4://正是
                    nr = "您在试用期的表现符合(" + zw + ")的要求，现在开始成为正式的" + zw + "！恭喜！";
                    break;
                case 5://离职
                    nr = "您的离职申请已正式批准！现已从(" + zw + ")岗位上离职！";
                    break;
                case 6://开出
                    nr = "您的简历不符合(zw)的要求！现已从(" + zw + ")岗位上被开除！";
                    break;
            }
        }
    }
}