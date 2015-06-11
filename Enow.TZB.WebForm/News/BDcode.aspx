<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BDcode.aspx.cs" Inherits="Enow.TZB.WebForm.News.BDcode" %>
<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<%@ Register Src="/UserControl/Menu.ascx" TagName="Menu1" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/TopBar.ascx" TagName="Topbar1" TagPrefix="uc2" %>
<%@ Register Src="/UserControl/Footer.ascx" TagName="footer1" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
    <meta name="keywords" content="" />
    <title>绑定码</title>
    <link href="/Css/style.css" rel="Stylesheet" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="/Js/common.js"></script>
    <style type="text/css">
    .sel{
    	margin-left:15px;
    	color: #fff;
    	font-size: 15px;
    	background: #1d8a73;
    	height: 25px;
    	width: 35px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <uc2:Topbar1 ID="topBar1" runat="server" />
    <uc1:Menu1 ID="menu1" runat="server" />
    <div class="head_line">
    </div>
    <div class="warp fixed">
        <div class="leftside">
            <h3>
                铁众享</h3>
            <div class="left_nav">
                <ul>
                    <li><a href="TieShare.aspx">爱上足球</a></li>
                    <li><a href="/Notice/Notice.aspx">培训报名</a></li>
                    <li><a href="/Notice/Notice.aspx">堂主风采</a></li>
                    <li><a href="TieShare.aspx?ClassId=100" class="on">铁子帮爱高</a></li>
                </ul>
            </div>
        </div>
        <div class="rightsied">
            <div class="news_box mt10">
                <div class="sideT fixed">
                    <h3>
                        铁之帮爱高</h3>
                    <div class="Rtit">
                        首页 >
                        铁之帮爱高
                        > 视频码绑定</div>
                </div>
                <div class="news">
                        <div class="contentbox">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
             <th width="150" height="40" align="right">
                    视频码：
                </th>
                <td>
                   <asp:TextBox ID="txtcode" runat="server"></asp:TextBox>
                   <asp:Button ID="butclisk" CssClass="sel" runat="server" Text="绑定" 
                        onclick="butclisk_Click" />
                </td>
            </tr>
        </table>
    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer1 ID="footer1" runat="server" />
    </form>
</body>
</html>
 <script type="text/javascript">
     /*
     $(function () {
     $(".bannerimg").YEXfocus({ direction: 'top' });
     });
     */
    </script>
