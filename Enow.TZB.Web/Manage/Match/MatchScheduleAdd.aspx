<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatchScheduleAdd.aspx.cs"
    MasterPageFile="~/MasterPage/FinaWinBackPage.Master" Inherits="Enow.TZB.Web.Manage.Match.MatchScheduleAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->
    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Js/TableUtil.js" type="text/javascript"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="contentbox">
        <div class="firsttable">
            <table border="0" cellspacing="0" cellpadding="0" width="98%" align="center">
                <tr>
                    <td colspan="2" height="38" align="center">
                        <center>
                            <h4>
                                赛事日程安排</h4>
                        </center>
                    </td>
                </tr>
                <tr>
                    <th width="80" height="32" align="center">
                        赛事名称
                    </th>
                    <td>
                        <asp:Literal ID="ltrMatchName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="80" height="32" align="center">
                        比赛时间
                    </th>
                    <td>
                        <asp:Literal ID="ltrMatchTime" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="80" height="32" align="center">
                        赛事阶段
                    </th>
                    <td>
                        <asp:Literal ID="ltrGameName" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="0" width="98%" align="center" id="tblGoods"
                class="table_C autoAdd">
                <tbody>
                    <tr>
                    <th>
                    场次号
                    </th>
                        <th>
                            开始时间
                        </th>
                        <th>
                            结束时间
                        </th>
                        <th>
                            主队代码
                        </th>
                        <th>
                            主队
                        </th>
                        <th>
                            客队代码
                        </th>
                        <th>
                            客队
                        </th>
                        <th>
                            状态
                        </th>
                        <th width="140">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                    <tr class="tempRow">
                    <td><input type="hidden" id="hidScheduleId" name="hidScheduleId" value="<%#Eval("Id") %>" />
                    <input type="text" class="input-txt formsize50" id="txtOrdinalNumber" name="txtOrdinalNumber"
                                valid="required" errmsg="请输入场次号!" value="<%#Eval("OrdinalNumber") %>" />
                    </td>
                        <td height="40">
                            <input type="text" class="input-txt formsize90 Wdate" id="txtStartDate" name="txtStartDate"
                                onfocus="WdatePicker({startDate:'%y-%M-%d',dateFmt:'yyyy-MM-dd HH:mm:ss'})" valid="required"
                                errmsg="请输入开始日期!" value="<%#Eval("GameStartTime","{0:yyyy-MM-dd HH:mm:ss}") %>" />
                        </td>
                        <td>
                            <input type="text" class="input-txt formsize90 Wdate" id="txtEndDate" name="txtEndDate"
                                onfocus="WdatePicker({startDate:'%y-%M-%d',dateFmt:'yyyy-MM-dd HH:mm:ss'})" valid="required"
                                errmsg="请输入截止日期!" value="<%#Eval("GameEndTime","{0:yyyy-MM-dd HH:mm:ss}") %>" />
                        </td>
                        <td>
                            <input type="text" class="input-txt formsize50" id="txtHomeMatchCode" name="txtHomeMatchCode"
                                valid="required" errmsg="请输入主队代码!" value="<%#Eval("HomeMatchCode") %>" />
                        </td>
                        <td>
                            <select name="ddlHomeTeamId" id="ddlHomeTeamId" class="HomeTeamId">
                                <%#InitTeam(Convert.ToString(Eval("HomeTeamId")))%>
                            </select>
                        </td>
                        <td>
                            <input type="text" class="input-txt formsize50" id="txtAwayMatchCode" name="txtAwayMatchCode"
                                valid="required" errmsg="请输入客队代码!" value="<%#Eval("AwayMatchCode") %>" />
                        </td>
                        <td>
                            <select name="ddlAwayTeamId" id="ddlAwayTeamId" class="AwayTeamId">
                                <%#InitTeam(Convert.ToString(Eval("AwayTeamId")))%>
                            </select>
                        </td>
                        <td>
                            <input type="checkbox" name="cbGameState" id="cbGameState" value="1"<%#Convert.ToString(Eval("GameState"))=="1"?" checked=\"checked\"":"" %>>发布
                        </td>
                        <td align="center">
                            <div class="caozuo">
                                <ul>
                                    <li><s class="add"></s><a class="addbtnFinaPlan" href="javascript:void(0)"><span>添加</span></a></li>
                                    <li><s class="delete"></s><a class="delbtnFinaPlan" href="javascript:void(0)"><span>
                                        删除</span></a></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phNoData" runat="server">
                    <tr class="tempRow">
                    <td><input type="hidden" id="hidScheduleId" name="hidScheduleId" value="0" />
                    <input type="text" class="input-txt formsize50" id="txtOrdinalNumber" name="txtOrdinalNumber"
                                valid="required" errmsg="请输入场次号!" />
                    </td>
                        <td height="40">
                            <input type="text" class="input-txt formsize90 Wdate" id="txtStartDate" name="txtStartDate"
                                onfocus="WdatePicker({startDate:'%y-%M-%d',dateFmt:'yyyy-MM-dd HH:mm:ss'})" valid="required"
                                errmsg="请输入开始日期!" />
                        </td>
                        <td>
                            <input type="text" class="input-txt formsize90 Wdate" id="txtEndDate" name="txtEndDate"
                                onfocus="WdatePicker({startDate:'%y-%M-%d',dateFmt:'yyyy-MM-dd HH:mm:ss'})" valid="required"
                                errmsg="请输入截止日期!" />
                        </td>
                        <td>
                            <input type="text" class="input-txt formsize50" id="txtHomeMatchCode" name="txtHomeMatchCode"
                                valid="required" errmsg="请输入主队代码!" />
                        </td>
                        <td>
                            <select name="ddlHomeTeamId" id="ddlHomeTeamId" class="HomeTeamId">
                                <%=InitTeam(Enow.TZB.Utility.Utils.GetQueryStringValue("id")) %>
                            </select>
                        </td>
                        <td>
                            <input type="text" class="input-txt formsize50" id="txtAwayMatchCode" name="txtAwayMatchCode"
                                valid="required" errmsg="请输入客队代码!" />
                        </td>
                        <td>
                            <select name="ddlAwayTeamId" id="ddlAwayTeamId" class="AwayTeamId">
                                <%=InitTeam(Enow.TZB.Utility.Utils.GetQueryStringValue("id")) %>
                            </select>
                        </td>
                        <td>
                            <input type="checkbox" name="cbGameState" id="cbGameState" value="1">发布
                        </td>
                        <td align="center">
                            <div class="caozuo">
                                <ul>
                                    <li><s class="add"></s><a class="addbtnFinaPlan" href="javascript:void(0)"><span>添加</span></a></li>
                                    <li><s class="delete"></s><a class="delbtnFinaPlan" href="javascript:void(0)"><span>
                                        删除</span></a></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    </asp:PlaceHolder>
                </tbody>
            </table>
            <input type="hidden" id="hidDelList" name="hidDelList" />
            <div class="Basic_btn fixed">
                <ul>
                    <li><a href="javascript:void(0);" id="btnSave">保 存 &gt;&gt;</a></li>
                    <li><a href="javascript:void(0);" id="btnCanel">返 回 &gt;&gt;</a></li>
                </ul>
                <div class="hr_10">
                </div>
            </div>
        </div>
        <script type="text/javascript">
            var PageJsDataObj = {
                Data: {
                    MId: '<%=Request.QueryString["MId"] %>',
                    SMId: '<%=Request.QueryString["SMId"] %>',
                    act: '<%=Request.QueryString["act"] %>',
                    CId: '<%=Request.QueryString["CId"] %>',
                    id: '<%=Request.QueryString["id"] %>'
                },
                DataBoxy: function () {/*弹窗默认参数*/
                    return {
                        url: '',
                        title: "",
                        width: "710px",
                        height: "420px"
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
                AddRowCallBack: function (tr) {
                    var $tr = tr;
                    $tr.find("input[name='hidScheduleId']").val("0");
                    /*
                    var HSELECT = $tr.find("select[name='ddlHomeTeamId']")
                    var ASELECT = $tr.find("select[name='ddlAwayTeamId']")
                    //过滤已安排的
                    $(".HomeTeamId").each(function() {
                    var SId = $(this).find("option:selected").val();
                    $(HSELECT).find("option[value='" + SId + "']").remove();
                    });
                    */
                    return true;
                },
                DelRowCallBack: function (tr) {
                    var $tr = tr;
                    var delId = $tr.find("input[name='hidScheduleId']").val() + ",";
                    var OrdinalNumber = $tr.find("input[name='txtOrdinalNumber']").val();
                    if (confirm("是否确认删除场次号为:" + OrdinalNumber + " 的排赛？")) {
                        $("#hidDelList").val($("#hidDelList").val() + delId);
                        //alert($("#hidDelList").val());
                    }
                    return true;
                },
                CheckForm: function () {
                    var form = $("form").get(0);
                    return ValiDatorForm.validator(form, "alert");
                },
                Form: null,
                Save: function () {
                    $("#btnSave").text("提交中...");
                    PageJsDataObj.submit();
                },
                submit: function () {
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "MatchScheduleAdd.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
                        data: $("#btnSave").closest("form").serialize(),
                        dataType: "json",
                        success: function (ret) {
                            //ajax回发提示
                            if (ret.result != "0") {
                                tableToolbar._showMsg(ret.msg, function () {
                                    window.location.href = "ScheduleList.aspx?" + $.param(PageJsDataObj.Data);
                                });
                            } else {
                                tableToolbar._showMsg(ret.msg);
                                $("#btnSave").text("保 存");
                                PageJsDataObj.BindBtn();
                            }
                        },
                        error: function () {
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                            $("#btnSave").text("保 存");
                            PageJsDataObj.BindBtn();
                        }
                    });
                },
                PageInit: function () {
                    $("#tblGoods").autoAdd({ tempRowClass: "tempRow", addButtonClass: "addbtnFinaPlan", delButtonClass: "delbtnFinaPlan", addCallBack: PageJsDataObj.AddRowCallBack, delCallBack: PageJsDataObj.DelRowCallBack });
                },
                BindBtn: function () {

                    $("#btnSave").unbind("click").click(function () {
                        if (PageJsDataObj.CheckForm()) {
                            PageJsDataObj.Save();
                        }
                    });
                    $("#btnCanel").unbind("click").click(function () {
                        window.history.go(-1);
                        return false;
                    })
                }
            }

            $(function () {
                PageJsDataObj.PageInit();
                PageJsDataObj.BindBtn();
            });
        </script>
</asp:Content>
