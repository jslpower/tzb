<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TZGatheringDetail.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.TZGatheringDetail" %>
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
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" runat="server" />
<div class="warp">
  <div class="msg_tab Tab_lie2"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0"  class="active"><a href="TZGatheringDetail.aspx?ActId=<%=Acid %>&types=<%=types %>">详情</a></li>
              <li id="n4Tab_Title1" ><a href="GatheringSignUp.aspx?ActId=<%=Acid %>&types=<%=types %>">已报名</a></li>
           </ul>
        </div>

        <div class="TabContent">
        
			<div id="n4Tab_Content0">
            
                <div class="juhui_cont padd10">
                   
                   <div>[<asp:Literal runat="server" ID="litgl"></asp:Literal>] <asp:Literal runat="server" ID="littitle"></asp:Literal></div>
                   <div>时间：<asp:Literal runat="server" ID="littime"></asp:Literal></div>
                   <asp:Literal runat="server" ID="litqcname"></asp:Literal>
                   <div>区域：<asp:Literal runat="server" ID="litquyu"></asp:Literal></div>
                   <div>地址：<asp:Literal runat="server" ID="litdizhi"></asp:Literal></div>
                   <div>费用：<asp:Literal runat="server" ID="litfeiyong"></asp:Literal></div>
                   <div>详情：</div>
                   <div class="indent2 font_gray">
                     <asp:Literal runat="server" ID="litneirong"></asp:Literal>
                   </div>
                   
                </div>
                
                <div class="mt20 padd_bot">
                <a href="javascript:void(0)" class="basic_btn" id="btnsave">报名</a></div>
            
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
                        alert("报名失败！");
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
