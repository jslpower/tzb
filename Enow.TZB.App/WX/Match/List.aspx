<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Enow.TZB.Web.WX.Match.List" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">

<title>赛事报名</title>
<link rel="stylesheet" href="/wx/css/style.css" type="text/css" media="screen" />
 <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/wx/css/IScrol.css" type="text/css">
    <script type="text/javascript" src="/Js/jq.mobi.min.js"></script>
<script type="text/javascript" src="/Js/IScroll/IScroll4.2.5.js"></script>
<script type="text/javascript">
    var myScroll, pullDownEl, pullDownOffset,
	pullUpEl, pullUpOffset,
	generatedCount = 0;

    function pullDownAction() {
        setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
            var el, li, i;
            el = document.getElementById('linelist');

            for (i = 0; i < 8; i++) {
                $(el).append("<li>Generated row " + (++generatedCount) + "</li>")
            }

            myScroll.refresh(); 	// Remember to refresh when contents are loaded (ie: on ajax completion)
        }, 1000); // <-- Simulate network congestion, remove setTimeout from production!
    }

    function pullUpAction() {
        setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
            var el, li, i;
            el = document.getElementById('linelist');
            /*==============================================================*/
            var index = parseInt($("#pageindex").val()) + 1;
            var pageEnd = $("#hidPageend").val();
            var kewords = $("#<%=txtKeyWords.ClientID %>").val();
            var CityId = '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("CityId")) %>';
            if (pageEnd == "0") {
                $("#pageindex").val(index);
                if (kewords == "")
                    kewords = "";
                var para = { KeyWord: kewords, Page: index, isGet: 1, CityId: CityId };
                $.ajax({
                    type: "Get",
                    url: "/CommonPage/AjaxMatchSet.aspx?" + $.param(para),
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
    <uc1:UserHome ID="UserHome1" Userhometitle="赛事报名" runat="server" />

<div class="search_head juhui_search">
   <div class="search_form">
   <span class="city_name" onClick="location.href='/WX/city.aspx?action=Match&fhdz=List'"><asp:Literal runat="server" ID="litcityname"></asp:Literal></span>
   <asp:TextBox ID="txtKeyWords" runat="server" CssClass="input_txt floatL" Text="" placeholder="赛事搜索"></asp:TextBox><asp:Button
       ID="btnSearch" CssClass="input_btn icon_search_i floatR" runat="server" 
           onclick="btnSearch_Click" />
   </div>
</div>
<div class="warp s_warp" id="wrapper">
<div  id="linelist" >
    <asp:Repeater ID="rptList" runat="server">
        <ItemTemplate>
      <div class="msg_list qiu_box nobot">
        <ul>
          <li style="padding:25px 0px 25px 0px"><strong><%#Eval("MatchName")%></strong></li>
          <li><label>报名时间：</label><%#Eval("SignBeginDate","{0:yyyy-MM-dd}")%>至<%#Eval("SignEndDate","{0:yyyy-MM-dd}")%></li>
          <li><label>比赛时间：</label><%#Eval("BeginDate","{0:yyyy-MM-dd}")%>至<%#Eval("EndDate","{0:yyyy-MM-dd}")%></li>
          <li><label>举办城市：</label><%#Eval("CityName")%></li>
          <li>
          <div class="lie"><label>保证金：</label><%#Eval("RegistrationFee","{0:C2}")%> 元</div> 
          <div class="lie2"><label>报名费：</label><%#Eval("EarnestMoney", "{0:C2}")%> 元</div></li>
          <li  style="padding:25px 0px 25px 10px">
             <!-- <div class="lie"><label>报名队数：</label><%#Eval("SignUpNumber")%>/<%#Eval("TeamNumber")%></div>  
             <div class="lie2"><label>报名人数：</label><%#Eval("PlayersMin")%>至<%#Eval("PlayersMax")%>人</div> !-->
             每队报名人数：</label><%#Eval("PlayersMin")%>-<%#Eval("PlayersMax")%>人
          </li>
        </ul>
      </div>
  
      <div class="qiu-caozuo"><a href="Detail.aspx?Id=<%#Eval("Id") %>" class="basic_btn">查看详情</a><%#SignUp(Eval("Id").ToString(), Convert.ToDateTime(Eval("SignBeginDate")), Convert.ToDateTime(Eval("SignEndDate")))%></div>
        
        </ItemTemplate>
    </asp:Repeater>
</div>
<asp:PlaceHolder ID="PlaceHolder1" runat="server">
                        <div id="pullUp">
                            <span class="pullUpIcon"></span><span class="pullUpLabel">上拉更新...</span>
                        </div>
                    </asp:PlaceHolder>
</div>
</form>
</body>

</html>
