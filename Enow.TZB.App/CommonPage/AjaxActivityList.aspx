<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxActivityList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxActivityList" %>

<asp:Repeater ID="rptList" runat="server">
          <ItemTemplate>
         <li>
            <div class="juhui-title"><em>[<%#Eval("Starname")%>]</em> <%#Eval("Title")%></div>
            <div class="juhui-box">
                <div class="btn">
                   <a href="TZGatheringDetail.aspx?ActId=<%#Eval("Id")%>&types=<%#Eval("Activitytypes")%>" class="basic_btn">查看</a>
                   <a href="GatheringSignUp.aspx?Actid=<%#Eval("Id")%>" class="basic_ybtn">报名</a>
                </div>
                <p class="font_gray">时间：<%#Enow.TZB.Utility.Utils.GetDateTime((Eval("StartDatetime")).ToString()).ToString("yyyy-MM-dd")%></p>
                <p>区域：<%#Eval("Countryname")%><%#Eval("Provincename")%><%#Eval("Cityname")%><%#Eval("Areaname")%></p>
                <p>地点：<%#Eval("Address")%></p>
                <p class="font_gray">费用：<%#Eval("CostNum")%></p>
            </div>
         </li>
         </ItemTemplate>
         </asp:Repeater>