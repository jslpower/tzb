<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="LotteryAdd.aspx.cs" Inherits="Enow.TZB.Web.Manage.Vote.LotteryAdd" %>
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
    <style type="text/css">
      .yingcang{
       display:none;	
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
 <asp:HiddenField ID="hdfdeleteOpId" runat="server" />
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <span class="firsttableT">抽奖信息</span>            
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                   <tr>
                    <th width="100">
                      类型：
                    </th>
                    <td>
                        <asp:DropDownList ID="droptypes" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" MaxLength="100" CssClass="input-txt formsize240"
                            errmsg="请填写名称!" valid="required"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                      发布至：
                    </th>
                    <td>
                        <asp:DropDownList ID="dropfbmb" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="zttpxq" class="yingcang">
                 <th width="100">
                      主体抽奖信息：
                    </th>
                    <td>
                    <select id="ztlx" name="zttypes">
                    <option value="1">活动</option>
                    <option value="2">赛事</option>
                    </select>
                    活动赛事名称：
                    <asp:DropDownList ID="drophd" CssClass="hdss yingcang" DataTextField="Title" DataValueField="Id" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="dropss" CssClass="hdss yingcang" DataTextField="MatchName" DataValueField="Id" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <th width="100">
                      抽奖选项：
                    </th>
                    <td id="tdtpxx">
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        预计抽奖人数：
                    </th>
                    <td>
                        <asp:TextBox ID="txtyujirenshu" runat="server" MaxLength="100" CssClass="input-txt formsize240"
                            errmsg="请填写预计抽奖人数!" valid="required"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        抽奖截止日期：
                    </th>
                    <td>
                        日期：
                        <asp:TextBox runat="server" ID="txtendtime" onfocus="WdatePicker()"  class="input-txt formsize80" valid="required|isDate" errmsg="请选择投票截止日期!|请选择投票截止日期!" ></asp:TextBox>
                        <span class="fontred">*</span>
                    </td>
                </tr>
                
                <tr>
                    <th>
                        详情：
                    </th>
                    <td>
                        <asp:TextBox ID="txtContent" runat="server" Height="400px" TextMode="MultiLine" Width="85%"></asp:TextBox>
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
            TrIndex: 0,
            voteuslist: <%=voteuslist %>,
            Data: {
                MId: '<%=Request.QueryString["MId"] %>',
                SMId: '<%=Request.QueryString["SMId"] %>',
                CId: '<%=Request.QueryString["CId"] %>',
                id: '<%=Request.QueryString["id"] %>'
            },
            CheckForm: function () {
                var form = $("form").get(0);
                KEditer.sync();
                return ValiDatorForm.validator(form, "alert");
            },
            Save: function () {
                $("#btnSave").text("提交中...");
                KEditer.sync();

                PageJsDataObj.submit();
            },
            submit: function () {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "LotteryAdd.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
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
            deletetrp: function (obj) {
                if (PageJsDataObj.TrIndex > 1) {

                    PageJsDataObj.TrIndex--;
                    var hdfid = $("#<%=hdfdeleteOpId.ClientID%>").val();
                    if (hdfid) {
                        hdfid += "," + $(obj).attr("data-id");
                    }
                    else {
                        hdfid = $(obj).attr("data-id");
                    }
                    $("#<%=hdfdeleteOpId.ClientID%>").val(hdfid);
                    $(obj).closest('p').remove();
                }
            },
            Addtrp: function (Base) {
                PageJsDataObj.TrIndex++;
                var strHtml = '<p><input type="hidden" class="VoteOptionId" name="OptionTrIndex" value="' + PageJsDataObj.TrIndex + '" />';
                strHtml += '奖项名称:';
                strHtml += '<input type="hidden" name="hidOldVoteOptionID" value="' + (Base ? Base.Oid : "") + '" />';
                strHtml += '<input name="OptionName" errmsg="请填写第' + PageJsDataObj.TrIndex + '个奖项名称!" valid="required" type="text" value="' + (Base ? Base.Otitle : "") + '" />';
                strHtml += '奖项数:';
                strHtml += '<input name="OptionNum" errmsg="请填写第' + PageJsDataObj.TrIndex + '个奖项数!" valid="required" type="text" value="' + (Base ? Base.ONumber : "0") + '" />';
                strHtml += '<a href="javascript:void(0);" class="Aadd">+</a><a data-id="' + (Base ? Base.Oid : "") + '" href="javascript:void(0);" class="Adelete">-</a>';
                strHtml += '<span class="fontred">*</span>';
                strHtml += '</p>';
                $("#tdtpxx").append(strHtml);
            },
            initBaseList: function () {
                if (!PageJsDataObj.voteuslist || PageJsDataObj.voteuslist.length <= 0) {
                    PageJsDataObj.Addtrp(null);
                    return;
                }
                for (var i = 0, j = PageJsDataObj.voteuslist.length; i < j; i++) {
                    var tmp = PageJsDataObj.voteuslist[i];
                    if (!tmp) continue;
                    PageJsDataObj.Addtrp(tmp);
                }
            },
            Selectopen:function(){
             var opval=$("#<%=droptypes.ClientID %>").val();
             if(opval=="3")
             {
                $("#zttpxq").removeClass("yingcang");
             }
             else {
                 $("#zttpxq").addClass("yingcang");
             }
            },
            RoleSelectopen:function(){
              var openval=$("#ztlx").val();
              $(".hdss").addClass("yingcang");
              if (openval=="1") {
                $("#<%=drophd.ClientID %>").removeClass("yingcang");
              }
              else
              {
                $("#<%=dropss.ClientID %>").removeClass("yingcang");
              }
            },
            BindBtn: function () {
                this.initBaseList();
                PageJsDataObj.Selectopen();
                PageJsDataObj.RoleSelectopen();
                $("#ztlx").val(<%=matchtype %>)
                $("#tdtpxx").find(".Aadd").live("click", function () {
                    PageJsDataObj.Addtrp(null);
                });
                $("#tdtpxx").find(".Adelete").live("click", function () {
                    PageJsDataObj.deletetrp(this);
                });
                $("#btnSave").unbind("click").click(function () {
                    if($("#<%=droptypes.ClientID %>").val()=="3"&&$("#ztlx").val()=="1")
                    {
                      if ($("#<%=drophd.ClientID %>").val()=="0") {
                         tableToolbar._showMsg("活动不能为空！请先添加！");
                         return;
                      }
                    }
                    else if($("#<%=droptypes.ClientID %>").val()=="3"&&$("#ztlx").val()=="2")
                    {
                      if ($("#<%=dropss.ClientID %>").val()=="") {
                         tableToolbar._showMsg("赛事不能为空！请先添加！");
                         return;
                      }
                    }
                    if (PageJsDataObj.CheckForm()) {
                        PageJsDataObj.Save();
                    }
                });
                $("#btnCanel").unbind("click").click(function () {
                    window.history.go(-1);
                    return false;
                });
                $("#<%=droptypes.ClientID %>").change(function() {
                PageJsDataObj.Selectopen();
                });
                $("#ztlx").change(function(){
                  PageJsDataObj.RoleSelectopen();
                });

            }
        }
        $(document).ready(function () {
            PageJsDataObj.BindBtn();
        });
    </script>
</asp:Content>
