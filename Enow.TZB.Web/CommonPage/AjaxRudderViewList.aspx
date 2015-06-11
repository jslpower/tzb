<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxRudderViewList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxRudderViewList" %>

 <asp:Repeater ID="rptList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="log_title">
                                            <%#Eval("ArticleTitle")%></div>
                                        <div class="log_date">
                                            <a href="javascript:void(0);" class="liuyan"></a><a href="javascript:void(0);" data-jid="<%#Eval("Id") %>" class="<%#Selgzyf(Eval("Id").ToString()) %> Agzbtn"></a>
                                            <%#Eval("IssueTime")%>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>