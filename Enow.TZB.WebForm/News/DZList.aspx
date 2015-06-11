<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DZList.aspx.cs" Inherits="Enow.TZB.WebForm.News.DZList" %>
<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<%@ Register Src="/UserControl/Menu.ascx" TagName="Menu1" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/TopBar.ascx" TagName="Topbar1" TagPrefix="uc2" %>
<%@ Register Src="/UserControl/Footer.ascx" TagName="footer1" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="utf-8">
    <meta name="keywords" content="" />
    <title>
      关于我们</title>
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
               关于我们</h3>
            <div class="left_nav">
                <ul>
                    <li><a href="AboutUs.aspx?Id=56&HmId=1">铁子帮简介</a></li>
                    <li><a href="AboutUs.aspx?Id=55&HmId=1">发展规划</a></li>
                    <li><a href="AboutUs.aspx?Id=79&HmId=1">管理团队</a></li>
                     <li><a href="DZList.aspx" class="on">舵主风采</a></li>
                     <li><a href="TZList.aspx">堂主风采</a></li>
                    <li><a href="AboutUs.aspx?Id=80&HmId=1">联系我们</a></li>
                </ul>
            </div>
        </div>
        <div class="rightsied">
        <%if (ImgCount > 0)
          {%>
            <div class="banner nei_banner">
                <div class="bannerimg">
                    <ul style="opacity: 1; top: 0px;" >
                        <asp:Repeater ID="rptPhotoList" runat="server">
                            <ItemTemplate>
                                <li><span><a href="#">
                                    <img src="<%#Eval("PhotoUrl")%>"></a></span></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <ol class="btn">
                        <asp:Repeater ID="rptCount" runat="server">
                            <ItemTemplate>
                                <li class="select">
                                    <%#Container.ItemIndex+1%></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ol>
                </div>
            </div>
            <%} %>
            <div class="news_box mt10">
                <div class="sideT fixed">
                    <h3>
                        <%=PageTitle %></h3>
                    <div class="Rtit">
                        首页 >
                        <%=PageTitle %>
                        > 文章列表</div>
                </div>
                <div class="news">
                    <ul>
                        <asp:Repeater ID="rptNewsList" runat="server">
                            <ItemTemplate>
                                <li><a href="DZDetail.aspx?id=<%#Eval("id")%>&ClassId=18&HmId=<%=HmId %>">
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
    <script type="text/javascript">

        $(function () {
            $(".bannerimg").YEXfocus({ direction: 'top' });
        });
    </script>