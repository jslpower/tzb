<%@ Page Title="" Language="C#" MasterPageFile="~/ShouYin/ShouYin.Master" AutoEventWireup="true" CodeBehind="MemberUpdate.aspx.cs" Inherits="Enow.TZB.Web.ShouYin.MemberUpdate" %>
<asp:Content ContentPlaceHolderID="CPH_ZhuTi" ID="cph_zhuti1" runat="server">
<form id="form1" runat="server">
    <div class="warp clearfix">
        <table width="450" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <th width="150" height="40" align="right">用户名：</th>
            <td>
                <asp:Literal ID="ltrUserName" runat="server"></asp:Literal>
              </td>
          </tr>          
          <tr>
            <th width="150" height="40" align="right">姓名：</th>
            <td>
                <asp:Literal ID="ltrContactName" runat="server"></asp:Literal>
              </td>
          </tr>
          <tr>
                <th width="150" height="40" align="right">
                    联系电话：
                </th>
                <td>
                    <asp:TextBox ID="txtContactTel" CssClass="input-txt formsize180" runat="server"></asp:TextBox><span
                        class="fontred">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactTel"
                        ErrorMessage="*">请填写联系电话</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
            <th width="150" height="40" align="right">登陆密码：</th>
            <td>
                <asp:TextBox ID="txtOldPassword" MaxLength="50" CssClass="input-txt formsize180" 
                    runat="server" TextMode="Password"></asp:TextBox>如不修改请置空</td>
          </tr>
          <tr>
            <th width="150" height="40" align="right">新登陆密码：</th>
            <td>
                <asp:TextBox ID="txtPwd" MaxLength="50" CssClass="input-txt formsize180" 
                    runat="server" TextMode="Password"></asp:TextBox>不修改请置空</td>
          </tr>
          <tr>
            <th width="150" height="40" align="right">重复新登陆密码：</th>
            <td>
                <asp:TextBox ID="txtPwd2" MaxLength="50" CssClass="input-txt formsize180" 
                    runat="server" TextMode="Password"></asp:TextBox>
              </td>
          </tr>
          <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" CssClass="Basic_btn" runat="server" Text="保 存" 
                    onclick="btnSave_Click" />
              </td>
          </tr>
          </table>
    </div>
    </form>
</asp:Content>