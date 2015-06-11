<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.Web.ShouYin.Default" MasterPageFile="~/ShouYin/ShouYin.Master"  Title="收银端首页"%>

<%@ Register Src="~/ShouYin/ShouYinHuoJia.ascx" TagName="ShouYinHuoJia" TagPrefix="uc1" %>
<%@ Register Src="~/ShouYin/ShouYinTai.ascx" TagName="ShouYinTai" TagPrefix="uc1" %>
<%@ Register Src="~/ShouYin/ShouYinJianPan.ascx" TagName="ShouYinJianPan" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="CPH_ZhuTi" ID="cph_zhuti1" runat="server">
    <div class="warp clearfix">
        <div class="left floatL">
            <uc1:ShouYinHuoJia runat="server" id="ShouYinHuoJia1"></uc1:ShouYinHuoJia>
        </div>
        <div class="right floatL">
            <div class="listbox borderline">
                <uc1:ShouYinTai runat="server" ID="ShouYinTai1"></uc1:ShouYinTai>
            </div>

            <div class="R_btnbox borderline">
                <uc1:ShouYinJianPan runat="server" ID="ShouYinJIanPan1"></uc1:ShouYinJianPan>
            </div>
        </div>
    </div>
</asp:Content>