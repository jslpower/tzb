<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BazaarList.aspx.cs" Inherits="Enow.TZB.App.WX.Member.BazaarList" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>义卖商品</title>
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
            height: 44px;
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
        .TabTitle{
          margin-top: 61px;
           margin-left:2%;
        }
        .TabTitle li 
        {
        	width: 48%;
        }
    </style>
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
            var kewords = "";
            if (pageEnd == "0") {
                $("#pageindex").val(index);
                if (kewords == "")
                    kewords = "";
                var para = { KeyWord: kewords, Page: index, isGet: 1 };
                $.ajax({
                    type: "Get",
                    url: "/CommonPage/AjaxBazaarList.aspx?" + $.param(para),
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
        }, 1000);              // <-- Simulate network congestion, remove setTimeout from production
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
<div id="header">
<div style="height:44px; position:fixed; top:0; left:0;width:100%;" class="head">
    <a href="/WX/index.aspx"><b class="icon_home"></b></a>
    <h1>我的义卖商品</h1>
    <a href="javascript:history.back();"><i class="returnico"></i></a>
</div>
</div>
  <div class="TabTitle">
           <ul class="fixed">
              <li class="active"><a href="javascript:void(0);">我的义卖商品</a></li>
              <li><a href="BazaarOrderList.aspx">义卖商品订单</a></li>
           </ul>
        </div>
<div class="s_warp" id="wrapper" style="bottom:60px;">
    <div id="scroller">
        <div class="qiu_list">
      <ul id="linelist">
      <asp:Repeater ID="rptList" runat="server">
      <ItemTemplate>
         <li> 
            <div class="item-img"><img src="<%#System.Configuration.ConfigurationManager.AppSettings["Uploadimgurl"]+Eval("GoodsPhoto").ToString()%>"/></div>
            <div class="item-box" style=" padding:0px;margin-left: 110px;">
               <div class="btn" style=" width:65px">
                   <a href="BazaarEdit.aspx?Id=<%#Eval("Id") %>" style=" width:65px" class="basic_btn">编辑</a>
                   <a href="javascript:void(0);" style=" width:65px" data-id="<%#Eval("Id") %>" class="basic_ybtn btndelete">删除</a>
                 </div>
                <dl>
                    <dt><%#Eval("GoodsName")%></dt>
                    <dd class="date">会员价:<%#Eval("MemberPrice")%></dd>
                    <dd class="date">修改时间：<%#Eval("IssueTime", "{0:yyyy-MM-dd}")%></dd>
                    <dd class="date">状态：<%#Eval("Status")!=null?((Enow.TZB.Model.义卖商品销售状态)Eval("Status")).ToString():""%></dd>
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
<%--<div class="QuickLink" id="footer"><a href="CreateTeam.aspx" class="basic_btn">创建球队</a></div>--%>
  
<%--   <div class="foot_fixed">--%>
        <div class="foot_box">
            <div class="paddL10 paddR10"><a href="BazaarEdit.aspx" class="basic_btn">添加商品</a></div>
        </div>
<%--   </div>--%>
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
                         alert("删除失败！");
                     }
                 });
             },
             Add: function (obj) {
                 var atid = $(obj).attr("data-id");
                 if (atid) {
                     var dataurl = "BazaarList.aspx?ation=delgod&Id=" + atid;
                     this.GoAjax(dataurl);
                 }
                 else {
                     alert("请刷新后重试!");
                 }
             },
             BindBtn: function () {
                 //添加
                 $(".btndelete").each(function () {
                     var obj = this;
                     $(obj).click(function () {
                         if (window.confirm("确定要删除吗？")) {
                             PageJsDataObj.Add(obj);
                         }
                     });
                 })
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
