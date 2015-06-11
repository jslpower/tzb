<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true"
    CodeBehind="FieldDetail.aspx.cs" Inherits="Enow.TZB.WebForm.News.FieldDetail" %>
    <asp:Content ID="Content2" ContentPlaceHolderID="Cph_Left" runat="server">
        <h3>铁丝网</h3>
    <div class="left_nav">
      <ul>
        <li><a href="FieldList.aspx?HmId=3" class="on">球场</a></li>
        <li><a href="#">餐厅</a></li>
        <li><a href="#">酒吧</a></li>
      </ul>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Cph_Body" runat="server">
    <div class="news_box">
        <div class="sideT fixed">
            <h3>
                铁丝网</h3>
            <div class="Rtit">
                首页 > 铁丝网</div>
        </div>
        <div class="news_cont">
            <div class="news_title cent">
                <asp:Label ID="lblFieldName" runat="server"></asp:Label></div>
            <p class="cent">
                <asp:Literal ID="ltrImg" runat="server"></asp:Literal></p>
            <div class="tisi_cont botline pb10 pt10">
                <ul>
                    <li><strong>铁丝特惠价：</strong>¥<asp:Literal ID="ltrPrice" runat="server"></asp:Literal></li>
                    <li><strong>市场价：</strong>¥<asp:Literal ID="ltrMarkPrice" runat="server"></asp:Literal></li>
                    <li><strong>球场数量：</strong><asp:Literal ID="ltrNumber" runat="server"></asp:Literal></li>
                    <li><strong>营业时间：</strong><asp:Literal ID="ltrHour" runat="server"></asp:Literal></li>
                    <li><strong>球场大小：</strong><asp:Literal ID="ltrSize" runat="server"></asp:Literal>
                    </li>
                </ul>
            </div>
            <p class="pt10">
                <strong>地址：</strong><asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
            </p>
            <p class="pt10">
                <strong>电话：</strong><asp:Literal ID="ltrContactTel" runat="server"></asp:Literal>
            </p>
            <p class="pb10 pt10">
                <strong>球场介绍</strong>
            </p>
            <p>
                <asp:Literal ID="ltrRemark" runat="server"></asp:Literal></p>
            <p>
                <asp:Literal ID="ltrImgList" runat="server"></asp:Literal>
            </p>
        </div>
    </div>
</asp:Content>
