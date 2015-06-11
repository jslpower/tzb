<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Enow.TZB.Web.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    	<OBJECT classid=CLSID:8856F961-340A-11D0-A96B-00C04FD705A2 height=0 id=WebBrowser width=0></OBJECT> 
<input name=Button onClick=document.all.WebBrowser.ExecWB(1,1) type=button value=打开>
<input name=Button onClick=document.all.WebBrowser.ExecWB(2,1) type=button value=关闭所有>
<input name=Button onClick=document.all.WebBrowser.ExecWB(4,1) type=button value=另存为> 
<input name=Button onClick=document.all.WebBrowser.ExecWB(6,1) type=button value=打印>
<input name=Button onClick=document.all.WebBrowser.ExecWB(6,2) type=button value=直接打印>
<input name=Button onClick=document.all.WebBrowser.ExecWB(7,1) type=button value=打印预览>
<input name=Button onClick=document.all.WebBrowser.ExecWB(8,1) type=button value=页面设置>
<input name=Button onClick=document.all.WebBrowser.ExecWB(10,1) type=button value=属性>
<input name=Button onClick=document.all.WebBrowser.ExecWB(17,1) type=button value=全选>
<input name=Button onClick=document.all.WebBrowser.ExecWB(22,1) type=button value=刷新>
<input name=Button onClick=document.all.WebBrowser.ExecWB(45,1) type=button value=关闭> 
    <!--
    手机号码：<asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
    随机码：<asp:TextBox ID="txtVerNumber" runat="server"></asp:TextBox>
        <asp:Button ID="btnSend" runat="server" Text="发送短信" onclick="btnSend_Click" />
        -->
    </div>
    <asp:Button ID="btnSendMail" runat="server" onclick="btnSendMail_Click" 
        Text="发送QQ邮件" />
    </form>
</body>
</html>
