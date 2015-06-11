<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BallField.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.BallField" %>

<%@ Register Src="../UserControls/UserHome.ascx" TagName="UserHome" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>大联盟</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
     <link rel="stylesheet" href="/wx/css/IScrol.css" type="text/css" />
    <script src="../../Js/jq.mobi.min.js" type="text/javascript"></script>
    <script src="../../Js/IScroll/IScroll4.2.5.js" type="text/javascript"></script>
    <script type="text/javascript">
        var myScroll, pullDownEl, pullDownOffset,
	pullUpEl, pullUpOffset,
	generatedCount = 0;

        function pullDownAction() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, i;
                el = document.getElementById('fiullist');

                for (i = 0; i < 8; i++) {
                    $(el).append("<li>Generated row " + (++generatedCount) + "</li>")
                }

                myScroll.refresh(); 	// Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000); // <-- Simulate network congestion, remove setTimeout from production!
        }

        function pullUpAction() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, i;
                el = document.getElementById('fiullist');
                /*==============================================================*/
                var index = parseInt($("#pageindex").val()) + 1;
                var pageEnd = $("#hidPageend").val();
                var KeyWord = '<%=Enow.TZB.Utility.Utils.GetQueryStringValue("KeyWord") %>';
                var CityId = '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("CityId"), 0) %>';
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    if (KeyWord == "")
                        KeyWord = "";
                    var para = { KeyWord: KeyWord, Page: index, CityId: CityId };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxBallFieldList.aspx?" + $.param(para),
                        cache: false,
                        success: function (result) {
                            if (result != "") {
                                $(el).append(result);
                            } else {
                                $("#pullUp").hide();
                                $("#hidPageend").val("1");
                            }
                        },
                        error: function () {
                            alert("异常请重新提交！");
                        }
                    });
                    myScroll.refresh(); 	// Remember to refresh when contents are loaded (ie: on ajax completion)
                }
            }, 1000);             // <-- Simulate network congestion, remove setTimeout from production
        }

        function loaded() {

            pullUpEl = document.getElementById('pullUp');
            if (pullUpEl == null) return;
            pullUpOffset = pullUpEl.offsetHeight

            myScroll = new iScroll('wrapper', {
                mouseWheel: true,
                click: true,
                checkDOMChanges: true,
                onRefresh: function () {
                    if (pullUpEl.className.match('loading')) {
                        pullUpEl.className = '';
                        pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
                    }
                },
                onScrollMove: function () {
                    if (this.y < (this.maxScrollY - 5) && !pullUpEl.className.match('flip')) {
                        pullUpEl.className = 'flip';
                        pullUpEl.querySelector('.pullUpLabel').innerHTML = '放开刷新..';
                        this.maxScrollY = this.maxScrollY;
                    } else if (this.y > (this.maxScrollY + 5) && pullUpEl.className.match('flip')) {
                        pullUpEl.className = '';
                        pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
                        this.maxScrollY = pullUpOffset;
                    }
                },
                onScrollEnd: function () {
                    if (pullUpEl.className.match('flip')) {
                        pullUpEl.className = 'loading';
                        pullUpEl.querySelector('.pullUpLabel').innerHTML = '正在加载...';
                        pullUpAction(); // Execute custom function (ajax call?)
                    }
                }
            });
        }

        document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);
        document.addEventListener('DOMContentLoaded', loaded, false);
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <input id="pageindex" type="hidden" value="<%=CurrencyPage %>" />
    <input id="hidPageend" type="hidden" value="0" />
    <uc1:UserHome ID="UserHome1" Userhometitle="大联盟" runat="server" />
    <div class="warp">
        <div class="search_head juhui_search">
            <div class="search_form">
                <span class="city_name" onclick="location.href='/WX/city.aspx?action=Team&fhdz=BallField'">
                    <asp:Literal runat="server" ID="litcityname"></asp:Literal></span>
                <input type="text" name="txtKeyWord" runat="server" id="txtKeyWord" class="input_txt floatL" />
                <asp:Button ID="btnSearch" runat="server" CssClass="input_btn icon_search_i floatR"
                    OnClick="btnSearch_Click" />
            </div>
        </div>
        <div class="msg_tab" style="  margin-top: 100px;">
            <div class="TabTitle">
                <ul class="fixed">
                    <li class="active"><a href="/WX/Team/BallField.aspx">球场</a></li>
                    <li><a href="/WX/AboutWar/Default.aspx">约战</a></li>
                    <li><a href="/WX/Member/TZGathering.aspx?types=2">培训</a></li>
                </ul>
            </div>
            <div class="TabContent" id="wrapper" style=" margin-top:150px;">
                <div id="n4Tab_Content0">
                    <div class="qiu_list tisi_list">
                        <ul id="fiullist">
                            <asp:Repeater ID="rptList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="item-img">
                                            <a href="BallFieldDetail.aspx?Id=<%#Eval("Id") %>">
                                                <img src="<%#System.Configuration.ConfigurationManager.AppSettings["Uploadimgurl"]+Eval("FieldPhoto") %>" /></a></div>
                                        <div class="item-box">
                                            <dl>
                                                <dt><a href="BallFieldDetail.aspx?Id=<%#Eval("Id") %>">
                                                    <%#Eval("FieldName")%></a><!--<span class="tiesi_type01"><%#(Enow.TZB.Model.EnumType.CourtEnum)Convert.ToInt32(Eval("TypeId"))%></span>--></dt>
                                                <dd>
                                                    ￥<%#Eval("Price","{0:F2}")%>元/2小时</dd>
                                                <dd>
                                                    营业时间：<%#Eval("Hours")%></dd>
                                                <dd>
                                                    球场大小：<%#Eval("FieldSize")%></dd>
                                            </dl>
                                            <div class="qiu-caozuo">
                                                <a href="BallFieldSignUp.aspx?QID=<%#Eval("Id") %>" class="basic_ybtn">点击预约</a><a
                                                    href="/WX/AboutWar/AboutWarAdd.aspx?QID=<%#Eval("Id") %>" class="basic_rbtn">约战</a></div>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                        <div id="pullUp">
                            <span class="pullUpIcon"></span><span class="pullUpLabel">上拉更新...</span>
                        </div>
                    </asp:PlaceHolder>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
