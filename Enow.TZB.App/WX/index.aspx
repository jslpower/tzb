<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Enow.TZB.App.WX.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />

<title>铁子帮</title>
<link rel="stylesheet" href="IndexCSS/style.css" type="text/css" media="screen" />
<link rel="stylesheet" href="IndexCSS/home.css" type="text/css" media="screen" />


<link rel="stylesheet" type="text/css" href="IndexCSS/slider.css" media="screen" />
<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
<script type="text/javascript" src="js/jquery.event.drag-1.5.min.js"></script>
<script type="text/javascript" src="js/jquery.touchSlider.js"></script>
<script type="text/javascript" src="cordova.js"></script>
<script type="text/javascript" src="js/enow.core.js"></script>
<script type="text/javascript">
    $(function () {
        var winwid = $(window).width();
        var imgwid = 480;
        var imghei = 156;
        var winhei = Number(winwid) / Number(imgwid) * Number(imghei);
        $('.main_visual').css("height", winhei + "px");
    })


    $(document).ready(function () {


        $(".main_image").touchSlider({
            flexible: true,
            speed: 200,
            btn_prev: $("#btn_prev"),
            btn_next: $("#btn_next"),
            paging: $(".flicking_con a"),
            counter: function (e) {
                $(".flicking_con a").removeClass("on").eq(e.current - 1).addClass("on");
            }
        });


        timer = setInterval(function () {
            $("#btn_next").click();
        }, 3000);

        $(".main_visual").hover(function () {
            clearInterval(timer);
        }, function () {
            timer = setInterval(function () {
                $("#btn_next").click();
            }, 3000);
        });

        $(".main_image").bind("touchstart", function () {
            clearInterval(timer);
        }).bind("touchend", function () {
            timer = setInterval(function () {
                $("#btn_next").click();
            }, 3000);
        });

    });
</script>
<style type="text/css">
.icon_hometb {
  float: right;
  height: 15px;
  top: 15px;
  right: 15px;
  cursor: pointer;
  position: relative;
  z-index: 9999;
  color: #f4fffd;
}
.returnicoasd
{
   float: left;
  height: 15px;
  top: 15px;
  left:10px;
  cursor: pointer;
  position: relative;
  z-index: 9999;
  color: #f4fffd;
  }
.whitelink {text-decoration:none; color:white;}
</style>
</head>
<body>
    <form id="form1" runat="server">
<header class="head">
<b class="returnicoasd"><asp:Literal ID="littxt" runat="server"></asp:Literal></b>
<b class="icon_hometb">
<asp:Literal ID="litright" runat="server"></asp:Literal>
    <asp:LinkButton ID="LinkButton1" style="color: #f4fffd;" runat="server" 
        onclick="LinkButton1_Click" Visible="false">退出</asp:LinkButton></b>
    <h1>铁子帮</h1>
</header>

<div class="search_head home_search">
   <div class="search_form">
       <input type="text" class="input_txt floatL" id="txtsel" placeholder="球队搜索" value="" />
       <input name="" type="button" class="input_btn icon_search_i floatR" id="btn_sel" />
   </div>
</div>


<div class="home_warp">

    <!--baner------------start-->
     <div class="main_visual">
            <div class="flicking_con">
                <a href="#">1</a>
                <a href="#">2</a>
                <a href="#">3</a>
                <a href="#">4</a>
                <a href="#">5</a>
            </div>
            <div class="main_image">
                <ul>
                    <li><a href=""><img src="images/banner00.jpg"></a></li>
                    <li><a href=""><img src="images/banner01.jpg"></a></li>
                    <li><a href=""><img src="images/banner02.jpg"></a></li>
                    <li><a href=""><img src="images/banner03.jpg"></a></li>
                    <li><a href=""><img src="images/banner04.jpg"></a></li>
                </ul>
                <a href="javascript:;" id="btn_prev"></a>
                <a href="javascript:;" id="btn_next"></a>
            </div>
        </div>       
         
    <!--baner------------end-->
    
    
    <!--nav------------start-->
    <nav class="menu">
        <ul class="fixed">
            <li onclick="location.href='Article/TieOriginal.aspx'"><s class="m-icon01"></s><h2>铁原创</h2></li>
            <li onclick="location.href='Job/DZJob.aspx'"><s class="m-icon02"></s><h2>铁报名</h2></li>
            <li onclick="location.href='Rudder/Default.aspx?types=1'"><s class="m-icon03"></s><h2>舵主</h2></li>
            <li onclick="location.href='Rudder/Default.aspx?types=2'"><s class="m-icon04"></s><h2>堂主</h2></li>
            <li onclick="location.href='Team/Default.aspx'"><s class="m-icon05"></s><h2>铁丝球队</h2></li>
            <li onclick="location.href='Match/List.aspx'" class="mid"><s class="m-icon06"></s><h2>铁丝联赛</h2></li>
            <li onclick="location.href='AboutWar/Default.aspx'"><s class="m-icon07"></s><h2>铁丝约战</h2></li>
            <li onclick="location.href='Member/TZGathering.aspx?types=3'"><s class="m-icon08"></s><h2>铁丝聚会</h2></li>
            <li onclick="location.href='Team/BallField.aspx'"><s class="m-icon09"></s><h2>大联盟</h2></li>
            <li onclick="location.href='Article/TieShare.aspx'"><s class="m-icon10"></s><h2>培训堂</h2></li>
            <li onclick="location.href='Article/BigLove.aspx'"><s class="m-icon11"></s><h2>铁公益</h2></li>
        </ul>
    </nav>
    <!--nav------------end-->
    
    <div class="mt10 ad"><img src="images/home_ad.jpg"></div>
    
    <!--nav------------start-->
    <nav class="menu mt10">
        <ul class="fixed">
            <li onclick="location.href='Mall/Mall_Type.aspx?type=4'"><s class="m-icon12"></s><h2>足球旅游</h2></li>
            <li onclick="location.href='Mall/Mall_Type.aspx?type=3'"><s class="m-icon13"></s><h2>足球培训</h2></li>
            <li onclick="location.href='Mall/Mall_Type.aspx?type=2'"><s class="m-icon14"></s><h2>公益拍卖</h2></li>
            <li onclick="location.href='Mall/Mall_Type.aspx?type=1'"><s class="m-icon15"></s><h2>爱心益卖</h2></li>
        </ul>
    </nav>
    <!--nav------------end-->

    <!--home_bot------------start-->
    <div class="home_bot">
        <div class="menu_bot">
           <ul class="fixed">
               <li><s class="mb-icon01"></s><p>铁子帮</p></li>
               <li onclick="location.href='SmartWeb/PlayTogether.aspx'"><s class="mb-icon02"></s><p>一起玩吧</p></li>
               <li onclick="location.href='Mall/Mall_Type.aspx?type=5'"><s class="mb-icon03"></s><p>铁丝购</p></li>
               <li onclick="location.href='Member/Default.aspx'"><s class="mb-icon04"></s><p>我的</p></li>
           </ul>
        </div>
    </div>
    <!--home_bot------------end-->

</div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btn_sel").click(function () {
                var txtval = $("#txtsel").val();
                if (txtval!="") {
                    window.location.href = "Team/Default.aspx?KeyWord=" + txtval;
                }
                
            });
        });
    </script>
</body>
</html>
