<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PAE.WebUI._Default" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Picasa Web Album Exporter</title>
	<meta name="description" content="Export Google Picasa albums to HTML with Captions" />
	<meta name="keywords" content="google,picasa,export,html,album,captions" />
	<meta name="google-site-verification" content="35Bsp2IMv-W4wvfWtLeBlHdy0vZ1u5tODJv450rfEaQ" />

	<style type="text/css">
		
		.paeTextArea
		{
			width: 650px;
			max-width: 650px;
			height: 100px;
		}
		
	</style>
</head>
<body>
	<form id="PaeForm" runat="server">
	<div style="padding-left: 140px; margin-bottom: 50px;">
		<h1 style="margin-left: 10px;">Picasa Web Album Exporter</h1>
		<br />
		<ul style="color: Gray;">
			<li>Export Google Picasa web albums as <b>HTML</b>, <b>BBCode</b>, etc.</li>
			<li>Export <b>unlisted</b> albums (with a password)</li>
			<li>Choose desired image size</li>
			<li>Include photo <b>captions</b>, <b>full-size</b> images and other info</li>
			<li>Embed in your blog (WordPress, LiveJournal, etc.)</li>
			<li>Instantly preview exported HTML</li>
		</ul>
	</div>
	<div>
		<table width="800px" style="margin-top: 20px;">
			<tbody>
				<tr>
					<td style="width:140px;">Username or email:</td>
					<td>
						<asp:TextBox ID="UsernameTextBox" runat="server" Width="180px"></asp:TextBox>
						<asp:CheckBox ID="IncludePrivateCheckBox" runat="server"  AutoPostBack="True" 
							OnCheckedChanged="IncludePrivateCheckBox_CheckedChanged" />
						<asp:TextBox ID="PasswordTextBox" runat="server" ToolTip="Password" TextMode="Password" Width="180px">
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
					<td>Select album:</td>
					<td>
						<asp:DropDownList ID="AlbumDropDownList" runat="server" Width="99%">
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>Photo template:</td>
					<td>
						<asp:TextBox ID="TemplateTextBox" runat="server" CssClass="paeTextArea"
							TextMode="MultiLine"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>Fit preview within:</td>
					<td>
						<asp:TextBox ID="WidthTextBox" runat="server" Width="50px" style="text-align: right;"></asp:TextBox>
						&nbsp;&nbsp;x&nbsp;&nbsp;
						<asp:TextBox ID="HeightTextBox" runat="server" Width="50px" style="text-align: right;"></asp:TextBox>
						&nbsp;&nbsp;pixels
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
						<asp:TextBox ID="ResultTextBox" runat="server" CssClass="paeTextArea"
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
				<tr>
					<td></td>
					<td>
						<asp:Label ID="MessageLabel" runat="server">
						</asp:Label>
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
