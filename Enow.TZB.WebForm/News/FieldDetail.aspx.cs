using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.News
{
    public partial class FieldDetail : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string id = Utils.GetQueryStringValue("Id");
                if (!string.IsNullOrWhiteSpace(id))
                {
                    InitData(id);
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }

            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="FieldId"></param>
        private void InitData(string FieldId)
        {
            this.Master.Page.Title = "铁丝网-球场详情";
            var model = BBallField.GetModel(FieldId);
            if (model != null)
            {
                lblFieldName.Text = model.FieldName;
                if (!string.IsNullOrWhiteSpace(model.FieldPhoto))
                {
                    ltrImg.Text = "<img src= \"" + model.FieldPhoto + "\" width=\"720px\" height=\"500\">";
                }
                ltrPrice.Text = model.Price.ToString("F2");
                ltrMarkPrice.Text = model.MarketPrice.ToString("F2");
                ltrNumber.Text = model.FieldNumber.ToString();
                ltrHour.Text = model.Hours.ToString();
                ltrSize.Text = model.FieldSize.ToString();
                ltrAddress.Text = model.Address.ToString();
                ltrContactTel.Text = model.ContactTel;
                ltrRemark.Text = model.Remark;
                if (!String.IsNullOrWhiteSpace(model.OtherPhotoXml))
                {
                    string[] ImgList = model.OtherPhotoXml.Split(',');
                    System.Text.StringBuilder tmpStr = new System.Text.StringBuilder();
                    for (int i = 0; i < ImgList.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            tmpStr.Append("<img src=\"" + ImgList[i] + "\" width=\"355px\" class=\"floatL\" style=\"margin-right:10px;\"/>");
                        }
                        else
                        {

                            tmpStr.Append("<img src=\""  + ImgList[i] + "\" width=\"355px\" class=\"floatL\"/>");
                        }
                    }
                    this.ltrImgList.Text = tmpStr.ToString();
                }

            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
    }
}