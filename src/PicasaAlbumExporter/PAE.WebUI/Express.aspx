<%@ Page Language="C#" MasterPageFile="~/Master/PAE.Master" AutoEventWireup="true" 
    CodeBehind="Express.aspx.cs" Inherits="PAE.WebUI.Express" ValidateRequest="false" %>
<%@ Register Src="~/Controls/ExporterControl.ascx" TagPrefix="pae" TagName="ExporterControl" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="ContentHead" runat="server">

    <style type="text/css">
    
    .headerSection h1
    {
        font-size: larger;
    }
    
    </style>

</asp:Content>

<asp:Content ID="contentHeaderTop" ContentPlaceHolderID="ContentHeaderTop" runat="server">

    <asp:LinkButton ID="FullModeLink" runat="server" Text="<%$ Resources : Strings, FullMode %>" OnClick="FullModeLink_Click" />

</asp:Content>

<asp:Content ID="contentBody" ContentPlaceHolderID="ContentBody" runat="server">
    
    <pae:ExporterControl ID="ExporterControl" runat="server" Mode="AlbumLink" />
    
</asp:Content>
