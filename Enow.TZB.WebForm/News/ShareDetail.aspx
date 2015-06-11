<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareDetail.aspx.cs" Inherits="Enow.TZB.WebForm.News.ShareDetail" %>
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
<form id="form1" runat="server">
   <uc1:TopBar ID="topbar1" runat="server" />
 <uc2:menu ID="menu1" runat="server" />
    <div class="head_line">
    </div>
    <div class="warp fixed">
        <div class="leftside">
            <h3>
                舵主风采</h3>
            <div class="left_nav">
                <ul>
                    <li><a href="TieShare.aspx" class="on">爱上足球</a></li>
                    <li><a href="/Notice/Notice.aspx">培训报名</a></li>
                    <li><a href="/Notice/Notice.aspx">堂主风采</a></li>
                    <li><a class="dul2" href="TieShare.aspx?ClassId=100">铁子帮爱高</a></li>
                </ul>
            </div>
        </div>
        <div class="rightsied">
            <div class="news_box">
                <div class="sideT fixed">
                    <h3>
                        <%=PageTitle %></h3>
                    <div class="Rtit">
                        首页 >  <%=PageTitle %> > 详细</div>
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
   <script type="text/javascript">
       var lodemodel = {
           classno: '<%=Enow.TZB.Utility.Utils.GetQueryStringValue("ClassId") %>',
           getclassload: function () {
               if (lodemodel.classno == "100") {
                   $(".left_nav ul li a").removeClass("on");
                   $(".dul2").addClass("on");
               }

           }
       }
       $(document).ready(function () {
           lodemodel.getclassload();
       });
   </script>
   </form>
</body>
</html>