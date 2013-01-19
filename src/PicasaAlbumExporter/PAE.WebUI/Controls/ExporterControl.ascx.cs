﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using PAE.Logic;
using Resources;

namespace PAE.WebUI.Controls
{
    public partial class ExporterControl : UserControl
    {
        #region Constants

        private const string ANGLE_BRACKET_OPEN = "<";
        private const string ANGLE_BRACKET_CLOSE = ">";
        private const string DASH = "-";
        private const string UNDERSCORE = "_";
        private const string DELIMITER_LITERAL_ID = "DelimiterLiteral";
        private const string DELIMITER_LITERAL_TEXT = "&nbsp;|&nbsp;";
        private const string LANGUAGE_CTRL_ID_FORMAT = "{0}LanguageControl";
        private const string LANGUAGE_LINK_URL_FORMAT = @"~/{0}";
        private const string EXPRESS_MODE_URL = @"~/exp";
        private const string ALBUM_DATA_TEXT_FIELD = "Title";
        private const string ALBUM_DATA_VALUE_FIELD = "FeedUri";
        private const string HIDE_EDITOR_CSS = "hide";
        private const string SHOW_EDITOR_CSS = "show";
        private const string ON_FOCUS_ATTRIBUTE = "onfocus";
        private const string ON_FOCUS_SCRIPT = "javascript:this.select();";
        private const string PLACEHOLDER_BUTTON_FORMAT = "<button class=\"placeholderButton\" id=\"btn_{0}\" title=\"{1}\""
                                                       + " onclick=\"InsertPlaceholder('{2}'); return false;\">{3}</button>";

        #endregion

        #region Enums

        public enum ExporterMode
        {
            Username = 0,
            AlbumLink = 1,
        }

        #endregion

        #region Properties

        public ExporterMode Mode
        {
            get;
            set;
        }

        private IDictionary<string, string> Placeholders
        {
            get
            {
                var output = new Dictionary<string, string>
				{
						{ AlbumExporter.Placeholders.CAPTION, Strings.PlaceholderCaption },
						{ AlbumExporter.Placeholders.COUNTER, Strings.PlaceholderCounter },
						{ AlbumExporter.Placeholders.FILE_NAME, Strings.PlaceholderFileName },
						{ AlbumExporter.Placeholders.IMAGE, Strings.PlaceholderImage },
						{ AlbumExporter.Placeholders.ORIGINAL, Strings.PlaceholderOriginal },
						{ AlbumExporter.Placeholders.PICASA_URL, Strings.PlaceholderPicasaUrl },
				};

                return output;
            }
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.TemplateEditorPanel.CssClass = HIDE_EDITOR_CSS;
                this.TemplateTextBox.Text = this.GetDefaultTemplateText();
                this.WidthTextBox.Text = AlbumExporter.DEFAULT_PREVIEW_WIDTH.ToString();
                this.HeightTextBox.Text = AlbumExporter.DEFAULT_PREVIEW_HEIGHT.ToString();

                this.ConfigureExporterMode();
            }

            this.PlaceholdersLiteral.Text = this.BuildPlaceholdersHtml();
            this.ResultTextBox.Attributes[ON_FOCUS_ATTRIBUTE] = ON_FOCUS_SCRIPT;
            this.SetMessage(string.Empty, Color.Black);
        }

        protected void IncludePrivateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.IncludePrivateCheckBox.Checked)
            {
                this.IncludePrivateCheckBox.Text = Strings.IncludeUnlistedAlbumsChecked;
                this.PasswordTextBox.Visible = true;
                this.PasswordTextBox.Focus();
            }
            else
            {
                this.IncludePrivateCheckBox.Text = Strings.IncludeUnlistedAlbumsUnchecked;
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
                this.AlbumDropDownList.DataTextField = ALBUM_DATA_TEXT_FIELD;
                this.AlbumDropDownList.DataValueField = ALBUM_DATA_VALUE_FIELD;
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
            this.TemplateTextBox.Text = this.GetDefaultTemplateText();
        }

        protected void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                string feedUri;
                string template = this.TemplateTextBox.Text;
                int width = int.Parse(this.WidthTextBox.Text);
                int height = int.Parse(this.HeightTextBox.Text);

                switch (this.Mode)
                {
                    case ExporterMode.AlbumLink:

                        AlbumSelector selector = new AlbumSelector();
                        feedUri = selector.GetAlbum(this.AlbumLinkTextBox.Text).FeedUri;

                        break;

                    default:

                        feedUri = this.AlbumDropDownList.SelectedValue;

                        break;
                }

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

        private void ConfigureExporterMode()
        {
            switch (this.Mode)
            {
                case ExporterMode.AlbumLink:

                    this.FullModePlaceHolder.Visible = false;
                    this.ExpressModePlaceHolder.Visible = true;

                    this.AlbumLinkTextBox.Focus();

                    break;

                default:

                    this.FullModePlaceHolder.Visible = true;
                    this.ExpressModePlaceHolder.Visible = false;

                    this.IncludePrivateCheckBox.Checked = false;
                    this.IncludePrivateCheckBox.Text = Strings.IncludeUnlistedAlbumsUnchecked;
                    this.PasswordTextBox.Visible = false;
                    this.PasswordTextBox.Text = string.Empty;
                    this.AlbumDropDownList.Enabled = false;

                    this.UsernameTextBox.Focus();

                    break;
            }
        }

        private string GetDefaultTemplateText()
        {
            string output = AlbumExporter.GetDefaultTemplate(Strings.OpenFullSize, Strings.ViewOnPicasa);
            return output;
        }

        private void ToggleTemplateEditor()
        {
            if (this.TemplateEditorPanel.CssClass == HIDE_EDITOR_CSS)
            {
                this.TemplateEditorPanel.CssClass = SHOW_EDITOR_CSS;
                this.ToggleEditorLink.Text = Strings.HideTemplateEditor;
            }
            else
            {
                this.TemplateEditorPanel.CssClass = HIDE_EDITOR_CSS;
                this.ToggleEditorLink.Text = Strings.EditTemplate;
            }
        }

        private string BuildPlaceholdersHtml()
        {
            StringBuilder html = new StringBuilder();

            foreach (var placeholder in this.Placeholders)
            {
                string cleanName = placeholder.Key
                    .Replace(ANGLE_BRACKET_OPEN, string.Empty)
                    .Replace(ANGLE_BRACKET_CLOSE, string.Empty)
                    .Replace(DASH, UNDERSCORE);
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