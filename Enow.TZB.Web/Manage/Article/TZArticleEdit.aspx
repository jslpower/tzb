<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TZArticleEdit.aspx.cs" Inherits="Enow.TZB.Web.Manage.Article.TZArticleEdit" %>
<%@ Register Src="/UserControls/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" content="IE=EmulateIE8" http-equiv="X-UA-Compatible" />
    <title>舵主日志</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
 
    <link href="/Css/swfupload/default.css" type="text/css" />
    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">

<%--     <div class="clear">
    </div>--%>
  
    <div class="contentbox">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tableInfo">
            <tr>
                <th width="150" height="40" align="right">
                    日志标题：
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
                    日志图片：
                </th>
                <td>
                    <uc1:UploadControl ID="UploadPhoto" runat="server" IsUploadMore="false" IsUploadSelf="true"
                        FileTypes="*.jpg;*.gif;*.jpeg;*.png;*.doc;*.ppt;*.xls;*.docx;*.pptx;*.xlsx;*.wps;*.pdf" />
                  
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    日志内容：
                </th>
                <td>
                   <asp:TextBox ID="txtContent" runat="server" class="editText" width="85%"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="Basic_btn fixed">
            <ul>
                <li>
                    <asp:LinkButton ID="linkBtnSave" runat="server" onclick="linkBtnSave_Click">保 存 >></asp:LinkButton>
                </li>
                <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"
                    hidefocus="true">返 回 >></a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
