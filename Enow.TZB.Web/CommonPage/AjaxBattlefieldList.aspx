<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxBattlefieldList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxBattlefieldList" %>
  <asp:Repeater ID="rptList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="juhui-title">
                                            <span class="green">约战进行中</span><em><%#Eval("title")%></em></div>
                                        <div class="juhui-box">
                                            <div class="btn" data-aid="<%#Eval("Aid")%>">
                                                <%#dzqx?Eval("MainID").ToString() != TeamId ? Eval("AboutState").ToString() == ((int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报待确认).ToString() ? "<a href=\"GathersResult.aspx?dotype=sel&AID=" + Eval("Aid") + "\" class=\"basic_btn\">查看战报</a><a href=\"javascript:void(0);\" class=\"basic_btn qrbtn\">确认</a><a href=\"javascript:void(0);\" class=\"basic_ybtn ctbtn\">重写战报</a>" : "" : "":""%>
                                                <%#dzqx?Eval("MainID").ToString() == TeamId ? Eval("AboutState").ToString() == ((int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报重填).ToString() ? "<a href=\"GathersResult.aspx?AID=" + Eval("Aid") + "\" class=\"basic_btn\">填写战报</a>" : "" : "":""%>
                                            </div>
                                            <p>
                                                主队：<%#Eval("MainName")%></p>
                                            <p>
                                                客队：<%#Eval("GuestName")%></p>
                                            <p class="font_gray">
                                                时间：<%#Eval("AboutTime", "{0:yyyy-MM-dd}")%></p>
                                            <p>
                                                地点：<%#Eval("Address")%></p>
                                            <p>
                                                球场：<%#Eval("CourtName")%></p>
                                            <p class="font_gray">
                                                <span class="paddR10">赛制：<%#Eval("Format").ToString()!="100"?Eval("Format")+"人":"其他"%></span>
                                                费用：<%#(Enow.TZB.Model.EnumType.GathersEnum.比赛费用)Convert.ToInt32(Eval("Costnum"))%></p>
                                            <p>
                                                状态：<%#(Enow.TZB.Model.EnumType.GathersEnum.约战状态)Eval("AboutState")%></p>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
