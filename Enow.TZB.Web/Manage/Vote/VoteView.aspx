<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoteView.aspx.cs" Inherits="Enow.TZB.Web.Manage.Vote.VoteView" %>
<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <meta charset="utf-8" content="IE=EmulateIE8" http-equiv="X-UA-Compatible" />
    <title>投票选项列表</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
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
                            序号
                        </th>
                        <th align="center">
                           <%=Types %>选项名称
                        </th>
                        <th align="center">
                            <%=Types %>数量
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <%#Eval("Otitle")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ONumber")%>
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
    </form>
</body>
</html>
