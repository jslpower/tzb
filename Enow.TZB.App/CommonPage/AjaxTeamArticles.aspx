<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxTeamArticles.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxTeamArticles" %>
<asp:Repeater ID="rptLogList" runat="server">
                        <ItemTemplate>
                            <li>
                              <div class="log_title" >
                                   <a href="/WX/Member/TeamArticleView.aspx?Id=<%#Eval("Id") %>"> <%#Eval("ArticleTitle")%></a>
                                </div>
                                <div class="log_date">
                                 <a href="/WX/Member/ArticleLeaveWord.aspx?Id=&articleId=<%#Eval("Id") %>&flag=leave&leaveid=" class="liuyan"></a>
                                  <%--  <a href="" class="fenxiang"></a>--%>
                                       
                                    <a href="#" flag="zan" onclick="PageJsDataObj.Greet('<%#Eval("Id") %>')"
                                            class="zan"></a>
                                    <%#Eval("IssueTime")%>
                                </div>
                                
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
