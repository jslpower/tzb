<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BallFieldSignUp.aspx.cs"
    Inherits="Enow.TZB.Web.WX.Team.BallFieldSignUp" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>场地预约</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/user.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="预约" runat="server" />
    <div class="warp">
        <div class="form_list">
            <ul>
                <li><span class="label_name">预约场地</span>
                    <asp:TextBox ID="txtcd" runat="server" Enabled="false" CssClass="u-input"></asp:TextBox>
                </li>
                <li><span class="label_name">预约时间</span>
                <asp:TextBox ID="txttime" runat="server" placeholder="请输入" CssClass="u-input"></asp:TextBox>
                </li>
                <li><span class="label_name">预约人数</span>
                <asp:TextBox ID="txtrenshu" runat="server" placeholder="请输入" CssClass="u-input"></asp:TextBox>
                </li>
                <li><span class="label_name">备注</span>
                 <textarea class="u-input" id="txtbeizhu" name="txtbeizhu" runat="server" placeholder="请输入备注说明"></textarea>
                </li>
            </ul>
        </div>
        <div class="msg_btn">
        <asp:Button ID="btnsave" runat="server" Text="提交" CssClass="basic_btn" 
                onclick="btnsave_Click" /></div>
    </div>
    </form>
     
</body>
</html>
