<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="ScheduleDetail.aspx.cs" Inherits="Enow.TZB.Web.Manage.Match.ScheduleDetail" %>
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
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                    <tr class="tempRow">
                    <td><%#Eval("OrdinalNumber") %></td>
                        <td height="40"><%#Eval("GameStartTime","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                        <td><%#Eval("GameEndTime","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                        <td><%#Eval("HomeMatchCode") %></td>
                        <td><%#Eval("HomeTeamName")%></td>
                        <td><%#Eval("AwayMatchCode") %></td>
                        <td><%#Eval("AwayTeamName")%></td>
                        <td><%#Convert.ToString(Eval("GameState"))=="1"?"发布":"未发布" %></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <input type="hidden" id="hidDelList" name="hidDelList" />
            <div class="Basic_btn fixed">
                <ul>
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
                BindBtn: function () {

                    $("#btnCanel").unbind("click").click(function () {
                        window.history.go(-1);
                        return false;
                    })
                }
            }

            $(function () {
                PageJsDataObj.BindBtn();
            });
        </script>
</asp:Content>
