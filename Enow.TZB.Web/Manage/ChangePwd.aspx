<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="Enow.TZB.Web.Manage.ChangePwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
<div class="contentbox">
        <div class="firsttable">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <th width="150" height="40" align="right">用户联系人：</th>
            <td>
                <asp:Literal ID="ltrContactName" runat="server"></asp:Literal>
              </td>
          </tr>
          <tr>
            <th width="150" height="40" align="right">用户名：</th>
            <td>
                <asp:Literal ID="ltrUserName" runat="server"></asp:Literal>
              </td>
          </tr>
          <tr>
            <th width="150" height="40" align="right">用户密码：</th>
            <td>
                <asp:TextBox ID="txtPwd" MaxLength="50" CssClass="input-txt formsize180" 
                    runat="server" errmsg="请填写用户密码!" valid="required" TextMode="Password"></asp:TextBox>如不修改请置空</td>
          </tr>
          <tr>
            <th width="150" height="40" align="right">重复密码：</th>
            <td>
                <asp:TextBox ID="txtPwd2" MaxLength="50" CssClass="input-txt formsize180" 
                    runat="server" errmsg="请填写重复密码!" valid="required" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtPwd" ControlToValidate="txtPwd2" 
                    ErrorMessage="重复密码必须与用户密码一致">*</asp:CompareValidator>
              </td>
          </tr>
          </table>
       <div class="Basic_btn fixed">
          <ul>
               <li>
                   <asp:LinkButton ID="linkBtnSave" runat="server" onclick="linkBtnSave_Click">保 存 >></asp:LinkButton>
               </li>
               <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()" hidefocus="true">返 回 >></a></li>
          </ul>
          <div class="hr_10"></div>
        </div>
	  </div><asp:ValidationSummary 
              ID="ValidationSummary1" runat="server" ShowMessageBox="True" 
              ShowSummary="False" />
        </div>
        </div>
</asp:Content>
