<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true"
    CodeBehind="CreateTeam.aspx.cs" Inherits="Enow.TZB.WebForm.Team.CreateTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cph_Left" runat="server">
    <h3>
        一起玩吧</h3>
    <div class="left_nav">
        <ul>
            <li><a href="Default.aspx" class="on">铁丝球队</a></li>
            <li><a href="#" class="wait">铁丝约战</a></li>
            <li><a href="/Match/Default.aspx">杯赛联赛</a></li>
            <li><a href="#" class="wait">铁丝聚会</a></li>
            <li><a href="#" class="wait">足球旅游</a></li>
            <li><a href="#" class="wait">相聚球星</a></li>
            <li><a href="#" class="wait">投票抽奖</a></li>
            <li><a href="#" class="wait">舵主风采</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cph_Body" runat="server">
    <div class="game_box mt10">
        <div class="sideT fixed">
            <h3>
                一起玩吧</h3>
            <div class="Rtit">
                首页 > 一起玩吧 > 铁丝球队 > 创建球队</div>
        </div>
        <div class="reg_form qiudui_form">
            <ul class="fixed">
                <li><span class="name">球队名称</span><asp:TextBox ID="txtTeamName" runat="server" CssClass="input_bk formsize400"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入球队名称。" ControlToValidate="txtTeamName">*请输入球队名称</asp:RequiredFieldValidator></li>
                <li><span class="name">球队照片</span><asp:FileUpload ID="imgFileUpload" runat="server"
                    CssClass="input_bk formsize400" /></li>
                <li class="w50"><span class="name">我的位置</span><select id="SQWZ" name="SQWZ">
                    <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                    (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球员位置),
                                new string[] { }), "")
                    %>
                </select></li>
                <li class="w50"><span class="name">我的球衣号</span><asp:TextBox ID="txtQYHM" runat="server"
                    MaxLength="4" CssClass=" input_bk formsize60"></asp:TextBox><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ErrorMessage="球衣号码只能为数字" ControlToValidate="txtQYHM"
                        ValidationExpression="\d{1,4}">*球衣号码只能为数字</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写球衣号码" ControlToValidate="txtQYHM">*请填写球衣号码</asp:RequiredFieldValidator></li>
                <li><span class="name">球队介绍</span>
                    <textarea class="input_bk" id="TeamInfo" cols="" rows="" name="TeamInfo" style="height: 300px; width: 600px;"></textarea>
                </li>
                <li><span class="name">创始人</span><strong><asp:Literal ID="ltrContactName" runat="server"></asp:Literal></strong></li>
            </ul>
            <div class="reg_btn pb10 mt10" style="text-align:center;">
                <asp:Button ID="btnCreate" runat="server" Text="立即创建" OnClick="btnCreate_Click" /></div>
        </div>
       </div>
       <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
       <script language="javascript" type="text/javascript">
           $(function () {

               KEditer.init("TeamInfo", { resizeMode: 1, items: keSimple, height: "300px" });
           });
       </script>
</asp:Content>
