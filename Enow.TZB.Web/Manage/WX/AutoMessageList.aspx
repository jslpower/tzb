<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="AutoMessageList.aspx.cs" Inherits="Enow.TZB.Web.Manage.WX.AutoMessageList" %>
<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/js/moveScroll.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="contentbox">
        <div class="searchbox fixed">
            <span class="searchT">
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_add add_gg">
                        新增</a></li>
                    <li class="line"></li>
                    <li><s class="delicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_delete">删除</a> </li>
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
                            类型
                        </th>
                        <th align="center">
                      留言内容</th>
                        <th align="center">
                            自动回复内容
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                            <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("Id") %>" />
                                </td>
                                <td align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>                   
                                <td align="left">
                                    <%#(Enow.TZB.Model.EnumType.微信回复类型)Convert.ToInt32(Eval("TypeId"))%>
                                </td>
                                <td align="left"><%#Eval("Question")%>
                                </td>
                                <td align="left">
                                    <%#ReplyText((Enow.TZB.Model.EnumType.微信回复类型)Convert.ToInt32(Eval("TypeId")),Convert.ToString(Eval("Answer")))%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
                        <tr>
                            <td colspan="5" align="center">
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
                sl: ''
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
                    url: '/Manage/WX',
                    title: "",
                    width: "900px",
                    height: "620px"
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
            Add: function () {
                var data = this.DataBoxy();
                data.url += '/AutoMsgAdd.aspx?';
                data.title = '添加自动回复信息';
                data.url += $.param({
                    sl: this.Query.sl,
                    MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                    act:"add",
                    doType: "add"
                });
                this.ShowBoxy(data);
            },
            Delete: function (objsArr) {
                var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                }
                    var data = this.DataBoxy();
                    data.url += '/AutoMessageList.aspx?';

                    data.url += $.param({
                        sl: this.Query.sl,
                        MId:<%=Request.QueryString["MId"] %>,
                        SMId:<%=Request.QueryString["SMId"] %>,
                        CId:<%=Request.QueryString["CId"] %>,
                        doType: "delete",
                        id: list.join(",")
                    });
                this.GoAjax(data.url);
            },
            BindBtn: function () {
                //添加
                $(".add_gg").click(function () {
                    PageJsDataObj.Add();
                    return false;
                });
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "微信自动回复",
                    deleteCallBack: function (objsArr) {
                        PageJsDataObj.Delete(objsArr);
                    }
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