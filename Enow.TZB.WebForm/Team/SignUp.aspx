<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true"
    CodeBehind="SignUp.aspx.cs" Inherits="Enow.TZB.WebForm.Team.SignUp" %>

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
    <div class="news_box">
        <div class="sideT fixed">
            <h3>
                一起玩吧</h3>
            <div class="Rtit">
                首页 > 一起玩吧 > 铁丝球队 > 加入球队</div>
        </div>
        <div class="news_cont">
            <div class="news_title cent">
                <asp:Literal ID="ltrTeamName" runat="server"></asp:Literal></div>
            <div class="cent">
                创建于：<asp:Literal ID="ltrTimeAndArea" runat="server"></asp:Literal></div>
            <div class="cent">
                <asp:Literal ID="ltrImg" runat="server"></asp:Literal></div>
            <div class="mt10">
                <p class="pb10">
                    <strong>球队介绍</strong>
                </p>
                <p>
                    <asp:Literal ID="ltrTeamInfo" runat="server"></asp:Literal>
                </p>
            </div>
            <asp:PlaceHolder ID="phJoin" runat="server" Visible="false">
            <div class="game_list">
            <ul>
            <li>
            <div class="Rbox cent">
             <div class="Rbtn"><a href="CreateTeam.aspx" class="yellowbg">
                                        登陆创建球队</a>
            </div>
            <div class="Rbtn" style="padding:0 100px 0 0px">
                 <a href="/" class="greenbg">登陆加入球队</a>
            </div>
            </div></li></ul></div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="PhSignUp" runat="server">
                <div class="reg_form qiudui_form mt20 pb10" style="border: #dfdfdf solid 1px;">
                    <ul class="fixed" style="padding-left: 0;">
                        <li class="w50"><span class="name">申请类型</span><select id="hidType" name="hidType">
                            <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球员角色),
                                new string[] {"1","4","5" }), "")
                            %>
                        </select></li>
                        <li class="w50"><span class="name">申请位置</span><select id="SQWZ" name="SQWZ">
                            <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                    (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球员位置),
                                new string[] { }), "")
                            %>
                        </select></li>
                        <li class="w50"><span class="name">申请球衣号码</span><asp:TextBox ID="txtQYHM" runat="server" MaxLength="4"
                            CssClass="input_bk formsize60"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                runat="server" ErrorMessage="球衣号码只能为数字" ControlToValidate="txtQYHM" ValidationExpression="\d{1,4}">*球衣号码只能为数字</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写球衣号码" ControlToValidate="txtQYHM">*请填写球衣号码</asp:RequiredFieldValidator></li>
                        <li><span class="name">加入优势</span>
                            <textarea class="input_bk" rows="" cols="" style="height: 200px; width: 500px;"></textarea>
                        </li>
                    </ul>
                  
                    <div class="reg_btn pb10 cent">
                        <asp:Button ID="btnAdd" runat="server" Text="加入球队" OnClick="btnAdd_Click" /></div>
                </div>
            </asp:PlaceHolder>
            
        </div>
    </div>
</asp:Content>
