<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PAE.WebUI._Default" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Picasa Web Album Exporter</title>
	<meta name="description" content="Export Google Picasa albums to HTML with Captions" />
	<meta name="keywords" content="google,picasa,export,html,album,captions" />
	<meta name="google-site-verification" content="35Bsp2IMv-W4wvfWtLeBlHdy0vZ1u5tODJv450rfEaQ" />
	
	<style type="text/css">
		
		h1
		{
			color: #666666;
		}
		
		.labelSection
		{
			width: 140px;
			color: Gray;
		}

		.credentialTextBox
		{
			width: 150px;
		}
		
		.paeTextArea
		{
			width: 605px;
			max-width: 605px;
			height: 100px;
		}
		
		.paeNumericTextBox
		{
			width: 50px;
			text-align: right;
		}
		
		.helpSection
		{
			width: 300px;
			padding-left: 25px;
			font-family: Arial; 
			font-size: small;
			color: #4682B4;
		}

		.contactSection
		{
			font-size: normal !important;
			font-weight: bold !important;
			color: #666666 !important;
		}
		
	</style>
	
	<script type="text/javascript">
		
		function InsertPlaceholder( text )
		{
			var el = document.getElementById('<%= this.TemplateTextBox.ClientID %>');
			var val = el.value, endIndex, range;
    		
			if (typeof el.selectionStart != "undefined" && typeof el.selectionEnd != "undefined") 
			{
				endIndex = el.selectionEnd;
				el.value = val.slice(0, endIndex) + text + val.slice(endIndex);
				el.selectionStart = el.selectionEnd = endIndex + text.length;
			} 
			else if (typeof document.selection != "undefined" && typeof document.selection.createRange != "undefined") 
			{
				el.focus();
				range = document.selection.createRange();
				range.collapse(false);
				range.text = text;
				range.select();
			}
		}
		
	</script>
</head>
<body>
	<form id="PaeForm" runat="server">
	<div style="padding-left: 140px; margin-bottom: 40px;">
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
		<table width="1060px" cellspacing="7px">
			<tbody>
				<tr>
					<td class="labelSection">Username:</td>
					<td>
						<asp:TextBox ID="UsernameTextBox" runat="server" CssClass="credentialTextBox"></asp:TextBox>
						<asp:CheckBox ID="IncludePrivateCheckBox" runat="server"  AutoPostBack="True" 
							OnCheckedChanged="IncludePrivateCheckBox_CheckedChanged" />
						<asp:TextBox ID="PasswordTextBox" runat="server" CssClass="credentialTextBox" ToolTip="Password" TextMode="Password">
						</asp:TextBox>
					</td>
					<td class="helpSection"><b>1.</b> Enter any Google/Picasa username
						<br />&nbsp;&nbsp;&nbsp;&nbsp;Provide password for unlisted albums</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Button ID="GetAlbumsButton" runat="server" Text="Get Albums" 
							OnClick="GetAlbumsButton_Click" />
					</td>
					<td class="helpSection"><b>2.</b> Get list of albums</td>
				</tr>
				<tr>
					<td class="labelSection">Select album:</td>
					<td>
						<asp:DropDownList ID="AlbumDropDownList" runat="server" Width="610px">
						</asp:DropDownList>
					</td>
					<td class="helpSection"><b>3.</b> Choose album to export</td>
				</tr>
				<tr>
					<td class="labelSection">Photo template:</td>
					<td>
						<asp:Button ID="ResetTemplateButton" runat="server" Text="Reset Template" 
							OnClick="ResetTemplateButton_Click" />
						<br />
						<asp:TextBox ID="TemplateTextBox" runat="server" CssClass="paeTextArea"
							TextMode="MultiLine"></asp:TextBox>
					</td>
					<td class="helpSection"><b>4.</b> Specify the template that will be applied to each photo in the album or use default</td>
				</tr>
				<tr>
					<td class="labelSection">Placeholders:</td>
					<td>
						<asp:Literal ID="PlaceholdersLiteral" runat="server" />
					</td>
					<td class="helpSection"><b>5.</b> Use placeholders to insert photo information into the template</td>
				</tr>
				<tr>
					<td class="labelSection">Fit preview within:</td>
					<td style="color: Gray;">
						<asp:TextBox ID="WidthTextBox" runat="server" CssClass="paeNumericTextBox"></asp:TextBox>
						&nbsp;&nbsp;x&nbsp;&nbsp;
						<asp:TextBox ID="HeightTextBox" runat="server" CssClass="paeNumericTextBox"></asp:TextBox>
						&nbsp;&nbsp;pixels
					</td>
					<td class="helpSection"><b>6.</b> Set max width for landscape and height for 
						portrait oriented images</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Button ID="ExportButton" runat="server" Text="Export" 
							OnClick="ExportButton_Click" />
					</td>
					<td class="helpSection"><b>7.</b> Generate code using the template</td>
				</tr>
				<tr>
					<td class="labelSection">Exported:</td>
					<td>
						<asp:TextBox ID="ResultTextBox" runat="server" CssClass="paeTextArea"
							TextMode="MultiLine"></asp:TextBox>
					</td>
					<td class="helpSection"><b>8.</b> Copy generated code and paste it in your blog editor</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Button ID="PreviewButton" runat="server" Text="Preview" 
							OnClick="PreviewButton_Click" />
					</td>
					<td class="helpSection"><b>9.</b> Render generated HTML</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Label ID="MessageLabel" runat="server">
						</asp:Label>
					</td>
					<td class="helpSection contactSection">
						Report problem, request feature, say hi
						<br /><a href="mailto:dusiadev@gmail.com?Subject=[PAE]">Contact developer</a>
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
