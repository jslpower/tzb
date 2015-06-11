<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Install.aspx.cs" Inherits="Enow.TZB.Web.Install.Install" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>系统安装配置</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta http-equiv="cache-control" content="no-cache,must-revalidate">
		<meta http-equiv="expires" content="-1">
		<style type="text/css">
		BODY { FONT-SIZE: 12px; MARGIN: 0px; COLOR: #333; FONT-FAMILY: "宋体" }
		TD { FONT-SIZE: 12px; COLOR: #333 }
		.inputStyle { BORDER-RIGHT: #bed2e8 1px solid; BORDER-TOP: #bed2e8 1px solid; BORDER-LEFT: #bed2e8 1px solid; BORDER-BOTTOM: #bed2e8 1px solid }
		.BlnFnt { FONT-WEIGHT: bold; FONT-SIZE: 14px; COLOR: #ff0000 }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table width="60%" border="0" align="center" cellpadding="8" cellspacing="1" bgcolor="#d0e2f5"
				style="MARGIN-TOP:30px">
				<tr>
					<td align="right" bgcolor="#e6f2ff">机器序列号：</td>
					<td align="left" bgcolor="#ffffff" class="BlnFnt">
						<asp:Literal id="ltrSerialNumber" runat="server"></asp:Literal></td>
				</tr>
				<tr>
					<td align="right" bgcolor="#e6f2ff">注册码：
					</td>
					<td align="left" bgcolor="#ffffff">
						<asp:TextBox id="txtSnNumber" runat="server" Width="248px" CssClass="inputStyle"></asp:TextBox>
					</td>
				</tr>
                <tr>
					<td align="right" bgcolor="#e6f2ff">注册码2：
					</td>
					<td align="left" bgcolor="#ffffff">
						<asp:TextBox id="txtTimeSN" runat="server" Width="248px" CssClass="inputStyle"></asp:TextBox>
					</td>
				</tr>
			</table>
			<table width="80%" border="0" align="center" cellpadding="10" cellspacing="0">
				<tr>
					<td align="center">
						<asp:Button id="btnUpdateConfig" runat="server" Text="更新配置" 
                            onclick="btnUpdateConfig_Click"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
