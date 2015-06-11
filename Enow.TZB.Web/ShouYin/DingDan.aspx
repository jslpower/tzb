<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DingDan.aspx.cs" MasterPageFile="~/ShouYin/ShouYin.Master" Inherits="Enow.TZB.Web.ShouYin.DingDan" Title="历史订单" %>

<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<%@ Register Src="~/ShouYin/ShouYinHuoJia.ascx" TagName="ShouYinHuoJia" TagPrefix="uc1" %>
<%@ Register Src="~/ShouYin/ShouYinTai.ascx" TagName="ShouYinTai" TagPrefix="uc1" %>
<%@ Register Src="~/ShouYin/ShouYinJianPan.ascx" TagName="ShouYinJianPan" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="CPH_ZhuTi" ID="cph_zhuti1" runat="server">
    <style type="text/css">
    .tuikuan_on1{ background:url(/shouyin/images/tuikuan_icon3.gif) no-repeat;width:32px; height:17px; display:inline-block;}
    </style>
    <div class="warp clearfix">
        <div class="left floatL">
            <uc1:ShouYinHuoJia runat="server" id="ShouYinHuoJia1"></uc1:ShouYinHuoJia>
        </div>
        <div class="right floatL">
            <div class="listbox borderline">
                <uc1:ShouYinTai runat="server" ID="ShouYinTai1" Display="none"></uc1:ShouYinTai>
                
                <asp:PlaceHolder runat="server" ID="phDingDan" Visible="false">
                <!--历史订单-列表-->
                <div class="list_table div_shouyin_qita" id="div_shouyin_dingdan">
                    <h3>
                        <%=QiuChangName %>-历史订单明细</h3>
                    <div class="search_box mt10">
                        <form id="chaxun_form" method="get">
                        流水号：<input name="txtJiaoYiHao" type="text" class="formsize120 input_style" style="width: 78px;" />
                        收银时间：<input name="txtShiJian1" type="text" class="formsize120 input_style" onfocus="WdatePicker()"
                            style="width: 78px;" /> - <input name="txtShiJian2" type="text" class="formsize120 input_style"
                                onfocus="WdatePicker()" style="width: 78px" /><input name="btnChaXun" type="submit"
                                    value="搜 索" class="boxy_btn" />
                        </form>
                    </div>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>
                                收银时间
                            </th>
                            <th>
                                收银人
                            </th>
                            <th>
                                收款类型
                            </th>
                            <th>
                                客户名
                            </th>
                            <th>
                                应收款
                            </th>
                            <th>
                                实收款
                            </th>
                            <th>
                                操作
                            </th>
                        </tr>
                        <asp:Repeater runat="server" ID="rptDingDan"><ItemTemplate>
                        <tr data-dingdanid="<%#Eval("OrderId") %>" data-leixing="<%#Eval("PayType") %>" class="dingdan_item">
                            <td>
                                <%#Eval("OperatorTime","{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                            <td>
                                <%#Eval("OperatorName")%>
                            </td>
                            <td>
                                <%#(Enow.TZB.Model.收款类型)(int)Eval("PayType")%>
                            </td>
                            <td>
                                <%#Eval("CustomerName")%>
                            </td>
                            <td>
                                <%#Eval("JinE","{0:F2}")%>
                            </td>
                            <td>
                                <%#Eval("ActualMoney","{0:F2}")%>
                            </td>
                            <td>
                                <a href="javascript:void(0)" class="dingdan_chakan">查看</a>
                            </td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                        <asp:PlaceHolder runat="server" ID="phEmptyDingDan" Visible="false">
                        <tr>
                        <td colspan="7">暂无订单信息</td>
                        </tr>
                        </asp:PlaceHolder>
                    </table>
                </div>
                <div class="page div_shouyin_qita" id="div_fenye">
                    
                </div>
                </asp:PlaceHolder>
                
                <asp:PlaceHolder runat="server" ID="phDingDanMingXi" Visible="false">
                <!--订单明细-列表-->
                <div class="list_table div_shouyin_qita" id="div_shouyin_dingdanmingxi">
                    <h3><%=DingDanMingXiBiaoTi %></h3><input type="hidden" name="hidPayType" id="hidPayType" value="<%=(int)ShouKuanFangShi %>">
                    <div class="table_head">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <th>
                                    名称
                                </th>
                                <th width="50">
                                    单位
                                </th>
                                <th width="50">
                                    数量
                                </th>
                                <th width="80">
                                    单价
                                </th>
                                <th width="80">
                                    应收款
                                </th>
                                <th width="80">
                                    铁丝价
                                </th>
                                <th width="50">
                                    退款
                                </th>
                            </tr>
                        </table>
                    </div>
                    <div class="table_cont">
                        <table width="100%" cellpadding="0" cellspacing="0" id="table_shouyin_dingdanmingxi">
                            <asp:Repeater runat="server" ID="rptDingDanMingXi">
                            <ItemTemplate>
                            <tr class="dingdanmingxi_item" data-mingxiid="<%#Eval("ID") %>">
                                <td>
                                    <%#Eval("GoodsName") %>
                                </td>
                                <td width="50">
                                    <%#Eval("Unit") %>
                                </td>
                                <td width="50">
                                    <%#Eval("Amount") %>
                                </td>
                                <td width="80">
                                    <%#Eval("Price","{0:F2}")%>
                                </td>
                                <td width="80">
                                    <%#GetYingShouJinE(Eval("Amount"),Eval("Price"),Eval("CPrice"))%>
                                </td>
                                <td width="80">
                                    <%#GetTieSiJinE(Eval("Amount"),Eval("CPrice"))%>
                                </td>
                                <td width="50">
                                    <%#GetTuiKuanTuBiao(Eval("State"),Eval("TypeId"))%>
                                </td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                            <asp:PlaceHolder runat="server" ID="phEmptyDingDanMingXi" Visible="false">
                                <tr>
                                    <td colspan="7">
                                        暂无明细信息
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                        </table>
                    </div>
                </div>
                </asp:PlaceHolder>
            </div>

            <div class="R_btnbox borderline">
                <uc1:ShouYinJianPan runat="server" ID="ShouYinJIanPan1"></uc1:ShouYinJianPan>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="/js/fenye.js"></script>
    <script type="text/javascript">
        var iPage = {
            //订单列表-查看链接事件
            chaKan: function (obj) {
                var _$tr = $(obj).closest("tr");
                var _dingdanid = _$tr.attr("data-dingdanid");
                var _leixing = _$tr.attr("data-leixing");
                window.location.href = "dingdan.aspx?dingdanid=" + _dingdanid + "&fs=dingdanmingxi";
                return false;
            },
            //订单明细-退款链接事件
            tuiKuan: function (obj) {
                var _$obj = $(obj);
                var _$tr = _$obj.closest("tr");
                if (_$obj.attr("data-status") == "yitui") {
                    alert("该商品已退款，不能再退款");
                    return false;
                }
                var _tui = _$obj.attr("data-tui");
                _$obj.removeClass("tuikuan_on1").removeClass("tuikuan");
                if (_tui == "0") {
                    _$obj.attr("data-tui", "1");
                    _$obj.addClass("tuikuan_on1");
                }
                if (_tui == "1") {
                    _$obj.attr("data-tui", "0");
                    _$obj.addClass("tuikuan");
                }

            }
        }

        $(document).ready(function () {
            top.shouYin.setT("B");
            var fenYePeiZhi = { pageSize: 6, pageIndex: 1, recordCount: 1, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page' };
            fenYePeiZhi.pageSize = parseInt("<%=PageSize %>");
            fenYePeiZhi.pageIndex = parseInt("<%=PageIndex %>");
            fenYePeiZhi.recordCount = parseInt("<%=RecordCount %>");

            if (fenYePeiZhi.recordCount > 0) AjaxPageControls.replace("div_fenye", fenYePeiZhi);

            $(".dingdan_chakan").click(function () { iPage.chaKan(this); });

            $(".dingdanmingxi_item").find("a[data-class='tuikuan']").click(function () { iPage.tuiKuan(this); })
        })
    </script>
</asp:Content>
