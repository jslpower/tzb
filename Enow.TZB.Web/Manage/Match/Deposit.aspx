<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Deposit.aspx.cs" Inherits="Enow.TZB.Web.Manage.Match.Deposit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>保证金分配</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="contentbox">
        <div class="firsttable">
            <span class="firsttableT">保证金分配</span>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tableInfo">
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                        
                <tr>
                    <th width="80" height="40" align="right"><input type="hidden" name="hidId" id="hidId" value="<%#Eval("Id")%>" /><input type="hidden" name="hidTeamId" id="hidTeamId" value="<%#Eval("TeamId")%>" />
                        球队名称：
                    </th>
                    <td>
                    <%#Eval("TeamName") %>
                    </td>
                    <th width="80" height="40" align="right">
                        保证金：
                    </th>
                    <td>
                    <%#(Convert.ToDecimal(Eval("DepositMoney")) - Convert.ToDecimal(Eval("DepositOverage"))).ToString("F2")%>元
                    </td>
                    <th width="80" height="40" align="right">
                        分配额度：
                    </th>
                    <td><input type="hidden" name="hidReFee" id="hidReFee" value="<%#(Convert.ToDecimal(Eval("DepositMoney")) - Convert.ToDecimal(Eval("DepositOverage"))).ToString("F2")%>" /><input type="text" maxlength="6" name="txtDepositMoney" id="txtDepositMoney" class="input-txt formsize80" valid="required|isMoney|range" min="0" max="<%#(Convert.ToDecimal(Eval("DepositMoney")) - Convert.ToDecimal(Eval("DepositOverage"))).ToString("F2")%>" errmsg="请填写分配额度!|分配额度不正确！|分配额度区间不正确!">元<span class="fontred">*</span>
                    </td>
                </tr></ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="Basic_btn fixed">
            <ul>
                <li><a href="javascript:void(0);" id="btnSave">保  存</a></li>
                <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"
                    hidefocus="true">关 闭</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </form>
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
                    url: "Deposit.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function (ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function () {
                                parent.window.location.reload();
                                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();  
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
            },
            BindBtn: function () {
                $("#btnSave").unbind("click").click(function () {
                    if (PageJsDataObj.CheckForm()) {
                        PageJsDataObj.Save();
                    }
                });
            }
        }

        $(function () {
            PageJsDataObj.PageInit();
            PageJsDataObj.BindBtn();
        });
</script>
</body>
</html>
