<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PAE.WebUI._Default" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Picasa Web Album Exporter</title>
</head>
<body>
	<form id="PaeForm" runat="server">
	<div>
		<table width="800px">
			<tbody>
				<tr>
					<td style="width:140px;">Username or Email:</td>
					<td>
						<asp:TextBox ID="UsernameTextBox" runat="server" Width="99%"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:CheckBox ID="IncludePrivateCheckBox" runat="server"  AutoPostBack="True" 
							OnCheckedChanged="IncludePrivateCheckBox_CheckedChanged" />
						<asp:TextBox ID="PasswordTextBox" runat="server" ToolTip="Password" TextMode="Password" Width="200px">
						</asp:TextBox>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Button ID="GetAlbumsButton" runat="server" Text="Get Albums" 
							OnClick="GetAlbumsButton_Click" />
					</td>
				</tr>
				<tr>
					<td>Select Album:</td>
					<td>
						<asp:DropDownList ID="AlbumDropDownList" runat="server" Width="99%">
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>Photo Template:</td>
					<td>
						<asp:TextBox ID="TemplateTextBox" runat="server" Width="99%" Height="100px"
							TextMode="MultiLine"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>Fit Preview Within:</td>
					<td>
						<asp:TextBox ID="WidthTextBox" runat="server" Width="50px" style="text-align: right;"></asp:TextBox>
						&nbsp;&nbsp;x&nbsp;&nbsp;
						<asp:TextBox ID="HeightTextBox" runat="server" Width="50px" style="text-align: right;"></asp:TextBox>
						&nbsp;&nbsp;Pixels
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Button ID="ExportButton" runat="server" Text="Export" 
							OnClick="ExportButton_Click" />
					</td>
				</tr>
				<tr>
					<td>Exported:</td>
					<td>
						<asp:TextBox ID="ResultTextBox" runat="server" Width="99%" Height="100px"
							TextMode="MultiLine"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Button ID="PreviewButton" runat="server" Text="Preview" 
							OnClick="PreviewButton_Click" />
					</td>
				</tr>
			</tbody>
		</table>
	</div>
	
	<div>
		<asp:Literal ID="PreviewLiteral" runat="server" />
	</div>
	</form>
</body>
</html>
