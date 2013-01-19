using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAE.WebUI.Master
{
    public partial class PAE : MasterPage
    {
        #region Constants

        private const string DASH = "-";
        private const string UNDERSCORE = "_";
        private const string DELIMITER_LITERAL_ID = "DelimiterLiteral";
        private const string DELIMITER_LITERAL_TEXT = "&nbsp;|&nbsp;";
        private const string LANGUAGE_CTRL_ID_FORMAT = "{0}LanguageControl";

        #endregion

        #region Properties

        protected string FacebookLocale
        {
            get
            {
                string output = Thread.CurrentThread.CurrentCulture.Name.Replace(DASH, UNDERSCORE);
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
            this.BuildLanguageSelector();
        }

        #endregion

        #region Implementation

        private void BuildLanguageSelector()
        {
            foreach (SupportedLanguages language in Enum.GetValues(typeof(SupportedLanguages)))
            {
                if (this.LanguagesPlaceHolder.Controls.Count > 0)
                {
                    Literal delimiter = new Literal() { ID = DELIMITER_LITERAL_ID, Text = DELIMITER_LITERAL_TEXT };
                    this.LanguagesPlaceHolder.Controls.Add(delimiter);
                }

                string languageName = Enum.GetName(typeof(SupportedLanguages), language).ToLower();
                var culture = CultureInfo.GetCultureInfo(languageName);
                Control control;

                if (languageName == this.CurrentPage.SelectedLanguage
                    || language == SupportedLanguages.En && string.IsNullOrEmpty(this.CurrentPage.SelectedLanguage))
                {
                    control = new Literal()
                    {
                        ID = string.Format(LANGUAGE_CTRL_ID_FORMAT, language),
                        Text = culture.ThreeLetterISOLanguageName.ToUpper()
                    };
                }
                else
                {
                    control = new LinkButton()
                    {
                        ID = string.Format(LANGUAGE_CTRL_ID_FORMAT, language),
                        Text = culture.ThreeLetterISOLanguageName.ToUpper(),
                        ToolTip = culture.NativeName,
                        PostBackUrl = string.Format(this.CurrentPage.LanguageUrlFormat, languageName)
                    };
                }

                this.LanguagesPlaceHolder.Controls.Add(control);
            }
        }

        #endregion
    }
}
