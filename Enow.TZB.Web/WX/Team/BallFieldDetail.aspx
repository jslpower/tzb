<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BallFieldDetail.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.BallFieldDetail" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>球场详情</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="球场详情" runat="server" />
<div class="warp">

   <div class="qiu_list tisi_list">
      <ul>
         <li>
            <div class="item-img"><asp:Literal ID="ltrImg" runat="server"></asp:Literal></div>
            <div class="item-box">
                <dl>
                    <dt><asp:Literal ID="ltrFieldName" runat="server"></asp:Literal><!--<span class="tiesi_type01"><asp:Literal ID="ltrType" runat="server"></asp:Literal></span>--></dt>
                    <dd>铁丝特惠价：￥<asp:Literal ID="ltrPrice" runat="server"></asp:Literal>元/2小时</dd>
                    <dd>市场价：￥<asp:Literal ID="ltrMarkPrice" runat="server"></asp:Literal>元/2小时</dd>
                    <dd>球场数量：<asp:Literal ID="ltrNumber" runat="server"></asp:Literal></dd>
                    <dd>营业时间：<asp:Literal ID="ltrHour" runat="server"></asp:Literal></dd>
                    <dd>球场大小：<asp:Literal ID="ltrSize" runat="server"></asp:Literal></dd>
                </dl>
            </div>
         </li>
         
         <li class="address"><asp:Literal ID="ltrAddress" runat="server"></asp:Literal></li>
          <li class="tel"><asp:Literal ID="ltrContactTel" runat="server"></asp:Literal></li>
      </ul>
   </div>
   
   <div class="qiu_box">
       <div class="qiu-cont">
       <asp:Literal ID="ltrRemark" runat="server"></asp:Literal>
       </div>
   </div>
   <asp:PlaceHolder ID="phImgList" runat="server">
   <div class="qiu_box mt10">
      <div class="qiu-cont tisi_img">
         <ul>
         <asp:Literal ID="ltrImgList" runat="server"></asp:Literal>
         </ul>
      </div>
   </div>
   </asp:PlaceHolder>

</div>
<div class="mt20 padd_bot">
                <a href="BallFieldSignUp.aspx?QID=<%=Enow.TZB.Utility.Utils.GetQueryStringValue("Id")%>" class="basic_btn" id="btnsave">预约</a></div>
    </form>
</body>
</html>
