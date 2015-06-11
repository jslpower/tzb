<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxArticles.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxArticles" %>

<asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <li>
                              <div class="log_title" >         
                               <a href="/WX/Member/ArticleView.aspx?Id=<%#Eval("Id") %>"> <%#Eval("ArticleTitle")%> </a>
                                   
                                </div>
                                <div class="log_date">
                                    <%#Eval("IssueTime")%>
                                </div>
                                <div class="Rbtn">
                                    <a href="/WX/Member/ArticleEdit.aspx?Id=<%#Eval("Id") %>" class="basic_ybtn">编辑</a>
                                    <a href="" onclick="PageJsDataObj.Delete('<%#Eval("Id")%>')"
                                        class="basic_gbtn">删除</a>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
