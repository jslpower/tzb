<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="Enow.TZB.Web.Manage.Cashier.UserEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta charset="utf-8" content="IE=EmulateIE8" http-equiv="X-UA-Compatible" />
<title>收银员信息修改</title>
<link href="/Css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
<script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
<script src="/Js/table-toolbar.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="contentbox">
       <table width="100%" border="0" cellspacing="0" cellpadding="0">                  
          <tr>
            <th width="150" height="40" align="right">用户名：</th>
            <td>
                <asp:Literal ID="ltrUserName" runat="server"></asp:Literal>
              </td>
          </tr>
          <tr>
                <th width="150" height="40" align="right">
                    所属球场：
                </th>
                <td>
                    <asp:DropDownList ID="ddlFieldId" runat="server">
                    </asp:DropDownList>
                    <span class="fontred">*</span>
                </td>
            </tr>
          <tr>
            <th width="150" height="40" align="right">真实姓名：</th>
            <td>
                <asp:TextBox ID="txtContactName" CssClass="input-txt formsize180" runat="server"></asp:TextBox><span
                        class="fontred">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContactName"
                        ErrorMessage="请填写收银员姓名！">*</asp:RequiredFieldValidator>
              </td>
          </tr>  
          <tr>
                <th width="150" height="40" align="right">
                    联系电话：
                </th>
                <td>
                    <asp:TextBox ID="txtTel" CssClass="input-txt formsize180" runat="server"></asp:TextBox><span
                        class="fontred">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTel"
                        ErrorMessage="请填写联系电话！">*</asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr>
            <th width="150" height="40" align="right">用户密码：</th>
            <td>
                <asp:TextBox ID="txtPwd" MaxLength="50" CssClass="input-txt formsize180" 
                    runat="server" TextMode="Password"></asp:TextBox>如不修改请置空</td>
          </tr>
          <tr>
            <th width="150" height="40" align="right">重复密码：</th>
            <td>
                <asp:TextBox ID="txtPwd2" MaxLength="50" CssClass="input-txt formsize180" 
                    runat="server" TextMode="Password"></asp:TextBox>
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
    </form>
</body>
</html>
