<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxUsAwList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxUsAwList" %>
  <asp:Repeater ID="rptList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="juhui-title">
                                            <%#Getyztitle(Eval("TeamId").ToString())%><em><%#Eval("title")%></em></div>
                                        <div class="juhui-box">
                                            <div class="btn" data-aid="<%#Eval("Aid")%>" data-wid="<%#Eval("Wid")%>">
                                            <%#Gettzqx(Eval("Wtypes").ToString(), Eval("TeamId").ToString(), Eval("AboutState").ToString(), Eval("Wstates").ToString())%>
                                            </div>
                                            <p>
                                                主队：<%#Eval("MainName")%></p>
                                            <p>
                                                客队：<%#Eval("GuestName").ToString() != "" ? Eval("GuestName").ToString() : Eval("Wtypes").ToString() == "1" ? (string.IsNullOrEmpty(Eval("GuestName").ToString()) ? "待确认" : "") : Eval("TeamName").ToString()%></p>
                                            <p class="font_gray">
                                                时间：<%#Eval("AboutTime", "{0:yyyy-MM-dd}")%></p>
                                            <p>
                                                地点：<%#Eval("Address")%></p>
                                            <p>
                                                球场：<%#Eval("CourtName")%></p>
                                            <p class="font_gray">
                                                <span class="paddR10">赛制：<%#Eval("Format").ToString()!="100"?Eval("Format")+"人":"其他"%></span>
                                                费用：<%#(Enow.TZB.Model.EnumType.GathersEnum.比赛费用)Convert.ToInt32(Eval("Costnum"))%></p>
                                            <%#GetAState(Eval("AboutState").ToString(), Eval("Wtypes").ToString(), Eval("Wstates").ToString())%>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>