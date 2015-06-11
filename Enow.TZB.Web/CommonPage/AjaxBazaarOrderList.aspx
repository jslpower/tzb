<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxBazaarOrderList.aspx.cs" Inherits="Enow.TZB.App.CommonPage.AjaxBazaarOrderList" %>
  <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="user-item font14 R_jiantou">
                                <a href="BazaarDetail.aspx?OrderId=<%#Eval("OrderId")%>">
                                    <%#Eval("OrderNo")%></a></div>
                            <div class="u-dindan-item">
                                <img alt="" src="<%#Getimgurl(Eval("OrderId").ToString())%>" />
                                <p class="font_gray">
                                    支付状态:<%#((Enow.TZB.Model.商城订单状态)(Enow.TZB.Utility.Utils.GetInt(Eval("PayStatus").ToString()))).ToString()%><span
                                        style="float: right;"><%#Eval("CreatTime")!=null?Enow.TZB.Utility.Utils.GetDateTime(Eval("CreatTime").ToString(),DateTime.MinValue).ToString("yyyy-MM-dd HH:mm"):DateTime.Now.ToString("yyyy-MM-dd HH:mm")%></span></p>
                                <p>
                                    <%#Getsum(Eval("OrderId").ToString())%></p>
                                <p class="font_gray">
                                    支付方式：<%#(Enow.TZB.Model.商城支付方式)Convert.ToInt32(Eval("PayType"))%>
                                    <asp:Literal ID="litOperation" runat="server"></asp:Literal>
                                </p>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
