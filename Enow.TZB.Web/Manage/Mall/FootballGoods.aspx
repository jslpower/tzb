<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="FootballGoods.aspx.cs" Inherits="Enow.TZB.Web.Manage.Mall.FootballGoods" %>

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
                    商品分类:
                    <asp:DropDownList ID="dropyjclass" DataTextField="Rolename" DataValueField="Id"  runat="server"></asp:DropDownList>
                    &nbsp;&nbsp;商品名称:
                    <input type="text" id="txtName" name="txtName" class="input-txt" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("Name")%>' />
                    &nbsp;&nbsp;商品状态:
                    <select id="selStatus" name="selStatus" class="inputselect">
                    <%=InitStatus(Enow.TZB.Utility.Utils.GetQueryStringValue("status"))%>
                    </select>&nbsp;&nbsp;
                    <input type="submit" id="btnSearch" class="search-btn" causesvalidation="False" value="搜索" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                  <asp:PlaceHolder ID="PhAdd" runat="server" Visible="false">
                    <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_add add_gg">
                        新增</a></li>
                    <li class="line"></li>
                    </asp:PlaceHolder>
                     <asp:PlaceHolder ID="phUpdate" runat="server" Visible="false">
                    <li><s class="updateicon"></s><a href="#" hidefocus="true" class="toolbar_update">修改</a></li>
                    <li class="line"></li>
                    </asp:PlaceHolder>
                     <asp:PlaceHolder ID="phdelete" runat="server" Visible="false">
                    <li><s class="delicon"></s><a href="#" hidefocus="true" class="toolbar_delete">删除</a></li>
                    <li class="line"></li>
                    </asp:PlaceHolder>
                     <asp:PlaceHolder ID="phstop" runat="server" Visible="false">
                    <li><s class="jinyicon"></s><a href="#" hidefocus="true" class="toolbar_stop">停售</a></li>
                    <li class="line"></li>
                    </asp:PlaceHolder>
                     <asp:PlaceHolder ID="phstart" runat="server" Visible="false">
                    <li><s class="qiyicon"></s><a href="#" hidefocus="true" class="toolbar_start">恢复销售</a></li>
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
                            状态
                        </th>
                        <th align="center">
                            商品名称
                        </th>
                       
                        <th align="center">
                            商品类别
                        </th>
                        <th align="center">
                             二级分类
                        </th>
                        <th align="center">
                            市场价
                        </th>
                        <th align="center">
                            会员价
                        </th>
                        <th align="center">
                            库存数量
                        </th>
                        <th align="center">
                            生产商
                        </th>
                        <th align="center">
                            添加日期
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("Id") %>" />
                                </td>
                                  <td align="center">
                                    <%#(Enow.TZB.Model.商品销售状态)int.Parse(Eval("Status").ToString())%>
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0)" class="transactions" data-rel='<%#Eval("Id") %>'>
                                        <%#Eval("GoodsName")%>
                                    </a>
                                </td>
                              
                                <td align="center">
                                 <%--   <%#Eval("ClassName")%>--%>
                                   <%#(Enow.TZB.Model.商品分类)int.Parse(Eval("GoodsClassId").ToString())%>
                                </td>
                                <td align="center">
                                    <%#Eval("RoleClassName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("MarketPrice")%>
                                </td>
                                <td align="center">
                                    <%#Eval("MemberPrice")%>
                                </td>
                                <td align="center">
                                    <%#Eval("StockNum")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Producer")%>
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
                    url: '/Manage/Mall',
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
                data.url += '/FootballGoodsEdit.aspx?';
                data.title = '新增商品';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Add"
                });
                this.ShowBoxy(data);
            },
            Update: function (objsArr) {
                var data = this.DataBoxy();
                data.url += '/FootballGoodsEdit.aspx?';
                data.title = '修改商品';
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
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息");
                    return;
                }
                if (window.confirm("确定删除此条记录？")) {

                    var data = this.DataBoxy();
                    data.url += '/FootballGoods.aspx?';

                    data.url += $.param({
                        sl: this.Query.sl,
                        doType: "delete",
                        id: objsArr[0].find('input[type="checkbox"]').val()
                    });

                }
                this.GoAjax(data.url);
            },
            StopSell: function (objsArr) {
                var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                }
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息");
                    return;
                }
                if (window.confirm("确定停止销售此商品吗？")) {

                    var data = this.DataBoxy();
                    data.url += '/FootballGoods.aspx?';

                    data.url += $.param({
                        sl: this.Query.sl,
                        doType: "stop",
                        id: objsArr[0].find('input[type="checkbox"]').val()
                    });

                }
                this.GoAjax(data.url);
            },
            StartSell: function (objsArr) {
                var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                }
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息");
                    return;
                }
                if (window.confirm("确定恢复销售此商品吗？")) {

                    var data = this.DataBoxy();
                    data.url += '/FootballGoods.aspx?';

                    data.url += $.param({
                        sl: this.Query.sl,
                        doType: "start",
                        id: objsArr[0].find('input[type="checkbox"]').val()
                    });

                }
                this.GoAjax(data.url);
            },
            Search: function () {
                var name = $("#txtName").val();
                var Status = $("#selStatus").val();
                var types = $("#<%=dropyjclass.ClientID %>").val();
                var MId=<%=Request.QueryString["MId"] %>;
                var SMId=<%=Request.QueryString["SMId"] %>;
                var CId=<%=Request.QueryString["CId"] %>;
                window.location.href = "FootballGoods.aspxMId="+MId+"&SMId="+SMId+"&CId="+CId+"&Name=" + name + "&Status=" + Status + "&types=" + types;
            },

            View: function (Id) {
                var data = this.DataBoxy();
                data.url += '/FootballGoodsEdit.aspx?';
                data.title = '商品查看';
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
                //停售
                $(".toolbar_stop").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.StopSell(rows);
                    return false;
                });
                //恢复销售
                $(".toolbar_start").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.StartSell(rows);
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
                    objectName: "商品管理",
                    updateCallBack: function (objsArr) {
                        PageJsDataObj.Update(objsArr);
                        return false;
                    },
                    deleteCallBack: function (objsArr) {
                        PageJsDataObj.Delete(objsArr);
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
