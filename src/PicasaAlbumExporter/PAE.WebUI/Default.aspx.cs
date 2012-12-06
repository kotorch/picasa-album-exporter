using System;
using System.Collections.Generic;
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
				this.WidthTextBox.Text = AlbumExporter.DEFALUT_PREVIEW_WIDTH.ToString();
				this.HeightTextBox.Text = AlbumExporter.DEFALUT_PREVIEW_HEIGHT.ToString();
			}
		}

		protected void IncludePrivateCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.IncludePrivateCheckBox.Checked)
			{
				this.IncludePrivateCheckBox.Text = INCLUDE_PRIVATE_CHECKED_TEXT;
				this.PasswordTextBox.Visible = true;
			}
			else
			{
				this.IncludePrivateCheckBox.Text = INCLUDE_PRIVATE_UNCHECKED_TEXT;
				this.PasswordTextBox.Visible = false;
			}

			this.PasswordTextBox.Text = string.Empty;
		}

		protected void GetAlbumsButton_Click(object sender, EventArgs e)
		{
			IEnumerable<AlbumInfo> albumsData = null;

			try
			{
				string username = this.UsernameTextBox.Text.Trim();
				string password = this.PasswordTextBox.Text;
			
				AlbumSelector provider = new AlbumSelector();
				albumsData = provider.GetAlbums(username, password);
			}
			catch (Exception ex)
			{
				this.ResultTextBox.Text = ex.Message;
			}

			if (albumsData != null && albumsData.Any())
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
				string feedUri = this.AlbumDropDownList.SelectedValue;
				string template = this.TemplateTextBox.Text;
				int width = int.Parse(this.WidthTextBox.Text);
				int height = int.Parse(this.HeightTextBox.Text);
				
				AlbumExporter provider = new AlbumExporter();
				this.ResultTextBox.Text = provider.ExportAlbum(feedUri, template, width, height);
			}
			catch (Exception ex)
			{
				this.ResultTextBox.Text = ex.Message;
			}
		}

		protected void PreviewButton_Click(object sender, EventArgs e)
		{
			this.PreviewLiteral.Text = this.ResultTextBox.Text;
		}

		#endregion
	}
}
