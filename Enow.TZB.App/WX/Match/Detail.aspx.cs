using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.Model;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Match
{
    public partial class Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Id = Utils.GetQueryStringValue("Id");
                if (!String.IsNullOrWhiteSpace(Id))
                {
                    var model = BMatch.GetModel(Id);
                    if (model != null)
                    {
                        this.ltrMatchName.Text = model.MatchName;
                        this.MasterOrganizer.Text = model.MasterOrganizer;
                        if (!string.IsNullOrWhiteSpace(model.CoOrganizers))
                        {
                            this.CoOrganizers.Text = "<li><label>协办方：</label>" + model.CoOrganizers+"</li>";
                        }
                        else
                        {
                            this.CoOrganizers.Text = "&nbsp;&nbsp;";
                        }
                        if (!string.IsNullOrWhiteSpace(model.Organizer))
                        {
                            this.Organizer.Text = "<li><label>承办方：</label>" + model.Organizer + "</li>";
                        }
                        else
                        {
                            this.Organizer.Text = "&nbsp;&nbsp;";
                        }
                        if (!string.IsNullOrWhiteSpace(model.Sponsors))
                        {
                            this.Sponsors.Text = "<li><label>赞助方：</label>" + model.Sponsors + "</li>";
                        }
                        else
                        {
                            this.Sponsors.Text = "&nbsp;&nbsp;";
                        }
                      
                        this.ltrSignUpTime.Text = model.SignBeginDate.ToString("yyyy-MM-dd") + " 至 " + model.SignEndDate.ToString("yyyy-MM-dd");
                        this.BreakTime.Text = model.BreakTime.ToString() + "分钟";
                        this.SignUpNumber2.Text = model.SignUpNumber.ToString()+"/"+model.TeamNumber.ToString();
                        this.PlayersMax.Text =model.PlayersMin.ToString()+"至" +model.PlayersMax.ToString();
                        this.TotalTime.Text = model.TotalTime.ToString()+"分钟";
                        this.BayMax.Text = model.BayMax.ToString();
                        this.BayMin.Text = model.BayMin.ToString();
                        this.MaxAge2.Text = model.MaxAge.ToString();
                        this.MinAge2.Text = model.MinAge.ToString();
                        this.LtrPlayerNumber.Text = model.PlayersMin.ToString() + "至" + model.PlayersMax.ToString() + " 人";
                        this.LtrBayNumber.Text = model.BayMin.ToString() + "-" + model.BayMax.ToString()+" 人";
                        this.LtrMatchTime.Text = model.BeginDate.ToString("yyyy-MM-dd") + " 至 " + model.EndDate.ToString("yyyy-MM-dd");
                        this.Remark.Text = model.Remark;
                        if (model.SignBeginDate <= DateTime.Now && model.SignEndDate >= DateTime.Now) { this.phSignUp1.Visible = this.phSignUp.Visible = true; }
                        else
                        {
                            this.phSignUp1.Visible = this.phSignUp.Visible = false;
                        }
                        var list = BMatchField.GetList(Id);
                        if (list.Count() > 0)
                        {
                            this.rptFieldList.DataSource = list;
                            this.rptFieldList.DataBind();
                        }
                    }
                    else
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                    }
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
        }
    }
}