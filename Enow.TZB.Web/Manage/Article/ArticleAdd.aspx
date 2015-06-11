<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleAdd.aspx.cs" ValidateRequest="false"
    Inherits="Enow.TZB.Web.Manage.Article.ArticleAdd" %>

<%@ Register Src="/UserControls/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" content="IE=EmulateIE8" http-equiv="X-UA-Compatible" />
    <title>添加文章</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="contentbox">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tableInfo">
            <tr>
                <th width="150" height="40" align="right">
                    文章标题：
                </th>
                <td>
                    <asp:TextBox ID="txtTitle" CssClass="input-txt formsize240" runat="server"></asp:TextBox><span
                        class="fontred">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                        ErrorMessage="请填写文章标题！">*</asp:RequiredFieldValidator>
                </td>
            </tr>
                              <tr>
                <th width="150" height="40" align="right">
                    文章概要：
                </th>
                <td>
                    <asp:TextBox ID="txtTitleSulg" CssClass="input-txt formsize240" runat="server"></asp:TextBox><span
                        class="fontred">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitleSulg"
                        ErrorMessage="请填写文章概要！">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    文章图片：
                </th>
                <td>
                    <uc1:UploadControl ID="UploadControl1" runat="server" IsUploadMore="false" IsUploadSelf="true"
                        FileTypes="*.jpg;*.gif;*.jpeg;*.png;*.doc;*.ppt;*.xls;*.docx;*.pptx;*.xlsx;*.wps;*.pdf" />
                  
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    资讯类别：
                </th>
                <td>
                    <asp:DropDownList ID="ddlClass" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    文章类型：
                </th>
                <td>
                    <asp:DropDownList ID="ddlPublishTarget" runat="server">
                    </asp:DropDownList>
                    <span class="fontred">*</span>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    文章内容：
                </th>
                <td>
                    <asp:TextBox ID="txtContent" runat="server" class="editText" Width="85%"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="Basic_btn fixed">
            <ul>
                <li>
                    <asp:LinkButton ID="linkBtnSave" runat="server" OnClick="linkBtnSave_Click">保 存 >></asp:LinkButton>
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
        var ArticleInfo = {
            InitEdit: function () {
                $("#tableInfo").find(".editText").each(function () {
                    KEditer.init($(this).attr("id"), {
                        resizeMode: 0,
                        items: keSimple_HaveImage,
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

            ArticleInfo.InitEdit();

            $("#<%= linkBtnSave.ClientID %>").click(function () {
                return ArticleInfo.CheckForm();
            });
        });
    </script>
</body>
</html>
