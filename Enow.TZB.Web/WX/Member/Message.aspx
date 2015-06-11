<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.Message" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>消息中心</title>
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
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    var para = {Page: index };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxMessageList.aspx?" + $.param(para),
                        cache: false,
                        success: function (result) {
                            if ($.trim(result) != '') {
                                $(el).append(result);
                                xaoxibtn.btndj();
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
    <uc1:UserHome ID="UserHome1" Userhometitle="消息中心" runat="server" />
<div class="warp">
  <div class="msg_tab"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
              <li class="active"><a href="Message.aspx">全部</a></li>
              <li><a href="MessageUnRead.aspx">未读</a></li>
              <li style="width:34%;"><a href="MessageRead.aspx">已读</a></li>
           </ul>
        </div>

        <div class="TabContent" id="wrapper" style=" margin-top:90px;">
        
			<div id="n4Tab_Content0">
            
                <div class="xiaoxi_list">
                    <ul  id="fiullist">
                        <asp:Repeater ID="rptList" runat="server">      
      <ItemTemplate> 
                        <li>
                            <div class="xiaoxi_t" data-id="<%#Eval("Id") %>" IsRead="<%#Convert.ToBoolean(Eval("IsRead"))?"1":"0"%>" style="cursor:Pointer;"><%#Convert.ToBoolean(Eval("IsRead"))?"":"<span class=\"ico\"></span>"%><%#Eval("MsgTitle")%><em class="font14"><%#Eval("SendTime","{0:yyyy-MM-dd}")%></em></div>
                            <div class="xiaoxi_cont" style="display:none;"><%#Eval("MsgInfo")%></div>
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

<script language="javascript" type="text/javascript">
    var xaoxibtn = {
        btndj: function () {
            $(".xiaoxi_t").click(function () {
                var Id = $(this).attr("data-id");
                var IsRead = $(this).attr("IsRead");
                $(this).closest("ul").find("div[class=xiaoxi_cont]").hide();
                $(this).closest("li").find("div[class=xiaoxi_cont]").show();
                if (IsRead == "0") {
                    $(this).attr("IsRead", "1");
                    $(this).closest("li").find("span[class=ico]").remove();
                    $.ajax({
                        type: "GET",
                        url: "/Ashx/MessageRead.ashx?Id=" + Id,
                        dataType: "json",
                        cache: false,
                        async: false,
                        success: function (response) {
                            if (response.result == "1") {
                            } else {
                                alert(response.msg);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(XMLHttpRequest.status);
                            //alert(XMLHttpRequest.readyState);
                            //alert(textStatus);
                            alert("连接服务器失败!");
                            return;
                        }
                    });
                }
            });
        }
    }
    $(function () {
        xaoxibtn.btndj();
    });
</script>
    </form>
</body>
</html>
