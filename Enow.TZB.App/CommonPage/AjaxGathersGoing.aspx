<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxGathersGoing.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxGathersGoing" %>
                      <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>

                         <li>
                            <div class="juhui-title"><span class="green">约战进行中</span><em><%#Eval("title")%></em></div>
                            <div class="juhui-box">
                                <div class="btn">
                                    <%#dzqx?Eval("MainID").ToString() == TeamId ? Eval("AboutState").ToString() == ((int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.进行中).ToString() ? "<a href=\"GathersResult.aspx?AID=" + Eval("Aid") + "\" class=\"basic_btn\">填写战报</a>":"" : "" : ""%>
                                   
                                </div>
                                <p>主队：<%#Eval("MainName")%></p>
                                <p>客队：<%#Eval("GuestName")%></p>
                                <p class="font_gray">时间：<%#Eval("AboutTime", "{0:yyyy-MM-dd}")%></p>
                                <p>地点：<%#Eval("Address")%></p>
                                <p>球场：<%#Eval("CourtName")%></p>
                                <p class="font_gray"><span class="paddR10">赛制：<%#Eval("Format").ToString()!="100"?Eval("Format")+"人":"其他"%></span> 费用：<%#(Enow.TZB.Model.EnumType.GathersEnum.比赛费用)Convert.ToInt32(Eval("Costnum"))%></p>
                            </div>
                         </li>
                         
                          </ItemTemplate>
                       </asp:Repeater>