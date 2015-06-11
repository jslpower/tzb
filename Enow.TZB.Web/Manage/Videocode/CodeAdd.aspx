<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeAdd.aspx.cs" Inherits="Enow.TZB.Web.Manage.Videocode.CodeAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" content="IE=EmulateIE8" http-equiv="X-UA-Compatible" />
    <title>新增码</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="contentbox">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
             <th width="100" height="40" align="right">
                    码分类：
                </th>
                <td>
                  <asp:DropDownList ID="droptype" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                </td>
                <th width="100" height="40" align="right">
                    码前缀：
                </th>
                <td>
                   <asp:TextBox ID="txtTypeName" CssClass="formsize120 input-txt" runat="server" />
                </td>
                 <th width="100" height="40" align="right">
                    码区间：
                </th>
                <td>
                   <asp:TextBox ID="txtsteat" CssClass="formsize120 input-txt" runat="server" />
                    -
                    <asp:TextBox ID="txtend" CssClass="formsize120 input-txt" runat="server" />
                </td>
                
            </tr>
        </table>
        <div class="Basic_btn fixed">
            <ul>
                <li>
                    <asp:LinkButton ID="linkBtnSave" runat="server" OnClientClick="return yanzheng(); " onclick="linkBtnSave_Click">生成 >></asp:LinkButton>
                </li>
                <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"
                    hidefocus="true">返 回 >></a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    </form>
    <script type="text/javascript">
        function yanzheng() {
            var qianzui = $("#<%=txtTypeName.ClientID %>").val();
            var tst = parseInt($("#<%=txtsteat.ClientID %>").val());
            var ted = parseInt($("#<%=txtend.ClientID %>").val());
            var parent = /^[A-Za-z]{3}$/;
            var num = /^[0-9]{1,5}$/;
            if (!parent.test(qianzui)) {
                alert("前缀格式不正确！只能输入3位英文字母！");
                return false;
            }
            if (!num.test(tst) || !num.test(ted)) {
                alert("码区间不能为空或只能输入1-5位数字！");
                return false;
            }
            if (tst > ted) {
                alert("前区间数不得大于后区间数！");
                return false;
            }
            $("#<%=linkBtnSave.ClientID %>").val("生成中...").unbind("click").css({ "color": "#999999" });

            return true;
        }
    </script>
</body>
</html>
