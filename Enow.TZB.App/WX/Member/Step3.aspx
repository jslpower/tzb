<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step3.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.Step3" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>偏号信息修改 step 3</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="偏号信息修改" runat="server" />
     <div class="warp">
        <div class="msg_tab" id="n4Tab">
            <div class="TabTitle">
                <ul class="fixed">
                    <li id="n4Tab_Title0"><a href="MemberUpdate.aspx">账号</a></li>
                    <li id="n4Tab_Title1"><a href="Step2.aspx">实名</a></li>
                    <li id="n4Tab_Title2" class="active" style="width: 34%;"><a href="Step3.aspx">偏好</a></li>
                </ul>
            </div>
            <div class="TabContent">
                <div id="n4Tab_Content2">
                    <div class="msg_list">
                        <ul>
                            <li>
                                <label>
                                    场上位置：</label><input id="txtCSWZ" name="txtCSWZ" type="text" runat="server" class="input_bk formsize100" /></li>
                            <li>
                                <label>
                                    常用球衣号：</label><input id="txtCYQY" name="txtCYQY" type="text" runat="server" class="input_bk formsize100" /></li>
                            <li>
                                <label>
                                    常用装备品牌：</label><input id="txtCYZB" name="txtCYZB" type="text" runat="server" class="input_bk formsize150" /></li>
                            <li>
                                <label>
                                    每周踢球次数：</label><input id="txtMZTQS" name="txtMZTQS" type="text" runat="server" class="input_bk formsize150" /></li>
                            <li>
                                <label>
                                    关注球队：</label><input id="txtGZQD" name="txtGZQD" type="text" runat="server" class="input_bk formsize150" /></li>
                        </ul>
                    </div>
                    <div class="msg_btn">
                                <asp:Button CssClass="basic_btn" ID="Button1" runat="server" Text="保 存" 
                                onclick="btnSave_Click" />
                </div>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>