<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" CodeBehind="TZArticle.aspx.cs" Inherits="Enow.TZB.Web.Manage.Article.TZArticle" %>

<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/js/moveScroll.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="contentbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    &nbsp;&nbsp;日志标题:
                    <input type="text" id="txtTitleName" name="txtTitleName" class="input-txt" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("Name")%>' />
                   
                    <input type="submit" id="btnSearch" class="search-btn" causesvalidation="False" value="搜索" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                  <asp:PlaceHolder ID="phadd" runat="server" Visible="true">
                    <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_add add_gg">
                        新增</a></li>
                    <li class="line"></li>
                    </asp:PlaceHolder>
                   <asp:PlaceHolder ID="phupdate" runat="server" Visible="true">
                    <li><s class="updateicon"></s><a href="#" hidefocus="true" class="toolbar_update">修改</a></li>
                    <li class="line"></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdelete" runat="server" Visible="true">
                    <li><s class="delicon"></s><a href="#" hidefocus="true" class="toolbar_delete">删除</a></li>
                   </asp:PlaceHolder>
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
                            日志标题
                        </th>
                        <th align="center">
                            发布人
                        </th>
                        <th align="center">
                            发布日期
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("Id") %>" />
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0)" class="transactions" data-rel='<%#Eval("Id") %>'>
                                        <%#Eval("ArticleTitle")%>
                                    </a>
                                </td>
                                <td align="center">
                                    <%#Eval("MemberName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>
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
                    url: '/Manage/Article',
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
            Add: function () {
                var data = this.DataBoxy();
                data.url += '/TZArticleEdit.aspx?';
                data.title = '新增日志';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Add"
                });
                this.ShowBoxy(data);
            },
            Update: function (objsArr) {
                var data = this.DataBoxy();
                data.url += '/TZArticleEdit.aspx?';
                data.title = '修改日志';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "update",
                    id: objsArr[0].find('input[type="checkbox"]').val()
                });
                this.ShowBoxy(data);
            },
            Delete: function (objsArr) {
                var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                }
              
                if (window.confirm("确定删除日志吗？")) {

                    var data = this.DataBoxy();
                    data.url += '/TZArticle.aspx?';

                    data.url += $.param({
                        sl: this.Query.sl,
                        doType: "delete",
                        id: objsArr[0].find('input[type="checkbox"]').val()
                    });

                }
                this.GoAjax(data.url);
            },

            Search: function () {
                var name = $("#txtTitleName").val();
               
                window.location.href = "TZArticle.aspx?Name=" + name + "";
            },

            View: function (Id) {
                var data = this.DataBoxy();
                data.url += '/TZArticleEdit.aspx?';
                data.title = '日志查看';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "view",
                    id: Id
                });
                this.ShowBoxy(data);
            },
            BindBtn: function () {
                //添加
                $(".add_gg").click(function () {
                    PageJsDataObj.Add();
                    return false;
                })
                //删除
                $(".toolbar_delete").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Delete(rows);
                    return false;
                });

                //搜索
                $("#btnSearch").click(function () {
                    PageJsDataObj.Search();
                    return false;
                });

                //查看文章
                $(".transactions").click(function () {
                    PageJsDataObj.View($(this).attr("Data-rel"));
                    return false;
                });
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "日志管理",
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
