<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTeam.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.CreateTeam" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>创建球队</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="创建球队" runat="server" />
<div class="warp">
  <div class="msg_list qiu_box">
      <ul>
                <li><label>球队名称：</label><input name="txtTeamName" runat="server" id="txtTeamName" type="text" class="input_bk formsize150"><asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入球队名称。" 
                        ControlToValidate="txtTeamName">*</asp:RequiredFieldValidator>
                </li>
                <li class="tishi">球队名称填写规则为：城市名+球队名+队。</li>
                <li><label>球队照片：</label><asp:FileUpload ID="imgFileUpload" runat="server" style="width:200px" /></li>
                <li class="tishi">球队照片内容为：球队成员合照或球队队标。非WIFI网络建议图片小于200KB。</li>
                <li><label>创建人：</label><asp:Literal ID="ltrCreateName" runat="server"></asp:Literal></li>
                <li><label>我的场上位置：</label><select id="SQWZ" name="SQWZ">
                           <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                    (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球员位置),
                                new string[] { }), "")
                                %>
                       </select></li>
                <li><label>我的球衣号码：</label><input id="txtQYHM" name="txtQYHM" type="text" runat="server" maxlength="4" class="input_bk formsize50" /><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ErrorMessage="球衣号码只能为数字" 
                        ControlToValidate="txtQYHM" ValidationExpression="\d{1,4}">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写球衣号码" 
                        ControlToValidate="txtQYHM">*</asp:RequiredFieldValidator></li>
         <li class="last"><label>球队介绍：</label><br/><textarea name="txtRemark" cols="" rows=""></textarea></li>
                
       </ul>

       <div class="mt20 padd20">
       <asp:Button CssClass="basic_btn" ID="btnSave" runat="server" Text="确  认" 
                                onclick="linkBtnSave_Click" /></div>

   </div>

</div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
