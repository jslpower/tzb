<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxLotteryUserlist.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxLotteryUserlist" %>
<asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <label>
                            <li>
                                <%#Container.ItemIndex+1%>.<%#Eval("ContactName")%>
                                <span style="float: right;">已中:<%#Eval("Otitle")%></span></li></label>
                    </ItemTemplate>
                </asp:Repeater>