<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Articles.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.Articles" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>我的日志</title>
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
   
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
    <link rel="stylesheet" href="/WX/css/tangzhu.css" type="text/css" media="screen"/>
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Js/jq.mobi.min.js"></script>
        <link rel="stylesheet" href="/wx/css/IScrol.css" type="text/css">
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

            var types = '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("types")) %>';
            if (pageEnd == "0") {
                $("#pageindex").val(index);

                var para = { Page: index, isGet: 1, types: types };
                $.ajax({
                    type: "Get",
                    url: "/CommonPage/AjaxArticles.aspx?" + $.param(para),
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
    <script type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
                sl: ''
            },
            GoAjax: function (url) {
                $.ajax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function (result) {
                        if (result.result == "1") {
                            //tableToolbar._showMsg(result.msg, function () {
                            alert(result.msg);
                            window.location.reload();
                            // });
                        }
                        else { alert(result.msg) }
                    },
                    error: function () {
                        //ajax异常--你懂得
                        //tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            DataBoxy: function () {/*弹窗默认参数*/
                return {
                    url: '/WX/Member',
                    title: "",
                    width: "830px",
                    height: "620px"
                }
            },
            Delete: function (recordid) {

                if (window.confirm("确定删除此条记录？")) {

                    var data = this.DataBoxy();

                    data.url += '/Articles.aspx?';

                    data.url += $.param({
                        sl: this.Query.sl,
                        doType: "delete",
                        id: recordid
                    });

                }
                this.GoAjax(data.url);

            }

         


        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="pageindex" type="hidden" value="<%=CurrencyPage %>" />
    <input id="hidPageend" type="hidden" value="0" />
            <uc1:UserHome ID="UserHome1" runat="server" />
   <div class="warp" >
    
   <%-- <div class="s_warp" id="wrapper">--%>
        <div id="wrapper" style="  margin-top: 50px;" >
        <div class="TabContent">

            <div class="tangzhu_log qiudui_log">
                <ul id="linelist">
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <li>
                              <div class="log_title" >         
                               <a href="/WX/Member/ArticleView.aspx?Id=<%#Eval("Id") %>"> <%#Eval("ArticleTitle")%> </a>
                                   
                                </div>
                                <div class="log_date">
                                    <%#Eval("IssueTime")%>
                                </div>
                                <div class="Rbtn">
                                    <a href="/WX/Member/ArticleEdit.aspx?Id=<%#Eval("Id") %>" class="basic_ybtn">编辑</a>
                                    <a href="" onclick="PageJsDataObj.Delete('<%#Eval("Id")%>')"
                                        class="basic_gbtn">删除</a>
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
    <div id="footer" class="foot_fixed">
        <div class="foot_box">
            <div class="paddL10 paddR10">
                <a href="/WX/Member/ArticleAdd.aspx?types=<%=Enow.TZB.Utility.Utils.GetQueryStringValue("types") %>" class="basic_btn">发布新日志</a></div>
        </div>
    </div>
 <%--   </div>--%>
    </form>
</body>
</html>
