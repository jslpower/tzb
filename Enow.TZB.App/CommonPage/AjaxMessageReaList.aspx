<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMessageReaList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxMessageReaList" %>
          <asp:Repeater ID="rptList" runat="server">      
      <ItemTemplate> 
                        <li>
                            <div class="xiaoxi_t" data-id="<%#Eval("Id") %>" style="cursor:Pointer;"><%#Eval("MsgTitle")%><em class="font14"><%#Eval("SendTime","{0:yyyy-MM-dd}")%></em></div>
                            <div class="xiaoxi_cont" style="display:none;"><%#Eval("MsgInfo")%></div>
                        </li>
                        </ItemTemplate>      
  </asp:Repeater>  
