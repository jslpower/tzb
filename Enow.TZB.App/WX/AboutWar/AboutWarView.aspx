<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutWarView.aspx.cs" Inherits="Enow.TZB.Web.WX.AboutWar.AboutWarView" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8"/>
 <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>约战详情</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/juhui.css" type="text/css" media="screen"/>

<script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
     <uc1:UserHome ID="UserHome1" Userhometitle="约战详情" runat="server" />
       <div class="warp">
  <div class="msg_tab Tab_lie2"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0"  class="active"><a href="javascript:void(0);">约战详情</a></li>
              <li id="n4Tab_Title1" ><a href="AboutWarTeam.aspx?ID=<%=Enow.TZB.Utility.Utils.GetQueryStringValue("ID") %>">主队详情</a></li>
           </ul>
        </div>

        <div class="TabContent">
        
			<div id="n4Tab_Content0">
            
                <div class="juhui_cont padd10">
                    
                   <div>名称：<asp:Literal ID="litTeamName" runat="server"></asp:Literal></div>
                   <asp:Literal ID="litzkdname" runat="server"></asp:Literal>
                   <div>时间：<asp:Literal ID="litTime" runat="server"></asp:Literal></div>
                   <div>球场：<asp:Literal ID="Literal1" runat="server"></asp:Literal></div>
                   <div>地点：<asp:Literal ID="litPlace" runat="server"></asp:Literal></div>
                   <div><span class="paddR10">赛制：<asp:Literal ID="litType" runat="server"></asp:Literal></span>费用：<asp:Literal ID="litFee" runat="server"></asp:Literal></div>
                   <div>状态：<asp:Literal ID="litState" runat="server"></asp:Literal></div>
                   <div>战书：</div>
                   <div class="indent2 font_gray">
                    <asp:Literal ID="litzhanshu" runat="server"></asp:Literal>
                   </div>
                   
                </div>
                
                <div class="mt20 padd_bot"><a id="IsAccept"  href="javascript:void(0);" class="basic_btn">挑战</a></div>
            </div>
        </div>
  </div>
</div>
    </form>
    <script type="text/javascript">
        var PageJsDataObj = {
            AID: '<%=Enow.TZB.Utility.Utils.GetQueryStringValue("ID") %>',
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
                        alert("挑战报名失败！");
                    }
                });
            },
            Add: function () {
                var dataurl = "AboutWarView.aspx?ation=inter&AID=" + PageJsDataObj.AID;
                this.GoAjax(dataurl);
            },
            BindBtn: function () {
                //添加
                $("#IsAccept").click(function () {
                    if (window.confirm("确定要挑战吗？")) {
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
