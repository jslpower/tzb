<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayBill.aspx.cs" Inherits="Enow.TZB.Web.WX.Fund.PayBill" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>账单</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="账单" runat="server" />
<div class="warp">

  <div class="msg_tab"  id="n4Tab">

        <div class="TabTitle">
           <ul class="fixed">
              <li><a href="Default.aspx">充值</a></li>
              <li><a href="PayPassword.aspx">支付密码</a></li>
              <li class="active" style="width:34%;"><a href="PayBill.aspx">账单</a></li>
           </ul>
        </div>

        <div class="TabContent">
        
			<div id="n4Tab_Content2">
            
               <div class="msg_list chongzhi_list">
                   <!--
                   <div class="chongzhi_s"><select name="">
                           <option>2014</option>
                       </select><select name="">
                           <option>11</option>
                       </select><select name="">
                           <option>充值</option>
                           <option>支付</option>
                           <option>代付</option>
                       </select></div>
                    -->
                   <ul>
                   <asp:Repeater ID="rptList" runat="server">
                   <ItemTemplate>
                   <li>
                           <div class="chongzhi_listL">
                              <p><%#((Enow.TZB.Model.EnumType.财务流水类型)Convert.ToInt32(Eval("TypeId"))).ToString()%></p>
                              <p class="font14 font_gray"><%#Eval("IssueTime","{0:MM-dd}")%></p>
                           </div>
                           <div class="chongzhi_listR">
                              <p><%#BillOperation((Enow.TZB.Model.EnumType.财务流水类型)Convert.ToInt32(Eval("TypeId")))%><%#Eval("TradeMoney", "{0:F2}")%></p>
                              <p class="font14 font_gray"><%#((Enow.TZB.Model.EnumType.财务流水类型)Convert.ToInt32(Eval("TypeId"))).ToString()%>成功</p>
                           </div>
                       </li>
                   </ItemTemplate>
                   </asp:Repeater>
                   </ul>
               
               
               </div>
                
            </div>
            
            
         </div>


   
  </div>  
   
   

</div>
    </form>
</body>
</html>