<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RefuseReason.aspx.cs" Inherits="Enow.TZB.Web.Manage.Team.RefuseReason" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <asp:PlaceHolder ID="phShenghe" runat="server" >
                    <tr>
                        <th width="100">
                            不通过原因：
                        </th>
                        <td>
                            <select id="ddlReason" name="ddlReason">
                                 <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球队审核不通过原因),
                                                                                              new string[] { }),"1")
                                %></select>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phdisband" runat="server">
                    <tr>
                        <th width="100">
                            解散拒绝理由：
                        </th>
                        <td>
                            <asp:TextBox ID="txtDisband" runat="server" TextMode="MultiLine" CssClass="input-txt formsize450"
                                Height="150px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <div class="Basic_btn fixed">
            <ul>
                <li>
                    <asp:LinkButton ID="linkBtnSave" runat="server" onclick="linkBtnSave_Click">保 存 >></asp:LinkButton>
                </li>
                <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"
                    hidefocus="true">关 闭 >></a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
