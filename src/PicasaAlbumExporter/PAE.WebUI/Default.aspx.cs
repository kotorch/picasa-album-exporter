using System;
using System.Linq;
using PAE.Logic;

namespace PAE.WebUI
{
	public partial class _Default : System.Web.UI.Page
	{
		#region Constants

		private const string INCLUDE_PRIVATE_CHECKED_TEXT = "Include Private / Password:";
		private const string INCLUDE_PRIVATE_UNCHECKED_TEXT = "Include Private";

		#endregion

		#region Event Handlers

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				this.IncludePrivateCheckBox.Checked = false;
				this.IncludePrivateCheckBox.Text = INCLUDE_PRIVATE_UNCHECKED_TEXT;
				this.PasswordTextBox.Visible = false;
				this.PasswordTextBox.Text = string.Empty;
				this.AlbumDropDownList.Enabled = false;
				this.TemplateTextBox.Text = AlbumExporter.DEFAULT_TEMPLATE;
			}
		}

		protected void IncludePrivateCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.IncludePrivateCheckBox.Checked)
			{
				this.IncludePrivateCheckBox.Text = INCLUDE_PRIVATE_CHECKED_TEXT;
				this.PasswordTextBox.Visible = true;
				this.PasswordTextBox.Text = string.Empty;
			}
			else
			{
				this.IncludePrivateCheckBox.Text = INCLUDE_PRIVATE_UNCHECKED_TEXT;
				this.PasswordTextBox.Visible = false;
				this.PasswordTextBox.Text = string.Empty;
			}
		}

		protected void GetAlbumsButton_Click(object sender, EventArgs e)
		{
			AlbumSelector provider = new AlbumSelector();
			var albumsData = provider.GetAlbums(this.UsernameTextBox.Text.Trim());

			if (albumsData.Any())
			{
				this.AlbumDropDownList.Enabled = true;
				this.AlbumDropDownList.DataTextField = "Title";
				this.AlbumDropDownList.DataValueField = "FeedUri";
				this.AlbumDropDownList.DataSource = albumsData;
				this.AlbumDropDownList.DataBind();
			}
			else
			{
				this.AlbumDropDownList.Enabled = false;
				this.AlbumDropDownList.DataSource = null;
				this.AlbumDropDownList.DataBind();
			}
		}

		protected void ExportButton_Click(object sender, EventArgs e)
		{
			try
			{
				AlbumExporter provider = new AlbumExporter();
				this.ResultTextBox.Text = provider.ExportAlbum
													(
														this.AlbumDropDownList.SelectedValue,
														this.TemplateTextBox.Text,
														null, null
													);
			}
			catch (Exception ex)
			{
				this.ResultTextBox.Text = ex.Message;
			}
		}

		#endregion
	}
}
