<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true"
    CodeBehind="FieldList.aspx.cs" Inherits="Enow.TZB.WebForm.News.FieldList" %>
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
    <div class="tisi_imgbox mt10">
        <div class="sideT fixed">
            <h3>
                铁丝网</h3>
            <div class="Rtit">
                首页 > 铁丝网</div>
        </div>
        <ul>
            <asp:Repeater ID="rpt_FieldList" runat="server">
                <ItemTemplate>
                    <li>
                        <div class="tisi_img">
                            <a href="FieldDetail.aspx?id=<%#Eval("id")%>">
                                <img src="<%#Eval("FieldPhoto") %>" width="142" height="88"></a></div>
                        <dl>
                            <dt><a href="FieldDetail.aspx?id=<%#Eval("id")%>">
                                <%#Eval("FieldName") %></a></dt>
                            <dd class="green">
                                <%#Eval("Hours") %></dd>
                            <dd>
                                球场大小：<%#Eval("FieldSize") %></dd>
                        </dl>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div class="page" id="div_fenye">
        </div>
    </div>
    <script type="text/javascript" src="/Js/fenye.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var fenYePeiZhi = { pageSize: 12, pageIndex: 1, recordCount: 1, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page' };
            fenYePeiZhi.pageSize = parseInt("<%=pageSize %>");
            fenYePeiZhi.pageIndex = parseInt("<%=pageIndex %>");
            fenYePeiZhi.recordCount = parseInt("<%=recordCount %>");

            if (fenYePeiZhi.recordCount > 0) AjaxPageControls.replace("div_fenye", fenYePeiZhi);
        })
    </script>
</asp:Content>
