<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BazaarOrderList.aspx.cs"
    Inherits="Enow.TZB.App.WX.Member.BazaarOrderList" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的订单</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/user.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jq.mobi.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/IScrol.css" />
    <script src="../../Js/IScroll/IScroll4.2.5.js" type="text/javascript"></script>
    <style type="text/css">
      .TabTitle{
          margin-top: 61px;
           margin-left:2%;
        }
        .TabTitle li 
        {
        	width: 48%;
        }
    </style>
    <script type="text/javascript">
        var myScroll, pullDownEl, pullDownOffset,
	pullUpEl, pullUpOffset,
	generatedCount = 0;

        function pullDownAction() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, i;
                el = document.getElementById('udianjilist');

                for (i = 0; i < 8; i++) {
                    $(el).append("<li>Generated row " + (++generatedCount) + "</li>")
                }

                myScroll.refresh(); 	// Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000); // <-- Simulate network congestion, remove setTimeout from production!
        }

        function pullUpAction() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, i;
                el = document.getElementById('udianjilist');
                /*==============================================================*/
                var index = parseInt($("#pageindex").val()) + 1;
                var pageEnd = $("#hidPageend").val();
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    var para = { Page: index };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxBazaarOrderList.aspx?" + $.param(para),
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
    <uc1:userhome id="UserHome1" userhometitle="义卖商品订单" runat="server" />
       <div class="TabTitle">
           <ul class="fixed">
              <li><a href="BazaarList.aspx">我的义卖商品</a></li>
              <li class="active"><a href="javascript:void(0);">义卖商品订单</a></li>
           </ul>
        </div>
    <div class="user_warp paddB10">
        <div class="u-dindan-list" id="wrapper" style="margin-top: 110px;">
            <ul id="udianjilist">
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="user-item font14 R_jiantou" onclick="location.href='BazaarDetail.aspx?OrderId=<%#Eval("OrderId")%>'">
                                
                                    <%#Eval("OrderNo")%></div>
                            <div class="u-dindan-item">
                                <img alt="" src="<%#Getimgurl(Eval("OrderId").ToString())%>" />
                                <p class="font_gray">
                                    支付状态:<%#((Enow.TZB.Model.商城订单状态)(Enow.TZB.Utility.Utils.GetInt(Eval("PayStatus").ToString()))).ToString()%><span
                                        style="float: right;"><%#Eval("CreatTime")!=null?Enow.TZB.Utility.Utils.GetDateTime(Eval("CreatTime").ToString(),DateTime.MinValue).ToString("yyyy-MM-dd HH:mm"):DateTime.Now.ToString("yyyy-MM-dd HH:mm")%></span></p>
                                <p>
                                    <%#Getsum(Eval("OrderId").ToString())%></p>
                                <p class="font_gray">
                                    支付方式：<%#(Enow.TZB.Model.商城支付方式)Convert.ToInt32(Eval("PayType"))%>
                                    <asp:Literal ID="litOperation" runat="server"></asp:Literal>
                                </p>
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
    </form>
</body>
</html>
