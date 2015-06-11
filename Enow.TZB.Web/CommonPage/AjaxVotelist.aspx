<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxVotelist.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxVotelist" %>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                    <li class="R_jiantou" onclick="location.href='<%#dizhiurl %>?Vid=<%#Eval("Vid")%>'"><%#Eval("Vtitle")%></li>
                    </ItemTemplate>
                </asp:Repeater>
