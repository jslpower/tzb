<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoteUser.aspx.cs" Inherits="Enow.TZB.Web.WX.Vote.VoteUser" %>
<%@ Register Src="../UserControls/UserHome.ascx" TagName="UserHome" TagPrefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
    <title><%=flTitle%></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/mall.css" rel="stylesheet" type="text/css" />
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
                var title = $("#<%=txtGoodsName.ClientID %>").val();
                var types=<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("types"), 1) %>;
                types=types>2?1:types;
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    if (title == "")
                        title = "";
                    var para = { title: title, Page: index, tzurl: (types==1?"VoteInfo.aspx":"LotteryInfo.aspx"), types: types };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxVotelist.aspx?" + $.param(para),
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
     <uc1:UserHome ID="UserHome1" runat="server" />
    <div class="search_head home_search">
        <div class="search_form">
            <asp:TextBox ID="txtGoodsName" runat="server" CssClass="input_txt floatL" Text=""
                placeholder="搜索"></asp:TextBox>
            <asp:Button ID="btnSerch" runat="server" CssClass="input_btn icon_search_i floatR"
                Text="" OnClick="btnSerch_Click" />
        </div>
    </div>
    <div class="home_warp" id="wrapper">
        <div class="list-item" >
            <ul id="fiullist">
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                    <li class="R_jiantou" onclick="location.href='<%#Eval("ColumnID").ToString()=="1"?"VoteInfo.aspx":"LotteryInfo.aspx"%>?Vid=<%#Eval("Vid")%>'"><%#Eval("Vtitle")%>    <span style=" float:right;"><%#Eval("ColumnID").ToString()=="1"?("已投:"+Eval("Otitle")):(Eval("AwardsNum").ToString() == "0" ? "未中奖" : ("已中:" + Eval("Otitle")))%></span></li>
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
