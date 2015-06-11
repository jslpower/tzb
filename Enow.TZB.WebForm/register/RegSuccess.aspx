<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegSuccess.aspx.cs" Inherits="Enow.TZB.WebForm.register.RegSuccess" %>


<%@ Register Src="/UserControl/TopBar.ascx" TagName="TopBar" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/Menu.ascx" TagName="menu" TagPrefix="uc2" %>
<%@ Register Src="/UserControl/Footer.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="utf-8">
    <meta name="keywords" content="" />
    <title>注册成功</title>
    <link href="/css/style.css" rel="stylesheet" />
    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>
</head>
<body>
<uc1:TopBar ID="topBar1" runat="server" />
<uc2:menu ID="menu1" runat="server" />
    <div class="head_line">
    </div>
    <div class="warp">
        <div class="reg_form">
            <div class="reg_ok">
                <div class="tishi">
                    <img src="/images/reg_ok.gif">&nbsp;注册成功！</div>
                   <div class="reg_txt mt10">
                    您可在微信中搜索关注铁子帮官方微信服务号（TIEZIBANG）并点击“铁微站-&gt;成为铁丝-&gt;帐号绑定”进行铁子帮帐号与微信帐号的绑定。</div>
                <div class="reg_txt mt10">
                    欢迎加入铁子帮<em id="emtime">10</em> 秒后跳转到首页<a href="/Default.aspx">立即跳转</a></div>
            </div>
        </div>
    </div>
   <uc3:footer ID="footer" runat="server" />
</body>
</html>
<script type="text/javascript" language="javascript">
    //设定倒数秒数  
    var t =10;
    //显示倒数秒数  
    function showTime() {
        t -= 1;
        document.getElementById('emtime').innerHTML = t;
        if (t == 1) {
            location.href = '/Default.aspx';
        }
        //每秒执行一次,showTime()  
        setTimeout("showTime()", 1000);
    }


    //执行showTime()  
    showTime();
</script>

