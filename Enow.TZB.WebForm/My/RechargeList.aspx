<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master"
    AutoEventWireup="true" CodeBehind="RechargeList.aspx.cs" Inherits="Enow.TZB.WebForm.My.RechargeList" %>

<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <div class="user_box">
        <h3>
            充值记录</h3>
        <div class="chongzhi_box">
            <div class="user-search">
                <ul class="fixed">
                    <p>
                        &nbsp;&nbsp;充值起始日期：
                        <input type="text" onfocus="WdatePicker()" name="txtStartTime" id="txtStartTime"
                            value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("begindate")%>' size="10" />-<input
                                type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtStartTime\')}'})" name="txtEndTime"
                                id="txtEndTime" size="10" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("enddate")%>' />
                        &nbsp;&nbsp;充值类型:<select id="selType" name="State">
                            <option value="-1">请选择</option>
                            <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.财务流水类型),
                                                                                              new string[] { }), Enow.TZB.Utility.Utils.GetInt(Request.QueryString["TypeId"], -1))
                            %></select>
                        &nbsp;&nbsp;
                        <input type="button" id="search-btn" name="search-btn" value="搜 索" class="line_sbtn" />
                    </p>
                </ul>
            </div>
            <div class="chongzhi_table">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th style="width: 10%;">
                            序号
                        </th>
                        <th style="width: 10%;">
                            充值日期
                        </th>
                        <th style="width: 10%;">
                            充值类型
                        </th>
                        <th style="width: 20%;">
                            金额
                        </th>
                        <th style="width: 50%;">
                            备注
                        </th>
                    </tr>
                        <asp:Repeater ID="rptRechargeList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Container.ItemIndex+1 %>
                                    </td>
                                    <td>
                                        <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td>
                                        <%#(Enow.TZB.Model.EnumType.财务流水类型)int.Parse(Eval("typeId").ToString())%>
                                    </td>
                                    <td>
                                        <%#BillOperation((Enow.TZB.Model.EnumType.财务流水类型)Convert.ToInt32(Eval("TypeId")))%><%#Eval("TradeMoney", "{0:F2}")%>
                                    </td>
                                    <td>
                                        <%#Eval("Remark")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                </table>
            </div>
            <br />
            <div class="pages" style="float: right;">
                <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        $(function () {

            $("#search-btn").bind('click', function () {
                var begindate = $("#txtStartTime").val();
                var enddate = $("#txtEndTime").val();
                var TypeId = $("#selType").val();

                window.location.href = "RechargeList.aspx?TypeId=" + TypeId + "&begindate=" + begindate + "&enddate=" + enddate + "";
            });
        });
    </script>
</asp:Content>
