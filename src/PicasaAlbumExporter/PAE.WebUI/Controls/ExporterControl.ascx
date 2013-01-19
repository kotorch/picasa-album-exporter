<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExporterControl.ascx.cs" Inherits="PAE.WebUI.Controls.ExporterControl" %>

<div>
	<table width="760px" cellspacing="7px">
		<tbody>
		    <asp:PlaceHolder ID="FullModePlaceHolder" runat="server">
			    <tr>
				    <td class="labelSection"><asp:Literal ID="LiteralEnterUsername" runat="server" Text="<%$ Resources : Strings, EnterUsername %>" /></td>
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
						    Text="<%$ Resources : Strings, GetAlbums %>" OnClick="GetAlbumsButton_Click" />
				    </td>
			    </tr>
			    <tr>
				    <td class="labelSection"><asp:Literal ID="LiteralSelectAlbum" runat="server" Text="<%$ Resources : Strings, SelectAlbum %>" /></td>
				    <td>
					    <asp:DropDownList ID="AlbumDropDownList" runat="server" CssClass="selectAlbum">
					    </asp:DropDownList>
				    </td>
			    </tr>
			</asp:PlaceHolder>
			<asp:PlaceHolder ID="ExpressModePlaceHolder" runat="server" Visible="false">
			    <tr>
			        <td class="labelSection"><asp:Literal ID="LiteralAlbumLink" runat="server" Text="<%$ Resources : Strings, AlbumLink %>" /></td>
			        <td>
			            <asp:TextBox ID="AlbumLinkTextBox" runat="server" CssClass="selectAlbum"></asp:TextBox>
			        </td>
			    </tr>
			</asp:PlaceHolder>
		    <tr>
			    <td></td>
			    <td>
				    <asp:LinkButton ID="ToggleEditorLink" runat="server" Text="<%$ Resources : Strings, EditTemplate %>"
					    OnClick="ToggleEditorLink_Click" />
				    <asp:Panel ID="TemplateEditorPanel" runat="server">
					    <asp:TextBox ID="TemplateTextBox" runat="server" CssClass="paeTextArea"
						    TextMode="MultiLine"></asp:TextBox>
					    <div class="placeholders">
						    <asp:Literal ID="PlaceholdersLiteral" runat="server" />
					    </div>
					    <div class="undoChanges">
						    <asp:Button ID="ResetTemplateButton" runat="server"
							    Text="<%$ Resources : Strings, UndoChanges %>" OnClick="ResetTemplateButton_Click" />
					    </div>
					    <div class="imageSizeSection">
						    <asp:Literal ID="LiteralFitImageWithin" runat="server" Text="<%$ Resources : Strings, FitImageWithin %>" />
						    <asp:TextBox ID="WidthTextBox" runat="server" CssClass="paeNumericTextBox"></asp:TextBox>
						    <asp:Literal ID="LiteralBy" runat="server" Text="<%$ Resources : Strings, By %>" />
						    <asp:TextBox ID="HeightTextBox" runat="server" CssClass="paeNumericTextBox"></asp:TextBox>
						    <asp:Literal ID="LiteralPixels" runat="server" Text="<%$ Resources : Strings, Pixels %>" />
					    </div>
				    </asp:Panel>
			    </td>
		    </tr>
			<tr>
				<td></td>
				<td>
					<asp:Button ID="ExportButton" runat="server" CssClass="mainButton" 
						Text="<%$ Resources : Strings, Export %>" OnClick="ExportButton_Click" />
				</td>
			</tr>
			<tr>
				<td class="labelSection"><asp:Literal ID="LiteralEmbedCode" runat="server" Text="<%$ Resources : Strings, EmbedCode %>" /></td>
				<td>
					<asp:TextBox ID="ResultTextBox" runat="server" CssClass="paeTextArea"
						TextMode="MultiLine"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td></td>
				<td>
					<asp:LinkButton ID="PreviewLink" runat="server" 
						Text="<%$ Resources : Strings, PreviewHtml %>" OnClick="PreviewLink_Click" />
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

<script type="text/javascript">

    function InsertPlaceholder(text) {
        var el = document.getElementById('<%= this.TemplateTextBox.ClientID %>');
        var val = el.value, endIndex, range;

        if (typeof el.selectionStart != "undefined" && typeof el.selectionEnd != "undefined") {
            endIndex = el.selectionEnd;
            el.value = val.slice(0, endIndex) + text + val.slice(endIndex);
            el.selectionStart = el.selectionEnd = endIndex + text.length;
        }
        else if (typeof document.selection != "undefined" && typeof document.selection.createRange != "undefined") {
            el.focus();
            range = document.selection.createRange();
            range.collapse(false);
            range.text = text;
            range.select();
        }
    }

</script>
