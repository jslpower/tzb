<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxBigLovelist.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxBigLovelist" %>
 <asp:Repeater ID="rptList" runat="server">      
      <ItemTemplate>            
                    <li><a href="ArticleDetail.aspx?Id=<%#Eval("Id") %>"><%#Enow.TZB.Utility.Utils.GetText2(Eval("Title").ToString(),12,true)%></a></li>
      </ItemTemplate>      
  </asp:Repeater>  
