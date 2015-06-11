<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMallModelList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxMallModelList" %>
      <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
             <li onClick="location.href='Mall_Detail.aspx?id=<%# Eval("Id")%>'">
                <div class="img_box"><img src="<%# System.Configuration.ConfigurationManager.AppSettings["Uploadimgurl"]+Eval("GoodsPhoto")%>"></div>
                <div class="txt_box">
                    <dl>
                       <dt><%#Eval("GoodsName")%></dt>
                       <dd><i class="line_x">门市价：¥<%#Eval("MarketPrice")%></i></dd>
                       <dd class="txt">会员价：<i class="font_yellow">¥<%#Eval("MemberPrice")%></i></dd>
                    </dl>
                </div>
             </li>
             </ItemTemplate>
           </asp:Repeater>
