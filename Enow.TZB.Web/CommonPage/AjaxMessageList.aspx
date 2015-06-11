<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMessageList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxMessageList" %>
 <asp:Repeater ID="rptList" runat="server">      
      <ItemTemplate> 
                        <li>
                            <div class="xiaoxi_t" data-id="<%#Eval("Id") %>" IsRead="<%#Convert.ToBoolean(Eval("IsRead"))?"1":"0"%>" style="cursor:Pointer;"><%#Convert.ToBoolean(Eval("IsRead"))?"":"<span class=\"ico\"></span>"%><%#Eval("MsgTitle")%><em class="font14"><%#Eval("SendTime","{0:yyyy-MM-dd}")%></em></div>
                            <div class="xiaoxi_cont" style="display:none;"><%#Eval("MsgInfo")%></div>
                        </li>
                        </ItemTemplate>      
  </asp:Repeater>  
