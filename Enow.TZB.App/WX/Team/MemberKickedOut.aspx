<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberKickedOut.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.MemberKickedOut" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>踢出球队</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="boxy">
    <div class="boxy_title">踢出球队<a href="javascript:parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();" class="close-btn"></a></div>
    <div class="msg_list boxy_form">
       <ul>
                <li><label>球衣号：</label><asp:Literal ID="txtQYHM" runat="server"></asp:Literal></li>
                <li><label>场上位置：</label><asp:Literal ID="SQWZ" runat="server"></asp:Literal></li>
       </ul>
    </div>
    
    <div><input type="hidden" name="hidTMId" id="hidTMId" runat="server" />
        <asp:Button ID="btnCheck" runat="server" CssClass="basic_btn" Text="确认踢除" 
            onclick="btnCheck_Click" /></div>  

</div>
    </form>
</body>
</html>
