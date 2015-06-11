<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="MemberEdit.aspx.cs" Inherits="Enow.TZB.Web.Manage.Memeber.MemberEdit" %>

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
            <span class="firsttableT">用户信息修改</span>
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <th width="100">
                        用户名：
                    </th>
                    <td>
                        <asp:Literal ID="ltrUserName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        昵称：
                    </th>
                    <td>
                        <asp:Literal ID="ltrNickName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        用户头像：
                    </th>
                    <td>
                        <uc2:UploadControl ID="UploadControl1" runat="server" IsUploadMore="false" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" />
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
                        性别：
                    </th>
                    <td>
                        <input name="radGender" type="radio" id="radMan" runat="server" checked="true" value="1" />
                        男
                        <input name="radGender" type="radio" value="0" id="radWoman" runat="server" />
                        女
                       
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        手机号码：
                    </th>
                    <td>
                        <asp:TextBox ID="txtMobile" runat="server" Enabled="false" CssClass="input-txt formsize240" errmsg="请填写手机号码!"
                            valid="required"></asp:TextBox>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        邮箱：
                    </th>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="input-txt formsize240" errmsg="请填写邮箱地址!"
                            valid="required"></asp:TextBox>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        真实姓名：
                    </th>
                    <td>
                        <asp:TextBox ID="txtContactName" runat="server" MaxLength="20" CssClass="input-txt formsize240"
                            errmsg="请填写真实姓名!" valid="required"></asp:TextBox>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        身份证号：
                    </th>
                    <td>
                        <asp:TextBox ID="txtPersonalId" runat="server" CssClass="input-txt formsize240" errmsg="请填写正确的身份证号!"
                            valid="required"></asp:TextBox>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        详细地址：
                    </th>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass=" input-txt formsize450" errmsg="请填写详细地址!"
                            valid="required"></asp:TextBox>
                        <span class="fontred">*</span>
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
                    url: "MemberEdit.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
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
