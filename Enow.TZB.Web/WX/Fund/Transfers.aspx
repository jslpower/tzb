<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transfers.aspx.cs" Inherits="Enow.TZB.Web.WX.Fund.Transfers" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>队员转账</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="队员转账" runat="server" />
<div class="warp">
  <div class="msg_tab"  id="n4Tab">
        <div class="TabContent">
        <div id="n4Tab_Content1">
 
                <div class="msg_list">
                  <ul>
                  <li><label>转入队友名：</label><asp:DropDownList ID="ddlTeamMember" runat="server">
                      </asp:DropDownList>
                  </li>
                                    <li><label>转入铁丝币：</label><asp:TextBox ID="txtMoney" MaxLength="6" CssClass="formsize100 input_bk" 
                                            runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ErrorMessage="请填写转账金额!" 
                                    ControlToValidate="txtMoney">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator1" runat="server" 
                                    ErrorMessage="转入铁丝币数量只能为数字!" ControlToValidate="txtMoney" 
                                    ValidationExpression="^\d+(\.\d+)?$">*</asp:RegularExpressionValidator></li>
                            <li><label>支&nbsp;付&nbsp;密&nbsp;码：</label><asp:TextBox ID="txtPayPassword" CssClass="formsize100 input_bk" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写支付密码!" 
                                    ControlToValidate="txtPayPassword">*</asp:RequiredFieldValidator></li>
                            
                   </ul>
                   
                </div>
                
                <div class="msg_btn">
                    <asp:Button ID="btnSave" runat="server" CssClass="basic_btn" Text="立即转账" 
                        onclick="btnSave_Click" /></div>
            
            </div>
         </div>
   
  </div>  
</div> <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>