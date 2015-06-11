<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamQuit.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.TeamQuit" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>队员信息</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/tangzhu.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jquery-1.10.2.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
   <uc1:UserHome ID="UserHome1" runat="server" />
    <div class="warp">
        <div class="msg_tab Tab_lie2" id="n4Tab">
            <div class="TabContent">
                <div id="n4Tab_Content0">
                    <div class="tangzhu_jianli padd10">
                        <div class="tangzhu_xinxi">
                            <asp:Literal ID="litimage" runat="server"></asp:Literal>
                            <p>
                                姓名:<asp:Literal ID="litname" runat="server"></asp:Literal>
                            </p>
                            <p>
                                职位:<asp:Literal ID="litgzjy" runat="server"></asp:Literal></p>
                            <p>
                                编号：<asp:Literal ID="litjuzhudi" runat="server"></asp:Literal></p>
                                 <p>
                                位置：<asp:Literal ID="Literal1" runat="server"></asp:Literal></p>
                            <p>
                                入队时间：<asp:Literal ID="litshouji" runat="server"></asp:Literal></p>
                        </div>
                        <%--<div class="tangzhu_jieshao paddT20 font_gray">
                            <asp:Literal ID="litContent" runat="server"></asp:Literal>
                        </div>--%>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
