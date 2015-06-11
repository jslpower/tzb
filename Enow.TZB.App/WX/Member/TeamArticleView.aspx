<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamArticleView.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.TeamArticleView" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html >
<head runat="server">
   <title>我的球队</title>
   <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
  <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
 <link rel="stylesheet" href="/WX/css/tangzhu.css" type="text/css" media="screen"/>
 <%--<script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>--%>
   <%-- <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />--%>
   <%-- <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>--%>
   <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Js/jq.mobi.min.js"></script>
   

</head>
<body>
    <form id="form1" runat="server">
   
     <input id="pageindex" type="hidden" value="<%=CurrencyPage %>" />
    <input id="hidPageend" type="hidden" value="0" />
    <uc1:UserHome ID="UserHome1" Userhometitle="我的球队" runat="server" />
<div class="warp">

  <div class="msg_tab"  id="n4Tab">     

        <div class="TabContent"> 
 			
                <!------------日志详情页面--也显示在当前标签下----->
          
                    <div class="tangzhu_jianli padd20" ">
                       <div class="font16 cent">  
                         <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                        </div>
                       <div class="font14 cent font_gray paddB10"> 
                         <asp:Literal ID="ltrIssueTime" runat="server"></asp:Literal>
                        </div>
                       
                       <div class="tangzhu_jieshao"><asp:Literal ID="ltrImg" runat="server"></asp:Literal><br />
                         <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                       </div>
                        <div class="log_date">
                       <a href="/WX/Member/ArticleLeaveWord.aspx?Id=&articleId=<%=ArticleId%>&flag=leave&leaveid=" class="liuyan"></a>
                       </div>
                </div>
                
                <!------------日志详情页面-end------>
                
                
                

        </div>
  </div>
</div>
    </form>
</body>
</html>
