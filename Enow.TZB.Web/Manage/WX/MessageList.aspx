<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="MessageList.aspx.cs" Inherits="Enow.TZB.Web.Manage.WX.MessageList" %>
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
                </ul>
                <div class="pages">
                    <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </div>
            </div>
            <!--列表表格-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle">
                    <tr>
                        <th align="center">
                            编号
                        </th>
                        <th align="center">
                            地域
                        </th><th align="center">
                      手机号码</th>
                        <th align="center">
                            姓名
                        </th>
                        <th align="center">
                            昵称
                        </th>
                        <th align="center">
                            留言内容
                        </th>
                        <th align="center">
                            留言时间
                        </th>
                        <th align="center">
                            是否回复
                        </th>
                        <th align="center" >
                        回复内容
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td align="left"><%#Eval("CountryName")%>-<%#Eval("ProvinceName")%>-<%#Eval("CityName")%>-<%#Eval("AreaName") %>
                                </td>                              
                                <td align="left">
                                    <%#Eval("MobilePhone")%>
                                </td>
                                <td align="left"><%#Eval("ContactName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("NickName")%>
                                </td>
                                <td align="left">
                                <%#GetMsg(Convert.ToInt32(Eval("MsgType")), Convert.ToString(Eval("TextMessageInfo")), Convert.ToString(Eval("MediaPath")), Convert.ToString(Eval("Format")), Convert.ToString(Eval("Recognition")))%>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td align="center">
                                <%#Convert.ToBoolean(Eval("IsReply"))?"是":"否"%>
                                </td>
                                <td align="center">
                                <%#Convert.ToBoolean(Eval("IsReply")) ? Eval("ReplyInfo") : "<input type=\"button\" value=\"回复\" onclick=\"PageJsDataObj.Reply('" + Eval("Id") + "')\">"%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
                        <tr>
                            <td colspan="7" align="center">
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
                    width: "830px",
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
            Reply: function (Id) {
                var data = this.DataBoxy();
                data.url += '/MsgReply.aspx?';
                data.title = '微信回复';
                data.url += $.param({
                    sl: this.Query.sl,
                    Id: Id
                });
                this.ShowBoxy(data);
            },
            Paly: function (Id) {
                document.getElementById(Id).play();
            },
            BindBtn: function () {
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "微信消息管理",
                    updateCallBack: function (objsArr) {
                        PageJsDataObj.Update(objsArr);
                        return false;
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
