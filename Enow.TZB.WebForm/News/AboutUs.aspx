<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="Enow.TZB.WebForm.News.AboutUs" %>
<%@ Register Src="/UserControl/TopBar.ascx" TagName="TopBar" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/Menu.ascx" TagName="menu" TagPrefix="uc2"%>
<%@ Register Src="/UserControl/Footer.ascx" TagName="footer" TagPrefix="uc3"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta name="keywords" content="" />
    <title>无标题文档</title>
    <link href="/css/style.css" rel="stylesheet" />
     <link href="/Css/boxy.css" rel="Stylesheet" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="/Js/table-toolbar.js"></script>
    <script type="text/javascript" src="/Js/jquery.boxy.js"></script>
</head>
<body>
   <uc1:TopBar ID="topbar1" runat="server" />
 <uc2:menu ID="menu1" runat="server" />
    <div class="head_line">
    </div>
    <div class="warp fixed">
        <div class="leftside">
            <h3>
                关于我们</h3>
            <div class="left_nav">
                <ul>
                    <li><a href="AboutUs.aspx?Id=56&HmId=1"<%if(Id==56){ %>  class="on"<%} %>>铁子帮简介</a></li>
                    <li><a href="AboutUs.aspx?Id=55&HmId=1"<%if(Id==55){ %>  class="on"<%} %>>发展规划</a></li>
                    <li><a href="AboutUs.aspx?Id=79&HmId=1"<%if(Id==79){ %>  class="on"<%} %>>管理团队</a></li>
                     <li><a href="DZList.aspx">舵主风采</a></li>
                     <li><a href="TZList.aspx">堂主风采</a></li>
                    <li><a href="AboutUs.aspx?Id=80&HmId=1"<%if(Id==80){ %>  class="on"<%} %>>联系我们</a></li>
                </ul>
            </div>
        </div>
        <div class="rightsied">
            <div class="news_box">
                <div class="sideT fixed">
                    <h3>
                        <%=PageTitle %></h3>
                    <div class="Rtit">
                        首页 >  <%=PageTitle %></div>
                </div>
                <div class="news_cont">
                    <div class="news_title cent">
                       <%=Title %></div>
                    <p class="cent">
                       <%=IssueTime.ToString("yyyy-MM-dd HH:mm:ss") %></p>
                    <p class="cent">
                      <asp:Literal ID="ltrPhoto" runat="server"></asp:Literal></p>
                       <%=Content %>
                </div>
            </div>
        </div>
    </div>
   <uc3:footer ID="footer" runat="server" />

</body>
</html>