<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DZJob.aspx.cs" Inherits="Enow.TZB.Web.WX.Job.DZJob" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
<title>舵主报名</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
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
                var KeyWord = $("#<%=txtKeyWords.ClientID %>").val();
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    if (KeyWord == "")
                        KeyWord = "";
                    var para = { KeyWord: KeyWord, Page: index, types: <%=(int)Enow.TZB.Model.EnumType.JobType.舵主 %> };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxDZJob.aspx?" + $.param(para),
                        cache: false,
                        success: function (result) {
                            if (result!="") {
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
     <input id="pageindex" type="hidden" value="1" />
    <input id="hidPageend" type="hidden" value="0" />
    <div class="TabTitle" style=" margin-top:50px;">
                <ul class="fixed">
                    <li id="n4Tab_Title0"  class="active" ><a href="DZJob.aspx">舵主</a></li>
                    <li id="n4Tab_Title1" ><a href="TZJob.aspx">堂主</a></li>
                    <li id="n4Tab_Title2" ><a href="ZZJob.aspx">站长</a></li>
                </ul>
            </div>
    <uc1:UserHome ID="UserHome1" Userhometitle="舵主报名" runat="server" />
     
<div class="search_head" style=" top:85px;">
   <div class="search_form">
   <asp:TextBox ID="txtKeyWords" runat="server" CssClass="input_txt floatL" Text="" placeholder="岗位搜索"></asp:TextBox><asp:Button
       ID="btnSearch" CssClass="input_btn icon_search_i floatR" runat="server" 
           onclick="btnSearch_Click" />
   </div>
</div>
<div id="wrapper" style=" margin-top:90px;">
<div class="warp" id="fiullist">
<asp:Repeater ID="rptList" runat="server">      
      <ItemTemplate>
      <div class="msg_list qiu_box nobot">
        <ul>
          <li><label>岗位名称：</label><%#Eval("JobName")%></li>
          <%#Eval("ProvinceName")%>-<%#Eval("CityName")%></li>-->
          <li><label>招聘起止时间：</label><%#Eval("StartDate","{0:yyyy-MM-dd}")%>至<%#Eval("EndDate", "{0:yyyy-MM-dd}")%></li>
          <li><label>招聘人数：</label><%#Eval("JobNumber")%></li>
        </ul>
      </div>  
      <div class="qiu-caozuo"><a href="JobDetail.aspx?Id=<%#Eval("Id") %>" class="basic_btn">查看详情</a> <a href="JobSignUp.aspx?Id=<%#Eval("Id") %>" class="basic_btn basic_ybtn">点击报名</a></div>
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
