<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAwList.aspx.cs" Inherits="Enow.TZB.Web.WX.AboutWar.UserAwList" %>

<%@ Register Src="../UserControls/UserHome.ascx" TagName="UserHome" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>我的约战</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/WX/css/juhui.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <link rel="stylesheet" href="/wx/css/IScrol.css" type="text/css" />
    <script src="../../Js/jquery-1.10.2.min.js" type="text/javascript"></script>
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
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    if (title == "")
                        title = "";
                    var para = { title: title, Page: index };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxUsAwList.aspx?" + $.param(para),
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
    <uc1:UserHome ID="UserHome1" Userhometitle="我的约战" runat="server" />
    <div class="search_head juhui_search">
        <div class="search_form" style="padding-left: 0px;">
            <asp:TextBox ID="txtGoodsName" runat="server" CssClass="input_txt floatL" Text=""
                placeholder="搜索"></asp:TextBox>
            <asp:Button ID="btnSerch" runat="server" CssClass="input_btn icon_search_i floatR"
                Text="" OnClick="btnSerch_Click" />
        </div>
    </div>
    <div class="warp s_warp">
        <div class="msg_tab" id="n4Tab">
            <div class="TabTitle">
                <ul class="fixed">
                    <li id="n4Tab_Title0" class="active"><a href="UserAwList.aspx">约战中</a></li>
                    <li id="n4Tab_Title1"><a href="GathersGoing.aspx">进行中</a></li>
                    <li id="n4Tab_Title2"><a href="BattlefieldList.aspx">战报</a></li>
                </ul>
            </div>
            <div class="TabContent">
                <div id="n4Tab_Content0">
                    <div class="juhui_list u_yuezhan mt10" id="wrapper" style="bottom:60px; margin-top:150px;">
                        <ul class="fixed " id="fiullist">
                            <asp:Repeater ID="rptList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="juhui-title">
                                            <%#Getyztitle(Eval("TeamId").ToString())%><em><%#Eval("title")%></em></div>
                                        <div class="juhui-box">
                                            <div class="btn" data-aid="<%#Eval("Aid")%>" data-wid="<%#Eval("Wid")%>">
                                            <%#Gettzqx(Eval("Wtypes").ToString(), Eval("TeamId").ToString(), Eval("AboutState").ToString(), Eval("Wstates").ToString())%>
                                            </div>
                                            <p>
                                                主队：<%#Eval("MainName")%></p>
                                            <p>
                                                客队：<%#Eval("GuestName").ToString() != "" ? Eval("GuestName").ToString() : Eval("Wtypes").ToString() == "1" ? (string.IsNullOrEmpty(Eval("GuestName").ToString()) ? "待确认" : "") : Eval("TeamName").ToString()%></p>
                                            <p class="font_gray">
                                                时间：<%#Eval("AboutTime", "{0:yyyy-MM-dd}")%></p>
                                            <p>
                                                地点：<%#Eval("Address")%></p>
                                            <p>
                                                球场：<%#Eval("CourtName")%></p>
                                            <p class="font_gray">
                                                <span class="paddR10">赛制：<%#Eval("Format").ToString()!="100"?Eval("Format")+"人":"其他"%></span>
                                                费用：<%#(Enow.TZB.Model.EnumType.GathersEnum.比赛费用)Convert.ToInt32(Eval("Costnum"))%></p>
                                            <%#GetAState(Eval("AboutState").ToString(), Eval("Wtypes").ToString(), Eval("Wstates").ToString())%>
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
                    <div class="foot_fixed">
                        <div class="foot_box">
                            <div class="paddL10 paddR10">
                                <a href="AboutWarAdd.aspx" class="basic_btn">发布约战</a></div>
                        </div>
                    </div>
                </div>
                <div id="n4Tab_Content1" class="none">
            
            </div>
            

			<div id="n4Tab_Content2" class="none">
            
            </div>
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
                        alert("请刷新后重试！");
                    }
                });
            },
            Update: function (strval, obj) {
                if (obj) {
                    var aid = $(obj).parent().attr("data-aid");
                    var wid = $(obj).parent().attr("data-wid");
                    if (aid != "" && wid != "") {
                        var dataurl = "UserAwList.aspx?ation=" + strval + "&AID=" + aid + "&WID=" + wid;
                        this.GoAjax(dataurl);
                    }
                    else {
                        alert("请刷新后重试！");
                    }
                }
                else {
                    alert("请刷新后重试！");
                }

            },
            BindBtn: function () {
                //添加
                $(".jjbtn").each(function () {
                    var obj = this;
                    $(obj).click(function () {
                        if (window.confirm("确定要拒绝吗？")) {
                            PageJsDataObj.Update("JuJue", obj);
                        }

                    });
                });

                //应战
                $(".yzbtn").each(function () {
                    var obj = this;
                    $(obj).click(function () {
                        if (window.confirm("确定要应战吗？")) {
                            PageJsDataObj.Update("YinZhan", obj);
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
