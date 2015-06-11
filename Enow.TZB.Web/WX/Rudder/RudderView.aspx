<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RudderView.aspx.cs" Inherits="Enow.TZB.Web.WX.Rudder.RudderView" %>

<%@ Register Src="../UserControls/UserHome.ascx" TagName="UserHome" TagPrefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>
        <%=Typetitle%>详情</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/tangzhu.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jquery-1.10.2.min.js" type="text/javascript"></script>
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
                var JobId = '<%=Enow.TZB.Utility.Utils.GetQueryStringValue("JobId")%>';
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    var para = { JobId: JobId, Page: index };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxRudderViewList.aspx?" + $.param(para),
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
    <script type="text/javascript">
        function nTabs(tabObj, obj) {
            var tabList = document.getElementById(tabObj).getElementsByTagName("li");
            for (i = 0; i < tabList.length; i++) {
                if (tabList[i].id == obj.id) {
                    document.getElementById(tabObj + "_Title" + i).className = "active";
                    document.getElementById(tabObj + "_Content" + i).style.display = "block";
                } else {
                    document.getElementById(tabObj + "_Title" + i).className = "normal";
                    document.getElementById(tabObj + "_Content" + i).style.display = "none";
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <input id="pageindex" type="hidden" value="<%=CurrencyPage %>" />
    <input id="hidPageend" type="hidden" value="0" />
    <uc1:UserHome ID="UserHome1" runat="server" />
    <div class="warp">
        <div class="msg_tab Tab_lie2" id="n4Tab">
            <div class="TabTitle">
                <ul class="fixed">
                    <li id="n4Tab_Title0" onclick="nTabs('n4Tab',this);" class="active"><a href="javascript:void(0);">
                        他的简历</a></li>
                    <li id="n4Tab_Title1" onclick="nTabs('n4Tab',this);"><a href="javascript:void(0);">他的日志</a></li>
                </ul>
            </div>
            <div class="TabContent">
                <div id="n4Tab_Content0">
                    <div class="tangzhu_jianli padd10">
                        <div class="tangzhu_xinxi">
                            <asp:Literal ID="litimage" runat="server"></asp:Literal>
                            <p>
                                姓名:<asp:Literal ID="litname" runat="server"></asp:Literal>
                            </p>
                            <p>
                                工作年限:<asp:Literal ID="litgzjy" runat="server"></asp:Literal>|性别:<asp:Literal ID="litsex"
                                    runat="server"></asp:Literal></p>
                            <p>
                                居住地：<asp:Literal ID="litjuzhudi" runat="server"></asp:Literal></p>
                            <p>
                                电 话：<asp:Literal ID="litshouji" runat="server"></asp:Literal>（手机）</p>
                            <p>
                                E-mail：<asp:Literal ID="litemail" runat="server"></asp:Literal></p>
                        </div>
                        <div class="tangzhu_jieshao paddT20 font_gray">
                            <asp:Literal ID="litContent" runat="server"></asp:Literal>
                        </div>
                        <div class="foot_btn">
                            <ul>
                                <li class="box-siz"><a href="javascript:void(0);" id="Agzbtn"><asp:Literal ID="litguanzhu" runat="server"></asp:Literal></a></li>
                                <li class="box-siz"><a href="Mesgsendout.aspx?JobId=<%=Enow.TZB.Utility.Utils.GetQueryStringValue("JobId")%>" class="basic_ybtn">发送信息</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div id="n4Tab_Content1" class="none">
                    <div class="tangzhu_log" id="wrapper" style="margin-top:90px;">
                        <ul id="fiullist">
                            <asp:Repeater ID="rptList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="log_title">
                                            <a href="/WX/Member/ArticleView.aspx?id=<%#Eval("Id") %>"><%#Eval("ArticleTitle")%></a></div>
                                        <div class="log_date">
                                            <a href="/WX/Member/ArticleLeaveWord.aspx?dzty=jobly&flag=leave&articleId=<%#Eval("Id") %>" class="liuyan"></a><a href="javascript:void(0);" data-jid="<%#Eval("Id") %>" class="<%#Selgzyf(Eval("Id").ToString()) %> Agzbtn"></a>
                                            <%#Eval("IssueTime")%>
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
     <script type="text/javascript">
         var PageJsDataObj = {
             dzid: '<%=Enow.TZB.Utility.Utils.GetQueryStringValue("JobId") %>',
             typ: '<%=Enow.TZB.Utility.Utils.GetQueryStringValue("typ") %>',
             sxfh: false,
             GoAjax: function (url) {
                 $.ajax({
                     type: "get",
                     url: url,
                     dataType: "json",
                     success: function (result) {
                         if (result.result == "1") {
                             alert(result.msg);
                             var urlret = "";
                             if (PageJsDataObj.sxfh) {
                                 urlret = "RudderView.aspx?typ=dzsx&JobId=" + PageJsDataObj.dzid;
                             }
                             else {
                                 urlret = "RudderView.aspx?JobId=" + PageJsDataObj.dzid;
                             }
                             window.location.href = urlret;
                         }
                         else {
                             alert(result.msg);
                         }
                     },
                     error: function (data) {
                         //ajax异常--你懂得
                         alert("保存失败！");
                     }
                 });
             },
             Add: function () {
                 if (PageJsDataObj.dzid) {
                     PageJsDataObj.sxfh = false;
                     var dataurl = "Default.aspx?ation=inter&JobId=" + PageJsDataObj.dzid;
                     this.GoAjax(dataurl);
                 }
                 else {
                     alert("操作失败!");
                 }
             },
             dianzan: function (obj) {
                 var atid = $(obj).attr("data-jid");
                 if (atid) {
                     PageJsDataObj.sxfh = true;
                     var dataurl = "RudderView.aspx?ation=inter&RzId=" + atid;
                     this.GoAjax(dataurl);
                 }
                 else {
                     alert("请选择日志!");
                 }
             },
             BindBtn: function () {
                 //添加
                 $("#Agzbtn").click(function () {
                     if (window.confirm("确定要" + $("#Agzbtn").html() + "吗？")) {
                         PageJsDataObj.Add();
                     }
                 });
                 //点赞
                 $(".Agzbtn").each(function () {
                     var obj = this;
                     $(obj).click(function () {
                         if (window.confirm("确定要" + ($(obj).attr("class") == "zan" ? "点赞" : "取消点赞") + "吗？")) {
                             PageJsDataObj.dianzan(obj);
                         }
                     });
                 })
             },
             PageInit: function () {
                 //绑定功能按钮
                 this.BindBtn();
                 if (PageJsDataObj.typ != "") {
                     $("#n4Tab_Content1").removeClass();
                     $("#n4Tab_Title0").removeClass();
                     $("#n4Tab_Content0").css("display", "none");
                     $("#n4Tab_Title0").addClass("normal");
                     $("#n4Tab_Content1").css("display", "");
                     $("#n4Tab_Title1").addClass("active");
                 }
             }
         }
         $(function () {
             PageJsDataObj.PageInit();
             return false;
         })
     </script>
</body>
</html>
