<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxApplicantsList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxApplicantsList" %>
 <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                         <li>
                            <div class="juhui-title"><%#Eval("ContactName")%></div>
                            <div class="juhui-box">
                                <div class="btn">
                                   <a href="javascript:void(0);" class="basic_gbtn">已报名</a>
                                </div>
                                <p class="font_gray">报名时间：<%#Eval("Indatetime")%></p>
                                <p>状态：<%#(Enow.TZB.Model.EnumType.ApplicantsStartEnum)(Enow.TZB.Utility.Utils.GetInt(Eval("IsState").ToString(), 0))%></p>
                            </div>
                         </li>
                         </ItemTemplate>
         </asp:Repeater>

