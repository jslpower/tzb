<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Enow.TZB.App.WX.login" %>

<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">

<title>登录</title>
<link rel="stylesheet" href="IndexCSS/style.css" type="text/css" media="screen" />
<link rel="stylesheet" href="IndexCSS/login.css" type="text/css" media="screen" />
<script src="JS/jquery-1.7.1.min.js" type="text/javascript"></script>

<body>
    <form id="form1" runat="server">
<header class="head">
    <a href="index.aspx"><b class="icon_home"></b></a>
    <h1>登录</h1>
    <a href="javascript:history.back();"><i class="returnico"></i></a>
</header>

<div class="user_warp">
  
    <div class="login_form">
        
           <ul>
               <li>
                  <span class="label_name">账号</span>
                  <asp:TextBox runat="server" ID="txtusname" CssClass="l-input" placeholder="请输入"></asp:TextBox>
               </li>
               <li>
                  <span class="label_name">密码</span>
                   <asp:TextBox runat="server" ID="txtuspassword" CssClass="l-input" 
                       placeholder="请输入" TextMode="Password"></asp:TextBox>
               </li>
          </ul>
     </div>
     
     <div class="mt20 cent">
          <asp:Button runat="server" ID="btn_dl" style="width:90%;" Text="登录" 
              OnClientClick="return Loginyz()" CssClass="basic_ybtn" onclick="btn_dl_Click" />
     </div>
     
     <div class="mt20 cent">
          <a href="Register/Step1.aspx" class="basic_wbtn" style="width:90%;">注册</a>
        
     </div>
  
  
</div>
    </form>
    <script type="text/javascript">
        function Loginyz() {
            var txtusname = $("#txtusname").val();
            var txtuspassword = $("#txtuspassword").val();
            if (txtusname == "") {
                alert("请输入账号密码！");
                return false;
            }
            if (txtuspassword == "") {
                alert("请输入账号密码！");
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
