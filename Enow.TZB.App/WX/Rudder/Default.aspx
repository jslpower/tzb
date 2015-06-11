<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.Web.WX.Rudder.Default" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">

<title><%=Typetitle %>风采</title>
 <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
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
                var types = '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("types"), 1) %>';
                var CityId = '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("CityId"), 0) %>';
                var keyword = $("#<%=txtKeyWord.ClientID %>").val();
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    var para = { keyword: keyword, Page: index, types: types, CityId: CityId };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxRudderList.aspx?" + $.param(para),
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
            }, 1000);              // <-- Simulate network congestion, remove setTimeout from production
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
   <div class="search_head juhui_search" >
   <div class="search_form">
   <span class="city_name" onClick="location.href='/WX/city.aspx?action=Rudder&fhdz=Default&types=<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("types"), 1) %>'"><asp:Literal runat="server" ID="litcityname"></asp:Literal></span>
      <input type="text" name="txtKeyWord" runat="server" id="txtKeyWord" class="input_txt floatL" />
      <asp:Button ID="btnSearch"
           runat="server" CssClass="input_btn icon_search_i floatR" 
           onclick="btnSerch_Click" />
   </div>
</div>
<div class="warp" id="wrapper" style="margin-top: 110px;">

   
   
   <div class="qiu_list player_list mt10" >
      <ul id="fiullist">
       <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
         <li>
            <div class="item-img"><img src="<%#System.Configuration.ConfigurationManager.AppSettings["Uploadimgurl"]+Eval("MemberPhoto")%>"/></div>
            <div class="item-box">
                <div class="Rbtn">
                   <a href="RudderView.aspx?JobId=<%# Eval("Id")%>">查看详情</a>
                   <a href="Mesgsendout.aspx?JobId=<%# Eval("MemberId")%>">发送信息</a>
                   <a href="javascript:void(0);" data-jid="<%# Eval("Id")%>" class="Agzbtn"><%#Selgzyf(Eval("MemberId").ToString())%></a>
                </div>
                <dl>
                    <dt><%# Eval("Jobtyoe")!=null?((Enow.TZB.Model.EnumType.JobType)Eval("Jobtyoe")).ToString():"会员"%>：<%# Eval("ContactName")%></dt>
                    <dd><span class="bg_green"><%# Eval("CSWZ")%></span><span class="bg_green"><%# Eval("CityName")%></span></dd>
                    <dd class="txt"><%#Eval("ApplyInfo").ToString()%></dd>
                </dl>
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
                         alert("操作失败！");
                     }
                 });
             },
             Add: function (obj) {
                 var atid = $(obj).attr("data-jid");
                 if (atid) {
                     var dataurl = "Default.aspx?ation=inter&JobId=" + atid;
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
