<%@ Page Language="C#" MasterPageFile="~/Master/PAE.Master" CodeBehind="Default.aspx.cs" 
    Inherits="PAE.WebUI.Default" AutoEventWireup="true" ValidateRequest="false" %>
<%@ Register Src="~/Controls/ExporterControl.ascx" TagPrefix="pae" TagName="ExporterControl" %>

<asp:Content ID="contentBody" ContentPlaceHolderID="ContentBody" runat="server">

	<pae:ExporterControl ID="ExporterControl" runat="server" />

</asp:Content>
