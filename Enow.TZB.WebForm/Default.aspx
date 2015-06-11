<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.WebForm.Default" %>

<%@ Register Src="UserControl/TopBar.ascx" TagName="TopBar" TagPrefix="uc1" %>
<%@ Register Src="UserControl/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<%@ Register Src="UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta name="keywords" content="" />
    <title>铁子帮-首页</title>
    <link href="Css/style.css" rel="Stylesheet" />
    <link href="Css/boxynew.css" rel="Stylesheet" />
    <link href="Css/jquery.slideBox.css" rel="Stylesheet" />
    <script type="text/javascript" src="Js/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="Js/common.js"></script>
    <script type="text/javascript" src="Js/ValiDatorForm.js"></script>
    <script type="text/javascript" src="Js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="Js/table-toolbar.js"></script>
    <script type="text/javascript" src="Js/jquery.slideBox.js"></script>
    <script type="text/javascript" src="Js/jquery.slideBox.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:TopBar ID="TopBar1" runat="server" />
    <uc2:Menu ID="Menu1" runat="server" />
    <div class="banner">
        <div class="bannerimg">
            <ul style="opacity: 1; top: 0px;">
                <li><span><a href="#" title="铁子帮">
                    <img src="images/banner00.jpg" /></a></span></li>
                <li><span><a href="#" title="铁子帮">
                    <img src="images/banner01.jpg"></a></span></li>
                <li><span><a href="#" title="铁子帮">
                    <img src="images/banner02.jpg"></a></span></li>
                <li><span><a href="#" title="铁子帮">
                    <img src="images/banner03.jpg"></a></span></li>
                <li><span><a href="#" title="铁子帮">
                    <img src="images/banner04.jpg"></a></span></li>
            </ul>
            <ol class="btn">
                <li class="select">1</li>
                <li class="">2</li>
                <li class="">3</li>
                <li class="">4</li>
                <li class="">5</li>
            </ol>
        </div>
        <div class="banner_box">
            <!-----------用户登录框------------->
            <asp:PlaceHolder ID="plnLogin" runat="server">
                <div class="loginbar" id="login_form">
                    <h4>
                        登录铁子帮</h4>
                    <ul class="login_form">
                        <li>
                            <input type="text" id="txtUid" name="txtUid" class="input-style name" value="请使用手机号码登录"
                                onfocus="javascript:if(this.value=='请使用手机号码登录')this.value='';" onblur="javascript:if(this.value=='')this.value='请使用手机号码登录';"
                                valid="required" errmsg="请使用手机号码登录！" /></li>
                        <li>
                            <input type="password" id="txtPassword" name="txtPassword" class="input-style pwd"
                                value="请填写密码" onfocus="javascript:if(this.value=='请填写密码')this.value='';" onblur="javascript:if(this.value=='')this.value='请填写密码';"
                                valid="required" errmsg="请填写密码！" /></li>
                        <li class="fixed">
                            <label class="floatL">
                                <input name="IsRemind" id="IsRemind" type="checkbox" value="" />&nbsp;<span>记住密码</span></label><a
                                    href="#" class="floatR" style="display: none;">忘记密码？</a></li>
                        <li><a href="javascript:void()" id="btnLogin" class="login-btn">登 录</a></li>
                        <li class="fixed"><a href="/register/default.aspx" class="floatL">我要成为铁丝</a><a href="Job/JobList.aspx"
                            class="floatR">我要成为舵主</a></li>
                    </ul>
                </div>
            </asp:PlaceHolder>
            <!-----------登录详细------------->
            <asp:PlaceHolder ID="plnDetail" runat="server" Visible="false">
                <div class="loginbar" id="loginSuc">
                    <ul class="login_form success">
                        <li class="first">
                            <asp:Literal ID="ltrPhoto" runat="server"></asp:Literal>
                            <dl>
                                <dt>
                                    <asp:Label ID="lblUserName" runat="server"></asp:Label></dt>
                                <dd>
                                </dd>
                                <dd>
                                </dd>
                                <dd class="pt10">
                                    <a href="Default.aspx?type=logout">退出</a></dd>
                            </dl>
                        </li>
                        <li>可用铁丝币：<em><asp:Literal ID="ltrCurrencyNumber" runat="server"></asp:Literal></em><a
                            href="Job/JobList.aspx" class="floatR" target="_blank">我要成为舵主</a></li>
                        <li><a href="My/Default.aspx" target="_blank" class="login-btn">进入系统</a></li>
                    </ul>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
    <div class="warp">
        <div class="box01 fixed">
            <div class="side01 padd borderR side03">
                <div class="title">
                    <h3>
                        铁丝球队</h3>
                    <a class="more" href="Team/Default.aspx" target="_blank">more +</a>
                </div>
                <ul class="fixed" style="margin-top: 9px;">
                    <asp:Repeater ID="rptTteam" runat="server">
                        <ItemTemplate>
                            <li><a href="Team/SignUp.aspx?id=<%#Eval("id") %>">
                                <img src="<%#Enow.TZB.Utility.PhotoThumbnail.F1(Convert.ToString(Eval("TeamPhoto")),70,70,DIRPATH)%>"
                                    title="<%#Eval("TeamName")%>"></a><br />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div style="margin-top: 12px;">
                    <div class="title">
                        <h3>
                            新入铁丝</h3>
                    </div>
                    <ul class="fixed">
                        <asp:Repeater ID="rptMemberList" runat="server">
                            <ItemTemplate>
                                <li>
                                    <img src="<%#Eval("HeadPhoto")%>" title="<%#Eval("TeamName")%>" /><br />
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <div class="side02 padd" id="n4Tab">
                <div class="Tabtitle">
                    <ul class="fixed">
                        <li id="n4Tab_Title0" dataclassid="15" onclick="nTabs('n4Tab',this);" class="active">
                            <a href="javascript:void(0);">铁文集</a></li>
                        <li id="n4Tab_Title1" dataclassid="16" onclick="nTabs('n4Tab',this);"><a href="javascript:void(0);">
                            铁资讯</a></li>
                        <li id="n4Tab_Title2" dataclassid="17" onclick="nTabs('n4Tab',this);"><a href="javascript:void(0);">
                            铁漫画</a></li>
                    </ul>
                    <a id="ArticleMore" class="more" href="News/NewsList.aspx?Classid=15&HmId=6" target="_blank">
                        more +</a>
                </div>
                <div class="">
                    <div id="n4Tab_Content0">
                        <div style="height: 260px;vertical-align: top;">
                            <table>
                                <tr>
                                    <td>
                                        <div id="slide1" class="slideBox">
                                            <ul class="items">
                                                <asp:Repeater ID="rpt_tiePhoto" runat="server">
                                                    <ItemTemplate>
                                                        <li><a href="News/NewsDetail.aspx?Id=<%#Eval("Id")%>&ClassId=<%#Eval("ClassId")%>"
                                                            target="_blank" title="<%#Eval("Title") %>">
                                                            <img src="<%#Eval("PhotoUrl")%>" alt="<%#Eval("Title") %>" /></a> </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </td>
                                    <td class="news" style="width: 320px; height: 240px; vertical-align: top;">
                                        <ul style="margin-left: 20px;">
                                            <asp:Repeater ID="rpt_tie" runat="server">
                                                <ItemTemplate>
                                                    <li><a href="/News/NewsDetail.aspx?Id=<%#Eval("Id")%>&ClassId=<%#Eval("ClassId")%>">
                                                        <%# Enow.TZB.Utility.Utils.GetText2(Eval("Title").ToString(),16,true) %></a><span><%#Eval("IssueTime", "{0:MM-dd}")%></span></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div id="n4Tab_Content1" class="none">
                        <div style="height: 260px;">
                            <table>
                                <tr>
                                    <td>
                                        <div id="slide2" class="slideBox">
                                            <ul class="items">
                                                <asp:Repeater ID="rpt_LballPhoto" runat="server">
                                                    <ItemTemplate>
                                                        <li><a href="News/NewsDetail.aspx?Id=<%#Eval("Id")%>&ClassId=<%#Eval("ClassId")%>"
                                                            target="_blank" title="<%#Eval("Title") %>">
                                                            <img src="<%#Eval("PhotoUrl")%>" alt="<%#Eval("Title") %>" /></a> </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </td>
                                    <td class="news" style="width: 320px; height: 240px; vertical-align: top;">
                                        <ul style="margin-left: 20px;">
                                            <asp:Repeater ID="rpt_Lball" runat="server">
                                                <ItemTemplate>
                                                    <li><a href="/News/NewsDetail.aspx?Id=<%#Eval("Id")%>&ClassId=<%#Eval("ClassId")%>">
                                                        <%# Enow.TZB.Utility.Utils.GetText2(Eval("Title").ToString(),16,true) %></a><span><%#Eval("IssueTime", "{0:MM-dd}")%></span></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div id="n4Tab_Content2" class="none">
                        <div style="height: 260px;">
                            <table>
                                <tr>
                                    <td>
                                        <div id="slide3" class="slideBox">
                                            <ul class="items">
                                                <asp:Repeater ID="rptMHArticlePhoto" runat="server">
                                                    <ItemTemplate>
                                                        <li><a href="News/NewsDetail.aspx?Id=<%#Eval("Id")%>&ClassId=<%#Eval("ClassId")%>"
                                                            target="_blank" title="<%#Eval("Title") %>">
                                                            <img src="<%#Eval("PhotoUrl")%>" alt="<%#Eval("Title") %>" /></a> </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </td>
                                    <td class="news" style="width: 320px; height: 240px; vertical-align: top;">
                                        <ul style="margin-left: 20px;">
                                            <asp:Repeater ID="rptMHArticle" runat="server">
                                                <ItemTemplate>
                                                    <li><a href="/News/NewsDetail.aspx?Id=<%#Eval("Id")%>&ClassId=<%#Eval("ClassId")%>">
                                                        <%# Enow.TZB.Utility.Utils.GetText2(Eval("Title").ToString(),16,true) %></a><span><%#Eval("IssueTime", "{0:MM-dd}")%></span></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="side02 padd">
                <div class="title">
                    <h3>
                        杯赛联赛</h3>
                    <a class="more" href="Match/Default.aspx" target="_blank">more +</a>
                </div>
                <div>
                    <ul class="xszq fixed">
                        <asp:Repeater ID="rptMatchList" runat="server">
                            <ItemTemplate>
                                <li><a href="/Match/Detail.aspx?id=<%#Eval("id")%>" target="_blank">
                                    <img src="<%#string.IsNullOrWhiteSpace(Convert.ToString(Eval ("MatchPhoto")))?"images/zq-001.jpg":Eval("MatchPhoto")  %>"
                                        width="120" height="90"><p class="name">
                                </a><a href="/Match/Detail.aspx?id=<%#Eval("id")%>" target="_blank">
                                    <%#Eval("MatchName") %></a> 
                                    <p class="cont">
                                    举办城市：<%#Eval("CountryName") %>-<%#Eval("ProvinceName") %>-<%#Eval("CityName") %>-<%#Eval("AreaName") %>
                                    </p>
                                    <p class="cont">
                                    赛事保证金：<%#Eval("RegistrationFee","{0:C2}")%> 元
                                    </p>
                                    </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <div class="side04" style="margin-top: 5px;">
                <div class="tsw padd borderR">
                    <div class="title">
                        <h3>
                            铁丝网</h3>
                        <a class="more" href="News/FieldList.aspx" target="_blank">more +</a>
                    </div>
                    <ul class="xszq fixed">
                        <asp:Repeater ID="rptBallField" runat="server">
                            <ItemTemplate>
                                <li><a href="/News/FieldDetail.aspx?Id=<%#Eval("Id")%>" target="_blank">
                                    <img src="<%#string.IsNullOrWhiteSpace(Convert.ToString(Eval ("FieldPhoto")))?"images/zq-001.jpg":Eval("FieldPhoto") %>"
                                        width="120" height="90" /></a> <a href="/News/FieldDetail.aspx?Id=<%#Eval("Id")%>">
                                            <%#Enow.TZB.Utility.Utils.GetText2(Eval("FieldName").ToString(),14,true) %>
                                        </a>
                                    <p class="cont">
                                        球场地址:<%#Eval("Address") %>
                                    </p>
                                    <p class="cont">
                                        营业时间:<%#Eval("Hours")%>
                                    </p>
                                    <p class="cont">
                                        球场大小：<%#Eval("FieldSize") %>
                                    </p>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="side_R padd ">
                    <div class="title">
                        <h3>
                            铁众享</h3>
                        <a class="more" href="News/NewsList.aspx?ClassId=13" target="_blank">more +</a>
                    </div>
                    <ul class="xszq fixed">
                        <asp:Repeater ID="rpt_Share" runat="server">
                            <ItemTemplate>
                                <li><a href="/News/NewsDetail.aspx?Id=<%#Eval("Id")%>&ClassId=13">
                                    <img src="<%#string.IsNullOrWhiteSpace(Convert.ToString(Eval ("PhotoUrl")))?"images/zq-001.jpg":Eval("PhotoUrl") %>"
                                        width="120" height="90" /></a> <a href="/News/NewsDetail.aspx?Id=<%#Eval("Id")%>&ClassId=20">
                                            <%#Enow.TZB.Utility.Utils.GetText2(Eval("Title").ToString(),14,true) %>
                                        </a></p>
                                    <p class="cont">
                                        <%#Enow.TZB.Utility.Utils.GetText2(Eval("TitleSulg").ToString(), 60, true)%></p>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
        <div class="foot_nav mt10">
            <a href="News/AboutUs.aspx">关于我们</a>|<a href="News/AboutUs.aspx?Id=80&HmId=1">联系我们</a></div>
    </div>
    <uc3:Footer ID="footer" runat="server" />
    <script type="text/javascript">
