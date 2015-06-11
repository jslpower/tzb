<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxBallFieldList.aspx.cs" Inherits="Enow.TZB.App.CommonPage.AjaxBallFieldList" %>
   <asp:Repeater ID="rptList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="item-img">
                                            <a href="BallFieldDetail.aspx?Id=<%#Eval("Id") %>">
                                                <img src="<%#System.Configuration.ConfigurationManager.AppSettings["Uploadimgurl"]+Eval("FieldPhoto") %>" /></a></div>
                                        <div class="item-box">
                                            <dl>
                                                <dt><a href="BallFieldDetail.aspx?Id=<%#Eval("Id") %>">
                                                    <%#Eval("FieldName")%></a><!--<span class="tiesi_type01"><%#(Enow.TZB.Model.EnumType.CourtEnum)Convert.ToInt32(Eval("TypeId"))%></span>--></dt>
                                                <dd>
                                                    ￥<%#Eval("Price","{0:F2}")%>元/2小时</dd>
                                                <dd>
                                                    营业时间：<%#Eval("Hours")%></dd>
                                                <dd>
                                                    球场大小：<%#Eval("FieldSize")%></dd>
                                            </dl>
                                            <div class="qiu-caozuo">
                                                <a href="BallFieldSignUp.aspx?QID=<%#Eval("Id") %>" class="basic_ybtn">点击预约</a><a
                                                    href="/WX/AboutWar/AboutWarAdd.aspx?QID=<%#Eval("Id") %>" class="basic_rbtn">约战</a></div>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
