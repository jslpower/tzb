<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LotteryUserlist.aspx.cs" Inherits="Enow.TZB.Web.WX.Vote.LotteryUserlist" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>抽奖</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/mall.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jq.mobi.min.js" type="text/javascript"></script>
     <link rel="stylesheet" href="/wx/css/IScrol.css" type="text/css" />
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
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    var Vid='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("Vid")%>';
                    var para = { Vid: Vid, Page: index };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxLotteryUserlist.aspx?" + $.param(para),
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
    <uc1:UserHome ID="UserHome1" Userhometitle="中奖名单" runat="server" />
    <div class="user_warp">
        <div class="TabTitle" style="margin-top: 10px;">
           <ul class="fixed">
              <li id="n4Tab_Title0"   style="margin-left: 15%;"><a href="LotteryInfo.aspx?Vid=<%=Enow.TZB.Utility.Utils.GetQueryStringValue("Vid")%>">详情</a></li>
              <li id="n4Tab_Title1" class="active" ><a href="javascript:void(0);">中奖名单</a></li>
           </ul>
        </div>
        <div class="cont gray_lineB font14" style="text-align: center;">
            <asp:Literal ID="ltrGoodsName" runat="server"></asp:Literal></div>
        <div class="list-item" id="wrapper" style="  position: static;">
            <ul id="fiullist">
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <label>
                            <li>
                                <%#Container.ItemIndex+1%>.<%#Eval("ContactName")%>
                                <span style="float: right;"><%#Eval("AwardsNum") == "0" ? "未中奖" : ("已中:" + Eval("Otitle"))%></span></li></label>
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
    </form>
</body>
</html>
