using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.Model;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Team
{
    public partial class BallFieldDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Id = Utils.GetQueryStringValue("Id");
                if (!String.IsNullOrWhiteSpace(Id))
                {
                    InitDetail(Id);
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
        }
        /// <summary>
        /// 加载信息
        /// </summary>
        /// <param name="Id"></param>
        private void InitDetail(string Id) {
            var model = BBallField.GetModel(Id);
            if (model != null)
            {
                if (!String.IsNullOrWhiteSpace(model.FieldPhoto))
                    this.ltrImg.Text = "<img src=\"" + model.FieldPhoto + "\">";
                this.ltrFieldName.Text = model.FieldName;
                this.ltrType.Text = Convert.ToString((Model.EnumType.CourtEnum)model.TypeId);
                this.ltrPrice.Text = model.Price.ToString("F2");
                this.ltrMarkPrice.Text = model.MarketPrice.ToString("F2");
                this.ltrNumber.Text = model.FieldNumber.ToString();
                this.ltrHour.Text = model.Hours;
                this.ltrSize.Text = model.FieldSize;
                if (!String.IsNullOrWhiteSpace(model.Latitude) && !String.IsNullOrWhiteSpace(model.Longitude))
                    this.ltrAddress.Text = "<a href=\"BallMapView.aspx?Id=" + Id + "\">" + model.Address + "</a>";
                else
                    this.ltrAddress.Text = model.Address;
                this.ltrContactTel.Text = "<a href=\"tel:" + model.ContactTel + "\">" + model.ContactTel + "</a>";
                this.ltrRemark.Text = model.Remark;
                if (!String.IsNullOrWhiteSpace(model.OtherPhotoXml))
                {
                    string[] ImgList = model.OtherPhotoXml.Split(',');
                    System.Text.StringBuilder tmpStr = new System.Text.StringBuilder();
                    for (int i = 0; i < ImgList.Length; i++) {
                        if (i % 2 == 0) {
                            tmpStr.Append("<li class=\"marR\"><img src=\"" + ImgList[i] + "\"/></li>");
                        } else {
                            tmpStr.Append("<li><img src=\"" + ImgList[i] + "\"/></li>");
                        }
                    }
                    this.ltrImgList.Text = tmpStr.ToString();
                }
                else {
                    this.phImgList.Visible = false;
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
    }
}