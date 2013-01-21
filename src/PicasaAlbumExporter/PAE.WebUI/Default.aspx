<%@ Page Language="C#" MasterPageFile="~/Master/PAE.Master" CodeBehind="Default.aspx.cs" Inherits="PAE.WebUI.Default" 
    AutoEventWireup="true" ValidateRequest="false" %>
<%@ Register Src="~/Controls/ExporterControl.ascx" TagPrefix="pae" TagName="ExporterControl" %>

<asp:Content ID="contentHeaderTop" ContentPlaceHolderID="ContentHeaderTop" runat="server">

	<div>
	    <asp:LinkButton ID="ExpressModeLink" runat="server" Text="<%$ Resources : Strings, ExpressMode %>" OnClick="ExpressModeLink_Click" />
	</div>

</asp:Content>

<asp:Content ID="contentBody" ContentPlaceHolderID="ContentBody" runat="server">

	<div class="headerSection">
		<ul>
			<li><asp:Literal ID="LiteralSiteFeature1" runat="server" Text="<%$ Resources : Strings, SiteFeature1 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature2" runat="server" Text="<%$ Resources : Strings, SiteFeature2 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature3" runat="server" Text="<%$ Resources : Strings, SiteFeature3 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature4" runat="server" Text="<%$ Resources : Strings, SiteFeature4 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature5" runat="server" Text="<%$ Resources : Strings, SiteFeature5 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature6" runat="server" Text="<%$ Resources : Strings, SiteFeature6 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature7" runat="server" Text="<%$ Resources : Strings, SiteFeature7 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature71" runat="server" Text="<%$ Resources : Strings, SiteFeature11 %>" /></li>
			<li><asp:Literal ID="LiteralSiteFeature8" runat="server" Text="<%$ Resources : Strings, SiteFeature8 %>" /></li>
			<li>
				<asp:Literal ID="LiteralSiteFeature9" runat="server" Text="<%$ Resources : Strings, SiteFeature9 %>" />
				<div class="fb-like" data-href='<%= "http://picasaalbumexporter.apphb.com/" + SelectedLanguage %>' data-send="false" data-layout="button_count" data-width="450" data-show-faces="false"></div>
			</li>
		</ul>
	</div>
	
	<pae:ExporterControl ID="ExporterControl" runat="server" Mode="Username" />

</asp:Content>
