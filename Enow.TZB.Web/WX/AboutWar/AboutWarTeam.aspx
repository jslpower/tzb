<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutWarTeam.aspx.cs" Inherits="Enow.TZB.Web.WX.AboutWar.AboutWarTeam" %>

<%@ Register Src="../UserControls/UserHome.ascx" TagName="UserHome" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>约战详情</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/WX/css/juhui.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="约战详情" runat="server" />
    <div class="warp">
        <div class="msg_tab Tab_lie2" id="n4Tab">
            <div class="TabTitle">
                <ul class="fixed">
                    <li id="n4Tab_Title0"><a href="AboutWarView.aspx?ID=<%=Enow.TZB.Utility.Utils.GetQueryStringValue("ID") %>">
                        约战详情</a></li>
                    <li id="n4Tab_Title1" class="active"><a href="javascript:void(0);">主队详情</a></li>
                </ul>
            </div>
            <div class="TabContent">
                <div id="n4Tab_Content1">
                    <div class="juhui_cont padd10">
                        <div>
                            主队名称：<asp:Literal ID="litTeamName" runat="server"></asp:Literal></div>
                        <div class="indent2 font_gray" id="divTeamInfo" runat="server">
                        </div>
                    </div>
                    <div class="qiu_list player_list u_yuezhan_list mt10">
                        <ul>
                            <asp:Repeater ID="rptTeamList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="item-img">
                                            <img alt="" src="<%#Eval("HeadPhoto")%>" /></div>
                                        <div class="item-box">
                                            <dl>
                                                <dt>
                                                    <%#Eval("ContactName")%></dt>
                                                <dd>
                                                    <span class="bg_red">
                                                        <%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></dd>
                                                <dd>
                                                    <%#Eval("DNQYHM")%>号</dd>
                                                <dd>
                                                    <%#Eval("DNWZ")%>
                                                </dd>
                                            </dl>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="foot_fixed">
                        <div class="foot_box">
                            <div class="paddL10 paddR10">
                                <a id="IsAccept"  href="javascript:void(0);" class="basic_btn">挑战</a></div>
                        </div>
                    </div>
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
