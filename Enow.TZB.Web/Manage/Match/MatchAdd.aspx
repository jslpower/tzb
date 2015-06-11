<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatchAdd.aspx.cs" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    Inherits="Enow.TZB.Web.Manage.Match.MatchAdd" %>

<%@ Register Src="~/UserControls/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>
    <link rel="stylesheet" type="text/css" href="/Css/jquery.multiselect.css" />
    <link rel="stylesheet" type="text/css" href="/Css/jquery.multiselect.filter.css" />
    <link rel="stylesheet" type="text/css" href="/Css/UI/jquery-ui.min.css" />
    <script src="/Js/UI/jquery.ui.widget.min.js" type="text/javascript"></script>
    <script src="/Js/UI/jquery.effects.core.min.js" type="text/javascript"></script>
    <script src="/Js/UI/jquery.ui.position.min.js" type="text/javascript"></script>
    <script src="/Js/jquery.multiselect.min.js" type="text/javascript"></script>
    <script src="/Js/jquery.multiselect.filter.min.js" type="text/javascript"></script>
    <script src="/Js/jquery.multiselect.zh-cn.js" type="text/javascript"></script>
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <span class="firsttableT">赛事信息</span>            
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
            <tr>
                    <th width="100">
                        赛事类型：
                    </th>
                    <td>
                        <select id="ddlMatchAreaType" name="ddlMatchAreaType">
                            <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.赛事类型),
                                                                                                                              new string[] { }), MatchAreaId)
                            %>
                        </select><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        赛事名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtMatchName" runat="server" MaxLength="100" CssClass="input-txt formsize240"
                            errmsg="请填写赛事名称!" valid="required"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        赛制：
                    </th>
                    <td>
                        <select id="ddlTypeId" name="ddlTypeId">
                            <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.赛制),
                                                                                              new string[] { }), TypeIdV)
                            %>
                        </select><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        地区：
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
                        同城报名：
                    </th>
                    <td>
                        <asp:RadioButtonList ID="radLimit" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">不限制</asp:ListItem>
                            <asp:ListItem Value="1">限制</asp:ListItem>
                        </asp:RadioButtonList>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        报名起止日期：
                    </th>
                    <td>
                        报名开始日期：
                        <asp:TextBox ID="txtSignBegin" onfocus="WdatePicker()" runat="server" CssClass="input-txt formsize80"
                            valid="required|isDate" errmsg="请选择报名开始日期!|请选择报名开始日期!"></asp:TextBox>
                        <span class="fontred">*</span> &nbsp;&nbsp;&nbsp;报名截止日期：<asp:TextBox ID="txtSignEnd"
                            runat="server" onfocus="WdatePicker()" valid="required|isDate" errmsg="请选择报名结束日期!|请选择报名结束日期!"
                            CssClass="input-txt formsize80"></asp:TextBox>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        赛事起止日期：
                    </th>
                    <td>
                        开始日期：
                        <asp:TextBox ID="txtStartDate" onfocus="WdatePicker()" runat="server" CssClass="input-txt formsize80"
                            valid="required|isDate" errmsg="请选择开始日期!|请选择开始日期!"></asp:TextBox>
                        <span class="fontred">*</span> &nbsp;&nbsp;&nbsp;结束日期：<asp:TextBox ID="txtEndDate"
                            runat="server" onfocus="WdatePicker()" valid="required|isDate" errmsg="请选择结束日期!|请选择结束日期!"
                            CssClass="input-txt formsize80"></asp:TextBox>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        参赛球场：
                    </th>
                    <td>
                        <select name="ddlField" id="ddlField" multiple="multiple" size="20" style="margin: -1px;">
                            <%=FiledSelectStr%>
                        </select>最大球队数：<asp:TextBox ID="txtMaxTeamNumber" runat="server" MaxLength="6" CssClass="input-txt formsize30"
                            errmsg="请填写球队数量|球队数量只能为整数!" valid="required|isInt" Width="54px"></asp:TextBox>支
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        赛事保证金：
                    </th>
                    <td>
                        <asp:TextBox ID="txtRegistrationFee" runat="server" MaxLength="6" CssClass="input-txt formsize80"
                            errmsg="请填写赛事保证金" valid="required"></asp:TextBox>元&nbsp;&nbsp;&nbsp;&nbsp;报名费用：<asp:TextBox ID="txtEarnestMoney" runat="server" MaxLength="6" CssClass="input-txt formsize80"
                            errmsg="请填写报名费用" valid="required">0</asp:TextBox>元<span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        主 办 方：
                    </th>
                    <td>
                        <asp:TextBox ID="txtMasterOrganizer" runat="server" MaxLength="100" CssClass="input-txt formsize600"
                            errmsg="请填写主办方信息!" valid="required"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        协 办 方：
                    </th>
                    <td>
                        <asp:TextBox ID="txtCoOrganizers" runat="server" MaxLength="100" CssClass="input-txt formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        承 办 方：
                    </th>
                    <td>
                        <asp:TextBox ID="txtOrganizer" runat="server" MaxLength="100" CssClass="input-txt formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        赞 助 方：
                    </th>
                    <td>
                        <asp:TextBox ID="txtSponsors" runat="server" MaxLength="100" CssClass="input-txt formsize600"
                           ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        参赛球队数量：
                    </th>
                    <td>
                        <asp:TextBox ID="txtTeamNumber" runat="server" MaxLength="6" CssClass="input-txt formsize30"
                            errmsg="请填写球队数量|球队数量只能为整数!" valid="required|isInt" Width="54px"></asp:TextBox>支<span
                                class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        报名人数限制：
                    </th>
                    <td>
                        最低名额:
                        <asp:TextBox ID="txtPlayersMin" runat="server" MaxLength="6" CssClass="input-txt formsize30"
                            errmsg="请填写最少报名名额|报名名额只能为整数!" valid="required|isInt" Width="54px"></asp:TextBox>人<span
                                class="fontred">*</span>&nbsp;&nbsp;&nbsp;最高名额:
                        <asp:TextBox ID="txtPlayersMax" runat="server" MaxLength="6" CssClass="input-txt formsize30"
                            errmsg="请填写最少报名名额|报名名额只能为整数!" valid="required|isInt" Width="54px"></asp:TextBox>人<span
                                class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        足球宝贝限制：
                    </th>
                    <td>
                        最低名额:
                        <asp:TextBox ID="txtBayMin" runat="server" MaxLength="6" CssClass="input-txt formsize30"
                            errmsg="请填写最少报名名额|报名名额只能为整数!" valid="required|isInt" Width="54px"></asp:TextBox>人<span
                                class="fontred">*</span>&nbsp;&nbsp;&nbsp;最高名额:
                        <asp:TextBox ID="txtBayMax" runat="server" MaxLength="6" CssClass="input-txt formsize30"
                            errmsg="请填写最少报名名额|报名名额只能为整数!" valid="required|isInt" Width="54px"></asp:TextBox>人<span
                                class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        比赛时间：
                    </th>
                    <td>
                        全场时间长度:
                        <asp:TextBox ID="txtTotalTime" runat="server" MaxLength="6" CssClass="input-txt formsize30"
                            errmsg="请填写全场比赛时间长度|比赛时间长度只能为整数!" valid="required|isInt" Width="54px"></asp:TextBox>分钟<span
                                class="fontred">*</span>&nbsp;&nbsp;&nbsp;中场休息时长:
                        <asp:TextBox ID="txtBreakTime" runat="server" MaxLength="6" CssClass="input-txt formsize30"
                            errmsg="请填写中场休息时间|中场休息时间只能为整数!" valid="required|isInt" Width="54px"></asp:TextBox>分钟<span
                                class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        年龄限制：
                    </th>
                    <td>
                        最小年龄:
                        <asp:TextBox ID="txtMinAge" runat="server" MaxLength="6" CssClass="input-txt formsize30"
                            errmsg="请填写最小年龄|年龄只能为整数!" valid="required|isInt" Width="54px"></asp:TextBox>岁<span
                                class="fontred">*</span>&nbsp;&nbsp;&nbsp;最大年龄:
                        <asp:TextBox ID="txtMaxAge" runat="server" MaxLength="6" CssClass="input-txt formsize30"
                            errmsg="请填写最大年龄|最大只能为整数!" valid="required|isInt" Width="54px"></asp:TextBox>岁<span
                                class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        赛事形象图：
                    </th>
                    <td>
                        <uc2:UploadControl ID="UploadControl1" runat="server" IsUploadMore="false" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png;*.doc;*.ppt;*.xls;*.docx;*.pptx;*.xlsx;*.wps;*.pdf" />
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        赛事规程：
                    </th>
                    <td>
                        <asp:TextBox ID="txtRemark" runat="server" Height="400px" TextMode="MultiLine" Width="85%"></asp:TextBox>
                    </td>
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
                KEditer.init("<%=txtRemark.ClientID %>", { resizeMode: 1, items: keMore_HaveImage, height: "450px" });
            },
            GetFieldList: function (CityId) {
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/Ashx/GetFieldListByCity.ashx?CityId=" + CityId,
                    dataType: "json",
                    success: function (ret) {
                        //ajax回发提示
                        if (ret.list.length > 0) {
                            var str = "<optgroup label=\"请选择球场\">";
                            for (var i = 0; i < ret.list.length; i++) {
                                str = str + "<option value=\"" + ret.list[i].Id + "\">" + ret.list[i].FieldName + "</option>";
                            }
                            str = str + "</optgroup>";
                            $("#ddlField").html("").html(str);
                            $("#ddlField").multiselect('refresh');
                        }
                    },
                    error: function () {
                        alert("连接服务器失败!");
                    }
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
                    url: "MatchAdd.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function (ret) {
                        //ajax回发提示
                        if (ret.result != "0") {
                            tableToolbar._showMsg(ret.msg, function () {
                                if (document.referrer != "" && document.referrer != "undefined")
                                    window.location.href = document.referrer;
                                else
                                    window.location.href = "CourtDefault.aspx?" + $.param(PageJsDataObj.Data);
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
                $("#ddlCity").change(function () {
                    PageJsDataObj.GetFieldList($(this).val());
                })
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
            $("#ddlField").multiselect();
            PageJsDataObj.PageInit();
            PageJsDataObj.BindBtn();
        });
    </script>
</asp:Content>
