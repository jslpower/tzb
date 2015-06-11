<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ajaxaboutwarlist.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.Ajaxaboutwarlist" %>
  <asp:Repeater ID="rptList" runat="server" >
                        <ItemTemplate>
                         <li>
                            <div class="juhui-title"><span class="green">发起了约战</span><%#Eval("title")%></div>
                            <div class="juhui-box">
                                <div class="btn btn01">
                                   <a href="AboutWarView.aspx?ID=<%#Eval("Aid") %>" class="basic_btn">查看</a>
                                   <a href="javascript:void(0);" data-aid="<%#Eval("Aid") %>" class="basic_ybtn">挑战</a>
                                </div>
                                <p>主队：<%#Eval("TeamName")%></p>
                                <p class="font_gray">时间：<%#Eval("AboutTime", "{0:yyyy-MM-dd}")%></p>
                                <p>地点：<%#Eval("Address")%></p>
                                <p class="font_gray"><span class="paddR10">赛制：<%#Eval("Format").ToString()!="100"?Eval("Format")+"人":"其他"%></span> 费用：<%#(Enow.TZB.Model.EnumType.GathersEnum.比赛费用)Convert.ToInt32(Eval("Costnum"))%></p>
                            </div>
                         </li>
                       </ItemTemplate>
                       </asp:Repeater>

