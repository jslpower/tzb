<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="LotteryList.aspx.cs" Inherits="Enow.TZB.Web.Manage.Vote.LotteryList" %>
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
                <p>
                    发布目标:
                    <asp:DropDownList ID="dropfbmb" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                    类型:
                    <asp:DropDownList ID="droptypes" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                    名称：
                    <input type="text" id="txtTitle" runat="server" name="txtTitle" class="input-txt" />
                    <input type="button" id="btnSearch" class="search-btn" causesvalidation="False" value="搜索" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                    <asp:PlaceHolder ID="phadd" runat="server" Visible="false">
                        <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_add add_gg">
                            新增</a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phUpdate" runat="server" Visible="false">
                        <li class="line"></li>
                        <li><s class="updateicon"></s><a href="#" hidefocus="true" class="toolbar_update">修改</a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phdelete" runat="server" Visible="false">
                        <li class="line"></li>
                        <li><s class="delicon"></s><a href="#" hidefocus="true" class="toolbar_delete">删除</a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="true">
                        <li class="line"></li>
                        <li><s class="delicon"></s><a href="#" hidefocus="true" class="Getbaoming">奖项信息</a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible="true">
                        <li class="line"></li>
                        <li><s class="delicon"></s><a href="#" hidefocus="true" class="Getzjyh">中奖信息</a></li>
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
                            序号
                        </th>
                        <th align="center">
                            类型
                        </th>
                        <th align="center">
                            名称
                        </th>
                         <th align="center">
                            发布目标
                        </th>
                        <th align="center">
                            截止日期
                        </th>
                        <th align="center">
                            发布时间
                        </th>
                       
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" data-title="<%#Eval("Vtitle")%>" value="<%#Eval("Vid") %>" />
                                </td>
                                <td align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <%#(Enow.TZB.Model.EnumType.VotecjEnum)Enow.TZB.Utility.Utils.GetInt(Eval("Vtype").ToString(), 1)%>
                                </td>
                                <td align="center">
                                    <%#Eval("Vtitle")%>
                                </td>
                                <td align="center">
                                   <%#(Enow.TZB.Model.EnumType.ReleaseEnum)Enow.TZB.Utility.Utils.GetInt(Eval("VRelease").ToString(), 1)%>
                                </td>
                                <td align="center">
                                   <%#Enow.TZB.Utility.Utils.GetDateTime(Eval("ExpireTime").ToString()).ToString("yyyy-MM-dd")%>
                                </td>
                                <td align="center">
                                    <%#Enow.TZB.Utility.Utils.GetDateTime(Eval("LaunchTime").ToString()).ToString("yyyy-MM-dd")%>
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
             MId:<%=Request.QueryString["MId"] %>,
            SMId:<%=Request.QueryString["SMId"] %>,
            CId:<%=Request.QueryString["CId"] %>,
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
                    url: '/Manage/Vote',
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
            Update: function (objsArr) {
                var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                     var vl=objsArr[i].find("input:checked").val();
                    if(vl!="")
                    list.push(vl);
                  
                }
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息");
                    return;
                }
                var data = this.DataBoxy();
                data.url += '/LotteryAdd.aspx?';
                data.title = '修改抽奖信息';
                data.url += $.param({
                    sl: this.Query.sl,
                    MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                    act:"update",
                    doType: "update",
                    id: objsArr[0].find('input[type="checkbox"]').val()
                });
                window.location.href = data.url;
            },
            Getbaoming: function (obj) {
                var list = new Array();
                var sname="";
                $("#liststyle").find("input[type='checkbox'][name='checkbox']:checked").each(function () {
                    var vl=$(this).val();
                    if(vl!="on"&&vl!="")
                    list.push(vl);
                    var getname=$(this).attr("data-title");
                    if(getname!="")
                    sname=getname;
                });
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息！");
                    return;
                }
                if (list.length <=0) {
                    tableToolbar._showMsg("未选择任何数据！");
                    return;
                }
                var data = this.DataBoxy();
                if (obj==1) {
                 data.url += '/VoteView.aspx?';
                }
                else
                {
                data.url += '/LotteryUserView.aspx?';
                }
                data.title = sname;
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "GetLottery",
                    id: list[0]
                });
                this.ShowBoxy(data);
            },
            Add: function () {
                var data = this.DataBoxy();
                data.url += '/LotteryAdd.aspx?';
                data.title = '添加抽奖信息';
                data.url += $.param({
                    sl: this.Query.sl,
                    MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                    act:"add",
                    doType: "add"
                });
                window.location.href = data.url;
            },
            Delete: function (objsArr) {
                         var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    var vl=objsArr[i].find("input:checked").val();
                    if(vl!="")
                    list.push(vl);
                }
                if (list.length <= 0) {
                    tableToolbar._showMsg("请选择删除的项！");
                    return;
                }
                 var data = this.DataBoxy();
                data.url += '/LotteryList.aspx?';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "delete",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
                
               
            },
            Search: function () {
                var Title = $("#<%=txtTitle.ClientID %>").val();
                var fbmb=$("#<%=dropfbmb.ClientID %>").val();
                var types=$("#<%=droptypes.ClientID %>").val();
                window.location.href = "LotteryList.aspx?"+$.param(PageJsDataObj.Query)+"&Title=" + Title + "&fbmb="+fbmb+"&types="+types;
            },
            BindBtn: function () {
                //查看报名人数
                $(".Getbaoming").click(function() {
                   PageJsDataObj.Getbaoming(1);
                   return false;
                });
                //查看中奖人数
                $(".Getzjyh").click(function() {
                   PageJsDataObj.Getbaoming(2);
                   return false;
                });
                //添加
                $(".add_gg").click(function () {
                    PageJsDataObj.Add();
                    return false;
                })
                //搜索
                $("#btnSearch").click(function () {
                    PageJsDataObj.Search();

                });
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "活动管理",
                    updateCallBack: function (objsArr) {
                        PageJsDataObj.Update(objsArr);
                        return false;
                    },
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
