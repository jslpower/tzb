<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsEdit.aspx.cs" Inherits="Enow.TZB.Web.Manage.Goods.GoodsEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" id="tableInfo">
            <tr>
                    <th width="100">
                        所属场地：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlFieldName" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        商品类别：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        商品名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtGoodsName" runat="server" CssClass="inputtext formsize240"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        单位：
                    </th>
                    <td>
                        <asp:TextBox ID="txtUnit" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        商品状态：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        生产厂商：
                    </th>
                    <td>
                        <asp:TextBox ID="txtProducer" runat="server" CssClass="inputtext formsize240"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        市场价：
                    </th>
                    <td>
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="inputtext formsize80" MaxLength="6"></asp:TextBox>&nbsp;元
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        铁丝价：
                    </th>
                    <td>
                        <asp:TextBox ID="txtCurrencyPrice" runat="server" CssClass="inputtext formsize80" MaxLength="6"></asp:TextBox>&nbsp;元
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        库存数量：
                    </th>
                    <td>
                        <asp:TextBox ID="txtStock" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        产品介绍：
                    </th>
                    <td>
                        <asp:TextBox ID="txtIntroduce" runat="server" class="editText"></asp:TextBox>
                    </td>
                </tr>
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
<script type="text/javascript">
    var GoodsInfo = {
        InitEdit: function () {
            $("#tableInfo").find(".editText").each(function () {
                KEditer.init($(this).attr("id"), {
                    resizeMode: 0,
                    items: keSimple,
                    height: $(this).attr("data-height") == undefined ? "360px" : $(this).attr("data-height"),
                    width: $(this).attr("data-width") == undefined ? "660px" : $(this).attr("data-width")
                });
            });
        },
        CheckForm: function () {
            KEditer.sync();

            return true;
        }
    };

    $(document).ready(function () {

        GoodsInfo.InitEdit();

        $("#<%= linkBtnSave.ClientID %>").click(function () {
            return GoodsInfo.CheckForm();
        });
    });
</script>
