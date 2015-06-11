<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master" AutoEventWireup="true" CodeBehind="Notice.aspx.cs" Inherits="Enow.TZB.WebForm.My.Notice" %>
<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <div class="user_box">
        <h3>
            消息中心</h3>
        <div class="chongzhi_box">
            <div class="chongzhi_table">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th style="width: 70%;">
                            消息内容
                        </th>
                        <th style="width: 10%;">
                            状态
                        </th>
                        <th style="width: 20%;">
                            发送时间
                        </th>
                      
                    </tr>
                    <tr>
                        <asp:Repeater ID="rptList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align:left;">
                                       <%#Eval("msgInfo") %>
                                    </td>
                                    <td>
                                        <%#bool.Parse(Eval("IsRead").ToString()) == true ? "已读" : "未读"%>
                                    </td>
                                    <td>
                                       
                                         <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>
                                    </td>
                                    
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
            </div>
            <br />
            <div class="pages" style="float: right;">
                <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
            </div>
        </div>
        </div>
</asp:Content>
