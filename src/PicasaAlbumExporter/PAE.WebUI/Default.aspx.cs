﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using PAE.Logic;

namespace PAE.WebUI
{
	public partial class _Default : BasePage
	{
		#region Constants

		private const string HIDE_EDITOR_CSS = "hide";
		private const string SHOW_EDITOR_CSS = "show";
		private const string INCLUDE_PRIVATE_CHECKED_TEXT = "Include unlisted albums / Password:";
		private const string INCLUDE_PRIVATE_UNCHECKED_TEXT = "Include unlisted albums";
		private const string PLACEHOLDER_BUTTON_FORMAT = "<button class=\"placeholderButton\" id=\"btn_{0}\" title=\"{1}\"" 
													   + " onclick=\"InsertPlaceholder('{2}'); return false;\">{3}</button>";

		#endregion

		#region Properties

		protected string FacebookLocale
		{
			get
			{
				string output = Thread.CurrentThread.CurrentCulture.Name.Replace("-", "_");
				return output;
			}
		}
		
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

				this.TemplateEditorPanel.CssClass = HIDE_EDITOR_CSS;
				this.TemplateTextBox.Text = AlbumExporter.DEFAULT_TEMPLATE;
				this.WidthTextBox.Text = AlbumExporter.DEFAULT_PREVIEW_WIDTH.ToString();
				this.HeightTextBox.Text = AlbumExporter.DEFAULT_PREVIEW_HEIGHT.ToString();
				
				this.UsernameTextBox.Focus();
			}
			
			this.PlaceholdersLiteral.Text = this.BuildPlaceholdersHtml();
			this.ResultTextBox.Attributes["onfocus"] = "javascript:this.select();";
			this.SetMessage(string.Empty, Color.Black);
		}

		protected void IncludePrivateCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.IncludePrivateCheckBox.Checked)
			{
				this.IncludePrivateCheckBox.Text = INCLUDE_PRIVATE_CHECKED_TEXT;
				this.PasswordTextBox.Visible = true;
				this.PasswordTextBox.Focus();
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
				this.SetMessage(ex.Message, Color.Red);
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

		protected void ToggleEditorLink_Click(object sender, EventArgs e)
		{
			this.ToggleTemplateEditor();
		}

		protected void ResetTemplateButton_Click(object sender, EventArgs e)
		{
			this.TemplateTextBox.Text = AlbumExporter.DEFAULT_TEMPLATE;
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
				this.ResultTextBox.Focus();				
			}
			catch (Exception ex)
			{
				this.SetMessage(ex.Message, Color.Red);
			}
		}

		protected void PreviewLink_Click(object sender, EventArgs e)
		{
			this.PreviewLiteral.Text = this.ResultTextBox.Text;
		}

		#endregion

		#region Implementation

		private void ToggleTemplateEditor()
		{
			if (this.TemplateEditorPanel.CssClass == HIDE_EDITOR_CSS)
			{
				this.TemplateEditorPanel.CssClass = SHOW_EDITOR_CSS;
				this.ToggleEditorLink.Text = "Hide template editor";
			}
			else
			{
				this.TemplateEditorPanel.CssClass = HIDE_EDITOR_CSS;
				this.ToggleEditorLink.Text = "Edit template...";
			}
		}

		private string BuildPlaceholdersHtml()
		{
			StringBuilder html = new StringBuilder();
		
			foreach(var placeholder in AlbumExporter.Placeholders)
			{
				string cleanName = placeholder.Key
					.Replace("<", string.Empty)
					.Replace(">", string.Empty)
					.Replace("-", "_");
				string htmlSafeKey = Server.HtmlEncode(placeholder.Key);
				html.AppendFormat(PLACEHOLDER_BUTTON_FORMAT, cleanName, placeholder.Value, placeholder.Key, htmlSafeKey);
				html.AppendLine();
			}
			
			string output = html.ToString();
			
			return output;
		}

		private void SetMessage(string message, Color color)
		{
			this.MessageLabel.Text = message;
			this.MessageLabel.ForeColor = color;
		}

		#endregion
	}
}
