<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxVoteUserList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxVoteUserList" %>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                    <li class="R_jiantou" onclick="location.href='<%#dizhiurl%>?Vid=<%#Eval("Vid")%>'"><%#Eval("Vtitle")%>    <span style=" float:right;">已投:<%#Eval("Otitle")%></span></li>
                    </ItemTemplate>
                </asp:Repeater>
