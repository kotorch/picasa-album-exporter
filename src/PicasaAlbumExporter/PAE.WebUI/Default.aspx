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
		
		.mainButton
		{
			font-size:20px;
			color:#444444;
			width:150px;
			height:40px;
			background-color:#CCCCCC;
			border-style:solid;
			border-color:#444444;
			border-width:2px;
		}
		
		.show
		{
			display: block;
		}
		
		.hide
		{
			display: none;
		}
		
		.placeholders
		{
			float: left;
			width: 70%;
		}
		
		.placeholders .placeholderButton
		{
			margin: 2px;
			width: 100px;
			font-size: xx-small;
			background: #e0e0e0;
			border: 1px solid black;
		}
		
		.undoChanges
		{
			float: right;
			margin: 2px;
		}
			
		.imageSizeSection
		{
			clear: both;
			padding-top: 7px;
		}
		
		.paeTextArea
		{
			width: 605px;
			max-width: 605px;
			height: 100px;
			margin-bottom: 7px;
		}
		
		.paeNumericTextBox
		{
			width: 50px;
			text-align: right;
		}
		
		.contactSection
		{
			padding: 30px 0 0 140px;
			font-weight: bold;
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
		<table width="760px" cellspacing="7px">
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
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Button ID="GetAlbumsButton" runat="server" CssClass="mainButton"
							Text="Get Albums" OnClick="GetAlbumsButton_Click" />
					</td>
				</tr>
				<tr>
					<td class="labelSection">Select album:</td>
					<td>
						<asp:DropDownList ID="AlbumDropDownList" runat="server" Width="610px">
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:LinkButton ID="ToggleEditorLink" runat="server" Text="Edit template..."
							OnClick="ToggleEditorLink_Click" />
						<asp:Panel ID="TemplateEditorPanel" runat="server">
							<asp:TextBox ID="TemplateTextBox" runat="server" CssClass="paeTextArea"
								TextMode="MultiLine"></asp:TextBox>
							<div class="placeholders">
								<asp:Literal ID="PlaceholdersLiteral" runat="server" />
							</div>
							<div class="undoChanges">
								<asp:Button ID="ResetTemplateButton" runat="server"
									Text="Undo Changes" OnClick="ResetTemplateButton_Click" />
							</div>
							<div class="imageSizeSection">
								Fit image within&nbsp;&nbsp;
								<asp:TextBox ID="WidthTextBox" runat="server" CssClass="paeNumericTextBox"></asp:TextBox>
								&nbsp;&nbsp;x&nbsp;&nbsp;
								<asp:TextBox ID="HeightTextBox" runat="server" CssClass="paeNumericTextBox"></asp:TextBox>
								&nbsp;&nbsp;pixels
							</div>
						</asp:Panel>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Button ID="ExportButton" runat="server" CssClass="mainButton" 
							Text="Export" OnClick="ExportButton_Click" />
					</td>
				</tr>
				<tr>
					<td class="labelSection">Exported:</td>
					<td>
						<asp:TextBox ID="ResultTextBox" runat="server" CssClass="paeTextArea"
							TextMode="MultiLine"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Button ID="PreviewButton" runat="server" 
							Text="Preview" OnClick="PreviewButton_Click" />
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
	<div class="contactSection">
		Report problem, request feature, say hi: <a href="mailto:dusiadev@gmail.com?Subject=[PAE]">contact developer</a>.
	</div>
	</form>
</body>
</html>
