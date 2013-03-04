<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExporterControl.ascx.cs" Inherits="PAE.WebUI.Controls.ExporterControl" %>

<div>
	<table class="mainTable" cellspacing="7px">
		<tbody>
		    <tr>
		        <td class="labelSection"><asp:Literal ID="LiteralExporterMode" runat="server" Text="<%$ Resources : Strings, ExportMode %>" /></td>
		        <td class="exportMode">
		            <asp:Menu ID="ExportModeMenu" runat="server" Orientation="Horizontal" 
		                OnMenuItemClick="ExportModeMenu_MenuItemClick" DynamicVerticalOffset="0">
		                <StaticMenuItemStyle CssClass="menuItem" />
		                <StaticSelectedStyle Font-Bold="true" />
		                <Items>
		                    <asp:MenuItem Value="AlbumLink" Text="<%$ Resources : Strings, ByAlbumLink %>" ImageUrl="~/img/picasa.png" Selected="True" />
		                    <asp:MenuItem Value="Username" Text="<%$ Resources : Strings, ByUsername %>" ImageUrl="~/img/google_user.png" />
		                </Items>
		            </asp:Menu>
		        </td>
		    </tr>
			<asp:PlaceHolder ID="AlbumLinkModePlaceHolder" runat="server" Visible="false">
			    <tr>
			        <td class="labelSection"><asp:Literal ID="LiteralAlbumLink" runat="server" Text="<%$ Resources : Strings, AlbumLink %>" /></td>
			        <td>
			            <asp:TextBox ID="AlbumLinkTextBox" runat="server" CssClass="selectAlbum"></asp:TextBox>
			        </td>
			    </tr>
			</asp:PlaceHolder>
		    <asp:PlaceHolder ID="UsernameModePlaceHolder" runat="server">
			    <tr>
				    <td class="labelSection"><asp:Literal ID="LiteralEnterUsername" runat="server" Text="<%$ Resources : Strings, EnterUsername %>" /></td>
				    <td>
					    <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="credentialTextBox"></asp:TextBox>
					    <asp:CheckBox ID="IncludePrivateCheckBox" runat="server"  AutoPostBack="True" 
						    OnCheckedChanged="IncludePrivateCheckBox_CheckedChanged" />&nbsp;
					    <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="credentialTextBox" ToolTip="Password" TextMode="Password">
					    </asp:TextBox>
				    </td>
			    </tr>
			    <tr>
				    <td></td>
				    <td>
					    <asp:Button ID="GetAlbumsButton" runat="server" CssClass="mainButton"
						    Text="<%$ Resources : Strings, GetAlbums %>" OnClick="GetAlbumsButton_Click" />
						    &nbsp;
						    <asp:Label ID="GetAlbumsErrorLabel" runat="server" CssClass="errorMessage" />
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
		    <tr>
			    <td></td>
			    <td>
				    <asp:LinkButton ID="ToggleEditorLink" runat="server" CssClass="editorToggler" 
					    Text="<%$ Resources : Strings, EditTemplate %>" OnClick="ToggleEditorLink_Click" />
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
				    &nbsp;
				    <asp:Label ID="ExportErrorLabel" runat="server" CssClass="errorMessage" />
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
					<asp:LinkButton ID="PreviewLink" runat="server" CssClass="preview" OnClick="PreviewLink_Click" 
					    ToolTip="<%$ Resources : Strings, PreviewHtmlToolTip %>" Text="<%$ Resources : Strings, PreviewHtml %>" />
				</td>
			</tr>
		</tbody>
	</table>
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
