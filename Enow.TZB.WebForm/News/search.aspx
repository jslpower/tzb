<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="Enow.TZB.WebForm.News.search" %>
<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/Menu.ascx" TagName="Menu1" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/TopBar.ascx" TagName="Topbar1" TagPrefix="uc2" %>
<%@ Register Src="/UserControl/Footer.ascx" TagName="footer1" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>搜索结果页</title>
    <link href="/Css/style.css" rel="Stylesheet" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="/Js/common.js"></script>
</head>
<body>
    <uc2:Topbar1 ID="topBar1" runat="server" />
    <uc1:Menu1 ID="menu1" runat="server" />
    <div class="head_line">
    </div>
    <div class="warp fixed">
        <div class="leftside">
            <h3>
                爱上足球</h3>
            <div class="left_nav">
                <ul>
                    <li><a href="NewsList.aspx?ClassId=16&HmId=1" class="on">爱上足球</a></li>
                    <li><a href="NewsList.aspx?ClassId=21&HmId=6">铁文集</a></li>
                </ul>
            </div>
        </div>
        <div class="rightsied">
            <div class="news_box mt10">
                <div class="sideT fixed">
                    <h3>
                        搜索结果</h3>
                    <div class="Rtit">
                        首页 > 搜索结果</div>
                </div>
                <div class="news">
                    <ul>
                        <asp:Repeater ID="rptNewsList" runat="server">
                            <ItemTemplate>
                                <li><a href="NewsDetail.aspx?id=<%#Eval("id")%>">
                                    <%#Enow.TZB.Utility.Utils.GetText2(Eval("Title").ToString() ,20,true)%></a><span><%#Convert.ToDateTime(Eval("IssueTime")).ToString("yyyy-MM-dd") %></span></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="page" id="page" style="width: 100%; text-align: center; margin: 0px auto 0px;
                    clear: both; margin-top: 15px; margin-bottom: 15px;">
                     <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <uc3:footer1 ID="footer1" runat="server" />
</body>
</html>
