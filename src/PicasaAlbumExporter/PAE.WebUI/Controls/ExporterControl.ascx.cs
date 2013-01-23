using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAE.Logic;
using PAE.WebUI.Properties;
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
        private const string ALBUM_DATA_TEXT_FIELD = "Title";
        private const string ALBUM_DATA_VALUE_FIELD = "FeedUri";
        private const string HIDE_EDITOR_CSS = "templateEditorHide";
        private const string SHOW_EDITOR_CSS = "templateEditorShow";
        private const string ON_FOCUS_ATTRIBUTE = "onfocus";
        private const string ON_FOCUS_SCRIPT = "javascript:this.select();";
        private const string PLACEHOLDER_BUTTON_FORMAT = "<button class=\"placeholderButton\" id=\"btn_{0}\" title=\"{1}\""
                                                       + " onclick=\"InsertPlaceholder('{2}'); return false;\">{3}</button>";

        private readonly string PREVIEW_OUTPUT_FORMAT = "<script>window.open('preview?" + Variables.LanguageQueryStringName + "={0}','_blank');</script>";

        #endregion

        #region Enums

        public enum ExporterMode
        {
            AlbumLink = 0,
            Username = 1,
        }

        #endregion

        #region Properties

        public ExporterMode Mode
        {
            get
            {
                ExporterMode output = (ExporterMode)Enum.ToObject(typeof(ExporterMode), this.ViewState[Variables.ExporterModeViewStateName]);
                return output;
            }

            set
            {
                this.ViewState[Variables.ExporterModeViewStateName] = value;
            }
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

        private BasePage CurrentPage
        {
            get
            {
                BasePage output = (BasePage)this.Page;
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

                this.Mode = ExporterMode.AlbumLink;
                this.ConfigureExporterMode();
            }

            this.PlaceholdersLiteral.Text = this.BuildPlaceholdersHtml();
            this.ResultTextBox.Attributes[ON_FOCUS_ATTRIBUTE] = ON_FOCUS_SCRIPT;

            this.GetAlbumsErrorLabel.Text = string.Empty;
            this.ExportErrorLabel.Text = string.Empty;
        }

        protected void ExportModeMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            this.Mode = (ExporterMode)Enum.Parse(typeof(ExporterMode), e.Item.Value);
            this.ConfigureExporterMode();
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
            string username = this.UsernameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                this.GetAlbumsErrorLabel.Text = Strings.PleaseEnterUsername;
            }
            else
            {
                try
                {
                    string password = this.PasswordTextBox.Text;

                    AlbumSelector provider = new AlbumSelector();
                    albumsData = provider.GetAlbums(username, password);
                }
                catch
                {
                    this.GetAlbumsErrorLabel.Text = Strings.AlbumRetrievalFailed;
                }
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
            string feedUri;

            switch (this.Mode)
            {
                case ExporterMode.AlbumLink:

                    if (string.IsNullOrEmpty(this.AlbumLinkTextBox.Text))
                    {
                        this.ExportErrorLabel.Text = Strings.PleaseEnterAlbumLink;
                        return;
                    }

                    AlbumSelector selector = new AlbumSelector();
                    AlbumInfo info = selector.GetAlbum(this.AlbumLinkTextBox.Text);

                    if (info == null)
                    {
                        this.ExportErrorLabel.Text = Strings.AlbumIsNotFound;
                        return;
                    }
                    else
                    {
                        feedUri = info.FeedUri;
                    }

                    break;

                default:

                    feedUri = this.AlbumDropDownList.SelectedValue;

                    break;
            }

            if (string.IsNullOrEmpty(feedUri))
            {
                this.ExportErrorLabel.Text = Strings.PleaseSelectAlbum;
                return;
            }

            try
            {
                string template = this.TemplateTextBox.Text;
                int width = int.Parse(this.WidthTextBox.Text);
                int height = int.Parse(this.HeightTextBox.Text);
                AlbumExporter provider = new AlbumExporter();
                this.ResultTextBox.Text = provider.ExportAlbum(feedUri, template, width, height);
                this.ResultTextBox.Focus();
            }
            catch
            {
                this.ExportErrorLabel.Text = Strings.ExportFailed;
            }
        }

        protected void PreviewLink_Click(object sender, EventArgs e)
        {
            this.Session[Variables.PreviewHtmlSessionName] = this.ResultTextBox.Text;
            string output = string.Format(PREVIEW_OUTPUT_FORMAT, this.CurrentPage.SelectedLanguage);
            this.Response.Write(output);
        }

        #endregion

        #region Implementation

        private void ConfigureExporterMode()
        {
            switch (this.Mode)
            {
                case ExporterMode.AlbumLink:

                    this.UsernameModePlaceHolder.Visible = false;
                    this.AlbumLinkModePlaceHolder.Visible = true;

                    this.AlbumLinkTextBox.Focus();

                    break;

                default:

                    this.UsernameModePlaceHolder.Visible = true;
                    this.AlbumLinkModePlaceHolder.Visible = false;

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

        #endregion
    }
}