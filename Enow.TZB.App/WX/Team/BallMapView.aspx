<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BallMapView.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.BallMapView" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>球场位置</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=v5eVZYKwoCUtgiLrzHoVx3Bi"></script>
    <style type="text/css">
	#allmap {width: 100%;height:100%;overflow: hidden;margin:0;font-family:"微软雅黑";}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="球场位置" runat="server" />
    <h1>球场位置</h1>
    </form>
    <div id="allmap"></div>
</body>
</html>
<script type="text/javascript">
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
    var point = new BMap.Point(<%=Longitude %>,<%=Latitude %>);
    map.centerAndZoom(point, 15);  // 初始化地图,设置中心点坐标和地图级别
    map.addControl(new BMap.MapTypeControl());   //添加地图类型控件
    map.setCurrentCity("");          // 设置地图显示的城市 此项是必须设置的
    var marker = new BMap.Marker(new BMap.Point(<%=Longitude %>,<%=Latitude %>));
    map.addOverlay(marker);
</script>