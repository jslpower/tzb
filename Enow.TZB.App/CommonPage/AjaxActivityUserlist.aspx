<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxActivityUserlist.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxActivityUserlist" %>
  <asp:Repeater ID="rptList" runat="server">
          <ItemTemplate>
         <li>
            <div class="juhui-title"><a href="TZGatheringDetail.aspx?ActId=<%#Eval("Id")%>&types=<%#Eval("Activitytypes")%>"><em>[<%#Eval("Starname")%>]</em> <%#Eval("Title")%></a></div>
            <div class="juhui-box">
                <div class="btn">
                   
                   <a href="javascript:void(0);" class="basic_gbtn">已报名</a>
                   <a href="javascript:void(0);" data-ActId="<%#Eval("Id")%>" class="basic_ybtn btndelete">取消报名</a>
                </div>
                <p class="font_gray">时间：<%#Enow.TZB.Utility.Utils.GetDateTime((Eval("StartDatetime")).ToString()).ToString("yyyy-MM-dd")%></p>
                <p>区域：<%#Eval("Countryname")%><%#Eval("Provincename")%><%#Eval("Cityname")%><%#Eval("Areaname")%></p>
                <p>地点：<%#Eval("Address")%></p>
                <p class="font_gray">费用：<%#Eval("CostNum")%></p>
            </div>
         </li>
         </ItemTemplate>
         </asp:Repeater>
