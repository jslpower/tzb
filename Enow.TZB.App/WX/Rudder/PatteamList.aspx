<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatteamList.aspx.cs" Inherits="Enow.TZB.Web.WX.Rudder.PatteamList" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>关注球队列表</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/wx/css/IScrol.css" type="text/css">
    <style type="text/css">
        #header
        {
            position: absolute;
            z-index: 2;
            top: 0;
            left: 0;
            width: 100%;
            height: 130px;
        }
        #footer
        {
            position: absolute;
            z-index: 2;
            bottom: 0;
            left: 0;
            width: 100%;
            height: 60px;
            padding: 0;
        }
    </style>
     <link rel="stylesheet" href="/wx/css/IScrol.css" type="text/css" />
    <script src="../../Js/jquery-1.10.2.min.js" type="text/javascript"></script>
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
                var KeyWord = '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("KeyWord"), 0) %>';
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    var para = { KeyWord: KeyWord, Page: index};
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxPatteamList.aspx?" + $.param(para),
                        cache: false,
                        success: function (result) {
                            if (result != "") {
                                $(el).append(result);
                                PageJsDataObj.PageInit();
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
<div id="header">
<div style="height:64px; position:fixed; top:0; left:0;width:100%;" class="head">
    <a href="/WX/Member/" target="_blank"><b class="icon_home"></b></a>
    <h1>关注球队</h1>
    <a href="javascript:history.back();"><i class="returnico"></i></a>
    <div class="search_head">   
</div>
</div>

<div class="search_head">
<div class="TabTitle" style="  margin-top: 10px;">
                <ul class="fixed">
                    <li id="n4Tab_Title0" ><a href="PatList.aspx?types=1">舵主</a></li>
                    <li id="n4Tab_Title1"><a href="PatList.aspx?types=2">堂主</a></li>
                    <li id="n4Tab_Title2" class="active"><a href="PatteamList.aspx">球队</a></li>
                </ul>
            </div>
   <div class="search_form">
       <input type="text" name="txtKeyWord" runat="server" id="txtKeyWord" class="input_txt floatL"><asp:Button ID="btnSearch"
           runat="server" CssClass="input_btn icon_search_i floatR" 
           onclick="btnSearch_Click" />
   </div>
</div>
</div>
<div class="s_warp" id="wrapper" style="top:50px">
    <div id="scroller">
        <div class="qiu_list">
      <ul id="fiullist">
      <asp:Repeater ID="rptList" runat="server">
      <ItemTemplate>
         <li> 
            <div class="item-img"><img src="<%#System.Configuration.ConfigurationManager.AppSettings["Uploadimgurl"]+Enow.TZB.Utility.PhotoThumbnail.F1(Eval("TeamPhoto").ToString(),80,111,DIRPATH)%>"/></div>
            <div class="item-box">
              
               <div class="btn" style=" width:65px">
                   <a href="SignUp.aspx?TeamId=<%#Eval("Id") %>" style=" width:65px" class="basic_btn">加入</a>
                   <a href="/WX/AboutWar/AboutWarAdd.aspx?Teamid=<%#Eval("Id") %>" style=" width:65px" class="basic_ybtn">约战</a>
                    <a href="javascript:void(0);" data-jid="<%#Eval("Id") %>" style=" width:65px" class="basic_btn Agzbtn"><%#Selgzyf(Eval("Id").ToString())%></a>
                 </div>
                <dl>
                    <dt><%#Eval("TeamName") %></dt>
                    <dd class="date"><%#Eval("CityName")%>-<%#Eval("AreaName")%></dd>
                    <dd class="date">创建于：<%#Eval("IssueTime","{0:yyyy-MM-dd}") %></dd>
                    <dd class="date">创始人：<%#Eval("MemberName")%></dd>
                </dl>
            </div>
         </li>
         </ItemTemplate>
      </asp:Repeater>
      </ul><asp:PlaceHolder ID="PlaceHolder1" runat="server">
                        <div id="pullUp">
                            <span class="pullUpIcon"></span><span class="pullUpLabel">上拉更新...</span>
                        </div>
                    </asp:PlaceHolder>
   </div>
   </div>
</div>
    </form>
    <script type="text/javascript">
        var PageJsDataObj = {
            GoAjax: function (url) {
                $.ajax({
                    type: "get",
                    url: url,
                    dataType: "json",
                    success: function (result) {
                        if (result.result == "1") {
                            alert(result.msg);
                            window.parent.location.reload();
                        }
                        else {
                            alert(result.msg);
                        }
                    },
                    error: function (data) {
                        //ajax异常--你懂得
                        alert("关注失败！");
                    }
                });
            },
            Add: function (obj) {
                var atid = $(obj).attr("data-jid");
                if (atid) {
                    var dataurl = "/WX/Team/Default.aspx?ation=inter&JobId=" + atid;
                    this.GoAjax(dataurl);
                }
                else {
                    alert("请刷新后重试!");
                }
            },
            BindBtn: function () {
                //添加
                $(".Agzbtn").each(function () {

                    var obj = this;
                    $(obj).click(function () {
                        if (window.confirm("确定要" + $(obj).html() + "吗？")) {
                            PageJsDataObj.Add(obj);
                        }
                    });
                });
            },
            PageInit: function () {
                //绑定功能按钮
                this.BindBtn();
            }
        }
        $(function () {
            PageJsDataObj.PageInit();
            return false;
        })
        </script>
</body>
</html>
