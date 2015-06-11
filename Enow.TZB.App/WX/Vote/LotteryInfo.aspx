<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LotteryInfo.aspx.cs" Inherits="Enow.TZB.Web.WX.Vote.LotteryInfo" %>

<%@ Register Src="../UserControls/UserHome.ascx" TagName="UserHome" TagPrefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>抽奖</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/mall.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jq.mobi.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="抽奖" runat="server" />
    <div class="user_warp">
        <div class="TabTitle" style="margin-top: 10px;">
           <ul class="fixed">
              <li id="n4Tab_Title0"  class="active" style="margin-left: 15%;"><a href="javascript:void(0);">详情</a></li>
              <li id="n4Tab_Title1" ><a href="LotteryUserlist.aspx?Vid=<%=Enow.TZB.Utility.Utils.GetQueryStringValue("Vid")%>">中奖名单</a></li>
           </ul>
        </div>
        <div class="cont gray_lineB font14" style="text-align: center;">
            <asp:Literal ID="ltrGoodsName" runat="server"></asp:Literal></div>
        <div class="list-item">
            <ul id="fiullist">
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <label>
                            <li>
                                <%#Container.ItemIndex+1%>.<%#Eval("Otitle")%>
                                <span style="float: right;">奖项数:<%#Eval("ONumber")%></span></li></label>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="cont gray_lineT mt10">
            <div class="padd10">
                <div class="font14 paddB10">
                    详情</div>
                <div id="divStandardInfo" class="indent2 font_gray">
                    <asp:Literal ID="litltxt" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
        <div class="mt20 padd_bot">
            <a href="javascript:void(0)" class="basic_btn" id="btnsave">抽奖</a></div>
    </div>
    <script type="text/javascript">
        var PageJsDataObj = {
            VoteID: '<%=Enow.TZB.Utility.Utils.GetQueryStringValue("Vid") %>',
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
                        alert("连接服务器失败！");
                    }
                });
            },
            Savebut: function () {

                var dataurl = "LotteryInfo.aspx?ation=SaveAdd&VoteID=" + PageJsDataObj.VoteID;
                this.GoAjax(dataurl);
            },
            BindBtn: function () {
                //投票
                $("#btnsave").click(function () {
                    PageJsDataObj.Savebut();
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
    </form>
</body>
</html>
