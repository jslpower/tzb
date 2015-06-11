<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.Web.WX.SmartWeb.Default" %>
<!DOCTYPE html>
<html>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen">
	<style type="text/css">
	body, html,#allmap {width: 100%;height: 100%;overflow: hidden;margin:0;font-family:"微软雅黑";}
	#result {width:100%;font-size:12px;}
	</style>
	<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=v5eVZYKwoCUtgiLrzHoVx3Bi"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/library/SearchInfoWindow/1.5/src/SearchInfoWindow_min.js"></script>
	<link rel="stylesheet" href="http://api.map.baidu.com/library/SearchInfoWindow/1.5/src/SearchInfoWindow_min.css" />
	<title>铁•门户</title>
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <div class="index_box" id="allmap"></div><div id="r-result"></div>
    <header class="index_head">

     <div class="index_search mt10">
        
        <div class="index_searchL"><a href="/WX/Member/Default.aspx"><img height="90%" src="../images/MemberIco.png" border="0" style="margin-left:-52px;" class="floatR" /></a></div>
        
       <div class="index_searchR">
            <div class="index_sform">
               <form id="form1" runat="server">
               <asp:TextBox ID="txtKeyword" runat="server" Text="" CssClass="input_txt2 floatL"></asp:TextBox>
               <img src="../images/SearchIco.png" border="0" style="margin-right:-41px;" class="floatR">
               </form>
            </div>
        </div>
        
     </div>

</header>

<nav>
  <div class="menu">
     <ul class="clearfix">
        <li><a href="/WX/Article/TieShare.aspx?type=3"><s class="menu01"></s><span>铁众享<em>Train</em></span></a></li>
        <li><a href="/WX/SmartWeb/PlayTogether.aspx"><s class="menu02"></s><span>一起玩吧<em>Let's Play</em></span></a></li>
        <li class="marginR"><a href="/WX/Team/BallField.aspx"><s class="menu03"></s><span>铁丝网<em>Family</em></span></a></li>
        <li><a href="/WX/Article/BigLove.aspx"><s class="menu04"></s><span>铁公益<em>Charity</em></span></a></li>
        <li><a href="/WX/Article/Default.aspx?TypeId=5"><s class="menu05"></s><span>坚强如铁<em>About</em></span></a></li>
        <li class="marginR"><a href="/WX/Article/TieOriginal.aspx"><s class="menu06"></s><span>铁原创<em>Article</em></span></a></li>
     </ul>
  </div>
</nav>


