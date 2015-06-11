<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiuShui.aspx.cs" MasterPageFile="~/ShouYin/ShouYin.Master"
    Inherits="Enow.TZB.Web.ShouYin.LiuShui" %>

<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<%@ Register Src="~/ShouYin/ShouYinHuoJia.ascx" TagName="ShouYinHuoJia" TagPrefix="uc1" %>
<%@ Register Src="~/ShouYin/ShouYinTai.ascx" TagName="ShouYinTai" TagPrefix="uc1" %>
<%@ Register Src="~/ShouYin/ShouYinJianPan.ascx" TagName="ShouYinJianPan" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="CPH_ZhuTi" ID="cph_zhuti1" runat="server">
    <div class="warp clearfix">
        <div class="left floatL">
            <uc1:ShouYinHuoJia runat="server" ID="ShouYinHuoJia1"></uc1:ShouYinHuoJia>
        </div>
        <div class="right floatL">
            <div class="listbox borderline">
                <uc1:ShouYinTai runat="server" ID="ShouYinTai1" Display="none"></uc1:ShouYinTai>
                <asp:PlaceHolder runat="server" ID="phOrderDetail" Visible="false">
                    <!--订单流水-->
                    <div class="list_table div_shouyin_qita">
                        <h3>
                            <%=QiuChangName %>-流水明细</h3>
                        <div class="search_box mt10">
                            <form id="chaxun_form" method="get">
                            收银时间：<input name="txtBeginTime" type="text" class="formsize120 input_style" onfocus="WdatePicker()"
                                style="width: 78px;" />
                            -
                            <input name="txtEndTime" type="text" class="formsize120 input_style" onfocus="WdatePicker()"
                                style="width: 78px" /><input name="btnChaXun" type="submit" value="搜 索" class="boxy_btn" />
                            </form>
                        </div>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <th width="40">
                                    流水号
                                </th>
                                <th width="80">
                                    收银项目
                                </th>
                                <th width="30">
                                    数量
                                </th>
                                <th width="50">
                                    收款金额
                                </th>
                                <th width="50">
                                    收款方式
                                </th>
                            </tr>
                            <asp:Repeater ID="rptList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Eval("JiaoYiHao") %>
                                        </td>
                                        <td>
                                            <%#Eval("GoodsName") %>
                                        </td>
                                        <td>
                                            <%#Eval("State").ToString() == "0" ? Eval("Amount") : "-"+Eval("Amount")%>
                                        </td>
                                        <td>
                                            <%#Eval("State").ToString() == "0" ?Eval("JinE","{0:F2}"):"-"+Eval("JinE","{0:F2}")%>
                                        </td>
                                        <td>
                                            <%#(Enow.TZB.Model.收款类型)(int)Eval("PayType") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                              <asp:PlaceHolder runat="server" ID="phEmptyDetail" Visible="false">
                        <tr>
                        <td colspan="7">暂无订单流水信息</td>
                        </tr>
                        </asp:PlaceHolder>
                        </table>
                    </div>
                    <div class="page div_shouyin_qita" id="div_fenye">
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
        $(document).ready(function () {
            var fenYePeiZhi = { pageSize: 6, pageIndex: 1, recordCount: 1, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page' };
            fenYePeiZhi.pageSize = parseInt("<%=PageSize %>");
            fenYePeiZhi.pageIndex = parseInt("<%=PageIndex %>");
            fenYePeiZhi.recordCount = parseInt("<%=RecordCount %>");

            if (fenYePeiZhi.recordCount > 0) AjaxPageControls.replace("div_fenye", fenYePeiZhi);
        })
    </script>
</asp:Content>
