<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GatheringSignUp.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.GatheringSignUp" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title><%=ApTitle%>详情</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/juhui.css" type="text/css" media="screen"/>
 <script src="../../Js/jquery-1.4.4.js" type="text/javascript"></script>
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
                var ActId = <%=Acid %>;
                var types = <%=types %>;
                if (pageEnd == "0") {
                    $("#pageindex").val(index);

                    var para = { ActId: ActId, Page: index, types: types };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxApplicantsList.aspx?" + $.param(para),
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
    <uc1:UserHome ID="UserHome1" runat="server" />
<div class="warp">
  <div class="msg_tab Tab_lie2"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0"  ><a href="TZGatheringDetail.aspx?ActId=<%=Acid %>&types=<%=types %>">详情</a></li>
              <li id="n4Tab_Title1" class="active"><a href="GatheringSignUp.aspx?ActId=<%=Acid %>&types=<%=types %>">已报名</a></li>
           </ul>
        </div>

        <div class="TabContent">

 			<div id="n4Tab_Content1" >
            
                <div class="juhui_list mt10" id="wrapper" style=" margin-top:110px;">
                      <ul id="fiullist">
                      <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                         <li>
                            <div class="juhui-title"><%#Eval("ContactName")%></div>
                            <div class="juhui-box">
                                <div class="btn">
                                   <a href="javascript:void(0);" class="basic_gbtn">已报名</a>
                                </div>
                                <p class="font_gray">报名时间：<%#Eval("Indatetime")%></p>
                                <p>状态：<%#(Enow.TZB.Model.EnumType.ApplicantsStartEnum)(Enow.TZB.Utility.Utils.GetInt(Eval("IsState").ToString(), 0))%></p>
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
                        <div class="paddL10 paddR10"><a href="javascript:void(0);" class="basic_btn" id="btnsave">报名</a></div>
                    </div>
               </div>

            
            </div>

			
           
        </div>

     
     
     
  </div>
</div>
    </form>
       <script type="text/javascript">
           var PageJsDataObj = {
               AcpID: '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("ActId"), 0) %>',
               GoAjax: function (url) {
                   $.ajax({
                       type: "get",
                       url: url,
                       dataType: "json",
                       success: function (result) {
                           if (result.result == "1") {
                               alert(result.msg);
                           }
                           else {
                               alert(result.msg);
                           }
                       },
                       error: function (data) {
                           //ajax异常--你懂得
                           alert("程序错误！请联系管理员！");
                       }
                   });
               },
               Add: function () {
                   var dataurl = "TZGatheringDetail.aspx?ation=inter&ActId=" + PageJsDataObj.AcpID;
                   this.GoAjax(dataurl);
               },
               BindBtn: function () {
                   //添加
                   $("#btnsave").click(function () {
                       if (window.confirm("确定要报名吗？")) {
                           PageJsDataObj.Add();
                       }
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
