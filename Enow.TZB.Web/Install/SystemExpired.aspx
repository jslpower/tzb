<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemExpired.aspx.cs" Inherits="Enow.TZB.Web.Install.SystemExpired" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>系统安装配置</title>
		<meta http-equiv="cache-control" content="no-cache,must-revalidate">
		<meta http-equiv="expires" content="-1">
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
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
					<td align="right" bgcolor="#e6f2ff">系统时间注册码：
					</td>
					<td align="left" bgcolor="#ffffff">
						<asp:TextBox id="txtSnNumber" runat="server" Width="248px" CssClass="inputStyle"></asp:TextBox>
					</td>
				</tr>
			</table>
			<table width="80%" border="0" align="center" cellpadding="10" cellspacing="0">
				<tr>
					<td align="center">
						<asp:Button id="btnUpdateConfig" runat="server" Text="更新时间配置"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
