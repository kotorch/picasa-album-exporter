<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PAE.WebUI._Default" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Picasa Web Album Exporter, Export Picasa Albums to HTML, Export Picasa Albums to XML</title>
	<meta name="description" content="Export Google Picasa albums to HTML, BBCode, XML, etc. Include photo captions, custom size images and other information." />
	<meta name="keywords" content="google,picasa,export,to,html,album,captions,photos,images" />
	<meta name="google-site-verification" content="35Bsp2IMv-W4wvfWtLeBlHdy0vZ1u5tODJv450rfEaQ" />
	
	<style type="text/css">
		
		.headerSection
		{
			padding-left: 140px; 
			margin-bottom: 40px;
		}
			
		.headerSection h1
		{
			color: #666666;
			margin-left: 10px;
		}
		
		.headerSection h3
		{
			color: #888888;
		}
		
		.headerSection ul
		{
			color: Gray;
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
		
		.mainButton1
		{
			font-size:20px;
			color:#444444;
			width:150px;
			height:40px;
			background-color:#CCCCCC;
			moz-border-radius: 15px;
  			-webkit-border-radius: 15px;
			border-style:solid;
			border-color:#444444;
			border-width:2px;
		}
		
		.selectAlbum
		{
			width: 610px;
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
		
	</style>
	
	<script type="text/javascript">

		var _gaq = _gaq || [];
		_gaq.push(['_setAccount', 'UA-36896557-1']);
		_gaq.push(['_trackPageview']);

		(function() {
			var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
			ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
			var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
		})();

		function InsertPlaceholder(text)
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
	<div class="headerSection">
		<h3>&alpha;</h3>&nbsp;<h1>Picasa Web Album Exporter</h1>
		<br />
		<ul>
			<li>Export Google Picasa web albums as <b>HTML</b>, <b>BBCode</b>, etc.</li>
			<li>Export <b>unlisted</b> albums (with a password)</li>
			<li>Choose desired image size</li>
			<li>Include photo <b>captions</b>, <b>full-size</b> images and other info</li>
			<li>Embed in your blog (WordPress, LiveJournal, etc.)</li>
			<li>Instantly <b>preview</b> exported HTML</li>
			<li><a href="mailto:dusiadev@gmail.com?Subject=[PAE]">Contact developer</a> to report problem or request new feature</li>
		</ul>
	</div>
	<div>
		<table width="760px" cellspacing="7px">
			<tbody>
				<tr>
					<td class="labelSection">Enter username:</td>
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
						<asp:DropDownList ID="AlbumDropDownList" runat="server" CssClass="selectAlbum">
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
					<td class="labelSection">Embed code:</td>
					<td>
						<asp:TextBox ID="ResultTextBox" runat="server" CssClass="paeTextArea"
							TextMode="MultiLine"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:LinkButton ID="PreviewLink" runat="server" 
							Text="Preview generated HTML..." OnClick="PreviewLink_Click" />
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
