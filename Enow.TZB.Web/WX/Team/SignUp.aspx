<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.SignUp" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>加入球队</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="<%=ltrTeamName%>" runat="server" />
<div class="warp">

   <div class="qiu_box">
       <div class="qiu-bigimg"><asp:Literal ID="ltrImg" runat="server"></asp:Literal></div>
       <div class="cent font_gray"><span class="mar_R20">创建于：<asp:Literal ID="ltrCreateDate" runat="server"></asp:Literal></span> 所属城市：<asp:Literal ID="ltrCity" runat="server"></asp:Literal></div>
       <div class="qiu-cont">
              <asp:Literal ID="ltrTeamInfo" runat="server"></asp:Literal>
       </div>
   </div>
   
   <div class="msg_list qiu_box mt10">
       <ul>
       <li><label>申请类型：</label><select id="hidType" name="hidType">
                           <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球员角色),
                                new string[] {"1","4","5" }), "")
                                %>
                       </select></li>

                <li><label>申请加入球队名称：</label><asp:Literal ID="ltrTeamName2" runat="server"></asp:Literal></li>
                <li><label>球队创始人：</label><asp:Literal ID="ltrCreateName" runat="server"></asp:Literal></li>
                <li><label>您所在城市：</label><asp:Literal ID="ltrMemeberCityName" runat="server"></asp:Literal></li>
                <li id="liPosition"><label>申请位置：</label><select id="SQWZ" name="SQWZ">
                           <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                    (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球员位置),
                                new string[] { }), "")
                                %>
                       </select></li>
                <li id="liDressNumber"><label>申请球衣号码：</label><input id="txtQYHM" name="txtQYHM" type="text" runat="server" maxlength="4" class="input_bk formsize50" /><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ErrorMessage="球衣号码只能为数字" 
                        ControlToValidate="txtQYHM" ValidationExpression="\d{1,4}">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写球衣号码" 
                        ControlToValidate="txtQYHM">*</asp:RequiredFieldValidator></li>
         <li class="last"><label>加入优势：</label><br/><textarea name="txtRemark" cols="" rows=""></textarea></li>
                
       </ul>

       <div class="mt20 padd20">
           <asp:Button CssClass="basic_btn" ID="Button1" runat="server" Text="加入球队" 
               onclick="Button1_Click" /></div>

   </div>  

</div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    function cGender(Sex) {
        if (Sex == "man") {
            $("#hidType").val("<%=(int)Enow.TZB.Model.EnumType.球员角色.队员 %>");
            $("#imgMan").attr("src", "../images/BOn.png");
            $("#imgWoman").attr("src", "../images/W.png");
        }
        else {
            $("#hidType").val("<%=(int)Enow.TZB.Model.EnumType.球员角色.足球宝贝 %>");
            $("#txtQYHM").val("0");
            $("#liPosition").hide();
            $("#liDressNumber").hide();
            $("#imgMan").attr("src", "../images/B.png");
            $("#imgWoman").attr("src", "../images/WOn.png");
        }
    }
</script>