<script type="text/javascript">
    $(".menu li img").hover(
    function () {
        $(this).attr("src", "../images/menuon" + $(this).attr("menuId") + ".png");
    },
    function () {
        $(this).attr("src", "../images/menu" + $(this).attr("menuId") + ".png");
    }

    ); 
    // 百度地图API功能
    var map = new BMap.Map("allmap");    // 创建Map实例
    map.setMapStyle({
        styleJson: [
          {
              "featureType": "water",
              "elementType": "geometry",
              "stylers": {
                  "color": "#333739"
              }
          },
          {
              "featureType": "land",
              "elementType": "all",
              "stylers": {
                  "color": "#2ecc71"
              }
          },
          {
              "featureType": "highway",
              "elementType": "geometry",
              "stylers": {
                  "color": "#2ecc71",
                  "lightness": -33
              }
          },
          {
              "featureType": "arterial",
              "elementType": "all",
              "stylers": {
                  "color": "#2ecc71",
                  "lightness": -14
              }
          },
          {
              "featureType": "local",
              "elementType": "all",
              "stylers": {
                  "color": "#2ecc71",
                  "lightness": -18
              }
          },
          {
              "featureType": "root",
              "elementType": "labels.text.fill",
              "stylers": {
                  "color": "#ffffff"
              }
          },
          {
              "featureType": "root",
              "elementType": "labels.text.stroke",
              "stylers": {
                  "color": "#2ecc71"
              }
          },
          {
              "featureType": "railway",
              "elementType": "geometry",
              "stylers": {
                  "color": "#2ecc71",
                  "lightness": -33
              }
          },
          {
              "featureType": "administrative",
              "elementType": "geometry",
              "stylers": {
                  "color": "#333739"
              }
          },
          {
              "featureType": "manmade",
              "elementType": "all",
              "stylers": {
                  "color": "#2ecc71"
              }
          },
          {
              "featureType": "green",
              "elementType": "all",
              "stylers": {
                  "color": "#2ecc71"
              }
          },
          {
              "featureType": "building",
              "elementType": "all",
              "stylers": {
                  "color": "#2ecc71"
              }
          }
]
      });
    var sContent =  '<div style="width:100%;text-align:center;"><span style="margin:0 0 5px 0;padding:0.2em 0;font-size:14px;font-weight:bold;"><a href="/WX/Article/Default.aspx?TypeId=5">铁子帮足球俱乐部</a></span>&nbsp;&nbsp;<a href="/WX/Article/Default.aspx?TypeId=5">查看详情</a></div>' + 
                    '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;铁子帮足球俱乐部是由李铁、邹达联合肖战波和曲乐恒于5月18日发起创办。2014年8月成立浙江铁子帮体育交流策划发展有限公司。<br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;公司愿景：全球华人草根足球和青少年足球普及的推广者。 <br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;公司口号：Let\'s&nbsp;&nbsp;play &nbsp;&nbsp;一起玩吧！';;
    var point = new BMap.Point(120.143529,30.328065);
    map.centerAndZoom(point, 6);  // 初始化地图,设置中心点坐标和地图级别
    map.addControl(new BMap.MapTypeControl());   //添加地图类型控件
    map.enableScrollWheelZoom();
    map.setCurrentCity("");          // 设置地图显示的城市 此项是必须设置的

    var pt = new BMap.Point(120.143529,30.328065);
    var myIcon = new BMap.Icon("http://<%=Domain %>/WX/images/Maplogo.png", new BMap.Size(30, 30));
    var marker2 = new BMap.Marker(pt, { icon: myIcon });  // 创建标注
    map.addOverlay(marker2);              // 将标注添加到地图中
    //默认文字
    var infoWindow = new BMap.InfoWindow(sContent, { enableMessage: false });   // 创建信息窗口对象
    map.openInfoWindow(infoWindow, point); //开启信息窗口
    document.getElementById("r-result").innerHTML = infoWindow.getContent();
    setTimeout(function(){
        
    },80);
    //多点提示    
    function baiduMapSearchInfoWin(Id,marker){    
    //取得球场信息
        $.ajax({
            type: "get", 
            cache: false, 
            dataType: "json",
            url: "/Ashx/GetFieldInfo.ashx?Id=" + Id,
            success: function (response) {
                if (response && response.IsResult == "1") {
                    var title = response.Data.FieldName;
                    var sContent =
	                    '<div style="width:100%;text-align:center;"><span style="margin:0 0 5px 0;padding:0.2em 0;font-size:14px;font-weight:bold;"><a href="/WX/Team/BallFieldDetail.aspx?Id=' + Id + '">' + title + '</a></span>&nbsp;&nbsp;<a href="/WX/Team/BallFieldDetail.aspx?Id=' + Id + '">【查看详情】</a></div>' + 
                    '地址：' + response.Data.Address+ '<br/>电话：<a href="tel:' + response.Data.ContactTel + '">' + response.Data.ContactTel + '</a><br/>铁丝特惠价：￥' + response.Data.Price+ '元/2小时<br/>营业时间：' + response.Data.Hours+ '<br/>球场大小：' + response.Data.FieldSize;
	                    
                    var infoWindow = new BMap.InfoWindow(sContent, { enableMessage: false });  // 创建信息窗口对象
                    marker.openInfoWindow(infoWindow);
                }
                else {
                    alert("获取球场信息失败！");
                    return;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("获取球场信息失败！");
                return;
            }
        });  
    }    
    //添加多点及多点提示
    var FieldLatitudeJson = <%=FieldLatitudeJson %>;
    for (var i = 0; i < FieldLatitudeJson.length; i ++) {
        var Id = FieldLatitudeJson[i].Id;
        var Longitude = FieldLatitudeJson[i].Longitude;
        var Latitude = FieldLatitudeJson[i].Latitude;
		var point = new BMap.Point(Longitude,Latitude);
		addMarker(Id,point);
	}
   // 编写自定义函数,创建标注
	function addMarker(Id,point){
	  var marker = new BMap.Marker(point);
      marker.addEventListener("click", function(e){
	        baiduMapSearchInfoWin(Id,marker);
        });
	  map.addOverlay(marker);
	}
    
</script>
</body>
</html>