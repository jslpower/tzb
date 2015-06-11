<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gathers.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.Gathers" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>约战</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/user.css" type="text/css" media="screen"/><script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
 <script type="text/javascript">
     $(function () {
         $("a[flag='mtype']").click(function () {
            
            
             if ($("#hdType").val()!="") {
                 alert('约战赛制只能选择一种！')
                
                 return;
             }
             $("#hdType").val($(this).attr("data"))
            
             $(this).addClass("on");
             return;
         })
         $("a[operat='fee']").click(function () {
         
           
             if ($("#hdFee").val() != "") {
                 alert('约战费用只能选择一种！')
                 return;
             }
             $("#hdFee").val($(this).attr("data"))
             $(this).addClass("on");
         })
     })
    </script>
</head>
<body>
    <uc1:UserHome ID="UserHome1" Userhometitle="约战" runat="server" />
    <form id="form1" runat="server">
    <input id="hdType" type="hidden" value="" runat="server" />
    <input id="hdFee" type="hidden" value="" runat="server"/>
    <div class="warp">
        
   <div class="form_list">
       <ul>
           <li>
               <span class="label_name">约战球队</span>
               <asp:TextBox ID="txtGatherName" runat="server" CssClass="u-input" placeholder="请输入"></asp:TextBox>
             
           </li>

           <li>
               <span class="label_name">约战时间</span>
               
               <asp:TextBox ID="txtGatherTime" onfocus="WdatePicker()" runat="server" CssClass="u-input" placeholder="请输入"></asp:TextBox>
              
           </li>

           <li>
               <span class="label_name">约战地点</span>
               <asp:TextBox ID="txtGatherPlace" runat="server" CssClass="u-input" placeholder="请输入"></asp:TextBox>
              
           </li>
           
           <li>
               <span class="label_name">赛制</span>
               
               <span class="feiyong">
                  <a flag="mtype" data="5" href="javascript:void(0)">5人</a>
                  <a flag="mtype" data="8" href="javascript:void(0)">8人</a>
                  <a flag="mtype" data="11" href="javascript:void(0)">11人</a>
               </span>
           </li>

           <li>
               <span class="label_name">费用</span>
               <span class="feiyong">
                  <a operat="fee" data="1" href="#">AA制</a>
                  <a operat="fee" data="2" href="#">主方</a>
                  <a  operat="fee" data="3" href="#">败方</a>
               </span>
           </li>

           <li>
               <span class="label_name">战书</span>
               <%--<asp:TextBox ID="txtWarBook"   runat="server" CssClass="u-input" TextMode="MultiLine" placeholder="请输入约战说明"></asp:TextBox>--%>
               <textarea id="txtWarBook" class="u-input"  runat="server"></textarea>
            
           </li>
           
       </ul>
   </div>
   
 
   <div class="foot_fixed">
           <div class="foot_box">
                  <div class="paddL10 paddR10">
                  <asp:Button CssClass="basic_btn" ID="btnSave" runat="server" Text="下一步" OnClick="btnSave_Click" />
                 <%-- <input type="button" value="下一步" class="basic_btn" onclick="location.href='Gathers_Step2.aspx'"/>--%>
                  </div>
           </div>
   </div>
   
   
</div>
    </form>
</body>
</html>
