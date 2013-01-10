<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PAE.WebUI._Default" ValidateRequest="false" %>
<%@ Import Namespace="Resources" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title><asp:Literal ID="LiteralPageTitle" runat="server" Text="<% $Resources:Strings, PageTitle %>" /></title>
	<meta name="description" content="<% $Resources:Strings, MetaDescription %>" />
	<meta name="keywords" content="<% $Resources:Strings, MetaKeywords %>" />
	<meta name="google-site-verification" content="35Bsp2IMv-W4wvfWtLeBlHdy0vZ1u5tODJv450rfEaQ" />
	<meta name="msvalidate.01" content="465E989B888FE3599826616299E23DE0" />

	<style type="text/css">
		
		.headerSection
		{
			padding-left: 140px; 
			margin-bottom: 40px;
			width: 610px;
		}
			
		.headerSection h1
		{
			color: #666666;
			margin-left: 10px;
			width: 500px;
		}
			
		.headerSection h1 sup
		{
			color:#cccccc;
			font-size: normal !important;
		}
		
		.headerSection .languageSelector
		{
			float: right;
		}
		
		.headerSection ul
		{
			color: Gray;
		}
		
		.headerSection ul li
		{
			margin-bottom: 2px;
		}
		
		.headerSection ul li div
		{
			display: inline;
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
	<div id="fb-root"></div>
	<script>(function(d, s, id) {
  		  var js, fjs = d.getElementsByTagName(s)[0];
		  if (d.getElementById(id)) return;
		  js = d.createElement(s); js.id = id;
		  js.src = '//connect.facebook.net/<%= FacebookLocale %>/all.js#xfbml=1';
		  fjs.parentNode.insertBefore(js, fjs);
		}(document, 'script', 'facebook-jssdk'));
	</script>
	<form id="PaeForm" runat="server">
	<div class="headerSection">
		<div class="languageSelector"><asp:PlaceHolder ID="LanguagesPlaceHolder" runat="server" /></div>
		<h1><asp:Literal ID="LiteralSiteHeader" runat="server" Text="<% $Resources:Strings, SiteHeader %>" /><sup>&nbsp;&beta;</sup></h1>
		<br />
		<ul>
			<li><asp:Literal ID="LiteralSiteFeature1" runat="server" Text="<% $Resources:Strings, SiteFeature1 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature2" runat="server" Text="<% $Resources:Strings, SiteFeature2 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature3" runat="server" Text="<% $Resources:Strings, SiteFeature3 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature4" runat="server" Text="<% $Resources:Strings, SiteFeature4 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature5" runat="server" Text="<% $Resources:Strings, SiteFeature5 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature6" runat="server" Text="<% $Resources:Strings, SiteFeature6 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature7" runat="server" Text="<% $Resources:Strings, SiteFeature7 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature8" runat="server" Text="<% $Resources:Strings, SiteFeature8 %>" /></li>
			<li>
				<asp:Literal ID="LiteralSiteFeature9" runat="server" Text="<% $Resources:Strings, SiteFeature9 %>" />
				<div class="fb-like" data-href='<%= "http://picasaalbumexporter.apphb.com/" + SelectedLanguage %>' data-send="false" data-layout="button_count" data-width="450" data-show-faces="false" data-action="recommend"></div>
				<asp:Literal ID="LiteralSiteFeature10" runat="server" Text="<% $Resources:Strings, SiteFeature10 %>" />
			</li>
		</ul>
	</div>
	<div>
		<table width="760px" cellspacing="7px">
			<tbody>
				<tr>
					<td class="labelSection"><asp:Literal ID="LiteralEnterUsername" runat="server" Text="<% $Resources:Strings, EnterUsername %>" /></td>
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
							Text="<% $Resources:Strings, GetAlbums %>" OnClick="GetAlbumsButton_Click" />
					</td>
				</tr>
				<tr>
					<td class="labelSection"><asp:Literal ID="LiteralSelectAlbum" runat="server" Text="<% $Resources:Strings, SelectAlbum %>" /></td>
					<td>
						<asp:DropDownList ID="AlbumDropDownList" runat="server" CssClass="selectAlbum">
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:LinkButton ID="ToggleEditorLink" runat="server" Text="<% $Resources:Strings, EditTemplate %>"
							OnClick="ToggleEditorLink_Click" />
						<asp:Panel ID="TemplateEditorPanel" runat="server">
							<asp:TextBox ID="TemplateTextBox" runat="server" CssClass="paeTextArea"
								TextMode="MultiLine"></asp:TextBox>
							<div class="placeholders">
								<asp:Literal ID="PlaceholdersLiteral" runat="server" />
							</div>
							<div class="undoChanges">
								<asp:Button ID="ResetTemplateButton" runat="server"
									Text="<% $Resources:Strings, UndoChanges %>" OnClick="ResetTemplateButton_Click" />
							</div>
							<div class="imageSizeSection">
								<asp:Literal ID="LiteralFitImageWithin" runat="server" Text="<% $Resources:Strings, FitImageWithin %>" />
								<asp:TextBox ID="WidthTextBox" runat="server" CssClass="paeNumericTextBox"></asp:TextBox>
								<asp:Literal ID="LiteralBy" runat="server" Text="<% $Resources:Strings, By %>" />
								<asp:TextBox ID="HeightTextBox" runat="server" CssClass="paeNumericTextBox"></asp:TextBox>
								<asp:Literal ID="LiteralPixels" runat="server" Text="<% $Resources:Strings, Pixels %>" />
							</div>
						</asp:Panel>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Button ID="ExportButton" runat="server" CssClass="mainButton" 
							Text="<% $Resources:Strings, Export %>" OnClick="ExportButton_Click" />
					</td>
				</tr>
				<tr>
					<td class="labelSection"><asp:Literal ID="LiteralEmbedCode" runat="server" Text="<% $Resources:Strings, EmbedCode %>" /></td>
					<td>
						<asp:TextBox ID="ResultTextBox" runat="server" CssClass="paeTextArea"
							TextMode="MultiLine"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:LinkButton ID="PreviewLink" runat="server" 
							Text="<% $Resources:Strings, PreviewHtml %>" OnClick="PreviewLink_Click" />
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
