<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="TeamEdit.aspx.cs" Inherits="Enow.TZB.Web.Manage.Team.TeamEdit" %>

<%@ Register Src="~/UserControls/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <span class="firsttableT">球队信息修改</span>
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <th width="100">
                        球队名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtTeamName" runat="server" MaxLength="100" CssClass="input-txt formsize450"
                            errmsg="请填写球队名称!" valid="required"></asp:TextBox>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        所在地区：
                    </th>
                    <td>
                        国家:
                        <select id="ddlCountry" name="ddlCountry" valid="required|RegInteger|isNo" novalue="0"
                            errmsg="请选择国家|请选择国家|请选择国家">
                        </select>省份:
                        <select id="ddlProvince" name="ddlProvince" valid="required|RegInteger|isNo" novalue="0"
                            errmsg="请选择省份|请选择省份|请选择省份">
                        </select>城市:
                        <select id="ddlCity" name="ddlCity" valid="required|RegInteger|isNo" novalue="0"
                            errmsg="请选择城市|请选择城市|请选择城市">
                        </select>区县:
                        <select id="ddlArea" name="ddlArea" valid="required|RegInteger|isNo" novalue="0"
                            errmsg="请选择区县|请选择区县|请选择区县">
                        </select><span class="fontred">*</span>
                </tr>
                <tr>
                    <th width="100">
                        球队创始人：
                    </th>
                    <td>
                        <asp:Label ID="lblMemberName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        创始人电话：
                    </th>
                    <td>
                        <asp:Label ID="lblMobile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        申请日期：
                    </th>
                    <td>
                        <asp:Label ID="lblIssueDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        申请状态：
                    </th>
                    <td>
                        <asp:Label ID="lblState" runat="server"></asp:Label>
                </tr>
                <tr>
                    <th>
                        球队照片：
                    </th>
                    <td>
                        <uc2:UploadControl ID="UploadControl2" runat="server" IsUploadMore="true" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" />
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        球队介绍：
                    </th>
                    <td>
                        <asp:TextBox ID="txtTeamInfo" runat="server" Height="400px" TextMode="MultiLine"
                            Width="85%"></asp:TextBox>
                </tr>
            </table>
        </div>
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
            CreatePlanEdit: function () {
                KEditer.init("<%=txtTeamInfo.ClientID %>", { resizeMode: 1, items: keMore_HaveImage, height: "450px" });
            },
            CheckForm: function () {
                var form = $("form").get(0);
                return ValiDatorForm.validator(form, "alert");

            },
            Form: null,
            Save: function () {
                $("#btnSave").text("提交中...");
                KEditer.sync();
                PageJsDataObj.submit();
            },
            submit: function () {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "TeamEdit.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function (ret) {
                        //ajax回发提示
                        if (ret.result != "0") {
                            tableToolbar._showMsg(ret.msg, function () {
                                if (document.referrer != "" && document.referrer != "undefined")
                                    window.location.href = document.referrer;
                                else
                                    window.location.href = "Default.aspx?" + $.param(PageJsDataObj.Data);
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
                PageJsDataObj.CreatePlanEdit();
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
            pcToobar.init({
                gID: "#ddlCountry",
                pID: "#ddlProvince",
                cID: "#ddlCity",
                xID: "#ddlArea",
                comID: '',
                gSelect: '<%=CId %>',
                pSelect: '<%=PId %>',
                cSelect: '<%=CSId %>',
                xSelect: '<%=AId %>'
            });
            PageJsDataObj.PageInit();
            PageJsDataObj.BindBtn();
        });
    </script>
</asp:Content>
