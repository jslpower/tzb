<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMemberArticleList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxMemberArticleList" %>
<asp:Repeater ID="rptList" runat="server">      
      <ItemTemplate>
      <li>
                            <div class="log_title"><a href="ArticleView.aspx?Id=<%#Eval("Id") %>"><%#Enow.TZB.Utility.Utils.GetText2(Eval("ArticleTitle").ToString(), 12, true)%></a></div>
                            
                            <div class="log_date">
                              <a href="" class="liuyan"></a><!--
                              <a href="" class="fenxiang"></a>-->
                              <a href="" class="zan"></a>
                             <%#Eval("IssueTime","{0:yyyy-MM-dd}") %>
                            </div>
                            
                            <div class="Rbtn">
                                   <a href="ArticleEdit.aspx?Id=<%#Eval("Id") %>" class="basic_ybtn">编辑</a>
                                   <a href="ArticleDel.aspx?Id=<%#Eval("Id") %>" class="basic_gbtn">删除</a>
                            </div>
                        </li>
      </ItemTemplate>      
</asp:Repeater>