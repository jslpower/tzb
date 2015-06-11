<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="BallotList.aspx.cs" Inherits="Enow.TZB.Web.Manage.Match.BallotList" %>
<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/js/moveScroll.js"></script>
    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
<div class="contentbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>球队名称：
                    <asp:TextBox ID="txtKeyWord" CssClass="formsize120 input-txt" runat="server" />
                    &nbsp;&nbsp;比赛名称：
                    <asp:TextBox ID="txtMatch" CssClass="formsize120 input-txt" runat="server" />
                    &nbsp;&nbsp;报名日期：
                    <asp:TextBox ID="txtStartDate" onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt"
                        MaxLength="10"></asp:TextBox>-<asp:TextBox ID="txtEndDate" onfocus="WdatePicker()"
                            runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="search-btn" CausesValidation="False"
                        OnClick="btnSearch_Click" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                        <li><s class="qiyicon"></s><a href="#" hidefocus="true" class="toolbar_enable">抽签</a></li>
                        <li class="line"></li>
                </ul>
                <div class="pages">
                    <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </div>
            </div>
            <!--列表表格-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle">
                    <tr>
                        <th width="30" class="thinputbg">
                            <input type="checkbox" name="checkbox" id="checkbox" />
                        </th>
                        <th align="center">
                            编号
                        </th>
                        <th align="center">
                            球队名称
                        </th>
                        <th align="center">
                            创始人姓名
                        </th>
                        <th align="center">
                            赛事名称
                        </th>
                        <th align="center">
                            球场名称
                        </th>
                        <th align="center">
                            报名状态
                        </th>
                        <th align="center">
                            抽签状态
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("Id") %>" data-state="<%#Convert.ToBoolean(Eval("IsBallot"))?"1":"0" %>" />
                                    <input type="hidden" name="hMid" id="hMid" value="<%#Eval("MatchId")%>" />
                                </td>
                                <td align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <%#Eval("TeamName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("TeamOwner")%>
                                </td>
                                <td align="center">
                                    <%#Eval("MatchName")%>
                                </td>
                                 <td align="center">
                                    <%#Eval("FieldName")%>
                                </td>
                                <td align="center">
                                    <%#(Enow.TZB.Model.EnumType.参赛审核状态)int.Parse(Eval("state").ToString())%>
                                </td>
                                <td align="center"><%#Convert.ToBoolean(Eval("IsBallot"))?"已抽":"未抽"%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
                        <tr>
                            <td colspan="8" align="center">
                                暂无数据
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
            </div>
            <!--列表结束-->
            <div class="tablehead botborder">
                <script type="text/javascript">
                    document.write(document.getElementById("tablehead").innerHTML);
                </script>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
                sl: '',
                title: '审核'
            },
            //浏览弹窗关闭后刷新当前浏览数
            BindClose: function () {
                $("a[data-class='a_close']").unbind().click(function () {
                    window.location.reload();
                    return false;
                })
            },
            DataBoxy: function () {/*弹窗默认参数*/
                return {
                    url: '/Manage/Match',
                    title: "",
                    width: "850px",
                    height: "600px"
                }
            },
            ShowBoxy: function (data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            GoAjax: function (url) {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function (result) {
                        if (result.result == "1") {
                            tableToolbar._showMsg(result.msg, function () {
                                window.location.reload();
                            });
                        }
                        else { tableToolbar._showMsg(result.msg); }
                    },
                    error: function () {
                        //ajax异常--你懂得
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            Ballot: function (objsArr) {
                var list = new Array();
                var state = new Array();
                var Mlist = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                    Mlist.push(objsArr[i].find("input[type='hidden']").val());
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '0') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录已抽签，不能再次抽签！");
                        return;
                    }
                }
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息");
                    return;
                }
                else if (list.length == 0) {
                    tableToolbar._showMsg("未选中任何信息");
                    return;
                }
                var data = this.DataBoxy();
                data.title = "球队抽签";
                data.url += "/BallotList.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Ballot",
                    id: list.join(","),
                    Mid: Mlist.join(",")
                });
                this.GoAjax(data.url);
            },
            BindBtn: function () {
                //抽签
                $(".toolbar_enable").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Ballot(rows);
                    return true;
                });
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "抽签管理"
                });
            },
            PageInit: function () {
                //绑定功能按钮
                this.BindBtn();
                //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
                $('.tablelist-box').moveScroll();
            },
            CallBackFun: function (data) {
                newToobar.backFun(data);
            }

        }
        $(function () {
            PageJsDataObj.PageInit();
            PageJsDataObj.BindClose();
            return false;
        })
    </script>
</asp:Content>
