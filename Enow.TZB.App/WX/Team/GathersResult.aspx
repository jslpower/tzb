<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GathersResult.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.GathersResult" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html >
<head runat="server">
    <meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>我的约战</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/user.css" type="text/css" media="screen"/>

<script type="text/javascript">
   
</script>
</head>
<body>
    <uc1:UserHome ID="UserHome1" Userhometitle="我的约战" runat="server" />
    <form id="form1" runat="server">
      
<div class="warp">
    
    <div class="msg_tab"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0"  ><a href="MyGathers.aspx">约战中</a></li>
              <li id="n4Tab_Title1" ><a href="GathersGoing.aspx">进行中</a></li>
              <li id="n4Tab_Title2"  class="active"><a href="GathersResult.aspx">战报</a></li>
           </ul>
        </div>
   <div class="form_list">
       <ul>
           <li>
               <span class="label_name">约战主队</span>
               <asp:TextBox ID="txtZTeam" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
               
           </li>
           
           <li>
               <span class="label_name">上</span>
                <asp:TextBox ID="txtZUp" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
               
               
           </li>

           <li>
            
               
               <span class="label_name">下</span>
               <asp:TextBox ID="txtZDown" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
             
           </li>
           <li>
               <span class="label_name">约战客队</span>
                <asp:TextBox ID="txtCTeam" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
               
              
           </li>

           <li>
               <span class="label_name">上</span>
                <asp:TextBox ID="txtCUp" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
               
               
           </li>

           <li>
               <span class="label_name">下</span>
                <asp:TextBox ID="txtCDown" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
               
              
           </li>

           <li>
               <span class="label_name">赛报填写</span>

              <%-- <span class="upload_img">--%>
              <%-- <i class="add_icon"></i>上传图片--%>
             
            <%--   </span>--%>
                <asp:FileUpload ID="imgFileUpload" runat="server" />
           </li>
           
       </ul>
   </div>
   
<%--   <div class="qiu_box">--%>
      <%-- <div class="qiu-cont" id="txtGatherInfo" runat="server"  contenteditable="true">
          
      
       </div>--%>
    
<%--   </div>--%>
    <asp:TextBox ID="txtGatherInfo" runat="server" TextMode="MultiLine" Width="1260px"></asp:TextBox>
   </div>
   
   <div class="msg_btn">
       <asp:Button ID="btnSave" CssClass="basic_btn"  runat="server" Text="提交" 
           onclick="btnSave_Click" />
  
   </div>
   
   
</div>

    </form>
</body>
</html>
