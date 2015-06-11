<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxPatList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxPatList" %>

<asp:repeater id="rptList" runat="server">
                    <ItemTemplate>
         <li>
            <div class="item-img"><img src="<%# Eval("MemberPhoto")%>"/></div>
            <div class="item-box">
                <div class="Rbtn">
                   <a href="RudderView.aspx?JobId=<%# Eval("Id")%>">查看详情</a>
                   <a href="Mesgsendout.aspx?JobId=<%# Eval("MemberId")%>">发送信息</a>
                   <a href="javascript:void(0);" data-jid="<%# Eval("Id")%>" class="Agzbtn">取消关注</a>
                </div>
                <dl>
                    <dt><%# Eval("Jobtyoe")!=null?((Enow.TZB.Model.EnumType.JobType)Eval("Jobtyoe")).ToString():"会员"%>：<%# Eval("ContactName")%></dt>
                    <dd><span class="bg_green"><%# Eval("CSWZ")%></span><span class="bg_green"><%# Eval("CityName")%></span></dd>
                    <dd class="txt"><%#Eval("ApplyInfo").ToString()%></dd>
                </dl>
            </div>
         </li>
        </ItemTemplate>
       </asp:repeater>
