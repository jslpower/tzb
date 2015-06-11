<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxSiteList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxSiteList" %>
<asp:Repeater ID="rptList" runat="server">
      <ItemTemplate>
      <li style=" height:160px;">
            <div class="item-img"><img src="<%#Eval("FieldPhoto") %>"/></div>
            <div class="item-box">
                <dl>
                    <dt><a href="/WX/Team/BallFieldDetail.aspx?Id=<%#Eval("BallID") %>"><%#Eval("FieldName")%></a><!--<span class="tiesi_type01"><%#(Enow.TZB.Model.EnumType.CourtEnum)Convert.ToInt32(Eval("TypeId"))%></span>--></dt>
                    <dd>预约时间：<%#Eval("Contracttime")%></dd>
                    <dd>预约人数：<%#Eval("Number")%></dd>
                    <dd>预约备注：<%#Eval("Remarks")%></dd>
                    <dd>预约状态：<%#(Enow.TZB.Model.EnumType.ApplicantsStartEnum)(Enow.TZB.Utility.Utils.GetInt(Eval("IsState").ToString(), 0))%></dd>
                </dl>
                  <div class="qiu-caozuo"><a href="javascript:void(0);" class="basic_btn">已预约</a><a href="javascript:void(0);" data-ActId="<%#Eval("Id") %>" class="basic_btn basic_ybtn btndelete">取消预约</a></div>
            </div>
         </li>
      </ItemTemplate>
      </asp:Repeater>