<!--
        $(function () {
            $(".bannerimg").YEXfocus({ direction: 'top' });
            //var noticeTimer = setInterval("infoAutoScroll('i.t_news', 36)", 3000)
            $('#demo3').slideBox({
                duration: 0.3, //滚动持续时间，单位：秒
                easing: 'linear', //swing,linear//滚动特效
                delay: 5, //滚动延迟时间，单位：秒
                hideClickBar: false, //不自动隐藏点选按键
                clickBarRadius: 10
            });
            $("#slide1").slideBox({
                duration: 0.3, //滚动持续时间，单位：秒
                easing: 'linear', //swing,linear//滚动特效
                delay: 5, //滚动延迟时间，单位：秒
                hideClickBar: false, //不自动隐藏点选按键
                clickBarRadius: 10
            });
        });
        //-->
        function nTabs(tabObj, obj) {
            var tabList = $("#" + tabObj).find("li");
            for (i = 0; i < tabList.length; i++) {
                if (tabList[i].id == obj.id) {
                    $("#" + tabObj + "_Title" + i).attr("class", "active");
                    $("#" + tabObj + "_Content" + i).show();
                } else {
                    $("#" + tabObj + "_Title" + i).attr("class", "normal");
                    $("#" + tabObj + "_Content" + i).hide();
                }
            }
            $("#ArticleMore").attr("href", "News/NewsList.aspx?Classid=" + $(obj).attr("dataclassid") + "&HmId=6");
        }
    </script>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var UserLogin = {
        checkForm: function () {
            return ValiDatorForm.validator($("#btnLogin").closest("form").get(0), "alert");
        },
        loginFn: function () {
            var _$form = $("#i_form_post");
            if (this.checkForm()) {
                $("#btnLogin").val("登录中").unbind("click").css({ "color": "#999999" });
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "Default.aspx?login=1",
                    dataType: "json",
                    data: $("#btnLogin").closest("form").serialize(),
                    success: function (ret) {
                        if (ret.result == "1") {
                            var rurl = '<%= Enow.TZB.Utility.Utils.GetQueryStringValue("rurl") %>';
                            tableToolbar._showMsg(ret.msg, function () {
                                if (rurl.length > 0)
                                { window.location.href = rurl; }
                                else {
                                    //                                    $("#login_form").css('display', 'none');
                                    //                                    $("#loginSuc").css('display', 'block');
                                    window.location.reload();
                                }
                            });
                        }
                        else {
                            tableToolbar._showMsg(ret.msg);
                            UserLogin.bindBtn();
                        }
                    },
                    error: function () {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        UserLogin.bindBtn();
                    }
                });
            }
        },
        bindBtn: function () {
            $("#btnLogin").click(function () { UserLogin.loginFn(); return false; }).css({ "color": "" });
        }
    };

    $(document).ready(function () {
        $("#txtUid").focus();
        UserLogin.bindBtn();
        $("#txtUid,#txtPassword").keypress(function (e) { if (e.keyCode == 13) { UserLogin.loginFn(); return false; } });
    });
</script>
